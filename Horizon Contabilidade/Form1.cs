using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

using System.Windows.Forms;
namespace Horizon_Contabilidade
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        static Cadastro cadastro = new Cadastro();
        static string os = "0";
        static bool c = false;
        DataTable d1 = new DataTable();
        Db db = new Db();
        static string sDBstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
        
       

        public string retornastringlocal()
        {

            string aux = sDBstr;
            return aux;
        }
        public DataRow exportadespadm(DataRow oDR)
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

        public void exportabanco(string tabela)
        {
            try
            {


                //definir a string de conexão

                //definir a string SQL
                string sSQL = "SELECT * from " + tabela;

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

                //Preencher o datarow com v Desp_Adm Tributos Desp_legais Desp_fin_eve

                if (tabela == "Desp_Adm")
                {
                    oDR = exportadespadm(oDR);
                }
                else if (tabela == "Tributos")
                {
                    oDR = exportatributos(oDR);
                }
                else if (tabela == "Desp_legais")
                {
                    oDR = exportalgais(oDR);
                }
                else if (tabela == "Desp_fin_eve")
                {
                    oDR = exportadespfineeve(oDR);
                }




                // oDR["Resultado_da_venda"] = txResultado_da_venda.Text;



                //Incluir um datarow ao dataset
                oDs.Tables[tabela].Rows.Add(oDR);
                //Usar o objeto Command Bulder para gerar o Comandop Insert
                OleDbCommandBuilder oCB = new OleDbCommandBuilder(oDA);
                //Atualizar o BD com valores do Dataset
                oDA.Update(oDs, tabela);
                //liberar o data adapter , o dataset , o comandbuilder e a conexao
                oDA.Dispose(); oDs.Dispose(); oCB.Dispose(); oCn.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }


        }
       // public void pesquisa(TextBox txPesquisa_princial)
       // {

            //definir a string de conexão


            //definir a string SQL
          //  string sSQL = "select * from DB where Or_os like " + txPesquisa_princial.Text + "%";

            //criar o objeto connection
          //  OleDbConnection oCn = new OleDbConnection(sDBstr);
            //abrir a conexão

        //    oCn.Open();
            //criar o data adapter e executar a consulta
        //    OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, oCn);

            //criar o DataSet
       //     DataSet oDs = new DataSet();

            //Preencher o dataset coom o data adapter
         //  oDA.Fill(oDs, "DB");

        //    dgvDados.DataSource = oDs;


       // }
        public string retornaosclicada(DataGridView dgvDados)
        {
            // vamos obter a linha da célula selecionada
            DataGridViewRow linhaAtual = dgvDados.CurrentRow;

            // vamos exibir o índice da linha atual
            int indice = linhaAtual.Index;

            //MessageBox.Show("O índice da linha atual é: " + indice);

            // configurando valor da primeira coluna, índice 0
            os = dgvDados.Rows[indice].Cells["Or_os"].Value.ToString();
       
            return os;
        }
        public bool retorna()
        {

            return c;
        }


        public string retornaos()
        {

            return os;
        }
        public Form1(DataTable data)
        {
            InitializeComponent();

            atualiza(data);
            retornastringlocal();
            retornaosclicada(dgvDados);
            retornaos();
            retorna();




        }
        public void ligadesliga(string dijunt)

        {
            bool dij = true;
            if (dijunt == "Liga")
            {
                dij = true;
            }
            else if (dijunt == "Desliga")
            {
                dij = false;
            }
            txEngtrab.Enabled = dij;
            txSal.Enabled = dij;
            txTransporte.Enabled = dij;
            txAluguel.Enabled = dij;
            txComunica.Enabled = dij;
            txEnergia.Enabled = dij;
            txSeg.Enabled = dij;
            txBoy.Enabled = dij;
            txMarket.Enabled = dij;
            txAlvara.Enabled = dij;
            txPis.Enabled = dij;
            txConfins.Enabled = dij;
            txInss.Enabled = dij;
            txIrpj.Enabled = dij;
            txCsll.Enabled = dij;
            txIss.Enabled = dij;
        }
        public AutoCompleteStringCollection Caixadesusgestaoos(string coluna, string DB)
        {
            AutoCompleteStringCollection stringCollection = new AutoCompleteStringCollection();
            try
            {
                //definir a string de conexão
                string sDBstr = retornastringlocal();

                //definir a string SQL
                string sSQL = "select " + coluna + " from " + DB + "";

                //criar o objeto connection
                OleDbConnection oCn = new OleDbConnection(sDBstr);
                //abrir a conexão

                oCn.Open();
                //criar o data adapter e executar a consulta
                OleDbDataAdapter oDA = new OleDbDataAdapter(sSQL, oCn);

                //criar o DataSet
                DataSet oDs = new DataSet();

                //Preencher o dataset coom o data adapter
                oDA.Fill(oDs, DB);

                //oDs.Tables.Add(d1);               

                foreach (DataRow row in oDs.Tables[0].Rows)
                {


                    stringCollection.Add(string.Join("", row.ItemArray));
                }


                oDA.Dispose(); oDs.Dispose(); oCn.Dispose();
                oCn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
            return stringCollection;
        }
        public double bruto(int x)
        {
            double valor = 0;
            try
            {

                string tabela = "DB";
                //definir a string de conexão

                //definir a string SQL
                string sSQL = "select * from " + tabela + "";

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

                double indexx = 0;
                double index = 0;
                int qtdlinha = oDs.Tables[0].Rows.Count;


                for (int i = 0; i <= qtdlinha - 1; i++)
                {
                    index = Convert.ToDouble(oDs.Tables[0].Rows[i]["Venda_da_lente"].ToString()) + Convert.ToDouble(oDs.Tables[0].Rows[i]["Venda_da_Armação"].ToString());
                    indexx += index;

                }
                if (x == 1) { 
                    valor = indexx - Desc_total(); }
                else
                {
                    valor = indexx;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
            return valor;
        }
        public double carne()
        {
            double valor = 0;
            try
            {

                string tabela = "Carne";
                //definir a string de conexão

                //definir a string SQL
                string sSQL = "select * from " + tabela + "";

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

                double indexx = 0;
                double index = 0;
                int qtdlinha = oDs.Tables[0].Rows.Count;


                for (int i = 0; i <= qtdlinha - 1; i++)
                {
                    index = Convert.ToDouble(oDs.Tables[0].Rows[i]["Valor"].ToString());
                    indexx += index;

                }
               
                    valor = indexx;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
            return valor;
        }
        public DataSet db1(string tabela)
        {
           
            //definir a string de conexão

            //definir a string SQL
            string sSQL = "select * from " + tabela + "";

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
            return oDs;
        }
        public void maisvendido()
        {
            try
            {

                string tabela = "Registrosip";
                //definir a string de conexão

                //definir a string SQL
                string sSQL = "select * from " + tabela + "";

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

               // double indexx = 0;

                int qtdlinha = oDs.Tables[0].Rows.Count;
                int[] vet = new int[5];
              //  int count = 0;
               // for (int i = 0; i <= 5; i++)
             //   {
               //     for (int j = 0; i <= qtdlinha; i++)
               //     {
               //         string arma = oDs.Tables[0].Rows[i]["Marca_armação"].ToString();
                //        if (arma == oDs.Tables[0].Rows[i]["Marca_armação"].ToString())
                //        {
                 //           count++;
                 //       }

                  //  }
                  //  vet[i] = count;
                //}
               // txBrutosd.Text = indexx.ToString("C");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }

        }
        
        public double liquido(string tabela, string coluna)
        {
            double valor = 0;
            try
            {

                    DataSet oDs = db1(tabela);


                    int count1 = oDs.Tables[0].Columns.Count;

                    double indexx = 0;
                    double index = 0;
                    int qtdlinha = oDs.Tables[0].Rows.Count;



                    for (int i = 0; i <= qtdlinha - 1; i++)
                    {
                    if (String.IsNullOrEmpty(oDs.Tables[0].Rows[i][coluna].ToString())) { }

                    else
                    {
                        index = Convert.ToDouble(oDs.Tables[0].Rows[i][coluna].ToString());
                        indexx += index;

                    }

                    }

                    
                    valor = indexx;
                    


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
            return valor;
        }
        public double Desc_total()
        {
            double valor = 0;
            try
            {

                string tabela = "Registrosip";
                //definir a string de conexão

                //definir a string SQL
                string sSQL = "select * from " + tabela + "";

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

                double indexx = 0;
                double index = 0;
                int qtdlinha = oDs.Tables[0].Rows.Count;



                for (int i = 0; i < qtdlinha ; i++)
                {
                    index = Convert.ToDouble(db.filtratexto(oDs.Tables[0].Rows[i]["Desconto_total"].ToString()));
                    indexx += index;


                }

                valor = indexx;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
            return valor;
        }
        public void atualiza(DataTable dt)
        {
            dt.Clear();
            OleDbCommand command = new OleDbCommand("select * from DB", db.abreconecxao());
            OleDbDataReader dr = command.ExecuteReader();

            dt.Load(dr);
            laQtd.Text = "Linhas : " + dt.Rows.Count;
            dgvDados.DataSource = dt;
            db.abreconecxao().Close();
            txBrutosd.Text = (bruto(1)).ToString("C");
            txBrutocd.Text = (bruto(0)).ToString("C");
            txDesconto_Total.Text = Desc_total().ToString("C");
            txLucro.Text= (carne() + (liquido("DB","Venda_da_Armação") 
                - liquido("Registrosip", "Desconto_armação")
                -liquido("DB", "Compra_da_Armação")
                + liquido("DB", "Venda_da_lente")
                - liquido("Registrosip", "Desconto_lente")
                 - liquido("DB", "Compra_da_lente")
                - liquido("DB", "Custos_com_venda"))).ToString("C");
            txLucrosdesc.Text= (carne() + liquido("DB", "Venda_da_Armação") - liquido("DB", "Compra_da_Armação") + liquido("DB", "Venda_da_lente") - liquido("DB", "Compra_da_lente") - liquido("DB", "Custos_com_venda") + Desc_total()).ToString("C");
            dgvDados.AutoResizeColumns();
            txPesquisa_princial.AutoCompleteCustomSource = Caixadesusgestaoos("Or_os", "DB");
        }
        public static bool verificatabela(string procurado, string comando)
        {
            bool result = false;
            DataTable d0 = new DataTable();
            OleDbConnection aConnection = new OleDbConnection(sDBstr);
            OleDbCommand comm = new OleDbCommand();
            comm.Connection = aConnection;
            aConnection.Open();
            comm.CommandText = comando;

            OleDbDataAdapter dr = new OleDbDataAdapter();
            comm.CommandText = comando;
            dr.SelectCommand = comm;
            dr.Fill(d0);
            List<string> listacoluna = new List<string>();
            foreach (DataRow row in d0.Rows)
            {

                string linha = string.Join(",", row.ItemArray);
                listacoluna.Add(linha);


            }
            result = string.Join(",", listacoluna).Contains(procurado);
            return result;
        }
        private void Add_registro_Click(object sender, EventArgs e)
        {
            c = cadastro.retorna2();
            try
            {
                Cadastro tela_add_servico = new Cadastro();
                tela_add_servico.ShowDialog();
                atualiza(this.d1);

            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }

        }
        private void caminho()
        {
            //JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                sDBstr= Properties.Settings.Default.Pastainicial;
                atualiza(d1);
                
            }
            catch (Exception)
            {
                sDBstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
                MessageBox.Show("  Arquivo Banco de Dados não encontrado");
                DialogResult drResult = ofd1.ShowDialog();

                if (drResult == System.Windows.Forms.DialogResult.OK)
                {

                    sDBstr += ofd1.FileName;
                    atualiza(d1);
                    txPesquisa_princial.AutoCompleteCustomSource = Caixadesusgestaoos("Or_os", "DB");
                    
     
                }
       
                else
                {

                }
            }
        }



        private void dgvDados_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            retornaosclicada(dgvDados);
            c = true;
            try
            {
                Cadastro tela_add_servico = new Cadastro();
                tela_add_servico.ShowDialog();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           dgvDados.DataSource=  db.pesquisaos("DB", txPesquisa_princial.Text).Tables[0];
            //pesquisa(txPesquisa_princial);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("Mês" == comboBox1.Text)
            {
                dptData.CustomFormat = "MM/yyyy";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ligadesliga("Liga");
        }

        private void buSalvar_Click(object sender, EventArgs e)
        {
            //  Desp_Adm Tributos Desp_legais Desp_fin_eve

            exportabanco("Desp_Adm");
            exportabanco("Tributos");
            ligadesliga("Desliga");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Pastainicial = sDBstr;
            Properties.Settings.Default.Save();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dptData_ValueChanged(object sender, EventArgs e)
        {
           dgvDados.DataSource=  db.Filtrodb(comboBox1.Text, "DB", dptData).Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult drResult = ofd1.ShowDialog();
            if (drResult == System.Windows.Forms.DialogResult.OK)
            {
                excel_manipulations excel_manipulations1 = new excel_manipulations();
                excel_manipulations1.Sourcedb = ofd1.FileName; ;

                Db1.importtoDb();


            }

            }
    }
}
