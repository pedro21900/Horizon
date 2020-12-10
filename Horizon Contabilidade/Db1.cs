

using System.Windows;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Horizon_Contabilidade
{
    class Db1
    {
        //Variaves
        static excel_manipulations excel_Manipulations = new excel_manipulations();
        static string sourcedb = Properties.Settings.Default.Pastainicial;
        static OleDbConnection conexaoDb;
        static OleDbConnection conexaotable;
        static private string sourcetable;
        static public string Setsoucetable
        {
            set
            {
                sourcetable = value;
            }
        }
        //conexão
        static private OleDbConnection ConectTable()
        {
          
            string Ext = Path.GetExtension(sourcetable);
            if (string.IsNullOrEmpty(sourcetable) == false)
            {

                //verifica a versão do Excel pela extensão
                if (Ext == ".xls")
                { //para o  Excel 97-03    
                    conexaotable = new OleDbConnection
                     ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+ sourcetable + ";Extended Properties='Excel 8.0;HDR=YES;'");
                    
                }
                else if (Ext == ".xlsx")
                { //para o  Excel 07 e superior
                    conexaotable = new OleDbConnection
                        ("Provider=Microsoft.ACE.OLEDB.12.0; Data Source =" + sourcetable + "; Extended Properties = 'Excel 8.0;HDR=YES'");
                        
                }
            }
            conexaotable.Open();
            return conexaotable;

        }
        static public OleDbConnection ConectDb()
        {
            conexaoDb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sourcedb + "; Persist Security Info=False;");
            conexaoDb.Open();
            return conexaoDb;
        }        
        //Metodos
        static private string NameTable(OleDbConnection Conect,int indexName)
        {
            DataTable dtSchema = Conect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string nomePlanilha = dtSchema.Rows[indexName]["TABLE_NAME"].ToString();
            return nomePlanilha;
        }
        private void lol()
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
            cmd.Connection = ConectTable();
            ConectTable().Open();
            DataTable dtSchema;

            dtSchema = ConectTable().GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string nomePlanilha = dtSchema.Rows[0]["TABLE_NAME"].ToString();

            //le todos os dados da planilha para o Data Table    
            cmd.CommandText = "SELECT * From [" + nomePlanilha + "]";
            dataAdapter.SelectCommand = cmd;
            //   dataAdapter.Fill(dt);

            //  conn.Close();
        }
            //import table to datatable 
        static public DataTable TableDb(string command)
        {
            OleDbCommand cmd = new OleDbCommand(command, Db1.ConectDb());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;

        }
            //Importa para o Banco de dados as planilhas
                    static public void importtoDb()
        {

            
            OleDbDataAdapter ada = new OleDbDataAdapter("select * from ["+ NameTable(ConectTable(),0) + "]", ConectTable());
            
            DataSet ds = new DataSet();
            
            ada.Fill(ds);
            
            excel_Manipulations.check_table(ds);
            
            ConectTable().Close();

            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * from [" + NameTable(ConectDb(), 21) + "]", ConectDb());

            DataTable dtAccess = new DataTable();

            DataTable dtCSV =ds.Tables[0];

            using (new OleDbCommandBuilder(adapter))
            {
                adapter.Fill(dtAccess);
                dtAccess.Merge(dtCSV);
                adapter.Update(dtAccess);
               
            }

            ConectDb().Close();
        }
        static private void InsertRow(string connectionString, string[] values, string[] colum)
        {


            string queryString =
                "INSERT INTO Customers (" + colum + ") Values('" + values + "')";
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
    }
}
