using System;

namespace Calmo.Core.Validator.Formats.Brazil
{
	/// <summary>
	/// Definition for brazilian phone number formatting
	/// </summary>
	public class PhoneFormatDefinition : FormatDefinition
    {
        private const string FORMAT_VALIDATION_REGEX = @"(?:\+\d\d)*\s*(?:\(\d\d\))*\s*\d*\d\d\d\d\-*\d\d\d\d";

        internal PhoneFormatDefinition() : base(FORMAT_VALIDATION_REGEX)
        {
            
        }

	    /// <summary>
	    /// Apply the brazilian phone (mobile or otherwise) mask in a given value
	    /// </summary>
	    /// <param name="value">unformatted value</param>
	    /// <returns>Formatted value</returns>
		public override string Format(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return null;

            value = this.Unformat(value);

            if (value.Length > 13)
                value = value.Substring(0, 13);
            else if (value.Length < 8)
                value = value.PadLeft(8, '0');

            if (value.Length == 13)
                return $"+{value.Substring(0, 2)} ({value.Substring(2, 2)}) {value.Substring(4, 5)}-{value.Substring(9, 4)}";

            if (value.Length == 12)
                return $"+{value.Substring(0, 2)} ({value.Substring(2, 2)}) {value.Substring(4, 4)}-{value.Substring(8, 4)}";

            if (value.Length == 11)
                return $"({value.Substring(0, 2)}) {value.Substring(2, 5)}-{value.Substring(7, 4)}";

            if (value.Length == 10)
                return $"({value.Substring(0, 2)}) {value.Substring(2, 4)}-{value.Substring(6, 4)}";

            if (value.Length == 9)
                return $"{value.Substring(0, 5)}-{value.Substring(5, 4)}";

            return $"{value.Substring(0, 4)}-{value.Substring(4, 4)}";
        }

		/// <summary>
		/// Remove the brazilian phone (mobile or otherwise) mask from a given value
		/// </summary>
		/// <param name="value">Formatted value</param>
		/// <returns>Unformatted value</returns>
		public override string Unformat(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return null;

            return value.Replace("-", String.Empty).Replace("(", String.Empty).Replace(")", String.Empty).Replace(" ", String.Empty).Replace("+", String.Empty);
        }
        
		/// <summary>
		/// Get the area code from a given formatted number
		/// </summary>
		/// <param name="value">formatted phone number</param>
		/// <returns>The brazilian two digit area code</returns>
        public string GetAreaCode(string value)
        {
            if (String.IsNullOrWhiteSpace(value)) return null;

            value = this.Unformat(value);

            if (value.Length > 13)
                value = value.Substring(0, 13);
            else if (value.Length < 10)
                return null;
            
            return value.Substring(0, 2);
        }

		/// <summary>
		/// Remove the area code from a full phone number
		/// </summary>
		/// <param name="value">phone number</param>
		/// <returns>Formatted phone number without area code</returns>
        public string GetNumberWithoutAreaCode(string value)
        {
            if (String.IsNullOrWhiteSpace(value)) return null;

            if (value.Length > 13)
                value = value.Substring(0, 13);
            else if (value.Length < 8)
                value = value.PadLeft(8, '0');

            if (value.Length >= 12)
                return value.Substring(4, value.Length - 4);

            if (value.Length >= 10)
                return value.Substring(2, value.Length - 2);

            return this.Format(value);
        }
    }
}
