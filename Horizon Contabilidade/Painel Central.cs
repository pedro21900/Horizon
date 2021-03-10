using System;
using System.Data;
using System.Linq;
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
        private static DataSet DB;

        // private static DataSet Carne;
        private static DataTable Len;
        private static DataTable Arma;
        private static readonly Cadastro cadastro = new Cadastro();
        private static string os = "0";
        private static bool c = false;
        private static int change = 0;
        private static int ini = 0;

        //Classes
        private DataTable d1 = new DataTable();
        private readonly Db db = new Db();
        private readonly Metodos_auxiliades ma = new Metodos_auxiliades();
        //Getters e Setters
        private static string sDBstr;
        public static string SDBstr { get => sDBstr; set => sDBstr = value; }
        //Metodos auxiliares
        public void fills_in()
        {
            if (ini != 1 && change == 1)
            {
                DB.Reset();

                //Carne.Reset();
                Len.Reset();
                Arma.Reset();
            }
            if (change == 1)

            {
               
                DB=ma.Database(dptData, comboBox2, comboBox1);
               
                    //db.Filtrodb(comboBox1.Text, comboBox2.Text, "DB", dptData, 0, "", "");


                //DB.Tables.Add(db.Filtrodb(comboBox1.Text, comboBox2.Text, "Carne", dptData, 0, "", "").Tables[0].Copy());

                Arma = db.Filtrodb(comboBox1.Text, comboBox2.Text, "Qtd", dptData, 1, "Fornecedor_Armação", "Armação").Tables[0];
                Len = db.Filtrodb(comboBox1.Text, comboBox2.Text, "Qtd", dptData, 1, "Fornecedor_Lente", "Lente").Tables[0];
               // ma.DatabaseMemory(dptData, comboBox2, comboBox1);
            }


        }
        public bool retorna()
        {

            return c;
        }
        public string retornaos()
        {

            return os;
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
        public AutoCompleteStringCollection Caixadesusgestaoos(string coluna)
        {
            AutoCompleteStringCollection stringCollection = new AutoCompleteStringCollection();
            string[] postSouce = DB.Tables[0].AsEnumerable().Select<System.Data.DataRow, String>(x => x.Field<string>("Or_os")).ToArray();
            stringCollection.AddRange(postSouce);

            return stringCollection;
        }
        //Metodos
        public double carne()
        {
            double index = DB.Tables[1].AsEnumerable().Sum(s => s.Field<double>("Valor"));
            return index;
        }
        public double carnelastmonth()
        {
            double index = 0;
            try
            {

                int count1 = DB.Tables[1].Columns.Count;
                int qtdlinha = DB.Tables[1].Rows.Count;

                for (int i = 0; i <= qtdlinha - 1; i++)
                {
                    DateTime datasale = Convert.ToDateTime(DB.Tables[1].Rows[i]["Data_de_Venda"].ToString());
                    DateTime datafat = Convert.ToDateTime(DB.Tables[1].Rows[i]["Data"].ToString());
                    DateTime data = dptData.Value;
                    if (comboBox1.Text == "Mês / Ano" && datafat.Month == data.Month && datasale.Month < data.Month ||
                        datafat.Year < data.Year && comboBox1.Text == "Mês / Ano" && datafat.Month == data.Month && datasale.Month < data.Month)
                    {
                        index += Convert.ToDouble(DB.Tables[1].Rows[i]["Valor"].ToString());


                    }
                    else if (comboBox1.Text == "Ano" && datafat.Year == data.Year || datasale.Year < data.Year && comboBox1.Text == "Ano" && datafat.Year == data.Year)
                    {
                        index += Convert.ToDouble(DB.Tables[1].Rows[i]["Valor"].ToString());

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
            return index;
        }
        public double carneaftermonth()
        {
            double index = 0;
            try
            {
                string comando = "";
                if (comboBox2.Text != "Todas as Lojas")
                {
                    comando = " WHERE (Loja LIKE '%" + comboBox2.Text + "%')";
                }
                DataTable carne = db.TableDb("Select * From Carne" + comando);
                int qtdlinha = carne.Rows.Count;

                for (int i = 0; i <= qtdlinha - 1; i++)
                {
                    DateTime datasale = Convert.ToDateTime(carne.Rows[i]["Data_de_Venda"].ToString());
                    DateTime datafat = Convert.ToDateTime(carne.Rows[i]["Data"].ToString());
                    DateTime data = dptData.Value;
                    if (comboBox1.Text == "Mês / Ano" && (datasale.Month == data.Month || datasale.Month != data.Month && datasale.Month != datafat.Month) && datafat.Month > data.Month && datafat.Year == data.Year ||
                        datafat.Year > data.Year && comboBox1.Text == "Mês / Ano" && datasale.Month == data.Month)
                    {
                        index += Convert.ToDouble(carne.Rows[i]["Valor"].ToString());

                    }
                    else if (datasale.Year < data.Year && comboBox1.Text == "Ano" && datafat.Year == data.Year)
                    {
                        index += Convert.ToDouble(carne.Rows[i]["Valor"].ToString());

                    }

                }
                carne.Clear(); carne.Dispose();
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

            else if (tabela == "Carne")
            {
                oDs = DB.Tables[1];
            }

            return oDs;
        }
        public double liquido(string tabela, string coluna)
        {
            double index = DB.Tables[0].AsEnumerable().Sum(s => s.Field<double>(coluna));
            return index;
        }
        public void atualiza()
        {
            fills_in();
            double carneafter = carneaftermonth();
            double carnelast = carnelastmonth();
            double paid_out = carnelast - carneafter;
            double carne1 = carne() - paid_out;

            double Venda_da_Armação = liquido("DB", "Venda_da_Armação");
            double Venda_da_Lente = liquido("DB", "Venda_da_Lente");
            double Custo_Com_Venda = liquido("DB", "Custo_Com_Venda");
            double DescT = liquido("DB", "Desconto_total");
            double Bruto = Venda_da_Armação + Venda_da_Lente;
            double Caixa = Bruto - DescT;
            double Hoya = Providers("HOYA", "Lente");
            double Rodenstock = (Providers("RODENSTOCK", "Lente") + Providers("RODENSTOCK", "Armação"));
            double labootica = Providers("LABOOTICA", "Lente");
            double icopa = Providers("ICOPA", "Lente");
            double opt = Providers("OPTIPRIME", "Lente");
            double tri = Providers("TRI-LAB", "Lente");
            double zeissbelem = Providers("ZEISS BELEM", "Lente");

            double Essilor = Providers("COMPROL", "Lente");

            double Safilo = Providers("SAFILO", "Armação");
            double Belaro = Providers("BELARO", "Armação");
            double Mizuno = Providers("MIZUNO", "Armação");
            double Marcolin = Providers("MARCOLIN", "Armação");

            //Lentes

            txHoya.Text = Hoya.ToString("C");
            txRodenstock.Text = Rodenstock.ToString("C");
            txLabootica.Text = labootica.ToString("C");
            txComprol.Text = Essilor.ToString("C");
            txOpt.Text = opt.ToString("C");
            txIcopa.Text = icopa.ToString("C");
            txTri.Text = tri.ToString("C");
            txZbelem.Text = zeissbelem.ToString("C");


            //Armação
            txSafilo.Text = Safilo.ToString("C");
            txBelaro.Text = Belaro.ToString("C");
            txMizuno.Text = Mizuno.ToString("C");
            txMarcolin.Text = Marcolin.ToString("C");



            laQtd.Text = "Linhas : " + DB.Tables[0].Rows.Count;
            dgvDados.DataSource = DB.Tables[0];
            dgvDadosC.DataSource = DB.Tables[1];
            txCarnenpg.Text = carneafter.ToString("C");
            txCustoVenda.Text = Custo_Com_Venda.ToString("C");
            txBrutocd.Text = (Bruto).ToString("C");
            txDesconto_Total.Text = DescT.ToString("C");
            if (comboBox1.Text == "Dia")
            {
                txLucro.Text = (Caixa - Custo_Com_Venda + paid_out).ToString("C");
                txCaixa.Text = (Caixa + carne1).ToString("C");
                tbxCarne.Text = carne1.ToString("C");
                txReceita.Text = ((Caixa + carne1) - carneafter).ToString("C");
            }
            else if (comboBox1.Text == "Mês / Ano")
            {
                txCaixa.Text = (Caixa + carne1).ToString("C");
                txLucro.Text = (Caixa - Custo_Com_Venda + paid_out).ToString("C");
                tbxCarne.Text = (carne1 + paid_out).ToString("C");
                txReceita.Text = ((Caixa + carne1) - carneafter).ToString("C");
            }
            else
            {
                txLucro.Text = (Caixa - Custo_Com_Venda + paid_out).ToString("C");
                txCaixa.Text = (Caixa + carne1).ToString("C");
                tbxCarne.Text = (paid_out + carne1).ToString("C");
                txReceita.Text = ((Caixa + carne1) - carneafter).ToString("C");
            }
            //Chart

            Armação.Series[0].Points.Clear();
            Armação.DataSource = Arma;

            Lente.Series[0].Points.Clear();
            Lente.DataSource = Len;

            dgvDados.AutoResizeColumns();
            dgvDados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            txPesquisa_princial.AutoCompleteCustomSource = Caixadesusgestaoos(comboBox3.Text);
            change = 0;
        }
        public double Providers(string nameProviders, string lens_or_frame)
        {
            double index = 0;

            index = DB.Tables[0].AsEnumerable().Where(DB => DB.Field<string>("Fornecedor").Contains(nameProviders)).Sum(s => s.Field<double>("Venda_da_" + lens_or_frame)) -
                -DB.Tables[0].AsEnumerable().Where(DB => DB.Field<string>("Fornecedor").Contains(nameProviders)).Sum(s => s.Field<double>("Desconto_" + lens_or_frame));///-
               // DB.Tables[0].AsEnumerable().Where(DB => DB.Field<string>("Fornecedor").Contains(nameProviders)).Sum(s => s.Field<double>("Compra_da_" + lens_or_frame));

            return index;
        }
        //Form
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'dBDataSet.DB'. Você pode movê-la ou removê-la conforme necessário.


            try
            {
                SDBstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Properties.Settings.Default.SourceDb;
                change = 1;
                ini = 1;
                atualiza();
                ini = 0;
                change = 0;
                d1.Clear();
                d1 = DB.Tables[0];


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
            //dgvDados.Rows.Clear();


            dgvDados.DataSource = DB.Tables[0].AsEnumerable().Where(DB => DB.Field<string>(comboBox3.Text).Contains(txPesquisa_princial.Text)).ToArray();
            //  dgvDados.DataSource
            //dgvDados.DataSource = db.pesquisaos("DB", comboBox3.Text, txPesquisa_princial.Text, "", "").Tables[0];
            //pesquisa(txPesquisa_princial);

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Dia")
            {
                label12.Text = "Receita do dia";
            }
            else if (comboBox1.Text == "Mês / Ano")
            {
                label12.Text = "Receita do Mês";
            }
            else if (comboBox1.Text == "Ano")
            {
                label12.Text = "Receita do Ano";
                txCaixa.Enabled = false;

            }
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


            ligadesliga("Desliga");
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Folder_Way = SDBstr;
            Properties.Settings.Default.Save();

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
        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            db.Deletalinha("DB", retornaosclicada(dgvDados));

            atualiza();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Metodos_auxiliades metodos_Auxiliades = new Metodos_auxiliades();
            metodos_Auxiliades.calcforn();
            atualiza();
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            change = 1;
            d1.Clear();
            d1 = DB.Tables[0];
            atualiza();
        }
        private void Add_registro_Click_1(object sender, EventArgs e)
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

        private void dgvDados_DoubleClick(object sender, EventArgs e)
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
            c = cadastro.retorna2();
            atualiza();
        }
    }
}
