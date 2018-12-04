using System;

namespace Calmo.Core.Validator.Formats.Brazil
{
	/// <summary>
	/// Definition for CEP (zip code) formatting
	/// </summary>
    public class CEPFormatDefinition : FormatDefinition
    {
        private const string FORMAT_VALIDATION_REGEX = @"^\d\d\d\d\d\-\d\d\d$";

        internal CEPFormatDefinition() : base(FORMAT_VALIDATION_REGEX)
        {
            
        }
		/// <summary>
		/// Apply the CEP mask in a given value
		/// </summary>
		/// <param name="value">unformatted value</param>
		/// <returns>Formatted value</returns>
		public override string Format(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return null;

            value = this.Unformat(value);

            if (value.Length > 8)
                value = value.Substring(0, 8);
            else if (value.Length < 8)
                value = value.PadLeft(8, '0');

            return value.Insert(5, "-");
        }

		/// <summary>
		/// Remove the CEP mask from a given value
		/// </summary>
		/// <param name="value">Formatted value</param>
		/// <returns>Unformatted value</returns>
        public override string Unformat(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return null;

            return value.Replace("-", String.Empty);
        }
    }
}
