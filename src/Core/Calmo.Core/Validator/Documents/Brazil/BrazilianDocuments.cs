using Calmo.Core.Validator.Documents.Brazil;

namespace Calmo.Core.Validator
{
	/// <summary>
	/// Container for the brazilian documents validation rules
	/// </summary>
    public class BrazilianDocuments
    {
        internal BrazilianDocuments()
        {

        }

		/// <summary>
		/// CPF Rules Definition
		/// </summary>
        public CPFDocumentDefinition CPF = new CPFDocumentDefinition();
		/// <summary>
		/// CNPJ Rules Definition
		/// </summary>
        public CNPJDocumentDefinition CNPJ = new CNPJDocumentDefinition();
    }
}