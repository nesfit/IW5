using Exercise.Console.Tests.Mocks;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Exercise.Console.Tests
{
    public class HomeworkTests
    {
        private readonly Homework _homework = new Homework(new DateTimeProviderMock(new DateTime(2020, 08, 01)));

        [Test]
        public void GetChessPlayerInfo_EmptyBirthdatesArray_ThrowsArgumentException()
        {
            // Arrange
            DateTime[] birthdates = new DateTime[0];

            // Act & Assert
            Action act = () => _homework.GetChessPlayerInfo(birthdates, default, default, default, default, default, default, default, default, default);
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetChessPlayerInfo_DifferentBirthdaysAndHeightInCentimetersArraysLength_ThrowsArgumentException()
        {
            // Arrange
            DateTime[] birthdates = new DateTime[] { DateTime.Now };
            int[] heightsInCentimeters = new int[] { 100, 200 };

            // Act & Assert
            Action act = () => _homework.GetChessPlayerInfo(birthdates, heightsInCentimeters, default, default, default, default, default, default, default, default);
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetChessPlayerInfo_DifferentBirthdaysAndThisYearTrainingTimeArraysLength_ThrowsArgumentException()
        {
            // Arrange
            DateTime[] birthdates = new DateTime[] { DateTime.Now };
            TimeSpan[] thisYearTrainingTime = new TimeSpan[] { TimeSpan.FromDays(10), TimeSpan.FromDays(20), TimeSpan.FromDays(30) };

            // Act & Assert
            Action act = () => _homework.GetChessPlayerInfo(birthdates, default, default, default, default, default, thisYearTrainingTime, default, default, default);
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetChessPlayerInfo_UnknownOrderByValue_ThrowsArgumentException()
        {
            // Arrange
            var birthdates = new DateTime[] { new DateTime(1990, 11, 30) };
            var heightsInCentimeters = new int[] { 178 };
            var eloRatings = new int[] { 2872 };
            var firstNames = new string[] { "Carlsen" };
            var lastNames = new string[] { "Magnus" };
            var countryCodes = new CountryCode[] { CountryCode.NOR };
            var thisYearTrainingTime = new TimeSpan[] { new TimeSpan(900, 10, 3), };
            var orderBy = OrderBy.Unknown;

            // Act & Assert
            Action act = () => _homework.GetChessPlayerInfo(birthdates, heightsInCentimeters, eloRatings, firstNames, lastNames, countryCodes, thisYearTrainingTime, orderBy, default, default);
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetChessPlayerInfo_OrderByValueOutsideTheEnumRange_ThrowsArgumentException()
        {
            // Arrange
            var birthdates = new DateTime[] { new DateTime(1990, 11, 30) };
            var heightsInCentimeters = new int[] { 178 };
            var eloRatings = new int[] { 2872 };
            var firstNames = new string[] { "Carlsen" };
            var lastNames = new string[] { "Magnus" };
            var countryCodes = new CountryCode[] { CountryCode.NOR };
            var thisYearTrainingTime = new TimeSpan[] { new TimeSpan(900, 10, 3), };
            OrderBy orderBy = (OrderBy)int.MaxValue;

            // Act & Assert
            Action act = () => _homework.GetChessPlayerInfo(birthdates, heightsInCentimeters, eloRatings, firstNames, lastNames, countryCodes, thisYearTrainingTime, orderBy, default, default);
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetChessPlayerInfo_GetPlayerWithHighestEloRatingWithoutFiltering_ReturnsCarlsenMagnus()
        {
            // Arrange
            var birthdates = new DateTime[] { new DateTime(1990, 11, 30), new DateTime(1992, 7, 30), new DateTime(1992, 10, 24) };
            var heightsInCentimeters = new int[] { 178, 168, 170 };
            var eloRatings = new int[] { 2872, 2822, 2805 };
            var firstNames = new string[] { "Carlsen", "Fabiano", "Ding" };
            var lastNames = new string[] { "Magnus", "Caruana", "Liren" };
            var countryCodes = new CountryCode[] { CountryCode.NOR, CountryCode.USA, CountryCode.CHN };
            var thisYearTrainingTime = new TimeSpan[] { TimeSpan.FromHours(800), TimeSpan.FromHours(700), TimeSpan.FromHours(600) };
            var orderBy = OrderBy.EloRating;
            var orderDirection = OrderDirection.Descending;

            // Act
            var result = _homework.GetChessPlayerInfo(birthdates, heightsInCentimeters, eloRatings, firstNames, lastNames, countryCodes, thisYearTrainingTime, orderBy, orderDirection, default);

            // Assert
            result.Should().Be(GetCarlsenMagnus());
        }

        [Test]
        public void GetChessPlayerInfo_GetPlayerWithLovestEloRatingWithoutFiltering_ReturnsDingLiren()
        {
            // Arrange
            var birthdates = new DateTime[] { new DateTime(1990, 11, 30), new DateTime(1992, 7, 30), new DateTime(1992, 10, 24) };
            var heightsInCentimeters = new int[] { 178, 168, 170 };
            var eloRatings = new int[] { 2872, 2822, 2805 };
            var firstNames = new string[] { "Carlsen", "Fabiano", "Ding" };
            var lastNames = new string[] { "Magnus", "Caruana", "Liren" };
            var countryCodes = new CountryCode[] { CountryCode.NOR, CountryCode.USA, CountryCode.CHN };
            var thisYearTrainingTime = new TimeSpan[] { TimeSpan.FromHours(800), TimeSpan.FromHours(700), TimeSpan.FromHours(600) };
            var orderBy = OrderBy.EloRating;
            var orderDirection = OrderDirection.Ascending;

            // Act
            var result = _homework.GetChessPlayerInfo(birthdates, heightsInCentimeters, eloRatings, firstNames, lastNames, countryCodes, thisYearTrainingTime, orderBy, orderDirection, default);

            // Assert
            result.Should().Be(GetDingLiren());
        }

        [Test]
        public void GetChessPlayerInfo_GetPlayerWithHighestEloRatingFilteringNorwayPlayers_ReturnsFabianoCaruana()
        {
            // Arrange
            var birthdates = new DateTime[] { new DateTime(1990, 11, 30), new DateTime(1992, 7, 30), new DateTime(1992, 10, 24) };
            var heightsInCentimeters = new int[] { 178, 168, 170 };
            var eloRatings = new int[] { 2872, 2822, 2805 };
            var firstNames = new string[] { "Carlsen", "Fabiano", "Ding" };
            var lastNames = new string[] { "Magnus", "Caruana", "Liren" };
            var countryCodes = new CountryCode[] { CountryCode.NOR, CountryCode.USA, CountryCode.CHN };
            var thisYearTrainingTime = new TimeSpan[] { TimeSpan.FromHours(800), TimeSpan.FromHours(700), TimeSpan.FromHours(600) };
            var orderBy = OrderBy.EloRating;
            var orderDirection = OrderDirection.Descending;
            var countryFilter = CountryCode.NOR;

            // Act
            var result = _homework.GetChessPlayerInfo(birthdates, heightsInCentimeters, eloRatings, firstNames, lastNames, countryCodes, thisYearTrainingTime, orderBy, orderDirection, countryFilter);

            // Assert
            result.Should().Be(GetFabianoCaruana());
        }

        [Test]
        public void GetChessPlayerInfo_GetOldestPlayerWithoutFiltering_ReturnsCarlsenMagnus()
        {
            // Arrange
            var birthdates = new DateTime[] { new DateTime(1990, 11, 30), new DateTime(1992, 7, 30), new DateTime(1992, 10, 24) };
            var heightsInCentimeters = new int[] { 178, 168, 170 };
            var eloRatings = new int[] { 2872, 2822, 2805 };
            var firstNames = new string[] { "Carlsen", "Fabiano", "Ding" };
            var lastNames = new string[] { "Magnus", "Caruana", "Liren" };
            var countryCodes = new CountryCode[] { CountryCode.NOR, CountryCode.USA, CountryCode.CHN };
            var thisYearTrainingTime = new TimeSpan[] { TimeSpan.FromHours(800), TimeSpan.FromHours(700), TimeSpan.FromHours(600) };
            var orderBy = OrderBy.Age;
            var orderDirection = OrderDirection.Descending;

            // Act
            var result = _homework.GetChessPlayerInfo(birthdates, heightsInCentimeters, eloRatings, firstNames, lastNames, countryCodes, thisYearTrainingTime, orderBy, orderDirection, default);

            // Assert
            result.Should().Be(GetCarlsenMagnus());
        }

        [Test]
        public void GetChessPlayerInfo_GetYoungestPlayerFilteringChinesePlayers_ReturnsFabianoCaruana()
        {
            // Arrange
            var birthdates = new DateTime[] { new DateTime(1990, 11, 30), new DateTime(1992, 7, 30), new DateTime(1992, 10, 24) };
            var heightsInCentimeters = new int[] { 178, 168, 170 };
            var eloRatings = new int[] { 2872, 2822, 2805 };
            var firstNames = new string[] { "Carlsen", "Fabiano", "Ding" };
            var lastNames = new string[] { "Magnus", "Caruana", "Liren" };
            var countryCodes = new CountryCode[] { CountryCode.NOR, CountryCode.USA, CountryCode.CHN };
            var thisYearTrainingTime = new TimeSpan[] { TimeSpan.FromHours(800), TimeSpan.FromHours(700), TimeSpan.FromHours(600) };
            var orderBy = OrderBy.Age;
            var orderDirection = OrderDirection.Ascending;
            var countryFilter = CountryCode.CHN;

            // Act
            var result = _homework.GetChessPlayerInfo(birthdates, heightsInCentimeters, eloRatings, firstNames, lastNames, countryCodes, thisYearTrainingTime, orderBy, orderDirection, countryFilter);

            // Assert
            result.Should().Be(GetFabianoCaruana());
        }

        [Test]
        public void GetChessPlayerInfo_GetTallestPlayerWithoutFiltering_ReturnsCarlsenMagnus()
        {
            // Arrange
            var birthdates = new DateTime[] { new DateTime(1990, 11, 30), new DateTime(1992, 7, 30), new DateTime(1992, 10, 24) };
            var heightsInCentimeters = new int[] { 178, 168, 170 };
            var eloRatings = new int[] { 2872, 2822, 2805 };
            var firstNames = new string[] { "Carlsen", "Fabiano", "Ding" };
            var lastNames = new string[] { "Magnus", "Caruana", "Liren" };
            var countryCodes = new CountryCode[] { CountryCode.NOR, CountryCode.USA, CountryCode.CHN };
            var thisYearTrainingTime = new TimeSpan[] { TimeSpan.FromHours(800), TimeSpan.FromHours(700), TimeSpan.FromHours(600) };
            var orderBy = OrderBy.Height;
            var orderDirection = OrderDirection.Descending;

            // Act
            var result = _homework.GetChessPlayerInfo(birthdates, heightsInCentimeters, eloRatings, firstNames, lastNames, countryCodes, thisYearTrainingTime, orderBy, orderDirection, default);

            // Assert
            result.Should().Be(GetCarlsenMagnus());
        }

        [Test]
        public void GetChessPlayerInfo_GetSmallestPlayerFilteringChinesePlayers_ReturnsDingLiren()
        {
            // Arrange
            var birthdates = new DateTime[] { new DateTime(1990, 11, 30), new DateTime(1992, 7, 30), new DateTime(1992, 10, 24) };
            var heightsInCentimeters = new int[] { 178, 168, 170 };
            var eloRatings = new int[] { 2872, 2822, 2805 };
            var firstNames = new string[] { "Carlsen", "Fabiano", "Ding" };
            var lastNames = new string[] { "Magnus", "Caruana", "Liren" };
            var countryCodes = new CountryCode[] { CountryCode.NOR, CountryCode.USA, CountryCode.CHN };
            var thisYearTrainingTime = new TimeSpan[] { TimeSpan.FromHours(800), TimeSpan.FromHours(700), TimeSpan.FromHours(600) };
            var orderBy = OrderBy.Height;
            var orderDirection = OrderDirection.Ascending;
            var countryFilter = CountryCode.USA;

            // Act
            var result = _homework.GetChessPlayerInfo(birthdates, heightsInCentimeters, eloRatings, firstNames, lastNames, countryCodes, thisYearTrainingTime, orderBy, orderDirection, countryFilter);

            // Assert
            result.Should().Be(GetDingLiren());
        }

        [Test]
        public void GetChessPlayerInfo_AllPlayersAreFiltered_ReturnsTupleWithDefaultValues()
        {
            // Arrange
            var birthdates = new DateTime[] { new DateTime(1990, 11, 30) };
            var heightsInCentimeters = new int[] { 178 };
            var eloRatings = new int[] { 2872 };
            var firstNames = new string[] { "Carlsen" };
            var lastNames = new string[] { "Magnus" };
            var countryCodes = new CountryCode[] { CountryCode.NOR };
            var thisYearTrainingTime = new TimeSpan[] { TimeSpan.FromHours(800) };
            var orderBy = OrderBy.Height;
            var orderDirection = OrderDirection.Ascending;
            var countryFilter = CountryCode.NOR;

            // Act
            var result = _homework.GetChessPlayerInfo(birthdates, heightsInCentimeters, eloRatings, firstNames, lastNames, countryCodes, thisYearTrainingTime, orderBy, orderDirection, countryFilter);

            // Assert
            result.Should().Be(GetTupleWithDefaultValues());
        }

        private static Tuple<string, int, int, int, CountryCode, TimeSpan> GetTupleWithDefaultValues()
        {
            return new Tuple<string, int, int, int, CountryCode, TimeSpan>(default, default, default, default, default, default);
        }

        private static Tuple<string, int, int, int, CountryCode, TimeSpan> GetCarlsenMagnus()
        {
            return new Tuple<string, int, int, int, CountryCode, TimeSpan>("Carlsen Magnus", 29, 178, 2872, CountryCode.NOR, TimeSpan.FromHours(100));
        }

        private static Tuple<string, int, int, int, CountryCode, TimeSpan> GetFabianoCaruana()
        {
            return new Tuple<string, int, int, int, CountryCode, TimeSpan>("Fabiano Caruana", 28, 168, 2822, CountryCode.USA, TimeSpan.FromHours(87.5));
        }

        private static Tuple<string, int, int, int, CountryCode, TimeSpan> GetDingLiren()
        {
            return new Tuple<string, int, int, int, CountryCode, TimeSpan>("Ding Liren", 27, 170, 2805, CountryCode.CHN, TimeSpan.FromHours(75));
        }
    }
}
