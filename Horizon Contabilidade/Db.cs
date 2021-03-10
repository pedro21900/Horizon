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
            try
            {
                //Usar o objeto Command Bulder para gerar o Comandop Insert
                OleDbCommandBuilder oCB = new OleDbCommandBuilder(oDA);

                //Atualizar o BD com valores do Dataset
                oDA.Update(oDs, tabela);
                //liberar o data adapter , o dataset , o comandbuilder e a conexao
                oDA.Dispose(); oDs.Dispose(); oCB.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro" + ex);


            }
        }
        public void addlinhalayout1(string tabela, string data, string or, string fornecedor, string compral, string vendal, string compraa, string vendaa,
                                   string col, string lab, string custodevenda, string vendavalor, string modeloarmação, string nomelente, string forenecedorl, string forenecedora,
                                    string descontoa, string descontol, string descontot, string marcaa, string marcal, string Obs, string Loja)
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
                     filtratexto(lab), filtratexto(custodevenda), filtratexto(vendavalor), filtratexto(modeloarmação), filtratexto(nomelente), filtratexto(forenecedorl)
                , filtratexto(forenecedora), filtratexto(descontol), filtratexto(descontoa), filtratexto(descontot), filtratexto(marcaa), filtratexto(marcal), filtratexto(Obs),filtratexto(Loja));

            salvatabela(oDA, oDs, tabela);

        }
        public void Deletalinha(string tabela, string pesquisa)
        {
            try
            {

                string sSQLs12 = "  DELETE* FROM " + tabela + " WHERE Or_os = '" + pesquisa+"'";
                OleDbCommand command = new OleDbCommand(sSQLs12, ConectDb());
                command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
        }
        public void atualizar(string tabela, string data, string or, string fornecedor, string compral, string vendal, string compraa, string vendaa, string lab,
                               string col, string custocv, string vendavalor, string loja, string modeloarmação, string nomelente, string forenecedorl, string forenecedora,
                                 string descontot, string descontol, string descontoa, string marcaa, string marcal, string Obs, string pesquisa)
        {
            Deletalinha(tabela, pesquisa);
            addlinhalayout1(tabela, data, or, fornecedor, compral, vendal, compraa, vendaa, col,
                                    lab, custocv, vendavalor, modeloarmação, nomelente, forenecedorl, forenecedora,
                                     descontol, descontoa, descontot, marcaa, marcal, Obs, loja);


        }
        public DataSet pesquisaos(string tabela, string coluna, string pesquisa, string pesquisa2, string pesquisa3)
        {
            string sSQL;
            //LentesValores.Clear();
            DataSet oDs = new DataSet();
            if (coluna != "Cod")
            {
                sSQL = "select * from " + tabela + " WHERE " + coluna + " like '%" + pesquisa + "%'";
            }
            else if (coluna == "Fornecedor")
            {
                sSQL = "select * from " + tabela + " WHERE " + coluna + " like '%" + pesquisa + "%' and Or_os like '%" + pesquisa2 + "%'";
            }
            else
            {
                sSQL = "select * from " + tabela + " WHERE " + coluna + " like '%" + pesquisa + "%' and Tratamento like '%" + pesquisa2 + "%' and Tipo like '%" + pesquisa3 + "%'";
            }
            //criar o data adapter e executar a consulta
            OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, ConectDb());
            //criar o DataSet

            //Preencher o dataset coom o data adapter
            oDA.Fill(oDs, tabela);
            // LentesValores = oDs;
            return oDs;

        }
        public DataSet Filtrodb(string key1, string key2, string tabela, DateTimePicker dtpData, int x, string tipo, string tipo2)
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
            if (x != 1)
            {
                if (key1 == "Mês / Ano" && key2 != "Todas as Lojas") { sSQL = "SELECT  * FROM " + tabela + " WHERE (Data LIKE '%" + data.Month.ToString() + "/" + data.Year.ToString() + "%') AND (Loja LIKE '%" + key2 + "%') ORDER BY Data"; }
                else if (key1 == "Dia" && key2 != "Todas as Lojas") { sSQL = "SELECT  * FROM " + tabela + " WHERE (Data LIKE '%" + dtpData.Value.ToString("d") + "%') AND (Loja LIKE '%" + key2 + "%') ORDER BY Or_os"; }
                else if (key1 == "Mês / Ano") { sSQL = "select * from " + tabela + " WHERE Data like '%" + data.Month.ToString() + "/" + data.Year.ToString() + "%' ORDER BY Data"; }
                else if (key1 == "Dia") { sSQL = "select * from " + tabela + " WHERE Data like '%" + dtpData.Value.ToString("d") + "%' ORDER BY Or_os"; }
                else if (key1 == "Ano" && key2 != "Todas as Lojas") { sSQL = "SELECT  * FROM " + tabela + " WHERE (Data LIKE '%" + data.Year.ToString() + "%') AND (Loja LIKE '%" + key2 + "%') ORDER BY Data"; }
                else { sSQL = "SELECT  * FROM " + tabela + " WHERE (Data LIKE '%" + data.Year.ToString() + "%') ORDER BY Data"; }
            }
            else if (tabela == "Qtd")
            {//LUCROOOO
                //sSQL = "SELECT [" + tipo + "], SUM(Venda_da_" + tipo2 + ") -SUM(Desconto_" + tipo2 + ") - SUM(Compra_da_" + tipo2 + ") AS Resultado FROM DB WHERE ([" + tipo + "] IS NOT NULL) AND (Venda_da_" + tipo2 + " <> 0) and (Data LIKE '%" + data.Month.ToString() + "/" + data.Year.ToString() + "%') and (Loja LIKE '%" + key2 + "%') AND ([" + tipo + "] <> 'DESCONHECIDO')  AND ([" + tipo + "] <> '0') AND ([" + tipo + "] <> 'COMERCIO') GROUP BY [" + tipo + "] ORDER BY " + tipo;
                if (key1 == "Mês / Ano" && key2 != "Todas as Lojas") { sSQL = " SELECT [" + tipo + "], COUNT([" + tipo + "]) AS Qtd, SUM(Venda_da_" + tipo2 + ") -SUM(Desconto_" + tipo2 + ") - SUM(Compra_da_" + tipo2 + ") AS Resultado FROM DB WHERE ([" + tipo + "] IS NOT NULL) AND (Data LIKE '%" + data.Month.ToString() + "/" + data.Year.ToString() + "%') AND(Fornecedor_Armação <> 'DESCONHECIDO') AND(Fornecedor_Lente <> '0') AND(Venda_da_" + tipo2 + " <> 0) AND ([" + tipo + "] <> 'COMERCIO') AND (Loja LIKE '%" + key2 + "%') GROUP BY [" + tipo + "] HAVING(COUNT([" + tipo + "]) > 1) ORDER BY COUNT([" + tipo + "]) DESC"; }
                // sSQL = "SELECT [" + tipo + "],COUNT([" + tipo + "]) AS Resultado FROM DB WHERE (Data LIKE '%" + data.Month.ToString() + "/" + data.Year.ToString() + "%') and ([" + tipo + "] <> '') and (Loja LIKE '%" + key2 + "%') AND ([" + tipo + "] <> '0') AND ([" + tipo + "] <> 'DESCONHECIDO')  GROUP BY[" + tipo + "] HAVING(COUNT([" + tipo + "]) > 1)ORDER BY COUNT([" + tipo + "]) DESC"; }

                else if (key1 == "Dia" && key2 != "Todas as Lojas") { sSQL = " SELECT [" + tipo + "], COUNT([" + tipo + "]) AS Qtd, SUM(Venda_da_" + tipo2 + ") -SUM(Desconto_" + tipo2 + ") - SUM(Compra_da_" + tipo2 + ") AS Resultado FROM DB WHERE ([" + tipo + "] IS NOT NULL) AND (Data LIKE '%" + dtpData.Value.ToString("d") + "%') AND(Fornecedor_Armação <> 'DESCONHECIDO') AND(Fornecedor_Lente <> '0') AND(Venda_da_" + tipo2 + " <> 0) AND ([" + tipo + "] <> 'COMERCIO') AND (Loja LIKE '%" + key2 + "%') GROUP BY [" + tipo + "] HAVING(COUNT([" + tipo + "]) > 1) ORDER BY COUNT([" + tipo + "]) DESC"; }

                //sSQL = " SELECT [" + tipo + "], COUNT([" + tipo + "]) AS Qtd, SUM(Venda_da_" + tipo2 + ") -SUM(Desconto_" + tipo2 + ") - SUM(Compra_da_" + tipo2 + ") AS Resultado FROM DB WHERE ([" + tipo + "] IS NOT NULL) AND Data LIKE '%" + data.Month.ToString() + "/" + data.Year.ToString() + "%') AND(Fornecedor_Lente <> 'DESCONHECIDO') AND(Fornecedor_Lente <> '0') AND(Venda_da_" + tipo2 + " <> 0) AND ([" + tipo + "] <> 'COMERCIO') GROUP BY [" + tipo + "] HAVING(COUNT([" + tipo + "]) > 1) ORDER BY COUNT([" + tipo + "]) DESC";}

                else if (key1 == "Mês / Ano") { sSQL = " SELECT [" + tipo + "], COUNT([" + tipo + "]) AS Qtd, SUM(Venda_da_" + tipo2 + ") -SUM(Desconto_" + tipo2 + ") - SUM(Compra_da_" + tipo2 + ") AS Resultado FROM DB WHERE ([" + tipo + "] IS NOT NULL) AND (Data LIKE '%" + data.Month.ToString() + "/" + data.Year.ToString() + "%') AND(Fornecedor_Armação <> 'DESCONHECIDO') AND(Fornecedor_Lente <> '0') AND(Venda_da_" + tipo2 + " <> 0) AND ([" + tipo + "] <> 'COMERCIO') GROUP BY [" + tipo + "] HAVING(COUNT([" + tipo + "]) > 1) ORDER BY COUNT([" + tipo + "]) DESC"; }
                else if (key1 == "Dia") { sSQL = " SELECT [" + tipo + "], COUNT([" + tipo + "]) AS Qtd, SUM(Venda_da_" + tipo2 + ") -SUM(Desconto_" + tipo2 + ") - SUM(Compra_da_" + tipo2 + ") AS Resultado FROM DB WHERE ([" + tipo + "] IS NOT NULL) AND (Data LIKE '%" + dtpData.Value.ToString("d") + "%') AND(Fornecedor_Armação <> 'DESCONHECIDO') AND(Fornecedor_Lente <> '0') AND(Venda_da_" + tipo2 + " <> 0) AND ([" + tipo + "] <> 'COMERCIO') AND (Loja LIKE '%" + key2 + "%') GROUP BY [" + tipo + "] HAVING(COUNT([" + tipo + "]) > 1) ORDER BY COUNT([" + tipo + "]) DESC"; }

                else if (key1 == "Ano" && key2 != "Todas as Lojas") { sSQL = " SELECT [" + tipo + "], COUNT([" + tipo + "]) AS Qtd, SUM(Venda_da_" + tipo2 + ") -SUM(Desconto_" + tipo2 + ") - SUM(Compra_da_" + tipo2 + ") AS Resultado FROM DB WHERE ([" + tipo + "] IS NOT NULL) AND (Data LIKE '%" + data.Year.ToString() + "%') AND(Fornecedor_Armação <> 'DESCONHECIDO') AND(Fornecedor_Lente <> '0') AND(Venda_da_" + tipo2 + " <> 0) AND ([" + tipo + "] <> 'COMERCIO') AND (Loja LIKE '%" + key2 + "%') GROUP BY [" + tipo + "] HAVING(COUNT([" + tipo + "]) > 1) ORDER BY COUNT([" + tipo + "]) DESC"; }

                //  sSQL = "SELECT [" + tipo + "],COUNT([" + tipo + "]) AS Resultado FROM DB  WHERE (Data LIKE '%" + data.Year.ToString() + "%') and [" + tipo + "] <> '' and (Loja LIKE '%" + key2 + "%') AND ([" + tipo + "] <> '0') AND ([" + tipo + "] <> 'DESCONHECIDO') GROUP BY[" + tipo + "] HAVING(COUNT([" + tipo + "]) > 1)ORDER BY COUNT([" + tipo + "]) DESC"; }

                else { sSQL = " SELECT [" + tipo + "], COUNT([" + tipo + "]) AS Qtd, SUM(Venda_da_" + tipo2 + ") -SUM(Desconto_" + tipo2 + ") - SUM(Compra_da_" + tipo2 + ") AS Resultado FROM DB WHERE ([" + tipo + "] IS NOT NULL) AND (Data LIKE '%" + data.Year.ToString() + "%') AND(Fornecedor_Armação <> 'DESCONHECIDO') AND(Fornecedor_Lente <> '0') AND(Venda_da_" + tipo2 + " <> 0) AND ([" + tipo + "] <> 'COMERCIO') GROUP BY [" + tipo + "] HAVING(COUNT([" + tipo + "]) > 1) ORDER BY COUNT([" + tipo + "]) DESC"; }
            }

            //definir a string SQL



            //criar o data adapter e executar a consulta
            OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, ConectDb());
            //criar o DataSet

            //Preencher o dataset coom o data adapter
            oDA.Fill(oDs, tabela);
            x = 0;
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
            OleDbCommand cmd = new OleDbCommand(command, ConectDb());
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
        public void importalente(TextBox stringcod, TextBox stringname, ComboBox stringfornecedor, ComboBox stringmarca,
            ComboBox stringtratamento, ComboBox stringtipo, TextBox stringvalorcompra, TextBox stringvalorvenda)
        {
            try
            {

                string sSQL = "select * from LentesValores WHERE  Cod = " + stringcod.Text;
                //criar o objeto connection


                //criar o data adapter e executar a consulta
                OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, ConectDb());
                //criar o DataSet
                DataSet oDs = new DataSet();
                //Preencher o dataset coom o data adapter
                oDA.Fill(oDs, "LentesValores");


                int count1 = oDs.Tables[0].Columns.Count;

                string indexx = "0";
                string index = "0";

                foreach (DataColumn indexx1 in oDs.Tables[0].Columns)
                {
                    indexx = indexx1.ToString();
                    index = oDs.Tables[0].Rows[0][indexx].ToString();

                    if (indexx == "Nome_Lente") { stringname.Text = index; }
                    else if (indexx == "Fornecedor_Lente") { stringfornecedor.Text = index; }
                    else if (indexx == "Marca") { stringmarca.Text = index; }
                    else if (indexx == "Tratamento") { stringtratamento.Text = index; }
                    else if (indexx == "Tipo") { stringtipo.Text = index; }
                    else if (indexx == "Valor_Compra") { stringvalorcompra.Text = index; }
                    else if (indexx == "Valor_Venda") { stringvalorvenda.Text = index; }

                }

                ConectDb().Close();
                oDA.Dispose(); oDs.Dispose();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
        }
        public void exportalente(TextBox stringcod, TextBox stringname, ComboBox stringfornecedor, ComboBox stringmarca,
            ComboBox stringtratamento, ComboBox stringtipo, TextBox stringvalorcompra, TextBox stringvalorvenda)
        {
            try
            {

                string sSQL = "select * from LentesValores";
                //criar o data adapter e executar a consulta
                OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, ConectDb());
                //criar o DataSet
                DataSet oDs = new DataSet();
                //Preencher o dataset coom o data adapter
                oDA.Fill(oDs, "LentesValores");
                DataRow oDR = oDs.Tables["LentesValores"].NewRow();

                oDs.Tables[0].Rows.Add(stringcod.Text, stringname.Text, stringfornecedor.Text, stringmarca.Text, stringtratamento.Text, stringtipo.Text, stringvalorcompra.Text, stringvalorvenda.Text);
                salvatabela(oDA, oDs, "LentesValores");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
        }
        public void registrocadlentes(string pesquisa, double valor)

        {
            string data, or, fornecedor, compral, vendal, compraa, vendaa, col, lab, custocv, vendavalor, modeloarmação, nomelente, forenecedorl, forenecedora,
                                     descontol, descontoa, descontot, marcaa, marcal, loja, Obs;
            DataTable linha = pesquisaos("DB", "Or_os", pesquisa, "", "").Tables[0];
            data = linha.Rows[0]["Data"].ToString();
            or = linha.Rows[0]["Or_os"].ToString();
            fornecedor = linha.Rows[0]["Fornecedor"].ToString();
            compral = valor.ToString("C");
            vendal = linha.Rows[0]["Venda_da_Lente"].ToString();
            compraa = linha.Rows[0]["Compra_da_Armação"].ToString();
            vendaa = linha.Rows[0]["Venda_da_Armação"].ToString();
            col = linha.Rows[0]["Coloração"].ToString();
            lab = linha.Rows[0]["Lab"].ToString();
            custocv = (valor + Convert.ToDouble(compraa) + Convert.ToDouble(col) + Convert.ToDouble(lab)).ToString("C");
            vendavalor = linha.Rows[0]["Valor_da_venda_cliente"].ToString();
            loja = linha.Rows[0]["Loja"].ToString();
            modeloarmação = linha.Rows[0]["Modelo_Armação"].ToString();
            nomelente = linha.Rows[0]["Nome_Lente"].ToString();
            forenecedorl = linha.Rows[0]["Fornecedor_Lente"].ToString();
            forenecedora = linha.Rows[0]["Fornecedor_Armação"].ToString();
            descontol = linha.Rows[0]["Desconto_Lente"].ToString();
            descontoa = linha.Rows[0]["Desconto_Armação"].ToString();
            descontot = linha.Rows[0]["Desconto_total"].ToString();
            marcaa = linha.Rows[0]["Marca_armação"].ToString();
            marcal = linha.Rows[0]["Marca_lente"].ToString();
            Obs = linha.Rows[0]["Obs"].ToString();


            Deletalinha("DB", pesquisa);
            addlinhalayout1("DB", data, or, fornecedor, compral, vendal, compraa, vendaa, col,
                                    lab, custocv, vendavalor, modeloarmação, nomelente, forenecedorl, forenecedora,
                                     descontol, descontoa, descontot, marcaa, marcal, loja, Obs);
        }
    }


}
