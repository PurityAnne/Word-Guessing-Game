/*
 * Students Name : Anne Purity
 * Student Number : BSCLMR178921
 * Date : 19/11/2023
 * Project Name : WordGuessingGame
 * File Name : MainWindow.xaml.cs
 */
using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Threading;
using System.Configuration;

namespace WordGuessingGame
{
    public partial class MainWindow : Window
    {
        private TcpClient client;
        private NetworkStream stream;
        private DispatcherTimer timer; // Timer for time limit

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer(); // Initialize the timer
            timer.Tick += Timer_Tick;

            // Load settings from App.config
            txtIPAddress.Text = ConfigurationManager.AppSettings["ServerIP"];
            txtPort.Text = ConfigurationManager.AppSettings["ServerPort"];
        }

        private async void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string serverIp = txtIPAddress.Text;
                if (!int.TryParse(txtPort.Text, out int port))
                {
                    MessageBox.Show("Please enter a valid port number.");
                    return;
                }

                client = new TcpClient();
                await client.ConnectAsync(serverIp, port);

                stream = client.GetStream();

                // Sending user name and time limit to the server (optional)
                string userInfo = $"{txtUserName.Text}|{txtTimeLimit.Text}";
                byte[] data = Encoding.ASCII.GetBytes(userInfo);
                await stream.WriteAsync(data, 0, data.Length);

                // Handle initial data from the server
                byte[] initialDataBuffer = new byte[1024];
                int initialBytesRead = await stream.ReadAsync(initialDataBuffer, 0, initialDataBuffer.Length);
                string initialResponse = Encoding.ASCII.GetString(initialDataBuffer, 0, initialBytesRead);

                // Display the game data (using initialResponse)
                txtGameInfo.Text = initialResponse;

                MessageBox.Show("Connected to the server!");

                // After connection, enable the guess button and input
                btnGuess.IsEnabled = true;
                txtGuess.IsEnabled = true;

                // Enable the Replay button
                btnReplay.IsEnabled = true;

                // Set and start the timer for time limit
                if (double.TryParse(txtTimeLimit.Text, out double timeLimit))
                {
                    timer.Interval = TimeSpan.FromSeconds(timeLimit);
                    timer.Start();
                }
                else
                {
                    MessageBox.Show("Please enter a valid time limit.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect: {ex.Message}");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Handle time limit expiration
            timer.Stop();
            MessageBox.Show("Time is up!");
        }

        private async void BtnGuess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string guess = txtGuess.Text;
                byte[] data = Encoding.ASCII.GetBytes(guess);
                await stream.WriteAsync(data, 0, data.Length);

                // Read server response (you'll need to adjust based on your server's protocol)
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                // Parse the response
                var parts = response.Split('|');
                if (parts.Length >= 2)
                {
                    string guessResult = parts[0]; // Correct or Incorrect
                    string correctWordsCount = parts[1]; // Number of correct words guessed
                    txtGameInfo.Text = $"Guess: {guessResult}, Correct Words: {correctWordsCount}";
                }

                txtGuess.Clear();

                // Inside the BtnGuess_Click method or another appropriate method
                if (parts.Length >= 3 && parts[2] == "PlayAgain?")
                {
                    if (MessageBox.Show("Do you want to play again?", "Play Again", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        // Send confirmation to the server to start a new game
                        string message = "YES_PLAY_AGAIN";
                        byte[] playAgainData = Encoding.ASCII.GetBytes(message);
                        await stream.WriteAsync(playAgainData, 0, playAgainData.Length);

                        // Disable the Replay button until a new game starts
                        btnReplay.IsEnabled = false;
                    }
                    else
                    {
                        // Send decline message to the server
                        string message = "NO_PLAY_AGAIN";
                        byte[] declineData = Encoding.ASCII.GetBytes(message);
                        await stream.WriteAsync(declineData, 0, declineData.Length);
                        client.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending guess: {ex.Message}");
            }
        }

        private async void BtnEndGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Inform the server that the client is ending the game session
                string message = "REQUEST_END_GAME"; // Adjust this based on your server's protocol
                byte[] data = Encoding.ASCII.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);

                // Optionally, read a response from the server

                client.Close();
                MessageBox.Show("Requested to end game session.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error ending game: {ex.Message}");
            }
        }

        private void BtnReplay_Click(object sender, RoutedEventArgs e)
        {
            // Reset game-related variables (replace these with your actual variable names and logic)
            
            // Clear the game information display
            txtGameInfo.Text = "";

            // Enable/disable UI elements as needed
            btnReplay.IsEnabled = false; // Disable the "Replay" button until the new game starts
            btnGuess.IsEnabled = true;   // Enable the "Guess" button
            txtGuess.IsEnabled = true;   // Enable the input field for guesses
        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (stream != null)
            {
                stream.Close();
            }
            if (client != null)
            {
                client.Close();
            }
        }
    }
}
