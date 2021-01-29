using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace Horizon_Contabilidade
{
    internal class Db
    {
        //Classes
        //static excel_manipulations excel_Manipulations = new excel_manipulations();
        private static readonly invalid_character ic = new invalid_character();

        //Variaves
        private static readonly DataSet Data = new DataSet();
        private static readonly string sourcedb = Properties.Settings.Default.SourceDb;
        private static OleDbConnection conexaoDb;
        private static OleDbConnection conexaotable;
        private static string sourcetable;

        //Properties.Settings.Default.Pastainicial = Form1.sDBstr;
        //   Properties.Settings.Default.Save();
        //Datas
        private static readonly DataSet ds = new DataSet();
        //Getters e Setters
        public static string Setsoucetable
        {
            get => sourcetable;
            set => sourcetable = value;
        }
        //conexão
        private static OleDbConnection ConectTable()
        {

            string Ext = Path.GetExtension(sourcetable);
            if (string.IsNullOrEmpty(sourcetable) == false)
            {

                //verifica a versão do Excel pela extensão
                if (Ext == ".xls")
                { //para o  Excel 97-03    
                    conexaotable = new OleDbConnection
                     ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sourcetable + ";Extended Properties='Excel 8.0;HDR=YES;'");

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
        public static OleDbConnection ConectDb()
        {
            try
            {
                conexaoDb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sourcedb + "; Persist Security Info=False;");
                conexaoDb.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);
                Form1 form1 = new Form1();
                form1.Close();

            }


            return conexaoDb;
        }
        //Metodos
        private static string NameTable(OleDbConnection Conect, int indexName)
        {
            DataTable dtSchema = Conect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string nomePlanilha = dtSchema.Rows[indexName]["TABLE_NAME"].ToString();
            return nomePlanilha;
        }

        public void salvatabela(OleDbDataAdapter oDA, DataSet oDs, string tabela)
        {
            //Usar o objeto Command Bulder para gerar o Comandop Insert
            OleDbCommandBuilder oCB = new OleDbCommandBuilder(oDA);

            //Atualizar o BD com valores do Dataset
            oDA.Update(oDs, tabela);
            //liberar o data adapter , o dataset , o comandbuilder e a conexao
            oDA.Dispose(); oDs.Dispose(); oCB.Dispose();
        }
        public void addlinhalayout1(string tabela, string data, string or, string fornecedor, string compral, string vendal, string compraa, string vendaa,
                                   string col, string lab, string custodevenda, string vendavalor, string loja)
        {
            string sSQL = "select * from " + tabela;
            //criar o data adapter e executar a consulta
            OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, ConectDb());
            //criar o DataSet
            DataSet oDs = new DataSet();
            //Preencher o dataset coom o data adapter
            oDA.Fill(oDs, tabela);
            DataRow oDR = oDs.Tables[tabela].NewRow();

            oDs.Tables[0].Rows.Add(data, filtratexto(or), filtratexto(fornecedor), filtratexto(compral),
                    filtratexto(vendal), filtratexto(compraa), filtratexto(vendaa), filtratexto(col),
                     filtratexto(lab), filtratexto(custodevenda), filtratexto(vendavalor), filtratexto(loja));

            salvatabela(oDA, oDs, tabela);

        }
        public void addlinhalayout2(string tabela, string or, string data, string modeloarmação, string nomelente, string lucroarmacao, string lucrolente, string forenecedorl, string forenecedora,
                                    string lucrototal, string descontot, string taxacartao, string foramdepagamento, string descontol, string descontoa,
                                    string tipodecompra, string tipodecompra1, string tipodecompra2, string marcaa, string marcal, string loja, string Obs)
        {
            string sSQL = "select * from " + tabela;
            //criar o data adapter e executar a consulta
            OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, ConectDb());
            //criar o DataSet
            DataSet oDs = new DataSet();
            //Preencher o dataset coom o data adapter
            oDA.Fill(oDs, tabela);
            oDs.Tables[0].Rows.Add(filtratexto(or), filtratexto(data), filtratexto(modeloarmação), filtratexto(nomelente), filtratexto(lucroarmacao),
    filtratexto(lucrolente), filtratexto(forenecedorl), filtratexto(forenecedora),
    filtratexto(lucrototal), filtratexto(descontot), filtratexto(taxacartao), filtratexto(foramdepagamento),
    filtratexto(descontol), filtratexto(descontoa), filtratexto(tipodecompra), filtratexto(tipodecompra1), filtratexto(tipodecompra2), filtratexto(marcaa), filtratexto(marcal), filtratexto(loja), filtratexto(Obs));
            salvatabela(oDA, oDs, tabela);
        }
        public void Deletalinha(string tabela, string pesquisa)
        {
            try
            {

                string sSQLs12 = "  DELETE* FROM " + tabela + " WHERE Or_os = " + pesquisa;
                OleDbCommand command = new OleDbCommand(sSQLs12, ConectDb());
                command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
        }
        public void atualizar(string tabela, string tabela1, string data, string or, string fornecedor, string compral, string vendal, string compraa, string vendaa, string lab,
                               string col, string custocv, string vendavalor, string resultado, string loja, string modeloarmação, string nomelente, string lucroarmacao, string lucrolente, string forenecedorl, string forenecedora,
                                string lucrototal, string descontot, string taxacartao, string foramdepagamento, string descontol, string descontoa,
                                string tipodecompra, string tipodecompra1, string tipodecompra2, string marcaa, string marcal, string tipo, string Obs, string pesquisa)
        {
            Deletalinha(tabela, pesquisa);
            Deletalinha(tabela1, pesquisa);
            addlinhalayout1(tabela, data, or, fornecedor, compral, vendal, compraa, vendaa, col,
                                    lab, custocv, vendavalor, loja);
            addlinhalayout2(tabela1, or, data, modeloarmação, nomelente, lucroarmacao, lucrolente, forenecedorl, forenecedora,
                                     lucrototal, descontot, taxacartao, foramdepagamento, descontol, descontoa,
                                     tipodecompra, tipodecompra1, tipodecompra2, marcaa, marcal, loja, Obs);

        }
        public DataSet pesquisaos(string tabela, string coluna, string pesquisa)
        {
            DataSet oDs = new DataSet();
            string sSQL = "select * from " + tabela + " WHERE " + coluna + " like '%" + pesquisa + "%'";


            //criar o data adapter e executar a consulta
            OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, ConectDb());
            //criar o DataSet

            //Preencher o dataset coom o data adapter
            oDA.Fill(oDs, tabela);

            return oDs;
        }
        public DataSet Filtrodb(string key1, string key2, string tabela, DateTimePicker dtpData)
        {

            DataSet oDs = new DataSet();
            string sSQL = "0";
            //Vamos considerar que a data seja o dia de hoje, mas pode ser qualquer data.
            DateTime data = dtpData.Value;

            //DateTime com o primeiro dia do mês
            DateTime primeiroDiaDoMes = new DateTime(data.Year, data.Month, 1);

            //DateTime com o último dia do mês
            DateTime ultimoDiaDoMes = new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month));


            //if (chave == "Mês / Ano") { sSQL = "select * from " + tabela + " WHERE Data like " + primeiroDiaDoMes.ToString("d") + " and " + ultimoDiaDoMes.ToString("d"); }
            if (key1 == "Mês / Ano" && key2 != "Todas as Lojas") { sSQL = "SELECT  * FROM " + tabela + " WHERE (Data LIKE '%" + data.Month.ToString() + "/" + data.Year.ToString() + "%') AND (Loja LIKE '%" + key2 + "%') ORDER BY Data"; }
            else if (key1 == "Dia" && key2 != "Todas as Lojas") { sSQL = "SELECT  * FROM " + tabela + " WHERE (Data LIKE '%" + dtpData.Value.ToString("d") + "%') AND (Loja LIKE '%" + key2 + "%') ORDER BY Or_os"; }
            else if (key1 == "Mês / Ano") { sSQL = "select * from " + tabela + " WHERE Data like '%" + data.Month.ToString() + "/" + data.Year.ToString() + "%' ORDER BY Data"; }
            else if (key1 == "Dia") { sSQL = "select * from " + tabela + " WHERE Data like '%" + dtpData.Value.ToString("d") + "%' ORDER BY Or_os"; }
            else if (key1 == "Ano" && key2 != "Todas as Lojas") { sSQL = "SELECT  * FROM " + tabela + " WHERE (Data LIKE '%" + data.Year.ToString() + "%') AND (Loja LIKE '%" + key2 + "%') ORDER BY Data"; }
            else { sSQL = "SELECT  * FROM " + tabela + " WHERE (Data LIKE '%" + data.Year.ToString() + "%') ORDER BY Data"; }
            //definir a string SQL



            //criar o data adapter e executar a consulta
            OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, ConectDb());
            //criar o DataSet

            //Preencher o dataset coom o data adapter
            oDA.Fill(oDs, tabela);

            return oDs;

        }
        public string filtratexto(string text1)
        {
            string texto = "0";
            if (text1 == "" || string.IsNullOrEmpty(text1)) { texto = "0"; }
            else
            {
                if (text1.Contains("R$ ")) { texto = text1.Replace("R$ ", ""); }
                else { texto = text1; }
            }
            return texto;
        }
        //import table to datatable 
        public DataTable TableDb(string command)
        {
            OleDbCommand cmd = new OleDbCommand(command, Db.ConectDb());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        public static void importtoDb()
        {
            //excel_Manipulations.check_table(sourcetable);
            //ConectTable().Close();



            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * from [" + NameTable(ConectDb(), 24) + "]", ConectDb());

            DataTable dtAccess = new DataTable();

            DataTable dtCSV = ds.Tables[0];
            dtCSV.Columns["N°# Nota"].ColumnName = "N° Nota";

            using (new OleDbCommandBuilder(adapter))
            {
                adapter.Fill(dtAccess);
                dtAccess.Merge(dtCSV);
                adapter.Update(dtAccess);

            }

            ConectDb().Close();
        }
        private static void InsertRow(string connectionString, string[] values, string[] colum)
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
