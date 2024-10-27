using ProductCatalogCore.Interfaces;

using System;
using System.Linq;

namespace ProductCatalogInfrastructure.Services
{
    public class Randomizer : IRandomizer
    {
        private static readonly Random random = new();
        public string RandomAlphabet(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string RandomAlphabet(int length, string dateFormat)
        {
            if (string.IsNullOrEmpty(dateFormat))
            {
                throw new Exception("RadomAlpabet Dataformat string can't be empty.");
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string date = DateTime.Now.ToString(dateFormat);
            string randomStr = new(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            string res = date + randomStr;
            return res;
        }

        public string RandomAlphabet(int length, string assignCode, string dateFormat)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string date = DateTime.Now.ToString(dateFormat);
            string randomStr = new(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            string res = assignCode + date + randomStr;
            return res;
        }

        public string RandomAlphanumeric(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string RandomAlphanumeric(int length, string dateFormat)
        {
            if (string.IsNullOrEmpty(dateFormat))
            {
                throw new Exception("RadomAlpabet Dataformat string can't be empty.");
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890";
            string date = DateTime.Now.ToString(dateFormat);
            string randomStr = new(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            string res = date + randomStr;
            return res;
        }

        public string RandomAlphanumeric(int length, string assignCode, string dateFormat)
        {
            if (string.IsNullOrEmpty(dateFormat))
            {
                throw new Exception("RadomAlpabet Dataformat string can't be empty.");
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890";
            string date = DateTime.Now.ToString(dateFormat);
            string randomStr = new(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            string res = assignCode + date + randomStr;
            return res;
        }
    }
}
