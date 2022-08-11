using System;
using System.Collections.Generic;

namespace Battleships
{
    // Imagine a game of battleships.
    //   The player has to guess the location of the opponent's 'ships' on a 10x10 grid
    //   Ships are one unit wide and 2-4 units long, they may be placed vertically or horizontally
    //   The player asks if a given co-ordinate is a hit or a miss
    //   Once all cells representing a ship are hit - that ship is sunk.
    public class Game
    {
        // ships: each string represents a ship in the form first co-ordinate, last co-ordinate
        //   e.g. "3:2,3:5" is a 4 cell ship horizontally across the 4th row from the 3rd to the 6th column
        // guesses: each string represents the co-ordinate of a guess
        //   e.g. "7:0" - misses the ship above, "3:3" hits it.
        // returns: the number of ships sunk by the set of guesses       
        public static int Play(string[] ships, string[] guesses)
        {            
            //sunk ships counter
            int sunkShipsCounter = 0;

            //create board of 10x10
            char[,] board = CreateBoard();

            //Place the ships on the board
            board = PlaceShips(ships, board, out var numberOfShips);

            // guess and fire (Hit=>H or Miss=>M)
            foreach (var guess in guesses)
            {
                //Fire: hit or miss, update the board.
                board = Fire(guess, board);
            }

            // Read hit position from the board,
            // and place it in temp dictionary
            var temp = new Dictionary<string, char>();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 'H')
                        temp[$"{i},{j}"] = board[i, j];
                }
            }            

            //Check where all cells (representing ship) are hit
            //or not, if hit, increment numberOfSunkShips counter.
            foreach (var key in numberOfShips.Keys)
            {
                if (temp.ContainsKey(key))
                {
                    int hitCount = 0;
                    foreach (var item in numberOfShips[key])
                    {
                        if (temp.ContainsKey(item.Key))
                        {
                            hitCount++;

                            //remove the position from temp to
                            //ignore the repeatation.
                            temp.Remove(item.Key);
                        }
                    }

                    //hit counts == ship position counts => ship sunk
                    if (hitCount == numberOfShips[key].Count)
                        sunkShipsCounter++;
                }
            }

            //number of sunk ships
            return sunkShipsCounter;
        }

        /// <summary>
        /// Fire if hit set to H else M
        /// </summary>
        /// <param name="guess"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        private static char[,] Fire(string guess, char[,] board)
        {
            var guessPosition = GetPositions(guess.Split(':'));

            //S represent the marker for ship.
            board[guessPosition.X, guessPosition.Y] = board[guessPosition.X, guessPosition.Y] == 'S' ? 'H' : 'M';

            return board;
        }

        /// <summary>
        /// Place ship on the board.
        /// </summary>
        /// <param name="ships"></param>
        /// <param name="board"></param>
        /// <param name="numberOfShips"></param>
        /// <returns></returns>
        private static char[,] PlaceShips(string[] ships, char[,] board, out Dictionary<string, Dictionary<string, char>> numberOfShips)
        {
            numberOfShips = new Dictionary<string, Dictionary<string, char>>();
            foreach (var ship in ships)
            {
                string[] arr = ship.Split(',');
                var firstPosition = GetPositions(arr[0].Split(':'));
                var lastPosition = GetPositions(arr[1].Split(':'));

                Dictionary<string, char> shipDictionary = new Dictionary<string, char>();
                string shipStartPosition = null;
                if (firstPosition.X == lastPosition.X)
                {
                    //horizontal => x direction => column
                    for (int i = firstPosition.Y; i <= lastPosition.Y; i++)
                    {
                        board[firstPosition.X, i] = 'S';
                        shipDictionary[firstPosition.X + "," + i] = board[firstPosition.X, i];
                        if(string.IsNullOrEmpty(shipStartPosition))
                            shipStartPosition = $"{firstPosition.X},{i}";
                    }
                }
                else
                {
                    //vertical => y direction => row
                    for (int i = firstPosition.X; i <= lastPosition.X; i++)
                    {
                        board[i, firstPosition.Y] = 'S';//S represent as ship marker
                        shipDictionary[i + "," + firstPosition.Y] = board[i, firstPosition.Y];
                        if (string.IsNullOrEmpty(shipStartPosition))
                            shipStartPosition = $"{i},{firstPosition.Y}";
                    }
                }
                numberOfShips.Add(shipStartPosition, shipDictionary);
            }

            return board;
        }


        /// <summary>
        /// Create board of 10 x 10 
        /// </summary>
        /// <returns></returns>
        private static char[,] CreateBoard()
        {
            var board = new char[10, 10];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = '.';
                }
            }

            return board;
        }

        /// <summary>
        /// Get Positions
        /// </summary>
        /// <param name="xyArray"></param>
        /// <returns></returns>
        private static Position GetPositions(string[] xyArray)
        {
            Position p = new Position
            {
                X = Convert.ToInt32(xyArray[0]),
                Y = Convert.ToInt32(xyArray[1])
            };
            return p;
        }
    }

    /// <summary>
    /// Helper for Positioning
    /// the ships
    /// </summary>
    class Position
    {
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;
    }
}
