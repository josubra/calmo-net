using System;

namespace Calmo.Core.Validator.Formats.Brazil
{
	/// <summary>
	/// Definition for CNPJ formatting
	/// </summary>
	public class CNPJFormatDefinition : FormatDefinition
    {
        private const string FORMAT_VALIDATION_REGEX = @"(^(\d{2}.\d{3}.\d{3}/\d{4}-\d{2})|(\d{14})$)";

        internal CNPJFormatDefinition() : base(FORMAT_VALIDATION_REGEX)
        {
            
        }

	    /// <summary>
	    /// Apply the CNPJ mask in a given value
	    /// </summary>
	    /// <param name="value">unformatted value</param>
	    /// <returns>Formatted value</returns>
		public override string Format(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return null;

            value = this.Unformat(value);
            return $@"{Convert.ToInt64(value):00\.000\.000\/0000\-00}";
        }

	    /// <summary>
	    /// Remove the CNPJ mask from a given value
	    /// </summary>
	    /// <param name="value">Formatted value</param>
	    /// <returns>Unformatted value</returns>
		public override string Unformat(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return null;

            return value.Replace(".", String.Empty).Replace("-", String.Empty).Replace("/", String.Empty);
        }
    }
}
