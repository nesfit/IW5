using Exercise.Console.DateTimeProvider;
using System;

namespace Exercise.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var homework = new Homework(new LocalDateTimeProvider());
            //var chessPlayer1 = Homework.GetChessPlayerInfo(
            //    new DateTime[] { },
            //    new int[] { },
            //    new int[] { },
            //    new string[] { },
            //    new string[] { },
            //    new CountryCode[] { },
            //    new TimeSpan[] { },
            //    OrderBy.Height,
            //    OrderDirection.Descending,
            //    CountryCode.IND
            //);

            var chessPlayer2 = homework.GetChessPlayerInfo(
                new DateTime[] { new DateTime(1990, 11, 30), new DateTime(1992, 7, 30), new DateTime(1992, 10, 24) },
                new int[] { 178, 168, 170 },
                new int[] { 2872, 2822, 2805 },
                new string[] { "Carlsen", "Fabiano", "Ding" },
                new string[] { "Magnus", "Caruana", "Liren" },
                new CountryCode[] { CountryCode.NOR, CountryCode.USA, CountryCode.CHN },
                new TimeSpan[] { new TimeSpan(900, 10, 3), new TimeSpan(876, 10, 3), new TimeSpan(850, 10, 3) },
                OrderBy.Age,
                OrderDirection.Descending,
                CountryCode.NOR
            );

            System.Console.WriteLine(@$"Returned chess player info:
Full name: {chessPlayer2.Item1},
Age: {chessPlayer2.Item2} years,
Height: {chessPlayer2.Item3}cm,
Elo rating: {chessPlayer2.Item4},
Country code: {chessPlayer2.Item5},
Month average training time: {chessPlayer2.Item6:dd\.hh\:mm\:ss}.");
            System.Console.ReadKey();
        }
    }
}
