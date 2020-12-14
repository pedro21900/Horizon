

using System.Windows;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System;
using System.Windows.Forms;

namespace Horizon_Contabilidade
{
    class Db1
    {
        //Classes
        static excel_manipulations excel_Manipulations = new excel_manipulations();
        static invalid_character ic = new invalid_character();
        //Variaves

        static string sourcedb= Properties.Settings.Default.Pastainicial;
        static OleDbConnection conexaoDb;
        static OleDbConnection conexaotable;
        static private string sourcetable;
        static string nameTable;
        static string dateColumn;

        //Datas
        static DataSet ds = new DataSet();
        static public string Setsoucetable
        {
            get
            {
                return sourcetable;
            }
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
                        ("Provider=Microsoft.ACE.OLEDB.12.0; Data Source =" + sourcetable + "; Extended Properties = 'Excel 12.0;HDR=YES'");
                        
                }
            }
            conexaotable.Open();
            return conexaotable;

        }
        static public OleDbConnection ConectDb()
        {
            conexaoDb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sourcedb +"; Persist Security Info=False;");
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
        private static string columnExport()
        {
            
            string column = "";
            if (excel_Manipulations.Qtdcolumn == 4) {
                column = "Emitente,Destinatário,[N°# Nota],Emissão,Valor";
                dateColumn = "Emissão";
                nameTable = "XmlTable";
            }
            else if (excel_Manipulations.Qtdcolumn == 8) {
                column = "[Forma Pgto#],Valor,Desconto,Faturado,Cliente,[NFCe/SAT/Cupom],[Código Fatura],DataFaturamento,Tipo";
                dateColumn = "DataFaturamento";
                nameTable = "RelFatTable";
            }
            else { }
            return column;
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
        static public DataSet importTable()
        {
            OleDbDataAdapter ada = new OleDbDataAdapter("select "+columnExport()+" from [" +
                NameTable(ConectTable(), 0) + "]", ConectTable());            

            ada.Fill(ds);            

            ConectTable().Close();

            return ds;

        }
        static public void importtoDb()
        {            
            excel_Manipulations.check_table(sourcetable);
            //ConectTable().Close();

            DateTime dateTable = Convert.ToDateTime(ds.Tables[0].Rows[0][dateColumn].ToString());


            OleDbCommand command = new OleDbCommand();
            command.Connection = ConectDb();
            command.CommandText = "CREATE TABLE [" +dateTable.Month.ToString()+dateTable.Year.ToString()+nameTable 
                + "] SELECT * FROM ["+nameTable+"]";
            command.ExecuteNonQuery();
            command.CommandText ="DELETE FROM [" + dateTable.Month.ToString() + " / " + dateTable.Year.ToString() + "." +
                nameTable + "]";
            command.ExecuteNonQuery();

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
