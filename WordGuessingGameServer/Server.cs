/*
 * Students Name : Anne Purity
 * Student Number : BSCLMR178921
 * Date : 19/11/2023
 * Project Name : WordGuessingGame
 * File Name : Server.cs
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WordGuessingGameServer;

namespace WordGuessingGameServer
{
    class Server
    {
        private TcpListener listener;
        private List<TcpClient> clients = new List<TcpClient>();
        private bool isRunning;

        public Server(IPAddress ipAddress, int port)
        {
            listener = new TcpListener(ipAddress, port);
        }

        public void Start()
        {
            listener.Start();
            isRunning = true;
            Console.WriteLine("Server started. Waiting for connections...");

            Task.Run(() => {
                while (isRunning)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        clients.Add(client);
                        Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                        clientThread.Start(client);
                    }
                    catch (SocketException ex)
                    {
                        // Handle exception (e.g., log it)
                        Console.WriteLine("SocketException in accepting client: " + ex.Message);
                    }
                }
            });
        }


        private void HandleClientComm(object clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj;
            NetworkStream stream = tcpClient.GetStream();
            GameLogic game = new GameLogic();
            game.StartNewGame();

            // Send initial game data to the client
            string initialData = $"{game.GetCurrentWord()}|{game.GetWordsToFind()}";
            byte[] initialDataBytes = Encoding.ASCII.GetBytes(initialData + "\n");
            stream.Write(initialDataBytes, 0, initialDataBytes.Length);

            try
            {
                while (tcpClient.Connected)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; // Client disconnected

                    string clientGuess = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();

                    // Handling the end game request from the client
                    if (clientGuess.Equals("REQUEST_END_GAME", StringComparison.OrdinalIgnoreCase))
                    {
                        // Send confirmation to end the game
                        string endGameConfirmation = "GAME_ENDED\n";
                        byte[] endGameBytes = Encoding.ASCII.GetBytes(endGameConfirmation);
                        stream.Write(endGameBytes, 0, endGameBytes.Length);
                        break;
                    }

                    // Processing the guess
                    bool isCorrect = game.MakeGuess(clientGuess);
                    string response = $"{(isCorrect ? "Correct" : "Incorrect")}|{game.GetWordsToFind()}\n";

                    // Send response back to client
                    byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                    stream.Write(responseBytes, 0, responseBytes.Length);

                    // Check if the game is complete
                    if (game.IsGameComplete())
                    {
                        // Send a message to the client to ask if they want to play again
                        string playAgainMsg = "PlayAgain?\n";
                        byte[] playAgainBytes = Encoding.ASCII.GetBytes(playAgainMsg);
                        stream.Write(playAgainBytes, 0, playAgainBytes.Length);

                        // Read client's response for playing again
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string playAgainResponse = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();

                        if (playAgainResponse.Equals("YES_PLAY_AGAIN", StringComparison.OrdinalIgnoreCase))
                        {
                            game.StartNewGame(); // Start a new game
                            initialData = $"{game.GetCurrentWord()}|{game.GetWordsToFind()}\n";
                            initialDataBytes = Encoding.ASCII.GetBytes(initialData);
                            stream.Write(initialDataBytes, 0, initialDataBytes.Length);
                        }
                        else
                        {
                            break; // End the session
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception or handle errors
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Close the connection
                tcpClient.Close();
            }
        }

        public void Stop()
        {
            isRunning = false;
            foreach (var client in clients)
            {
                // Optionally send a message to client about server shutdown
                client.Close();
            }
            listener.Stop();
            Console.WriteLine("Server stopped.");
        }

    }
}
