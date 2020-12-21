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
        //Variaveis Globais
        static DataSet DB;
        static DataSet Registrosip;
        static DataSet Carne;        
        static Cadastro cadastro = new Cadastro();
        static string os = "0";
        static bool c = false;
        static int change = 0;
        //Classes
        DataTable d1 = new DataTable();
         Db db = new Db();
        //Getters e Setters
        private static string sDBstr;
        public static string SDBstr { get => sDBstr; set => sDBstr = value; }
        //Metodos auxiliares
        public void fills_in()
        {
            if (change == 1)
            {
                DB.Reset();
                Registrosip.Reset();
                Carne.Reset();
            }
            
            DB = db.Filtrodb(comboBox1.Text, comboBox2.Text, "DB", dptData);
            
            Registrosip = db.Filtrodb(comboBox1.Text, comboBox2.Text, "Registrosip", dptData);
            
            Carne = db.Filtrodb(comboBox1.Text, comboBox2.Text, "Carne", dptData);
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
                OleDbConnection oCn = new OleDbConnection(SDBstr);
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
            
            atualiza();
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

                //definir a string SQL
                string sSQL = "select " + coluna + " from " + DB + "";

                //criar o objeto connection
                OleDbConnection oCn = new OleDbConnection(SDBstr);
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
        public double carne()
        {
            double index = 0;
            try
            {

                int count1 = Carne.Tables[0].Columns.Count;
                int qtdlinha = Carne.Tables[0].Rows.Count;

                for (int i = 0; i <= qtdlinha - 1; i++)
                {
                    index += Convert.ToDouble(Carne.Tables[0].Rows[i]["Valor"].ToString());
                }    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
            return index;
        }
        public DataTable db1(string tabela)
        {

            DataTable oDs = new DataTable();


                if (tabela == "DB")
                {
                    oDs = DB.Tables[0];
                }
                else if (tabela == "Registrosip")
                {
                    oDs = Registrosip.Tables[0];
                }
                else if (tabela == "Carne")
                {
                    oDs = Carne.Tables[0];
                }           
            
            return oDs;
        }
        public double liquido(string tabela, string coluna)
        {
            double index = 0;
            try
            {
                    DataTable oDs = db1(tabela);
                    int qtdlinha = oDs.Rows.Count;

                for (int i = 0; i <= qtdlinha - 1; i++)
                {
                    if (String.IsNullOrEmpty(oDs.Rows[i][coluna].ToString())) { }

                    else
                    {
                        double index2= Convert.ToDouble(oDs.Rows[i][coluna].ToString());
                        index += index2;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);
            }
            return index;
        }
        public double Desc_total()
        {
            double index = 0;
            try
            {
                int qtdlinha = Registrosip.Tables[0].Rows.Count;
                for (int i = 0; i < qtdlinha ; i++)
                {
                 double index2 = Convert.ToDouble(db.filtratexto(Registrosip.Tables[0].Rows[i]["Desconto_total"].ToString()));

                    index += index2;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
            return index;
        }
        public void atualiza()
        {
            fills_in();
            double carne1= carne();
            double Venda_da_Armação= liquido("DB", "Venda_da_Armação");
            double Venda_da_lente = liquido("DB", "Venda_da_lente");
            double Custo_Com_Venda = liquido("DB", "Custo_Com_Venda");
            double DescT = Desc_total();
            laQtd.Text = "Linhas : " + DB.Tables[0].Rows.Count;
            dgvDados.DataSource = DB.Tables[0];
            txCustoVenda.Text= Custo_Com_Venda.ToString("C");
            txBrutocd.Text = (Venda_da_Armação + Venda_da_lente).ToString("C");
            txDesconto_Total.Text = DescT.ToString("C");
            if (comboBox1.Text == "Dia" )
            {
                txLucro.Text = (Venda_da_Armação + Venda_da_lente - DescT - Custo_Com_Venda + carne1).ToString("C");
            }
            else
            {
                txLucro.Text = (Venda_da_Armação + Venda_da_lente - DescT - Custo_Com_Venda).ToString("C");
            }
            tbxCarne.Text = carne1.ToString("C");
            dgvDados.AutoResizeColumns();
            txPesquisa_princial.AutoCompleteCustomSource = Caixadesusgestaoos("Or_os", "DB");
            change = 0;
        }
        public static bool verificatabela(string procurado, string comando)
        {
            bool result = false;
            DataTable d0 = new DataTable();
            OleDbConnection aConnection = new OleDbConnection(SDBstr);
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
                atualiza();

            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                SDBstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+ Properties.Settings.Default.SourceDb;
                atualiza();
                d1.Clear();
               d1= DB.Tables[0];


            }
            catch (Exception)
            {
               
                MessageBox.Show("  Arquivo Banco de Dados não encontrado");
                DialogResult drResult = ofd1.ShowDialog();

                if (drResult == System.Windows.Forms.DialogResult.OK)
                {

                    SDBstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
                    SDBstr += ofd1.FileName;
                    Properties.Settings.Default.SourceDb = ofd1.FileName;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Folder_Way = SDBstr;
                    fills_in();
                    atualiza();
                    d1.Clear();
                    d1 = DB.Tables[0];

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
            change = 1;
            d1.Clear();
            d1 = DB.Tables[0];
            atualiza();
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
            Properties.Settings.Default.Folder_Way = SDBstr;
            Properties.Settings.Default.Save();

        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void dptData_ValueChanged(object sender, EventArgs e)
        {
            change = 1;
            d1.Clear();
            d1 = DB.Tables[0];
            atualiza();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult drResult = ofd1.ShowDialog();
            if (drResult == System.Windows.Forms.DialogResult.OK)
            {
                Db.Setsoucetable = ofd1.FileName; ;
               
                Db.importtoDb();


            }

            }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            change = 1;
            d1.Clear();
            d1 = DB.Tables[0];
            atualiza();
        }

    }
}
