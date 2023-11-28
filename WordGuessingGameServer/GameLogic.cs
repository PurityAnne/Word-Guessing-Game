/*
 * Students Name : Anne Purity
 * Student Number : BSCLMR178921
 * Date : 19/11/2023
 * Project Name : WordGuessingGame
 * File Name : GameLogic.cs
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.Threading;

namespace WordGuessingGameServer
{
    internal class GameLogic
    {
        private string currentWord;
        private int wordsToFind;
        private List<string> foundWords;
        private List<string> validWords;


        
            public void StartNewGame()
            {
                var lines = File.ReadAllLines("valid_words.txt");
                currentWord = lines[0]; // 80-character string
                wordsToFind = int.Parse(lines[1]); // Number of words
                validWords = lines.Skip(2).ToList(); // List of words
                foundWords = new List<string>();
            }

        


        public bool MakeGuess(string guessedWord)
        {
            //check if the guessed word is valid and not already found 
            if (validWords.Contains(guessedWord) && !foundWords.Contains(guessedWord))
            {
                //check if the guessed word is in the currentWord
                if (currentWord.Contains(guessedWord))
                {
                    //update the state of the game 
                    foundWords.Add(guessedWord);
                    wordsToFind -= 1;
                    return true;// Guessed Correctly 


                }

            }
            return false;// Guessed incorrectly
        }


        public bool IsGameComplete()
        {
            return wordsToFind == 0;
        }

        public string GetCurrentWord()
        {
            return currentWord;
        }

        public int GetWordsToFind()
        {
            return wordsToFind;
        }
    }

  
}
