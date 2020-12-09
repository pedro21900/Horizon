using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows;

namespace Horizon_Contabilidade
{
    class excel_manipulations
    {
        private OleDbConnection conexaotable;

        static string command_default= "SELECT * FROM [";
        private string sourcedb=Properties.Settings.Default.Pastainicial;
        private string sourcetable;
        static string[] colummrelfat = new string[] { "Forma Pgto.", "Valor", "Desconto Faturado", "Dt.Vencimento", "Cliente", "NFCe/SAT/Cupom", "Código", "Fatura", "DataFaturamento", "Tipo" };
        static string[] colummxml = new string[] {
            "Emitente", "Tipo Doc.", "Finalidade", "Destinatário", "N°. Nota", "Série", "Chave de Acesso", "Emissão", "Operação"," Valor"};
        // Lista nome de colunas e retorna em um objeto
        static private string[] listNameColumns(string tables)
        {
            string[] listaNameColumns = Db1.TableDb(tables).Columns.OfType<DataColumn>().Select(x => x.ColumnName).ToArray();

            return listaNameColumns;
        } 
        //Verrifica se a tabela é uma de relatorio de faturamento ou xml
        static private bool check_table(string tables)
        {
           
            if (string.Join(",", listNameColumns(command_default+ tables+"]")) == string.Join(",", colummrelfat))
            {
                return true;
            }
            else if (string.Join(",", listNameColumns(command_default + tables + "]")) == string.Join(",", colummxml))
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
        public OleDbConnection Conexaotable { get => conexaotable; set => conexaotable = value; }
        public string Sourcedb { get => sourcedb; set => sourcedb = value; }
        public string Sourcetable { get => sourcetable; set => sourcetable = value; }


    }
}
