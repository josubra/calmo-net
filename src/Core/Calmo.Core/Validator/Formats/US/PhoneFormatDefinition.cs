using System;

namespace Calmo.Core.Validator.Formats.US
{
	/// <summary>
	/// Definition for united states phone number formatting
	/// </summary>
	public class PhoneFormatDefinition : FormatDefinition
    {
        private const string FORMAT_VALIDATION_REGEX = @"(?:\+\d)*\s*\(*\d{3}\)*\s*\d{3}-*\d{4}";

        internal PhoneFormatDefinition() : base(FORMAT_VALIDATION_REGEX)
        {
            
        }

	    /// <summary>
	    /// Apply the US phone (mobile or otherwise) mask in a given value
	    /// </summary>
	    /// <param name="value">unformatted value</param>
	    /// <returns>Formatted value</returns>
		public override string Format(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return null;

            value = this.Unformat(value);

            if (value.Length > 11)
                value = value.Substring(0, 11);
            else if (value.Length < 10)
                value = value.PadLeft(10, '0');

            if (value.Length == 11)
                return $"+{value.Substring(0, 1)} ({value.Substring(1, 3)}) {value.Substring(4, 3)}-{value.Substring(7, 4)}";

            return $"({value.Substring(0, 3)}) {value.Substring(3, 3)}-{value.Substring(6, 4)}";
        }

	    /// <summary>
	    /// Remove the US phone (mobile or otherwise) mask from a given value
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

            if (value.Length > 11)
                value = value.Substring(0, 11);
            else if (value.Length < 10)
                return null;
            
            return value.Substring(0, 3);
        }
    }
}
