using System;
using System.Collections.Generic;
using System.Linq;
using ChessLogic;
using System.Threading;
using System.IO;

using ChessLogic.Generators;

namespace ConsoleTester
{
    class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var game = CreateGame();
            string oldBoard = "";
            var q = new Queue<string>();
            q.Enqueue(game.Board);
            int idd = 0;
            var start = DateTime.Now;
            try
            {
                while (game.Board != oldBoard)
                {
                    //Thread.Sleep(100);

                    //PrintGame(game);

                    var generator = new AllMovesGenerator(game);
                    var moves = generator.Generate().ToList();
                    if (moves.Count == 0) break;
                    InputMove inpMove = null;
                    ValidationResult s = null;
                    for (int i = 0; i < moves.Count; i++)
                    {
                        inpMove = CreateMove(moves, game);
                        s = Engine.Execute(inpMove);
                        if (s.IsValid)
                        {
                            oldBoard = game.Board;
                            game = s.Game;
                            break;
                        }
                    }
                    //Console.WriteLine(s.EndGameState);
                    q.Enqueue(game.Board);
                    idd++;
                    if (idd == 1) break;
                }
                Console.WriteLine(DateTime.Now - start);
            }
            finally
            {
                GenerateJson(q);
            }

        }

        static Game CreateGame()
        {
            //var board =
            //    "rnbqkbnr" +
            //    "pppppppp" +
            //    "EEEEEEEE" +
            //    "EEEEEEEE" +
            //    "EEEEEEEE" +
            //    "EEEEEEEE" +
            //    "PPPPPPPP" +
            //    "RNBQKBNR";

            //board = "rEbEEbnrEEpnEkpEppEEEEEpEEBpppEEEEEEPEPPNEEPEEENPPPqEPERREEQKBEE";
            //var board =
            //    "EEEkEEEE" +
            //    "EEEEEEEE" +
            //    "EEEEEEEE" +
            //    "EEPpEEEE" +
            //    "EEEEEEEE" +
            //    "EEEEEEEE" +
            //    "EEEEEEEE" +
            //    "EEEKEEEE";

            var board =
                "EEErEkEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEE" +
                "EEEEEEEp" +
                "rEEEEEEP" +
                "EEEEKEER";

            var game = new Game(board, true, true, true, new Move(new Square(1, 3), new Square(3, 3)));

            return game;
        }

        static InputMove CreateMove(List<Move> moves, Game game)
        {
            var index = rand.Next(moves.Count);
            var move = moves[index];
            moves.RemoveAt(index);

            return new InputMove()
            {
                SourceRow = move.Source.Row,
                SourceCol = move.Source.Col,
                DestinationRow = move.Destination.Row,
                DestinationCol = move.Destination.Col,
                Game = game
            };
        }

        static void PrintGame(Game game)
        {
            Console.Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(game.GetPieceAt(i, j));
                }
                Console.WriteLine();
            }
        }

        static void GenerateJson(Queue<string> q)
        {
            using (var w = new StreamWriter("../../moves.js"))
            {
                w.Write(@"var s = [");

                while (q.Count != 1)
                {
                    w.Write("'{0}',", q.Dequeue());
                }

                w.Write("'{0}'", q.Dequeue());

                w.Write("]");
            }
        }
    }
}
