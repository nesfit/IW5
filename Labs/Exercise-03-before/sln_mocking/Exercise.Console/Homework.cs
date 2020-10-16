using System;

namespace Exercise.Console
{
    public class Homework
    {
        /// <summary>
        /// Returns the first chess player info that satisfies the specified conditions.
        /// Players are ordered based on orderBy parameter value in the ascending or descending direction based on orderDirection parameter value
        /// and filtered based on countryFilter parameter value.
        /// <para>Exceptions: "System.ArgumentException" Thrown when birthdates array does not contain any elements.
        /// Thrown when not all arrays have the same length.
        /// Thrown when orderBy parameter is Unknown or outside of the OrderBy enum values.</para>
        /// </summary>
        /// <param name="birthdates">Array containing birthdates.</param>
        /// <param name="heightsInCentimeters">Array containing heights in centimeters.</param>
        /// <param name="eloRatings">Array containing Elo ratings.</param>
        /// <param name="firstNames">Array containing first names.</param>
        /// <param name="lastNames">Array containing last names.</param>
        /// <param name="countryCodes">Array containing country codes.</param>
        /// <param name="thisYearTrainingTime">Array containing this year training time.</param>
        /// <param name="orderBy">Order chess players by this parameter.</param>
        /// <param name="orderDirection">Order direction.</param>
        /// <param name="countryFilter">Coutry filter.</param>
        /// <returns>The first chess player info containing his/her full name, age in years, height in centimeters, elo rating, country code and month average training time (thisYearTrainingTime / number of months already elapsed this year).</returns>
        public Tuple<string, int, int, int, CountryCode, TimeSpan> GetChessPlayerInfo(
            DateTime[] birthdates,
            int[] heightsInCentimeters,
            int[] eloRatings,
            string[] firstNames,
            string[] lastNames,
            CountryCode[] countryCodes,
            TimeSpan[] thisYearTrainingTime,
            OrderBy orderBy,
            OrderDirection orderDirection,
            CountryCode countryFilter)
        {
            ValidateBirthdatesArray(birthdates);
            ValidateArraysSizes(birthdates, heightsInCentimeters, eloRatings, firstNames, lastNames, countryCodes, thisYearTrainingTime);
            ValidateOrderByValue(orderBy);

            int playerIndex = GetPlayerIndexAccordingToOrderByValue(birthdates, heightsInCentimeters, eloRatings, countryCodes, orderBy, orderDirection, countryFilter);
            if (WasPlayerFound(playerIndex))
            {
                return new Tuple<string, int, int, int, CountryCode, TimeSpan>(
                    GetFullName(firstNames[playerIndex], lastNames[playerIndex]),
                    GetAgeInYears(birthdates[playerIndex]),
                    heightsInCentimeters[playerIndex],
                    eloRatings[playerIndex],
                    countryCodes[playerIndex],
                    GetMonthAverageTrainingTime(thisYearTrainingTime[playerIndex]));
            }
            else
            {
                return new Tuple<string, int, int, int, CountryCode, TimeSpan>(default, default, default, default, default, default);
            }
        }

        private int GetPlayerIndexAccordingToOrderByValue(DateTime[] birthdates, int[] heightsInCentimeters, int[] eloRatings, CountryCode[] countryCodes, OrderBy orderBy, OrderDirection orderDirection, CountryCode countryFilter)
        {
            int playerIndex;
            switch (orderBy)
            {
                case OrderBy.Age:
                    playerIndex = GetPlayerIndexByAge(birthdates, orderDirection, countryCodes, countryFilter);
                    break;
                case OrderBy.Height:
                    playerIndex = GetPlayerIndexByHeight(heightsInCentimeters, orderDirection, countryCodes, countryFilter);
                    break;
                case OrderBy.EloRating:
                    playerIndex = GetPlayerIndexByEloRating(eloRatings, orderDirection, countryCodes, countryFilter);
                    break;
                default:
                    throw new ArgumentException("Unexpected OrderBy value", nameof(orderBy));
            }
            return playerIndex;
        }

        private int GetPlayerIndexByAge(DateTime[] birthdates, OrderDirection orderDirection, CountryCode[] countryCodes, CountryCode countryFilter)
        {
            int playerIndex = -1;
            int highestOrLovestAge = GetHighestOrLovestValueByOrderDirection(orderDirection);

            for (int i = 0; i < birthdates.Length; i++)
            {
                if (countryCodes[i] == countryFilter)
                {
                    continue;
                }
                (playerIndex, highestOrLovestAge) = GetPlayerIndex(orderDirection, GetAgeInYears(birthdates[i]), highestOrLovestAge, playerIndex, i);
            }
            return playerIndex;
        }

        private int GetPlayerIndexByHeight(int[] heightsInCentimeters, OrderDirection orderDirection, CountryCode[] countryCodes, CountryCode countryFilter)
        {
            int playerIndex = -1;
            int highestOrLovestHeight = GetHighestOrLovestValueByOrderDirection(orderDirection);

            for (int i = 0; i < heightsInCentimeters.Length; i++)
            {
                if (countryCodes[i] == countryFilter)
                {
                    continue;
                }
                (playerIndex, highestOrLovestHeight) = GetPlayerIndex(orderDirection, heightsInCentimeters[i], highestOrLovestHeight, playerIndex, i);
            }
            return playerIndex;
        }


        private int GetPlayerIndexByEloRating(int[] eloRatings, OrderDirection orderDirection, CountryCode[] countryCodes, CountryCode countryFilter)
        {
            int playerIndex = -1;
            int highestOrLovestEloRating = GetHighestOrLovestValueByOrderDirection(orderDirection);

            for (int i = 0; i < eloRatings.Length; i++)
            {
                if (countryCodes[i] == countryFilter)
                {
                    continue;
                }
                (playerIndex, highestOrLovestEloRating) = GetPlayerIndex(orderDirection, eloRatings[i], highestOrLovestEloRating, playerIndex, i);
            }
            return playerIndex;
        }

        private int GetHighestOrLovestValueByOrderDirection(OrderDirection orderDirection)
        {
            return orderDirection == OrderDirection.Ascending
                ? int.MaxValue
                : int.MinValue;
        }

        private (int playerIndex, int highestOrLovestAge) GetPlayerIndex(OrderDirection orderDirection, int currentValue, int highestOrLovestValue, int playerIndex, int currentIndex)
        {
            if (orderDirection == OrderDirection.Ascending
                && currentValue < highestOrLovestValue)
            {
                playerIndex = currentIndex;
                highestOrLovestValue = currentValue;
            }
            else if (currentValue > highestOrLovestValue)
            {
                playerIndex = currentIndex;
                highestOrLovestValue = currentValue;
            }
            return (playerIndex, highestOrLovestValue);
        }

        private bool WasPlayerFound(int playerIndex)
        {
            return playerIndex != -1;
        }

        // https://stackoverflow.com/a/1404
        private int GetAgeInYears(DateTime birthdate)
        {
            // Save today's date.
            var today = DateTime.Today;

            // Calculate the age.
            var age = today.Year - birthdate.Year;

            // Go back to the year the person was born in case of a leap year
            if (birthdate.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        private string GetFullName(string firstName, string lastName)
        {
            return $"{firstName} {lastName}";
        }

        private void ValidateOrderByValue(OrderBy orderBy)
        {
            if (orderBy == OrderBy.Unknown)
            {
                throw new ArgumentException("OrderBy value cannot be Unknown.", nameof(orderBy));
            }

            if (!Enum.IsDefined(typeof(OrderBy), orderBy))
            {
                throw new ArgumentException("OrderBy value is outside the range of allowed values.", nameof(orderBy));
            }
        }

        private void ValidateArraysSizes(DateTime[] birthdates, int[] heightsInCentimeters, int[] eloRatings, string[] firstNames, string[] lastNames, CountryCode[] countryCodes, TimeSpan[] thisYearTrainingTime)
        {
            int firstArrayLength = birthdates.Length;
            if (firstArrayLength != heightsInCentimeters?.Length
                || firstArrayLength != eloRatings?.Length
                || firstArrayLength != firstNames?.Length
                || firstArrayLength != lastNames?.Length
                || firstArrayLength != countryCodes?.Length
                || firstArrayLength != thisYearTrainingTime?.Length)
            {
                throw new ArgumentException("Data arrays does not have the same size.");
            }
        }

        private void ValidateBirthdatesArray(DateTime[] birthdates)
        {
            if (birthdates.Length == 0)
            {
                throw new ArgumentException("Birthday array is empty.", nameof(birthdates));
            }
        }

        private TimeSpan GetMonthAverageTrainingTime(TimeSpan timeSpan)
        {
            return timeSpan / DateTime.Now.Month;
        }
    }
}
