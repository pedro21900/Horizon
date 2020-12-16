using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace Horizon_Contabilidade
{
    class Db
    {
        //Classes
        static excel_manipulations excel_Manipulations = new excel_manipulations();
        static invalid_character ic = new invalid_character();
        //Variaves
        static string sourcedb = Properties.Settings.Default.Pastainicial;
        static OleDbConnection conexaoDb;
        static OleDbConnection conexaotable;
        static private string sourcetable;
        static string nameTable;
        static string dateColumn;
        //Datas
        static DataSet ds = new DataSet();
        //Getters e Setters
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
        static public OleDbConnection ConectDb()
        {
            conexaoDb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sourcedb + "; Persist Security Info=False;");
            conexaoDb.Open();
            return conexaoDb;
        }
        public OleDbConnection abreconecxao()
        {
            string local = Form1.sDBstr;

            //criar o objeto connection
            OleDbConnection oCn = new OleDbConnection(local);
            //abrir a conexão
            oCn.Open();
            //Salva caminho db
            Properties.Settings.Default.Pastainicial = Form1.sDBstr;
            Properties.Settings.Default.Save();

            return oCn;
        }
        //Metodos
        static private string NameTable(OleDbConnection Conect, int indexName)
        {
            DataTable dtSchema = Conect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string nomePlanilha = dtSchema.Rows[indexName]["TABLE_NAME"].ToString();
            return nomePlanilha;
        }
        private static string columnExport()
        {

            string column = "";
            if (excel_Manipulations.Qtdcolumn == 4)
            {
                column = "Emitente,Destinatário,[N°# Nota],Emissão,Valor";
                dateColumn = "Emissão";
                nameTable = "XmlTable";
            }
            else if (excel_Manipulations.Qtdcolumn == 8)
            {
                column = "[Forma Pgto#],Valor,Desconto,Faturado,Cliente,[NFCe/SAT/Cupom],[Código Fatura],DataFaturamento,Tipo";
                dateColumn = "DataFaturamento";
                nameTable = "RelFatTable";
            }
            else { }
            return column;
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
        public void addlinhalayout1(string tabela, string data, string or, string fornecedor, string compral, string vendal, string compraa, string vendaa, string lab,
                                   string col,string custodevenda, string vendavalor,string loja)
        {
            string sSQL = "select * from " + tabela;
            //criar o data adapter e executar a consulta
            OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, abreconecxao());
            //criar o DataSet
            DataSet oDs = new DataSet();
            //Preencher o dataset coom o data adapter
            oDA.Fill(oDs, tabela);
            DataRow oDR = oDs.Tables[tabela].NewRow();

            oDs.Tables[0].Rows.Add(data, filtratexto(or), filtratexto(fornecedor), filtratexto(compral),
                    filtratexto(vendal), filtratexto(compraa), filtratexto(vendaa), filtratexto(lab),
                    filtratexto(col), filtratexto(custodevenda), filtratexto(vendavalor), filtratexto(loja));

            salvatabela(oDA, oDs, tabela);

        }
        public void addlinhalayout2(string tabela, string or,string data, string modeloarmação, string nomelente, string lucroarmacao, string lucrolente, string forenecedorl, string forenecedora,
                                    string lucrototal, string descontot, string taxacartao, string foramdepagamento, string descontol, string descontoa,
                                    string tipodecompra, string tipodecompra1, string tipodecompra2, string marcaa, string marcal, string loja, string Obs)
        {
            string sSQL = "select * from " + tabela;
            //criar o data adapter e executar a consulta
            OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, abreconecxao());
            //criar o DataSet
            DataSet oDs = new DataSet();
            //Preencher o dataset coom o data adapter
            oDA.Fill(oDs, tabela);
            oDs.Tables[0].Rows.Add(filtratexto(or), filtratexto(data), filtratexto(modeloarmação), filtratexto(nomelente), filtratexto(lucroarmacao),
    filtratexto(lucrolente), filtratexto(forenecedorl), filtratexto(forenecedora),
    filtratexto(lucrototal), filtratexto(descontot), filtratexto(taxacartao), filtratexto(foramdepagamento),
    filtratexto(descontol), filtratexto(descontoa), filtratexto(tipodecompra), filtratexto(tipodecompra1), filtratexto(tipodecompra2), filtratexto(marcaa), filtratexto(marcal),  filtratexto(loja), filtratexto(Obs));
            salvatabela(oDA, oDs, tabela);
        }
        public void Deletalinha(string tabela, string pesquisa)
        {
            try
            {

                string sSQLs12 = "  DELETE* FROM " + tabela + " WHERE Or_os = " + pesquisa;
                OleDbCommand command = new OleDbCommand(sSQLs12, abreconecxao());
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
            addlinhalayout1(tabela, data, or, fornecedor, compral, vendal, compraa, vendaa, lab,
                                    col, custocv, vendavalor, loja);
            addlinhalayout2(tabela1, or,data, modeloarmação, nomelente, lucroarmacao, lucrolente, forenecedorl, forenecedora,
                                     lucrototal, descontot, taxacartao, foramdepagamento, descontol, descontoa,
                                     tipodecompra, tipodecompra1, tipodecompra2, marcaa, marcal, loja, Obs);

        }
        public DataSet pesquisaos(string tabela, string pesquisa)
        {
            DataSet oDs = new DataSet();
            string sSQL = "select * from " + tabela + " WHERE Or_os like '" + pesquisa + "%'";


            //criar o data adapter e executar a consulta
            OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, abreconecxao());
            //criar o DataSet

            //Preencher o dataset coom o data adapter
            oDA.Fill(oDs, tabela);

            return oDs;
        }
        public DataSet Filtrodb(string chave, string tabela, DateTimePicker dtpData)
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
            if (tabela == "Carne" || chave == "Mês / Ano") { sSQL = "select * from " + tabela + " WHERE Data like '%" + data.Month.ToString() + "/" + data.Year.ToString() + "%'"; }
            else if (chave == "Dia") { sSQL = "select * from " + tabela + " WHERE Data like '%" + dtpData.Value.ToString("d") + "%'"; }
            else {/* sSQL = "select * from " + tabela;*/ }
            //definir a string SQL



            //criar o data adapter e executar a consulta
            OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, abreconecxao());
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
        static public DataSet Tables(string tabela)
        {
            DataSet Tables = new DataSet();
            //definir a string SQL
            string sSQL = "select * from " + tabela;

            //criar o objeto connection
            OleDbConnection oCn = new OleDbConnection(Properties.Settings.Default.Pastainicial);
            //abrir a conexão
            oCn.Open();
            //criar o data adapter e executar a consulta
            OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, oCn);
            //criar o DataSet

            //Preencher o dataset coom o data adapter
            oDA.Fill(Tables, tabela);

            return Tables;


        }
        //import table to datatable 
        static public DataTable TableDb(string command)
        {
            OleDbCommand cmd = new OleDbCommand(command, Db.ConectDb());
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;

        }
        //Importa para o Banco de dados as planilhas
        static public DataSet importTable()
        {
            OleDbDataAdapter ada = new OleDbDataAdapter("select " + columnExport() + " from [" +
                NameTable(ConectTable(), 0) + "]", ConectTable());

            ada.Fill(ds);

            ConectTable().Close();

            return ds;

        }
        static public void importtoDb()
        {
            excel_Manipulations.check_table(sourcetable);
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
       

 

        /*   public void importabanco(string tabela, string pesquisa, int ntabela)
           {
               try
               {

                   //definir a string SQL
                   string sSQL = "select * from " + tabela + " WHERE  Data = " + pesquisa + "";


                   //criar o data adapter e executar a consulta
                   OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, abreconecxao());
                   //criar o DataSet
                   DataSet oDs = new DataSet();
                   //Preencher o dataset coom o data adapter
                   oDA.Fill(oDs, tabela);

                   int count1 = oDs.Tables[0].Columns.Count;

                   string indexx = "0";
                   string index = "0";
                   string data, string or, string fornecedor,string compral, string vendal, string compraa, string vendaa, string labs,
                                      string labm, string custocv, string vendavalor, string resultado, string loja)
                   string tabela, string or, string modeloarmação, string nomelente,string lucroarmacao, string lucrolente, string forenecedorl, string forenecedora,
                                       string lucrototal, string descontot, string taxacartao, string foramdepagamento, string descontol, string descontoa,
                                       string tipodecompra, string tipodecompra1, string tipodecompra2, string marcaa, string marcal, string tipo, string loja,string Obs

                   foreach (DataColumn indexx1 in oDs.Tables[0].Columns)
                   {
                       indexx = indexx1.ToString();
                       index = oDs.Tables[0].Rows[0][indexx].ToString();
                       index=filtratexto(index);
                       if (indexx == "Data") { dateTimePicker1.Text =  index; }
                       else if (indexx == "Or_os") { txOs_Or.Text = index; }
                       else if (indexx == "Compra_da_lente") { txCompra_lente.Text = index; }
                       else if (indexx == "Venda_da_lente") { txVenda_lente.Text = index; }                    
                       else if (indexx == "Lab_surf") { txLab_Surf.Text = index; }
                       else if (indexx == "Lab_mont") { txLab_montagem.Text = index; }
                       else if (indexx == "Venda_da_Armação") { txVenda_armacao.Text = index; }
                       else if (indexx == "Compra_da_Armação") { txCompra_armacao.Text = index; }
                       else if (indexx == "Loja") { cb = index; }

                       else if (indexx == "Modelo_Armação") { txModelo_armacao.Text = index; }
                       else if (indexx == "Marca_armação") { txMarca_armacao.Text = index; }
                       else if (indexx == "Fornecedor_Armação") { txFornecedor_armacao.Text = index; }
                       else if (indexx == "Nome_Lente") { txNome_lente.Text = index; }
                       else if (indexx == "Fornecedor_Lente") { txFornecedor_lente.Text = index; }
                       else if (indexx == "Marca_lente") { txMarca_lente.Text = index; }
                       else if (indexx == "Desconto_lente") { txDesconto_Lente.Text = index; }
                       else if (indexx == "Lucro_Lente") { txLucro_lente.Text = index; }
                       else if (indexx == "Lucro_Armação") { txLucro_armacao.Text = index; }
                       else if (indexx == "Desconto_armação") { txDesconto_Armacao.Text = index; }
                       else if (indexx == "Loja") { cb = index; }
                   }



                   //Incluir um datarow ao dataset
                   //oDs.Tables[tabela].Rows.Add(oDR);
                   //Usar o objeto Command Bulder para gerar o Comandop Insert
                   // OleDbCommandBuilder oCB = new OleDbCommandBuilder(oDA);
                   //Atualizar o BD com valores do Dataset
                   ///   oDA.Update(oDs, tabela);
                   //liberar o data adapter , o dataset , o comandbuilder e a conexao
                   oDA.Dispose(); oDs.Dispose(); //oCB.Dispose();
                   abreconecxao().Dispose();

               }
               catch (Exception ex)
               {
                   MessageBox.Show("Erro :" + ex.Message);


               }
           }
       }
   }
   /* 

    * public DataRow exportadespadm(DataRow oDR)
       {

           oDR["ID"] = Convert.ToDateTime(dptData.Value).ToShortDateString();
           oDR["Eng_Traba"] = txEngtrab.Text;
           oDR["Salario"] = txSal.Text;
           oDR["Desp_transp"] = txTransporte.Text;
           oDR["Aluguel"] = txAluguel.Text;
           oDR["Comunicação"] = txComunica.Text;
           oDR["Energia"] = txEnergia.Text;
           oDR["Segurança_loja"] = txSeg.Text;
           oDR["Malote"] = txBoy.Text;
           oDR["Divulgação"] = txMarket.Text;
           oDR["Alvara"] = txAlvara.Text;
           return oDR;
       }
       public DataRow exportatributos(DataRow oDR)
       {
           oDR["ID"] = Convert.ToDateTime(dptData.Value).ToShortDateString();
           oDR["Pis"] = txPis.Text;
           oDR["Confis"] = txConfins.Text;
           oDR["inss"] = txInss.Text;
           oDR["irpj"] = txIrpj.Text;
           oDR["csll"] = txCsll.Text;
           oDR["iss"] = txInss.Text;


           return oDR;
       }
       public DataRow exportalgais(DataRow oDR)
       {
           oDR["ID"] = Convert.ToDateTime(dptData.Value).ToShortDateString();
           oDR["13º"] = txEngtrab.Text;
           oDR["Ferias"] = txSal.Text;
           oDR["13º_Conta"] = txTransporte.Text;
           oDR["13º_Alu"] = txAluguel.Text;
           oDR["13º_Hono"] = txComunica.Text;
           oDR["Energia"] = txEnergia.Text;
           oDR["Outros"] = txSeg.Text;

           return oDR;
       }
       public DataRow exportadespfineeve(DataRow oDR)
       {
           oDR["ID"] = Convert.ToDateTime(dptData.Value).ToShortDateString();
           oDR["saida_ex"] = txEngtrab.Text;
           oDR["Luros_lis"] = txSal.Text;
           oDR["tarif_bank"] = txTransporte.Text;
           oDR["antecipa"] = txAluguel.Text;
           oDR["aluguel_maq"] = txComunica.Text;
           oDR["Juros_empres"] = txEnergia.Text;
           oDR["outros"] = txSeg.Text;

           return oDR;
       }



       public void importabanco(string tabela, string pesquisa, int ntabela)
       {
           try
           {
               string sDBstr = localdb(); ;

               //definir a string de conexão

               //definir a string SQL
               string sSQL = "select * from " + tabela + " WHERE  Or_os = " + pesquisa + "";

               //criar o objeto connection
               OleDbConnection oCn = new OleDbConnection(sDBstr);
               //abrir a conexão
               oCn.Open();
               //criar o data adapter e executar a consulta
               OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, oCn);
               //criar o DataSet
               DataSet oDs = new DataSet();
               //Preencher o dataset coom o data adapter
               oDA.Fill(oDs, tabela);

               //criar um objeto Data Row
               DataRow oDR = oDs.Tables[tabela].NewRow();

               int count1 = oDs.Tables[0].Columns.Count;

               string indexx = "0";
               string index = "0";

               foreach (DataColumn indexx1 in oDs.Tables[0].Columns)
               {
                   indexx = indexx1.ToString();
                   index = oDs.Tables[0].Rows[0][indexx].ToString();
                   if (comboBox1.Text == "Somente Lente" && ntabela == 1)
                   {

                       if (indexx == "Or_os") { txOs_Or.Text = index; }
                       else if (indexx == "Data") { dateTimePicker1.Text = index; }
                       else if (indexx == "Venda_da_lente") { txVenda_lente.Text = index; }
                       else if (indexx == "Compra_da_lente") { txCompra_lente.Text = index; }
                       else if (String.IsNullOrEmpty(index) && indexx == "Lab_surf") { txLab_Surf.Text = "0"; }
                       else if (indexx == "Lab_surf") { txLab_Surf.Text = index; }
                       else if (String.IsNullOrEmpty(index) && indexx == "Lab_mont") { txLab_montagem.Text = "0"; }
                       else if (indexx == "Lab_mont") { txLab_montagem.Text = index; }
                       else if (indexx == "Loja") { cb = index; }

                   }
                   else if (comboBox1.Text == "Somente Lente" && ntabela == 2)
                   {

                       if (indexx == "Nome_Lente") { txNome_lente.Text = index; }
                       else if (indexx == "Fornecedor_Lente") { txFornecedor_lente.Text = index; }
                       else if (indexx == "Marca_lente") { txMarca_lente.Text = index; }
                       else if (indexx == "Desconto_lente") { txDesconto_Lente.Text = index; }
                       else if (indexx == "Lucro_Lente") { txLucro_lente.Text = index; }

                   }
                   else if (comboBox1.Text == "Somente Armação" && ntabela == 1)
                   {

                       if (indexx == "Or_os") { txOs_Or.Text = index; }
                       else if (indexx == "Data") { dateTimePicker1.Text = index; }
                       if (indexx == "Venda_da_Armação") { txVenda_armacao.Text = index; }
                       if (indexx == "Compra_da_Armação") { txCompra_armacao.Text = index; }
                       else if (String.IsNullOrEmpty(index) && indexx == "Lab_surf") { txLab_Surf.Text = "0"; }
                       else if (indexx == "Lab_surf") { txLab_Surf.Text = index; }
                       else if (String.IsNullOrEmpty(index) && indexx == "Lab_mont") { txLab_montagem.Text = "0"; }
                       else if (indexx == "Lab_mont") { txLab_montagem.Text = index; }
                       else if (indexx == "Loja") { cb = index; }
                   }
                   else if (comboBox1.Text == "Somente Armação" && ntabela == 2)

                   {


                       if (indexx == "Modelo_Armação") { txModelo_armacao.Text = index; }
                       else if (indexx == "Marca_armação") { txMarca_armacao.Text = index; }
                       else if (indexx == "Fornecedor_Armação") { txFornecedor_armacao.Text = index; }
                       else if (indexx == "Desconto_armação") { txDesconto_Armacao.Text = index; }
                       else if (indexx == "Lucro_Armação") { txLucro_armacao.Text = index; }


                   }
                   else if (comboBox1.Text == "Lente + Armação" && ntabela == 1)
                   {
                       if (indexx == "Or_os") { txOs_Or.Text = index; }
                       else if (indexx == "Data") { dateTimePicker1.Text = index; }
                       else if (indexx == "Venda_da_Armação") { txVenda_armacao.Text = index; }
                       else if (indexx == "Compra_da_Armação") { txCompra_armacao.Text = index; }
                       else if (indexx == "Venda_da_lente") { txVenda_lente.Text = index; }
                       else if (indexx == "Compra_da_lente") { txCompra_lente.Text = index; }

                       else if (indexx == "Lab_surf") { txLab_Surf.Text = index; }
                       else if (indexx == "Lab_mont") { txLab_montagem.Text = index; }

                   }
                   else if (comboBox1.Text == "Lente + Armação" && ntabela == 2)
                   {
                       if (indexx == "Nome_Lente") { txNome_lente.Text = index; }
                       else if (indexx == "Marca_lente") { txMarca_lente.Text = index; }
                       else if (indexx == "Fornecedor_Lente") { txFornecedor_lente.Text = index; }
                       else if (indexx == "Modelo_Armação") { txModelo_armacao.Text = index; }
                       else if (indexx == "Fornecedor_Armação") { txFornecedor_armacao.Text = index; }
                       else if (indexx == "Desconto_lente") { txDesconto_Lente.Text = index; }




                       else if (indexx == "Desconto_armação") { txDesconto_Armacao.Text = index; }
                       else if (indexx == "Marca_armação") { txMarca_armacao.Text = index; }
                   }
               }


               //Incluir um datarow ao dataset
               //oDs.Tables[tabela].Rows.Add(oDR);
               //Usar o objeto Command Bulder para gerar o Comandop Insert
               // OleDbCommandBuilder oCB = new OleDbCommandBuilder(oDA);
               //Atualizar o BD com valores do Dataset
               ///   oDA.Update(oDs, tabela);
               //liberar o data adapter , o dataset , o comandbuilder e a conexao
               oDA.Dispose(); oDs.Dispose(); //oCB.Dispose();
               oCn.Dispose();

           }
           catch (Exception ex)
           {
               MessageBox.Show("Erro :" + ex.Message);


           }
       }
   }
   }

   */

    }
}
