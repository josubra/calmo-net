namespace Calmo.Core.Validator.Documents
{
	/// <summary>
	/// Abstract class that represents all document defition
	/// </summary>
    public abstract class DocumentDefinition
    {
		/// <summary>
		/// Validation method
		/// </summary>
		/// <param name="value">string value to be validated</param>
		/// <returns>if the given value is true</returns>
        public abstract bool Validate(string value);
    }
}