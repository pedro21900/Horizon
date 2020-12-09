

using System.Data;
using System.Data.OleDb;

namespace Horizon_Contabilidade
{
    class Db1
    {
        static excel_manipulations excel_Manipulations = new excel_manipulations();
        static OleDbConnection conexaoDb;
        static string sourcetable = excel_Manipulations.Sourcetable;
        //conexão
        static public OleDbConnection ConectDb()
        {
            conexaoDb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel_Manipulations.Sourcedb + "; Persist Security Info=False;");

            return conexaoDb;
        }
        static private OleDbConnection ConectTable()
        {
            if (string.IsNullOrEmpty(sourcetable) == false)
            {

                excel_Manipulations.Conexaotable = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sourcetable + ";" + "Extended Properties = Excel 12.0;HDR =YES;");
            }
            return excel_Manipulations.Conexaotable;

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
        static void importtoDb()
        {
            OleDbDataAdapter ada = new OleDbDataAdapter("select * from Sheet1", ConectTable());
            DataSet ds = new DataSet();

            ada.Fill(ds);
            ConectTable().Close();


            ConectDb().Open();

            string queryString = "SELECT * from tabledb";//+ lblTable.Text;

            OleDbDataAdapter adapter = new OleDbDataAdapter(queryString, ConectDb());

            DataTable dtAccess = new DataTable();

            DataTable dtCSV = new DataTable();

            dtCSV = ds.Tables[0];

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
