using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace Puzzle
{
    public class Board
    {
        public Tile[,] BoardSize;
        public int Tiles;
        private readonly int NrOfShuffles;
        private int TileX, TileY, OutOfBounds, NrOfMoves = 0,Py,Px;

        public enum  Directions {
            Left, Right, Up, Down
        }

        public void Shuffle()
        {
            
            var rnd = new Random();
            

            for (var i = 0; i < NrOfShuffles; i++)
            {
                var dir = rnd.Next(1, 5);

                
                switch (dir)
                {
                    case 1:
                        Move(Directions.Right);

                        break;

                    case 2:
                        Move(Directions.Left);

                        break;

                    case 3:
                        Move(Directions.Up);

                        break;
                    case 4:
                        Move(Directions.Down);
                        break;

                    default:
                        break;
                }
            }

            this.NrOfMoves = 0;
        }

        public Board(int PosX, int PosY)
        {
            Px = PosX;
            Py = PosY;
            Tiles = PosX;
            TileX = PosX-1;
            TileY = PosY-1;
            BoardSize = new Tile[PosX, PosY];
            int Number = 1;
            OutOfBounds = PosX-1;
            NrOfShuffles = TileX * TileX * 1000;

            for (int i = 0; i < PosY; i++)
            {
                
                for (int j = 0; j < PosX; j++)
                {
                    BoardSize[i, j] = new Tile(Number);
                    Number++;
                }
                BoardSize[PosX - 1, PosY - 1] = new Tile(0);
            }


        }

        public void Draw()
        {
            Console.Clear();
            for (var j = 0; j < Py; j++)
            {
                for (var k = 0; k < Px; k++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("+----");
                }
                Console.WriteLine("+");
                for (var i = 0; i < Px; i++)
                {

                    Console.Write("| {0,2} ", BoardSize[j, i].Number);
                }

                Console.WriteLine("|");

            }
            for (var l = 0; l < Px; l++)
            {

                Console.Write("+----");
            }
            Console.WriteLine("+");
            Console.WriteLine("Moves: {0}", NrOfMoves);
            Console.WriteLine("UP    = Up Arrow Key");
            Console.WriteLine("Down  = Down Arrow Key");
            Console.WriteLine("Left  = Left Arrow Key");
            Console.WriteLine("Right = Right Arrow Key");

        }


        public void Move(Directions directions)
        {
            switch (directions)
            {
                case Directions.Up:
                    if (TileX > 0)
                    {
                        var temp = BoardSize[TileX, TileY];
                        BoardSize[TileX, TileY] = BoardSize[TileX -1, TileY];
                        BoardSize[TileX -1, TileY] = temp;
                        TileX = TileX - 1;
                        NrOfMoves++;
                    }
                    break;

                case Directions.Down:
                    if (TileX < OutOfBounds)
                    {
                        var temp = BoardSize[TileX, TileY];
                        BoardSize[TileX, TileY] = BoardSize[TileX + 1, TileY];
                        BoardSize[TileX + 1, TileY] = temp;
                        TileX = TileX + 1;
                        NrOfMoves++;
                    }
                    break;

                case Directions.Left:
                    if (TileY > 0)
                    {
                        var temp = BoardSize[TileX, TileY];
                        BoardSize[TileX, TileY] = BoardSize[TileX, TileY - 1];
                        BoardSize[TileX, TileY - 1] = temp;
                        TileY = TileY - 1;
                        NrOfMoves++;
                    }
                    break;

                case Directions.Right:
                    if (TileY < OutOfBounds)
                    {
                        var temp = BoardSize[TileX, TileY];
                        BoardSize[TileX, TileY] = BoardSize[TileX, TileY + 1];
                        BoardSize[TileX, TileY + 1] = temp;
                        TileY = TileY + 1;
                        NrOfMoves++;

                    }
                    break;

            }

        }


    }
    public class Tile
    {

        public int Number;



        public Tile(int number)
        {
            Number = number;

        }
    }

    class Game
    {
        public Board board;
        public Board boardCheck;
        private bool IsRunning = true;

        public Game()
        {
            int Dimension;

            do
            {
                Console.Write("Enter size of board (2-10): ");
                string dim = Console.ReadLine();
                if (int.TryParse(dim, out Dimension))
                {
                    if (Dimension > 1 && Dimension < 11)
                    {
                        Console.Clear();
                        Console.WriteLine("Dimension = {0} x {0}", Dimension);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid Input");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Input");
                }
            } while (!(Dimension > 1 && Dimension < 11));

            board = new Board(Dimension, Dimension);
            boardCheck = new Board(Dimension, Dimension);

            board.Shuffle();

            while (IsRunning)
            {
                board.Draw();
                KeyPress();

                if (CheckIfSame(board, boardCheck) == 1)
                {
                    IsRunning = false;
                    board.Draw();
                    Console.WriteLine("YOU FUCKING DID IT!");
                }
            }
        }


        public int CheckIfSame(Board board, Board boardCheck)
        {


            for (int i = 0; i < board.Tiles; i++)
            {
                
                for (int j = 0; j < board.Tiles; j++)
                {
                    if ((board.BoardSize[i, j].Number != boardCheck.BoardSize[i, j].Number))
                    {
                        return 0;
                        
                    }

                }
                
            }

            return 1;
        }
    

    public void KeyPress()
        {

                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        board.Move(Board.Directions.Left);
                        break;
                    

                    case ConsoleKey.RightArrow:
                        board.Move(Board.Directions.Right);
                        break;

                    case ConsoleKey.UpArrow:
                        board.Move(Board.Directions.Up);
                        break;

                    case ConsoleKey.DownArrow:
                        board.Move(Board.Directions.Down);
                        break;

                    case ConsoleKey.Escape:
                        IsRunning = false;
                        break;
                }

        }

    }
    class Program
        {
            static void Main(string[] args)
            {
                Game game = new Game();
                
            }
        }  
}