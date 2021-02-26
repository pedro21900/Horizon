using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Horizon_Contabilidade
{
    public partial class Cadastro : Form
    {
        public Cadastro()
        {
            InitializeComponent();
            retorna2();
            tf1();

        }

        private static readonly Form1 form1 = new Form1();
        private static readonly Calculo calculo = new Calculo();
        private readonly Db db = new Db();
        private static string labs = "0";
        private static string labm = "0";
        private static string decl = "0";
        private static string deca = "0";
        private static string dect = "0";
        private static string cdv = "0";
        //private static string fdp1 = "0";
        private static string cb = "0";
        private static string Cod;
        private static string Fornecedorlente;
        private static string Marcalente;
        private static string Nome;
        private static string Tratamento;
        private static string Tipo;
        private static string Chamado;
        private static string Vendalente;
        public string[] tf1()
        {

            string[] lente = new string[] { Cod, Fornecedorlente, Marcalente, Nome, Tratamento, Tipo, Chamado, Vendalente };
            return lente;
        }
        public string perda()
        {
            string porcento = calculo.perda(calcSoma(txDesconto_Total.Text, calcSoma(txLab.Text, txCol.Text)),
                calcSoma(txCompra_lente.Text, txCompra_armacao.Text),
                calcSoma(txVenda_armacao.Text, txVenda_lente.Text)).ToString("P");
            return porcento;


        }
        public string ganho()
        {
            if (txLucro_total.Text.Contains("-")) { txGanho.Enabled = false; }
            string porcento = calculo.ganho(txLucro_total.Text,
                calcSoma(txVenda_armacao.Text, txVenda_lente.Text)).ToString("P");
            return porcento;
        }
        public void retunrtratamento(string[] Tratamentos)
        {
            cbTratamento.Items.Clear();
            cbTratamento.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbTratamento.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cbTratamento.AutoCompleteCustomSource.AddRange(Tratamentos);
            cbTratamento.Items.AddRange(Tratamentos);
        }
        private static readonly string sDBstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Properties.Settings.Default.SourceDb;
        public AutoCompleteStringCollection Caixadesusgestaoos(string coluna, string DB)
        {
            AutoCompleteStringCollection stringCollection = new AutoCompleteStringCollection();
            try
            {
                //definir a string de conexão


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
        public void sugestao()
        {
            txPesquisa_Cadastro.AutoCompleteCustomSource = Caixadesusgestaoos("Or_os", "DB");
            txFornecedor_armacao.AutoCompleteCustomSource = Caixadesusgestaoos("Fornecedor_Armação", "DB");
            txMarca_armacao.AutoCompleteCustomSource = Caixadesusgestaoos("Marca_armação", "DB");
            txModelo_armacao.AutoCompleteCustomSource = Caixadesusgestaoos("Modelo_Armação", "DB");
            txFornecedor_lente.AutoCompleteCustomSource = Caixadesusgestaoos("Fornecedor_Lente", "LentesValores");
            txMarca_lente.AutoCompleteCustomSource = Caixadesusgestaoos("Marca", "LentesValores");
            txNome_lente.AutoCompleteCustomSource = Caixadesusgestaoos("Nome_Lente", "LentesValores");
        }
        public void apagatudo(string porta)
        {

            if (porta == "Lente")
            {
                txOs_Or.Text = "";
                txFornecedor_lente.Text = "";
                txMarca_lente.Text = "";
                txNome_lente.Text = "";
                txVenda_lente.Text = "";
                txCompra_lente.Text = "";
                txLucro_lente.Text = "";
                txDesconto_Lente.Text = "";
            }
            else if (porta == "Armação")
            {
                txOs_Or.Text = "";
                txFornecedor_armacao.Text = "";
                txMarca_armacao.Text = "";
                txModelo_armacao.Text = "";
                txVenda_armacao.Text = "";
                txCompra_armacao.Text = "";
                txLucro_armacao.Text = "";
                txDesconto_Armacao.Text = "";
            }
            else
            {
                txOs_Or.Text = "";
                txFornecedor_armacao.Text = "";
                txMarca_armacao.Text = "";
                txModelo_armacao.Text = "";
                txVenda_armacao.Text = "";
                txCompra_armacao.Text = "";
                txLucro_armacao.Text = "";
                txFornecedor_lente.Text = "";
                txMarca_lente.Text = "";
                txNome_lente.Text = "";
                txVenda_lente.Text = "";
                txCompra_lente.Text = "";
                txLucro_lente.Text = "";
                txCusto_com_venda.Text = "";
                txLucro_total.Text = "";
                txTxcartao.Text = "";
                txDesconto_Lente.Text = "";
                txDesconto_Armacao.Text = "";
                txCol.Text = "";
                txLab.Text = "";

            }
        }
        public void travatudo()
        {
            txFornecedor_armacao.Enabled = false;
            txMarca_armacao.Enabled = false;
            txModelo_armacao.Enabled = false;
            txVenda_armacao.Enabled = false;
            txCompra_armacao.Enabled = false;
            txLucro_armacao.Enabled = false;
            txDesconto_Armacao.Enabled = false;
            txFornecedor_lente.Enabled = false;
            txMarca_lente.Enabled = false;
            txNome_lente.Enabled = false;
            txVenda_lente.Enabled = false;
            txCompra_lente.Enabled = false;
            txLucro_lente.Enabled = false;
            txDesconto_Lente.Enabled = false;
            txCol.Enabled = false;
            txLab.Enabled = false;
        }
        private void liga_delisgatx(int ld, string tipo)
        {

            if (ld == 1 && tipo == "Armação")
            {
                txFornecedor_armacao.Enabled = false;
                txMarca_armacao.Enabled = false;
                txModelo_armacao.Enabled = false;
                txVenda_armacao.Enabled = false;
                txCompra_armacao.Enabled = false;
                txLucro_armacao.Enabled = false;
                txDesconto_Armacao.Enabled = false;
                txFornecedor_armacao.Text = "";
                txMarca_armacao.Text = "";
                txModelo_armacao.Text = "";
                txVenda_armacao.Text = "";
                txCompra_armacao.Text = "";
                txLucro_armacao.Text = "";
                txDesconto_Armacao.Text = "";



            }
            else if (ld == 0 && tipo == "Armação")
            {
                txFornecedor_armacao.Enabled = true;
                txMarca_armacao.Enabled = true;
                txModelo_armacao.Enabled = true;
                txVenda_armacao.Enabled = true;
                txCompra_armacao.Enabled = true;
                txLucro_armacao.Enabled = true;
                txDesconto_Armacao.Enabled = true;
                txCol.Enabled = true;
                txLab.Enabled = true;

            }
            else
            {
                if (ld == 1 && tipo == "Lente")
                {
                    txFornecedor_lente.Enabled = false;
                    txMarca_lente.Enabled = false;
                    txNome_lente.Enabled = false;
                    txVenda_lente.Enabled = false;
                    txCompra_lente.Enabled = false;
                    txLucro_lente.Enabled = false;
                    txDesconto_Lente.Enabled = false;
                    txFornecedor_lente.Text = "";
                    txMarca_lente.Text = "";
                    txNome_lente.Text = "";
                    txVenda_lente.Text = "";
                    txCompra_lente.Text = "";
                    txLucro_lente.Text = "";
                    txDesconto_Lente.Text = "";


                }
                else if (ld == 0 && tipo == "Lente")
                {
                    txFornecedor_lente.Enabled = true;
                    txMarca_lente.Enabled = true;
                    txNome_lente.Enabled = true;
                    txVenda_lente.Enabled = true;
                    txCompra_lente.Enabled = true;
                    txLucro_lente.Enabled = true;
                    txDesconto_Lente.Enabled = true;
                    txCol.Enabled = true;
                    txLab.Enabled = true;

                }
            }

        }
        public void exportabanco(string tabela)
        {
            Db db = new Db();
            try
            {
                if (tabela == "DB")
                {
                    db.addlinhalayout1(tabela, Convert.ToDateTime(dateTimePicker1.Value).ToShortDateString(), txOs_Or.Text, txFornecedor_armacao.Text + " / " + txFornecedor_lente.Text,
                txCompra_lente.Text, txVenda_lente.Text, txCompra_armacao.Text, txVenda_armacao.Text, txCol.Text, txLab.Text, txCusto_com_venda.Text, txVenda.Text, txModelo_armacao.Text,
                txNome_lente.Text, txFornecedor_lente.Text, txFornecedor_armacao.Text,txDesconto_Lente.Text,txDesconto_Armacao.Text, txDesconto_Total.Text, txMarca_armacao.Text,
                txMarca_lente.Text,  txObs.Text, cb);
                }
                else if (tabela == "Registrosip")
                {
                    db.addlinhalayout2(tabela, txOs_Or.Text, Convert.ToDateTime(dateTimePicker1.Value).ToShortDateString(), txModelo_armacao.Text, txNome_lente.Text,txFornecedor_lente.Text, txFornecedor_armacao.Text, txDesconto_Total.Text
                      , txMarca_armacao.Text, txMarca_lente.Text, cb, txObs.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
        }
        public void importabanco(string tabela, string pesquisa)
        {
            try
            {


                //definir a string de conexão

                //definir a string SQL
                string sSQL = "select * from " + tabela + " WHERE  Or_os = " + pesquisa;

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


                int count1 = oDs.Tables[0].Columns.Count;

                string indexx = "0";
                string index = "0";

                foreach (DataColumn indexx1 in oDs.Tables[0].Columns)
                {
                    indexx = indexx1.ToString();
                    index = oDs.Tables[0].Rows[0][indexx].ToString();
                    
                        //Posição
                        if (indexx == "Or_os") { txOs_Or.Text = index; }
                        else if (indexx == "Data") { dateTimePicker1.Text = index; }
                        //Lente

                        else if (indexx == "Venda_da_lente") { txVenda_lente.Text = index; }
                        else if (indexx == "Compra_da_lente") { txCompra_lente.Text = index; }                    
                        else if (indexx == "Coloração") { txCol.Text = index; }
                        else if (indexx == "Nome_Lente") { txNome_lente.Text = index; }
                        else if (indexx == "Fornecedor_Lente") { txFornecedor_lente.Text = index; }
                        else if (indexx == "Marca_lente") { txMarca_lente.Text = index; }
                        else if (indexx == "Desconto_lente") { txDesconto_Lente.Text = index; }
                        else if (indexx == "Lab") { txLab.Text = index; }                                          
                        
                        

                    //Armação
                        if (indexx == "Modelo_Armação") { txModelo_armacao.Text = index; }
                        else if (indexx == "Venda_da_Armação") { txVenda_armacao.Text = index; }
                        else if (indexx == "Compra_da_Armação") { txCompra_armacao.Text = index; }
                        else if (indexx == "Marca_armação") { txMarca_armacao.Text = index; }
                        else if (indexx == "Fornecedor_Armação") { txFornecedor_armacao.Text = index; }
                        else if (indexx == "Desconto_armação") { txDesconto_Armacao.Text = index; }
                        else if (indexx == "Modelo_Armação") { txModelo_armacao.Text = index; }                                                                    
                        
                        //Extras
                        else if (indexx == "Obs") { txObs.Text = index; }
                        else if (indexx == "Loja") { cb = index; }
                    
                }

                //liberar o data adapter , o dataset , o comandbuilder e a conexao
                oDA.Dispose(); oDs.Dispose(); //oCB.Dispose();
                oCn.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
        }
        public double calcSub(string txVenda_lente, string txCompra_Lente, string txDesconto, string txCusto_com_venda)
        {

            double txLucro_lente = calculo.calcSub(txVenda_lente, txCompra_Lente, txDesconto, txCusto_com_venda);

            return txLucro_lente;
        }
        public string calcSoma(string tx1, string tx2)
        {

            string txVenda = calculo.calcSoma(tx1, tx2);
            return txVenda;
        }
        public string calcSomaeSub(string txVenda_lente, string txDesconto_Lente, string txVenda_armacao, string txDesconto_Armacao)
        {
            string retorno = "0";
            retorno = calculo.calcSomaeSub(txVenda_lente, txDesconto_Lente, txVenda_armacao, txDesconto_Armacao);
            return retorno;
        }
        public bool verificarcampos()
        {
            string data = "Competencia esta dia de hoje";
            string x1 = "Campo ";
            string x2 = " vazio ";
            int vari = 0;
            bool boo = true;
            DateTime thisDay = DateTime.Today;
            //DialogResult confirm;
            if (comboBox1.Text == "Somente Lente")
            {
                if (string.IsNullOrEmpty(txOs_Or.Text))
                {
                    MessageBox.Show("Or/os Vazia");
                    vari++;
                }

                if (thisDay.ToString("d") == Convert.ToDateTime(dateTimePicker1.Value).ToShortDateString())

                {
                    //pegar sequancia de data do banco de dados 
                    //
                    MessageBox.Show(data, "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }
                //if (confirm.ToString().ToUpper() == "YES") { }

                if (string.IsNullOrEmpty(txFornecedor_lente.Text))
                {
                    MessageBox.Show(x1 + "fornecedor de lente " + x2, "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    vari++;
                }
                if (string.IsNullOrEmpty(txCompra_lente.Text))
                {

                    MessageBox.Show("Campo valor de compra da lente vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }
                if (string.IsNullOrEmpty(txVenda_lente.Text))
                {

                    MessageBox.Show("Campo valor de venda da lente vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }


                if (string.IsNullOrEmpty(txLab.Text))
                {
                    DialogResult result = MessageBox.Show("Campo valor de sufaçagem vazio , deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        labs = "0";
                    }
                    else if (result == DialogResult.No)
                    {

                        vari++;
                    }
                }

                if (string.IsNullOrEmpty(txCol.Text))
                {
                    MessageBox.Show("Campo valor de montagem vazio", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }

                if (string.IsNullOrEmpty(txNome_lente.Text))
                {
                    MessageBox.Show("Campo nome da lente vazio", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }

                if (string.IsNullOrEmpty(txDesconto_Lente.Text))
                {
                    DialogResult result = MessageBox.Show("Campo valor desconto lente vazio , deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        decl = "0";
                    }
                    else if (result == DialogResult.No)
                    {
                        vari++;
                    }


                }

                if (vari >= 1)
                {
                    boo = false;
                }
                else if (vari == 0)
                {
                    boo = true;
                }

            }
            if (comboBox1.Text == "Somente Armação")
            {
                if (string.IsNullOrEmpty(txOs_Or.Text))
                {
                    MessageBox.Show("Or/os Vazia");
                    vari++;
                }

                if (thisDay.ToString("d") == Convert.ToDateTime(dateTimePicker1.Value).ToShortDateString())

                {
                    //pegar sequancia de data do banco de dados 
                    //
                    MessageBox.Show(data, "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }
                //if (confirm.ToString().ToUpper() == "YES") { }
                if (string.IsNullOrEmpty(txFornecedor_armacao.Text))
                {

                    MessageBox.Show(x1 + "fornecedor de armação " + x2, "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }


                if (string.IsNullOrEmpty(txCompra_armacao.Text))
                {
                    MessageBox.Show("Campo valor de venda da armação vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }

                if (string.IsNullOrEmpty(txVenda_armacao.Text))
                {
                    MessageBox.Show("Campo valor de venda da armação vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }



                if (string.IsNullOrEmpty(txCol.Text))
                {
                    DialogResult result = MessageBox.Show("Campo valor de montagem vazio , deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        labm = "0";
                    }
                    else if (result == DialogResult.No)
                    {
                        vari++;
                    }

                }



                if (string.IsNullOrEmpty(txModelo_armacao.Text))
                {
                    MessageBox.Show("Campo modelo da armação vazio ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }

                if (string.IsNullOrEmpty(txDesconto_Armacao.Text))
                {
                    DialogResult result = MessageBox.Show("Campo valor desconto armação vazio , deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        deca = "0";
                    }
                    else if (result == DialogResult.No)
                    {
                        vari++;
                    }

                }

                if (vari >= 1)
                {
                    boo = false;
                }
                else if (vari == 0)
                {
                    boo = true;
                }

            }
            if (comboBox1.Text == "Lente + Armação")
            {
                if (string.IsNullOrEmpty(txOs_Or.Text))
                {
                    MessageBox.Show("Or/os Vazia");
                    vari++;
                }

                if (thisDay.ToString("d") == Convert.ToDateTime(dateTimePicker1.Value).ToShortDateString())

                {
                    //pegar sequancia de data do banco de dados 
                    //
                    MessageBox.Show(data, "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }
                //if (confirm.ToString().ToUpper() == "YES") { }
                if (string.IsNullOrEmpty(txFornecedor_armacao.Text))
                {

                    MessageBox.Show(x1 + "fornecedor de armação " + x2, "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }
                if (string.IsNullOrEmpty(txFornecedor_lente.Text))
                {
                    MessageBox.Show(x1 + "fornecedor de lente " + x2, "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }
                if (string.IsNullOrEmpty(txCompra_lente.Text))
                {

                    MessageBox.Show("Campo valor de compra da lente vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }
                if (string.IsNullOrEmpty(txVenda_lente.Text))
                {

                    MessageBox.Show("Campo valor de venda da lente vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }
                if (string.IsNullOrEmpty(txCompra_armacao.Text))
                {
                    MessageBox.Show("Campo valor de venda da armação vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }

                if (string.IsNullOrEmpty(txVenda_armacao.Text))
                {
                    MessageBox.Show("Campo valor de venda da armação vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }

                if (string.IsNullOrEmpty(txLab.Text))
                {
                    DialogResult result = MessageBox.Show("Campo valor de sufaçagem vazio , deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        labs = "0";
                    }
                    else if (result == DialogResult.No)
                    {
                        vari++;
                    }
                }

                if (string.IsNullOrEmpty(txCol.Text))
                {
                    DialogResult result = MessageBox.Show("Campo valor de montagem vazio , deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        labm = "0";
                    }
                    else if (result == DialogResult.No)
                    {
                        vari++;
                    }

                }



                if (string.IsNullOrEmpty(txModelo_armacao.Text))
                {
                    MessageBox.Show("Campo modelo da mrmação vazio ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }

                if (string.IsNullOrEmpty(txNome_lente.Text))
                {
                    MessageBox.Show("Campo nome da lente vazio", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }



                if (string.IsNullOrEmpty(txDesconto_Lente.Text))
                {
                    DialogResult result = MessageBox.Show("Campo valor desconto da lente vazio, deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        decl = "0";
                    }
                    else if (result == DialogResult.No)
                    {
                        vari++;
                    }

                }


                if (string.IsNullOrEmpty(txDesconto_Armacao.Text))
                {
                    DialogResult result = MessageBox.Show("Campo valor desconto da armação vazio, deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        deca = "0";
                    }
                    else if (result == DialogResult.No)
                    {
                        vari++;
                    }

                    if (vari >= 1)
                    {
                        boo = false;
                    }
                    else if (vari == 0)
                    {
                        boo = true;
                    }

                }
            }

            return boo;

        }
        public bool retorna2()
        {

            return false;
        }
        private void txVenda_lente_TextChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text == "" && form1.retorna() == false)
            {
                MessageBox.Show("Selecione o tipo da venda");
            }
            txVenda.Text = calcSomaeSub(txVenda_lente.Text, txDesconto_Lente.Text, txVenda_armacao.Text, txDesconto_Armacao.Text);
            txLucro_lente.Text = calcSub(txVenda_lente.Text, txCompra_lente.Text, txDesconto_Lente.Text, calcSoma(labs, labm)).ToString("C");

            txGanho.Text = ganho();
            txPerda.Text = perda();

        }
        private void txVenda_armacao_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" && form1.retorna() == false)
            {
                MessageBox.Show("Selecione o tipo da venda");
            }
            //txVenda.Text = calcSomaeSub(txVenda_lente.Text, txDesconto_Lente.Text, txVenda_armacao.Text, txDesconto_Armacao.Text);
            txDesconto_Total.Text = calcSoma(txDesconto_Lente.Text, txDesconto_Armacao.Text);
            txVenda.Text = calcSomaeSub(txVenda_lente.Text, txDesconto_Lente.Text, txVenda_armacao.Text, txDesconto_Armacao.Text);
            txLucro_armacao.Text = calcSub(txVenda_armacao.Text, txCompra_armacao.Text, txDesconto_Armacao.Text, calcSoma(labs, labm)).ToString("C");

            txGanho.Text = ganho();
            txPerda.Text = perda();

        }
        private void txCompra_Lente_TextChanged(object sender, EventArgs e)
        {

            txCusto_com_venda.Text = calcSoma(calcSoma(txCompra_lente.Text, txCompra_armacao.Text), calcSoma(txLab.Text, txCol.Text));
            txVenda.Text = calcSomaeSub(txVenda_lente.Text, txDesconto_Lente.Text, txVenda_armacao.Text, txDesconto_Armacao.Text);
            txDesconto_Total.Text = calcSoma(txDesconto_Lente.Text, txDesconto_Armacao.Text);
            txLucro_lente.Text = calcSub(txVenda_lente.Text, txCompra_lente.Text, txDesconto_Lente.Text, calcSoma(labs, labm)).ToString("C");

            txGanho.Text = ganho();
            txPerda.Text = perda(); ;

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Somente Lente")
            {
                liga_delisgatx(1, "Armação");
                liga_delisgatx(0, "Lente");

            }
            if (comboBox1.Text == "Somente Armação")
            {
                liga_delisgatx(1, "Lente");
                liga_delisgatx(0, "Armação");
            }
            if (comboBox1.Text == "Lente + Armação")
            {
                liga_delisgatx(0, "Armação");
                liga_delisgatx(0, "Lente");
            }
        }
        private void txCompra_armacao_TextChanged(object sender, EventArgs e)
        {

            txCusto_com_venda.Text = calcSoma(calcSoma(txCompra_lente.Text, txCompra_armacao.Text), calcSoma(txLab.Text, txCol.Text));
            txDesconto_Total.Text = calcSoma(txDesconto_Lente.Text, txDesconto_Armacao.Text);
            txVenda.Text = calcSomaeSub(txVenda_lente.Text, txDesconto_Lente.Text, txVenda_armacao.Text, txDesconto_Armacao.Text);
            txLucro_armacao.Text = calcSub(txVenda_armacao.Text, txCompra_armacao.Text, txDesconto_Armacao.Text, calcSoma(labs, labm)).ToString("C");

            txGanho.Text = ganho();
            txPerda.Text = perda();

        }
        private void Cadastro_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "";

            sugestao();
            if (string.IsNullOrEmpty(form1.retornaos()) || form1.retorna() == false) { }
            else if (form1.retorna())
            {
                travatudo();
                importabanco("DB", form1.retornaos());               
                btSalvar.Text = "Atualizar";
                retorna2();
            }
        }
        private void txDesconto_Lente_TextChanged(object sender, EventArgs e)
        {
            txCusto_com_venda.Text = calcSoma(calcSoma(txCompra_lente.Text, txCompra_armacao.Text), calcSoma(txLab.Text, txCol.Text));
            decl = txDesconto_Lente.Text.Replace("R$", "");
            txVenda.Text = calcSomaeSub(txVenda_lente.Text, txDesconto_Lente.Text, txVenda_armacao.Text, txDesconto_Armacao.Text);
            txDesconto_Total.Text = calcSoma(txDesconto_Lente.Text, txDesconto_Armacao.Text);
            txLucro_lente.Text = calcSub(txVenda_lente.Text, txCompra_lente.Text, txDesconto_Lente.Text, calcSoma(txLab.Text, txCol.Text)).ToString("C");

            txGanho.Text = ganho();
            txPerda.Text = perda();

        }
        private void txDesconto_Armacao_TextChanged(object sender, EventArgs e)
        {

            txCusto_com_venda.Text = calcSoma(calcSoma(txCompra_lente.Text, txCompra_armacao.Text), calcSoma(txLab.Text, txCol.Text));
            deca = txDesconto_Armacao.Text.Replace("R$", "");
            txDesconto_Total.Text = calcSoma(txDesconto_Lente.Text, txDesconto_Armacao.Text);
            txVenda.Text = calcSomaeSub(txVenda_lente.Text, txDesconto_Lente.Text, txVenda_armacao.Text, txDesconto_Armacao.Text);
            txLucro_armacao.Text = calcSub(txVenda_armacao.Text, txCompra_armacao.Text, txDesconto_Armacao.Text, calcSoma(labs, labm)).ToString("C");

            txGanho.Text = ganho();
            txPerda.Text = perda();

        }
        private void Editar_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Somente Lente")
            {
                liga_delisgatx(1, "Armação");
                liga_delisgatx(0, "Lente");

            }
            if (comboBox1.Text == "Somente Armação")
            {
                liga_delisgatx(1, "Lente");
                liga_delisgatx(0, "Armação");
            }
            if (comboBox1.Text == "Lente + Armação")
            {
                liga_delisgatx(0, "Armação");
                liga_delisgatx(0, "Lente");
            }
            if (comboBox1.Text == "")
            {
                liga_delisgatx(0, "Armação");
                liga_delisgatx(0, "Lente");
            }
        }
        private void txLimpar_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Somente Lente")
            {
                apagatudo("Lente");

            }
            if (comboBox1.Text == "Somente Armação")
            {
                apagatudo("Armação");
            }
            if (comboBox1.Text == "Lente + Armação")
            {
                apagatudo("");

            }
        }
        private void txDesconto_Total_TextChanged(object sender, EventArgs e)
        {
            dect = txDesconto_Total.Text.Replace("R$", "");
            txGanho.Text = ganho();
            txPerda.Text = perda();
        }
        private void txCusto_com_venda_TextChanged(object sender, EventArgs e)
        {
            cdv = txCusto_com_venda.Text.Replace("R$", "");
            txGanho.Text = ganho();
            txPerda.Text = perda();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {

            if (verificarcampos() && btSalvar.Text == "Salvar")
            {
                if (cb2.Checked == false && cb1.Checked == false) { MessageBox.Show("Por favor Escolha a Loja"); }

                else
                {
                    exportabanco("DB");
                    //exportabanco("Registrosip");
                    db.exportaArmaLen(dateTimePicker1,txFornecedor_lente,txFornecedor_armacao,cb);
                    sugestao();
                    MessageBox.Show("Registro Salvo");

                }
            }
            else if (btSalvar.Text == "Atualizar")
            {

                db.atualizar("DB", Convert.ToDateTime(dateTimePicker1.Value).ToShortDateString(), txOs_Or.Text, txFornecedor_armacao.Text + " / " + txFornecedor_lente.Text,
                txCompra_lente.Text, txVenda_lente.Text, txCompra_armacao.Text, txVenda_armacao.Text, txLab.Text,txCol.Text, txCusto_com_venda.Text, txVenda.Text, cb,

                txModelo_armacao.Text, txNome_lente.Text, txFornecedor_lente.Text, txFornecedor_armacao.Text, txDesconto_Total.Text, txDesconto_Lente.Text,
                txDesconto_Armacao.Text,txMarca_armacao.Text, txMarca_lente.Text,  txObs.Text, txOs_Or.Text);

                MessageBox.Show("Atualizado");
                btSalvar.Text = "Salvar";
            }

        }
        private void txOs_Or_TextChanged(object sender, EventArgs e)
        {
            btSalvar.Text = "Salvar";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Carne tela_add_servico = new Carne();
            tela_add_servico.ShowDialog();
        }
        private void txLucro_armacao_TextChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Somente Lente")
            {
                txLucro_total.Text = txLucro_lente.Text;
                txLucro_armacao.Text = "";
            }
            else if (comboBox1.Text == "Somente Armação")
            {
                txLucro_total.Text = txLucro_armacao.Text;
                txLucro_lente.Text = "";
            }
            else
            {
                txLucro_total.Text = calcSoma(txLucro_lente.Text, txLucro_armacao.Text);
            }
        }
        private void txLucro_lente_TextChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Somente Lente")
            {
                txLucro_total.Text = txLucro_lente.Text;
            }
            else if (comboBox1.Text == "Somente Armação")
            {
                txLucro_total.Text = txLucro_armacao.Text;
            }
            else
            {
                txLucro_total.Text = calcSoma(txLucro_lente.Text, txLucro_armacao.Text);
            }
        }
        private void cb2_CheckedChanged(object sender, EventArgs e)
        {

            cb = cb2.Text;
        }
        private void cb1_CheckedChanged(object sender, EventArgs e)
        {
            cb = cb1.Text;
        }
        private void txLab_TextChanged(object sender, EventArgs e)
        {

            labs = txLab.Text.Replace("R$", "");
            txCusto_com_venda.Text = calcSoma(calcSoma(txCompra_lente.Text, txCompra_armacao.Text), calcSoma(txLab.Text, txCol.Text));
            txDesconto_Total.Text = calcSoma(txDesconto_Lente.Text, txDesconto_Armacao.Text);
            txLucro_lente.Text = calcSub(txVenda_lente.Text, txCompra_lente.Text, txDesconto_Lente.Text, calcSoma(labs, labm)).ToString("C");
            txLucro_armacao.Text = calcSub(txVenda_armacao.Text, txCompra_armacao.Text, txDesconto_Armacao.Text, calcSoma(labs, labm)).ToString("C");
            txGanho.Text = ganho();
            txPerda.Text = perda();
        }
        private void txCol_TextChanged(object sender, EventArgs e)
        {

            labm = txCol.Text.Replace("R$", "");
            txCusto_com_venda.Text = calcSoma(calcSoma(txCompra_lente.Text, txCompra_armacao.Text), calcSoma(txLab.Text, txCol.Text));
            txDesconto_Total.Text = calcSoma(txDesconto_Lente.Text, txDesconto_Armacao.Text);
            txLucro_lente.Text = calcSub(txVenda_lente.Text, txCompra_lente.Text, txDesconto_Lente.Text, calcSoma(labs, labm)).ToString("C");
            txLucro_armacao.Text = calcSub(txVenda_armacao.Text, txCompra_armacao.Text, txDesconto_Armacao.Text, calcSoma(labs, labm)).ToString("C");
            txGanho.Text = ganho();
            txPerda.Text = perda();
        }

        public void invocaform()
        {
            CadastroLente.CadastroLente tela_add_servico = new CadastroLente.CadastroLente();
            tela_add_servico.ShowDialog();
        }
        public void pesquisa(string tratamento, string tipo)
        {
            DataTable tb = db.pesquisaos("LentesValores", "Cod", txCod.Text, tratamento, tipo).Tables[0];
            try
            {
                if (tb.Rows[0][0].ToString() == txCod.Text)
                {

                    txFornecedor_lente.Text = tb.Rows[0]["Fornecedor_Lente"].ToString();
                    txMarca_lente.Text = tb.Rows[0]["Marca"].ToString();
                    txNome_lente.Text = tb.Rows[0]["Nome_Lente"].ToString();
                    txVenda_lente.Text = tb.Rows[0]["Valor_Venda"].ToString();
                    txCompra_lente.Text = tb.Rows[0]["Valor_Compra"].ToString();
                    cbTratamento.Text = tb.Rows[0]["Tratamento"].ToString();
                    txTipo.Text = tb.Rows[0]["Tipo"].ToString();
                }
            }
            catch (Exception)
            {
                DialogResult confirm = MessageBox.Show("Lente não cadastrada , Deseja cadastrar?", "Lente não encontrada", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                if (confirm.ToString().ToUpper() == "YES")
                {

                    Cod = txCod.Text;
                    Fornecedorlente = txFornecedor_lente.Text;
                    Marcalente = txMarca_lente.Text;
                    Nome = txNome_lente.Text;
                    Tratamento = cbTratamento.Text;
                    Tipo = txTipo.Text;
                    Chamado = "1";
                    Vendalente = txVenda_lente.Text;
                    invocaform();
                }
            }
            {

            }
        }
        private void BuCadastroL_Click(object sender, EventArgs e)
        {
            invocaform();
        }

        private void buCalc_Click(object sender, EventArgs e)
        {
            txCompra_armacao.Text = (Convert.ToDouble(txVenda_armacao.Text) / 3.56).ToString();
        }

        private void txCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // aqui ele reconhece que foi apertado o ENTER, isso sei que está funcionando
            {
                pesquisa("", "");
            }
        }

        private void txCompra_armacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // aqui ele reconhece que foi apertado o ENTER, isso sei que está funcionando
            {
                txCompra_armacao.Text = (Convert.ToDouble(txVenda_armacao.Text) / 3.56).ToString();
            }
        }

        private void txMarca_lente_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] TratamentoEssilor = new string[] { "SEM AR", "PROPRIO", "OPTIFOG", "C EASY", "C FORTE", "C SAPHIRE" };
            string[] TratamentoHoya = new string[] { "SEM AR", "PROPRIO", "CLEAN EXTRA", "NO-RISK", "NO-RISK +  BC", "BCONTROL/ LONGLIFE" };
            string[] TratamentoRodenstock = new string[] { "SEM AR", "PROPRIO", "SOLITARE 2", "SOL.HIDROCOAT", "SOL.DIG BLUE", "X-TRA CLEAN" };
            string[] TratamentoSynchrony = new string[] { "SEM AR", "PROPRIO", "VISIONSET", "Dv CHROME" };
            string[] TratamentoVisionset = new string[] { "SEM AR", "MAR EXTRA", "PROPRIO", "VISIONSET", "VISIONSET BLUE" };
            string[] TratamentoZeiss = new string[] { "SEM AR", "PROPRIO", "MAR EXTRA", "DV CHROME", "DV SILVER", "DV PLATINUM", "DV BLUE PROTECT" };

            if (txMarca_lente.Text == "ESSILOR")
            {
                retunrtratamento(TratamentoEssilor);

            }
            else if (txMarca_lente.Text == "HOYA")
            {
                retunrtratamento(TratamentoHoya);
            }
            else if (txMarca_lente.Text == "RODENSTOCK")
            {
                retunrtratamento(TratamentoRodenstock);
            }
            else if (txMarca_lente.Text == "VISIONSET")
            {
                retunrtratamento(TratamentoVisionset);
            }
            else if (txMarca_lente.Text == "ZEISS")
            {
                retunrtratamento(TratamentoZeiss);
            }
            else if (txMarca_lente.Text == "SYNCHRONY")
            {
                retunrtratamento(TratamentoSynchrony);
            }
        }

        private void cbTratamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            pesquisa(cbTratamento.Text, "");
            Chamado = "0";
        }

        private void txPesquisa_Cadastro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // aqui ele reconhece que foi apertado o ENTER, isso sei que está funcionando
            {
                
                //BUASCAR POR OS               
                travatudo();
                importabanco("DB", txPesquisa_Cadastro.Text);
                
                btSalvar.Text = "Atualizar";
                if (btSalvar.Text == "Atualizar")
                {
                    if (cb == "Parque (Matriz)") { cb2.Checked = true; }
                    if (cb == "Boulevard (Filial)") { cb1.Checked = true; }

                }
            }
        }

        private void txTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            pesquisa(cbTratamento.Text, txTipo.Text);
            Chamado = "0";
        }

    }
}

