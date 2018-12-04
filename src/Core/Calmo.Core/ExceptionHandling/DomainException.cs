using System.Collections.Generic;

namespace System
{
	/// <summary>
	/// Exception type used for capturing validation/business errors
	/// </summary>
    public class DomainException : Exception
    {
        private string _separator = Environment.NewLine;

        public readonly IEnumerable<string> Errors;

		/// <summary>
		/// Throw new domain exception with multiple errors using the default separator
		/// </summary>
		/// <param name="errors">An IEnumerable of strings, each one with a different error</param>
        public DomainException(IEnumerable<string> errors)
        {
            this.Errors = errors;
        }

		/// <summary>
		/// Throw new domain exception with a single error
		/// </summary>
		/// <param name="error">The error message</param>
        public DomainException(string error)
        {
            this.Errors = new[] { error };
        }

		/// <summary>
		/// Throw new domain exception with multiple errors using a custom separator
		/// </summary>
		/// <param name="errors">An IEnumerable of strings, each one with a different error</param>
		/// <param name="separator">The separator that will be used to join the error list</param>
		public DomainException(IEnumerable<string> errors, string separator)
        {
            this.Errors = errors;
            _separator = separator ?? _separator;
        }

		/// <summary>
		/// Display all the messages provided using the default or provided separator
		/// </summary>
        public override string Message => String.Join(_separator, this.Errors);
    }
}