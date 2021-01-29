using System.Text.RegularExpressions;

namespace Horizon_Contabilidade
{
    internal class invalid_character
    {
        public string ObterStringSemAcentosECaracteresEspeciais(string str)
        {
            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }
            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };

            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                str = str.Replace(caracteresEspeciais[i], "");
            }

            /** Troca os espaços no início por "" **/
            str = str.Replace("^\\s+", "");
            /** Troca os espaços no início por "" **/
            str = str.Replace("\\s+$", "");
            /** Troca os espaços duplicados, tabulações e etc por " " **/
            str = str.Replace("\\s+", " ");
            return str;
        }
        public string TratarTermoComCaracteresEspeciais(string termo)
        {
            termo = ObterStringSemAcentosECaracteresEspeciais(termo);
            string termotratado = "";

            // Recebo o termo digitado com o tratamento de substituição de caracteres
            termotratado = ObterStringSemAcentosECaracteresEspeciais(termo);

            // Recebo o termo já tratado, mas agora com apenas os caracteres
            // de determinado intervalo
            termotratado = Regex.Replace(termotratado, @"[^a-zA-Z0-9:,]+", "");

            // Por fim recebo termo tratado com todos os caracteres em caixa alta
            termotratado = termotratado.ToUpperInvariant();

            return termotratado;
        }
    }
}
