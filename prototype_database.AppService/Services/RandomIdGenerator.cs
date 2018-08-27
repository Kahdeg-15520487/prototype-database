using prototype_database.Contract;
using System;
using System.Text;

namespace prototype_database.AppService.Services
{
    public class RandomIdGenerator : IRandomIdGenerator
    {
        private readonly char[] Digits =
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
            .ToCharArray();

        private Random _random = new Random();

        public void Init(int seed)
        {
            _random = new Random(seed);
        }

        public string GetBase62(int length)
        {
            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                sb.Append(Digits[_random.Next(62)]);
            }

            return sb.ToString();
        }

        public string GetBase36(int length)
        {
            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                sb.Append(Digits[_random.Next(36)]);
            }

            return sb.ToString();
        }

        public long Base36ToDecimalSystem(string number)
        {
            int radix = 36;

            if (string.IsNullOrEmpty(number))
            {
                return 0;
            }

            long result = 0;
            long multiplier = 1;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                char c = number[i];
                if (i == 0 && c == '-')
                {
                    // This is the negative sign symbol
                    result = -result;
                    break;
                }

                int digit = Array.IndexOf(Digits, c);
                if (digit == -1)
                {
                    throw new ArgumentException(
                            "Invalid character in the arbitrary numeral system number",
                            "number");
                }

                result += digit * multiplier;
                multiplier *= radix;
            }

            return result;
        }

        public long Base62ToDecimalSystem(string number)
        {
            int radix = 62;

            if (string.IsNullOrEmpty(number))
            {
                return 0;
            }

            long result = 0;
            long multiplier = 1;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                char c = number[i];
                if (i == 0 && c == '-')
                {
                    // This is the negative sign symbol
                    result = -result;
                    break;
                }

                int digit = Array.IndexOf(Digits, c);
                if (digit == -1)
                {
                    throw new ArgumentException(
                            "Invalid character in the arbitrary numeral system number",
                            "number");
                }

                result += digit * multiplier;
                multiplier *= radix;
            }

            return result;
        }

        public string GetId(int length)
        {
            return GetBase36(length);
        }
    }
}
