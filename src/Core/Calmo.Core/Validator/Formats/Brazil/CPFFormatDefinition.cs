using System;

namespace Calmo.Core.Validator.Formats.Brazil
{
	/// <summary>
	/// Definition for CPF formatting
	/// </summary>
	public class CPFFormatDefinition : FormatDefinition
    {
        private const string FORMAT_VALIDATION_REGEX = @"^\d\d\d\.\d\d\d\.\d\d\d\-\d\d$";

        internal CPFFormatDefinition() : base(FORMAT_VALIDATION_REGEX)
        {
            
        }

		/// <summary>
		/// Apply the CPF mask in a given value
		/// </summary>
		/// <param name="value">unformatted value</param>
		/// <returns>Formatted value</returns>
		public override string Format(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return null;

            value = this.Unformat(value);
            return $@"{Convert.ToInt64(value):000\.000\.000\-00}";
        }

		/// <summary>
		/// Remove the CPF mask from a given value
		/// </summary>
		/// <param name="value">Formatted value</param>
		/// <returns>Unformatted value</returns>
		public override string Unformat(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return null;

            return value.Replace("-", String.Empty).Replace(".", String.Empty);
        }
    }
}
