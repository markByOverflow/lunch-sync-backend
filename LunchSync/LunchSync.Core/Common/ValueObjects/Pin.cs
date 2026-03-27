using LunchSync.Core.Exceptions;
using System.Security.Cryptography;

namespace LunchSync.Core.Common.ValueObjects
{
    public class Pin
    {
        public string Value { get; private set; }

        private Pin(string value)
        {
            Value = value;
        }

        // Tao ma PIN 6 chu so de dung cho flow session.
        public static Pin Generate()
        {
            string pinValue = RandomNumberGenerator.GetInt32(0, 1_000_000).ToString("D6");
            return new Pin(pinValue);
        }

        public override string ToString() => Value;
        public override bool Equals(object? obj) => obj is Pin other && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();

        public static Pin Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 6 || !value.All(char.IsDigit))
            {
                throw new BusinessRuleViolationException("PIN must be exactly 6 digits.");
            }

            return new Pin(value);
        }
    }
}
