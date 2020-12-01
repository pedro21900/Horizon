using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Horizon_Contabilidade
{
    class Db
    {
        public DataSet pesquisaos(string tabela,string pesquisa) {
            DataSet oDs = new DataSet();
            string sSQL = "select * from " + tabela + " WHERE Or_os like '" + pesquisa+"%'";
          

            //criar o data adapter e executar a consulta
            OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, abreconecxao());
            //criar o DataSet

            //Preencher o dataset coom o data adapter
            oDA.Fill(oDs, tabela);

            return oDs;
        }
        public DataSet Filtrodb(string chave,string tabela,DateTimePicker dtpData)
        {
            DataSet oDs = new DataSet();
            string sSQL = "0";
            //Vamos considerar que a data seja o dia de hoje, mas pode ser qualquer data.
            DateTime data = dtpData.Value;

            //DateTime com o primeiro dia do mês
            DateTime primeiroDiaDoMes = new DateTime(data.Year, data.Month, 1);

            //DateTime com o último dia do mês
            DateTime ultimoDiaDoMes = new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month));

          
            
            if (chave== "Mês / Ano") { sSQL = "select * from " + tabela + " WHERE Data BETWEEN " + primeiroDiaDoMes.ToString("d") + " and " + ultimoDiaDoMes.ToString("d"); }
            else if (chave== "Dia") { sSQL = "select * from " + tabela + " WHERE Data like '" + dtpData.Value.ToString("d") + "%'"; }
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
        public OleDbConnection abreconecxao()
        {
            string local = Properties.Settings.Default.Pastainicial;
            //criar o objeto connection
            OleDbConnection oCn = new OleDbConnection(local);
            //abrir a conexão
            oCn.Open();

            return oCn;
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
        public void addlinhalayout1(string tabela, string data, string or, string fornecedor, string compral, string vendal, string compraa, string vendaa, string labs,
                                   string labm, string custocv, string vendavalor, string resultado, string loja)
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
                    filtratexto(vendal), filtratexto(compraa), filtratexto(vendaa),
                    filtratexto(labs), filtratexto(labm), filtratexto(custocv), filtratexto(vendavalor),
                    filtratexto(resultado), filtratexto(loja));
           
            salvatabela(oDA, oDs, tabela);

        }
        public void addlinhalayout2(string tabela, string or, string modeloarmação, string nomelente, string lucroarmacao, string lucrolente, string forenecedorl, string forenecedora,
                                    string lucrototal, string descontot, string taxacartao, string foramdepagamento, string descontol, string descontoa,
                                    string tipodecompra, string tipodecompra1, string tipodecompra2, string marcaa, string marcal, string tipo, string loja, string Obs)
        {
            string sSQL = "select * from " + tabela;
            //criar o data adapter e executar a consulta
            OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, abreconecxao());
            //criar o DataSet
            DataSet oDs = new DataSet();
            //Preencher o dataset coom o data adapter
            oDA.Fill(oDs, tabela);
            oDs.Tables[0].Rows.Add(filtratexto(or), filtratexto(modeloarmação), filtratexto(nomelente), filtratexto(lucroarmacao),
    filtratexto(lucrolente), filtratexto(forenecedorl), filtratexto(forenecedora),
    filtratexto(lucrototal), filtratexto(descontot), filtratexto(taxacartao), filtratexto(foramdepagamento),
    filtratexto(descontol), filtratexto(descontoa), filtratexto(tipodecompra), filtratexto(tipodecompra1), filtratexto(tipodecompra2), filtratexto(marcaa), filtratexto(marcal), filtratexto(tipo), filtratexto(loja), filtratexto(Obs));
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
        public void atualizar(string tabela, string tabela1, string data, string or, string fornecedor, string compral, string vendal, string compraa, string vendaa, string labs,
                               string labm, string custocv, string vendavalor, string resultado, string loja, string modeloarmação, string nomelente, string lucroarmacao, string lucrolente, string forenecedorl, string forenecedora,
                                string lucrototal, string descontot, string taxacartao, string foramdepagamento, string descontol, string descontoa,
                                string tipodecompra, string tipodecompra1, string tipodecompra2, string marcaa, string marcal, string tipo, string Obs, string pesquisa)
        {
            Deletalinha(tabela, pesquisa);
            Deletalinha(tabela1, pesquisa);
            addlinhalayout1(tabela, data, or, fornecedor, compral, vendal, compraa, vendaa, labs,
                                    labm, custocv, vendavalor, resultado, loja);
            addlinhalayout2(tabela1, or, modeloarmação, nomelente, lucroarmacao, lucrolente, forenecedorl, forenecedora,
                                     lucrototal, descontot, taxacartao, foramdepagamento, descontol, descontoa,
                                     tipodecompra, tipodecompra1, tipodecompra2, marcaa, marcal, tipo, loja, Obs);

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
