using Calmo.Core.Validator.Documents;

namespace Calmo.Core.Validator
{
	/// <summary>
	/// Extension for documents validations
	/// </summary>
    public static class DocumentValidationExtensions
    {
		/// <summary>
		/// Validate a document
		/// </summary>
		/// <param name="value">value to be validated</param>
		/// <param name="definition">Rule definition to be applied</param>
		/// <returns>if the value match the given rule set</returns>
        public static bool ValidateDocument(this string value, DocumentDefinition definition)
        {
            return definition.Validate(value);
        }
    }
}