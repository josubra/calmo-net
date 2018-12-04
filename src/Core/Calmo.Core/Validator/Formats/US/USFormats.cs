using Calmo.Core.Validator.Formats;
using Calmo.Core.Validator.Formats.US;

namespace Calmo.Core.Validator
{
	/// <summary>
	/// Container for all string formatters used in US values (documents, zip codes and phones)
	/// </summary>
	public class USFormats
    {
        internal USFormats()
        {

        }
        
        public FormatDefinition Phone = new PhoneFormatDefinition();
    }
}
