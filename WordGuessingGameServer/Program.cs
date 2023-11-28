/*
 * Students Name : Anne Purity
 * Student Number : BSCLMR178921
 * Date : 19/11/2023
 * Project Name : WordGuessingGame
 * File Name : Program.cs
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
// The following code is extracted from the MSDN site:

//
namespace WordGuessingGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Specify the IP address and port number for the server
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // Change to your desired IP address
            int port = 12345; // Change to your desired port number

            Server server = new Server(ipAddress, port);
            server.Start();

            Console.WriteLine("Press ENTER to stop the server...");
            Console.ReadLine();

            server.Stop();
        }
    }
}