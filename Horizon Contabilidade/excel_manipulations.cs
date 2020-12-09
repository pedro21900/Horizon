using System.Data;
using System.Data.OleDb;

namespace Horizon_Contabilidade
{
    class excel_manipulations
    {
        static OleDbConnection conexao;
        static string command_default= "SELECT * FROM [";
        static string source=Properties.Settings.Default.Pastainicial;
        static string[] colummrelfat = new string[] { "Forma Pgto.", "Valor", "Desconto Faturado", "Dt.Vencimento", "Cliente", "NFCe/SAT/Cupom", "Código", "Fatura", "DataFaturamento", "Tipo" };
        static string[] colummxml = new string[] { };
        //conecção
        static private OleDbConnection ConectDb()
        {
           conexao = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+source+";Extended Properties='Excel 8.0;HDR=YES;'");
           
            return conexao;
        }
        //importa tabela 
        static private DataTable TableDb(string command)
        {
           OleDbCommand cmd = new OleDbCommand(command, ConectDb());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            return dt;

        }
        
        //Verrifica se a tabela é uma de relatorio de faturamento ou xml
        static private bool check_table()
        {
            if ( TableDb(command_default).Columns.ToString()==string.Join(",", colummrelfat))
            {
                return true;
            }
            else if (TableDb(command_default).Columns.ToString() == string.Join(",", colummxml))
            {
                return true;
            }
            else
            {
                //mensagem dizendo que o arquivo não é compativél
                return false;
            }

        }



        static private void InsertRow(string connectionString ,string[] values, string[] colum)
        {
           
            
            string queryString =
                "INSERT INTO Customers ("+colum+") Values('"+values+"')";
            OleDbCommand command = new OleDbCommand(queryString);

            using (ConectDb())
            {
                command.Connection = ConectDb();
                ConectDb().Open();
                command.ExecuteNonQuery();

                // The connection is automatically closed at
                // the end of the Using block.
            }

            
        }
        static void importDb()
        {
            OleDbConnection conn = new OleDbConnection(("Provider=Microsoft.ACE.OLEDB.12.0; " + ("data source=C:\\Pasta1.xlsx; " + "Extended Properties=Excel 12.0;")));
            // Select the data from Sheet1 of the workbook.


            OleDbDataAdapter ada = new OleDbDataAdapter("select * from Pasta1", conn);
            DataSet ds = new DataSet();

            ada.Fill(ds);
            conn.Close();


            OleDbConnection myConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"C:\\Database.accdb\";Persist Security Info=False;");
            myConnection.Open();

            string queryString = "SELECT * from Teste Teste  ";//+ lblTable.Text;

            OleDbDataAdapter adapter = new OleDbDataAdapter(queryString, myConnection);

            DataTable dtAccess = new DataTable();

            DataTable dtCSV = new DataTable();

            dtCSV = ds.Tables[0];

            using (new OleDbCommandBuilder(adapter))
            {
                adapter.Fill(dtAccess);
                dtAccess.Merge(dtCSV);
                adapter.Update(dtAccess);
            }

            myConnection.Close();
        }
    }
}
