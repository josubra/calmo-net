using System;

namespace Calmo.Core.Validator.Documents.Brazil
{
	/// <summary>
	/// Rules to validate a brazilian CNPJ document
	/// </summary>
	public class CNPJDocumentDefinition : DocumentDefinition
    {
		/// <summary>
		/// Validate a given value against CNPJ rules
		/// </summary>
		/// <param name="value">string with the CNPJ to be validated</param>
		/// <returns>if the given string is a valid CNPJ</returns>
		public override bool Validate(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return false;

            value = FormatValidation.Brazil.CNPJ.Unformat(value);

            if (value == "00000000000000")
                return false;

            //sequencia de numeros com 12 digitos para fazer o calculo para o primeiro digito
            var multiplicadorPrimeiroDigito = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicadorSegundoDigito = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            value = value.Trim();
            value = value.Replace(".", "").Replace("-", "").Replace("/", "");

            if (value.Length != 14)
                return false;

            var novoCnpj = value.Substring(0, 12);
            var somar = 0;

            for (int pos = 0; pos < 12; pos++)
                somar = somar + Convert.ToInt32(novoCnpj[pos].ToString()) * multiplicadorPrimeiroDigito[pos];

            //Aplica Modulo 11
            var restoDivisao = (somar % 11);

            if (restoDivisao < 2)
                restoDivisao = 0;
            else
                restoDivisao = 11 - restoDivisao;

            var numDigito = restoDivisao.ToString();

            novoCnpj = novoCnpj + numDigito;
            somar = 0;

            // repeti��o para calcular o segundo digito do CNPJ
            for (int pos = 0; pos < 13; pos++)
                somar = somar + Convert.ToInt32(novoCnpj[pos].ToString()) * multiplicadorSegundoDigito[pos];

            //Aplica Modulo 11
            restoDivisao = (somar % 11);

            if (restoDivisao < 2)
                restoDivisao = 0;
            else
                restoDivisao = 11 - restoDivisao;

            novoCnpj = novoCnpj + restoDivisao;

            //aqui fazemos a compara��o com o CNPJ informado com o CNPJ calculado
            return novoCnpj == value;
        }
    }
}