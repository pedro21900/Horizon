using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows;

namespace Horizon_Contabilidade
{
    class excel_manipulations
    {        
        static invalid_character ic=  new invalid_character();
        static string[] colummrelfat = new string[] { "Forma Pgto.", "Valor", "Desconto Faturado", "Dt.Vencimento", "Cliente", "NFCe/SAT/Cupom", "Código", "Fatura", "DataFaturamento", "Tipo" };
        static string[] colummxml = new string[] {"Emitente", "Tipo Doc.", "Finalidade", "Destinatário", "N°. Nota", "Série", "Chave de Acesso", "Emissão", "Operação"," Valor"};
        // Lista nome de colunas e retorna em um objeto
        static private string[] listNameColumns(DataSet tables)
        {
            string[] listaNameColumns = tables.Tables[0].Columns.OfType<DataColumn>().Select(x => x.ColumnName).ToArray();

            return listaNameColumns;
        } 
        //Verrifica se a tabela é uma de relatorio de faturamento ou xml
        public bool check_table(DataSet tables)
        {
           
            if (ic.TratarTermoComCaracteresEspeciais(string.Join(",", listNameColumns(tables)))
                == ic.TratarTermoComCaracteresEspeciais(string.Join(",", colummrelfat)))
            {
                return true;
            }
            else if (ic.TratarTermoComCaracteresEspeciais(string.Join(",", listNameColumns(tables)))
                == ic.TratarTermoComCaracteresEspeciais(string.Join(",", colummxml)))
            {
                return true;
            }
            else
            {
                //mensagem dizendo que o arquivo não é compativél
                MessageBox.Show("Erro : Planilha não compatível");               
                return false;
            }

        }
        //Metodos gets e settes
        
        

    }
}
