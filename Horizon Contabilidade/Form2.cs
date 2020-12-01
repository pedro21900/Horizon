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


        }
        static Form1 form1 = new Form1();
        static Calculo calculo = new Calculo();
         Db db = new Db();
        static string labs = "0";
        static string labm = "0";
        static string decl = "0";
        static string deca = "0";
        static string dect = "0";
        static string cdv = "0";
        static string fdp1 = "0";
        static string cb = "0";
        public string perda()
        {
            string porcento = calculo.perda(calcSoma(txDesconto_Total.Text, txCusto_com_venda.Text),
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
        static string sDBstr = Properties.Settings.Default.Pastainicial;
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
            txFornecedor_armacao.AutoCompleteCustomSource = Caixadesusgestaoos("Fornecedor_Armação", "Registrosip");
            txMarca_armacao.AutoCompleteCustomSource = Caixadesusgestaoos("Marca_armação", "Registrosip");
            txModelo_armacao.AutoCompleteCustomSource = Caixadesusgestaoos("Modelo_Armação", "Registrosip");
            txFornecedor_lente.AutoCompleteCustomSource = Caixadesusgestaoos("Fornecedor_Lente", "Registrosip");
            txMarca_lente.AutoCompleteCustomSource = Caixadesusgestaoos("Marca_lente", "Registrosip");
            txNome_lente.AutoCompleteCustomSource = Caixadesusgestaoos("Nome_Lente", "Registrosip");
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
                txLab_montagem.Text = "";
                txLab_Surf.Text = "";

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
            txLab_montagem.Enabled = false;
            txLab_Surf.Enabled = false;
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
                txLab_montagem.Enabled = true;
                txLab_Surf.Enabled = true;

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
                    txCusto_com_venda.Text = "";

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
                    txLab_montagem.Enabled = true;
                    txLab_Surf.Enabled = true;

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
                txCompra_lente.Text, txVenda_lente.Text, txCompra_armacao.Text, txVenda_armacao.Text, txLab_Surf.Text, txLab_montagem.Text, txCusto_com_venda.Text, txVenda.Text, "0", cb);
                }
                else if (tabela == "Registrosip")
                {
                    db.addlinhalayout2(tabela, txOs_Or.Text, txModelo_armacao.Text, txNome_lente.Text, txLucro_armacao.Text,
                       txLucro_lente.Text, txFornecedor_lente.Text, txFornecedor_armacao.Text, txLucro_total.Text, txDesconto_Total.Text
                        , txTxcartao.Text, cbForma_de_Pagamento.Text, txDesconto_Lente.Text,
                      txDesconto_Armacao.Text, "0", "0", "0", txMarca_armacao.Text, txMarca_lente.Text, "0", cb, txObs.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
        }
        public void importabanco(string tabela, string pesquisa, int ntabela)
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
                        else if (indexx == "Obs") { txObs.Text = index; }
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
                        else if (indexx == "Obs") { txObs.Text = index; }
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
                        else if (indexx == "Marca_armação") {txMarca_armacao.Text = index; }
                        else if (indexx == "Obs") { txObs.Text = index; }
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

                if (String.IsNullOrEmpty(txFornecedor_lente.Text))
                {
                    MessageBox.Show(x1 + "fornecedor de lente " + x2, "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    vari++;
                }
                if (String.IsNullOrEmpty(txCompra_lente.Text))
                {

                    MessageBox.Show("Campo valor de compra da lente vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }
                if (String.IsNullOrEmpty(txVenda_lente.Text))
                {

                    MessageBox.Show("Campo valor de venda da lente vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }


                if (String.IsNullOrEmpty(txLab_Surf.Text))
                {
                    var result = MessageBox.Show("Campo valor de sufaçagem vazio , deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        labs = "0";
                    }
                    else if (result == DialogResult.No)
                    {

                        vari++;
                    }
                }

                if (String.IsNullOrEmpty(txLab_montagem.Text))
                {
                    MessageBox.Show("Campo valor de montagem vazio", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }

                if (String.IsNullOrEmpty(txNome_lente.Text))
                {
                    MessageBox.Show("Campo nome da lente vazio", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }

                if (String.IsNullOrEmpty(txDesconto_Lente.Text))
                {
                    var result = MessageBox.Show("Campo valor desconto lente vazio , deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        decl = "0";
                    }
                    else if (result == DialogResult.No)
                    {
                        vari++;
                    }
                    if (String.IsNullOrEmpty(cbForma_de_Pagamento.Text))
                    {
                        MessageBox.Show("Favor escolha a foma de pagamento ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        vari++;
                    }
                    if (String.IsNullOrEmpty(cbForma_de_Pagamento.Text) && String.IsNullOrEmpty(cbForma_de_Pagamento1.Text))
                    {

                        MessageBox.Show("Favor escolha a foma de pagamento ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        vari++;
                    }
                    if (String.IsNullOrEmpty(cbForma_de_Pagamento.Text))
                    {
                        MessageBox.Show("Favor escolha a foma de pagamento ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (String.IsNullOrEmpty(txFornecedor_armacao.Text))
                {

                    MessageBox.Show(x1 + "fornecedor de armação " + x2, "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }


                if (String.IsNullOrEmpty(txCompra_armacao.Text))
                {
                    MessageBox.Show("Campo valor de venda da armação vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }

                if (String.IsNullOrEmpty(txVenda_armacao.Text))
                {
                    MessageBox.Show("Campo valor de venda da armação vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }



                if (String.IsNullOrEmpty(txLab_montagem.Text))
                {
                    var result = MessageBox.Show("Campo valor de montagem vazio , deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        labm = "0";
                    }
                    else if (result == DialogResult.No)
                    {
                        vari++;
                    }

                }



                if (String.IsNullOrEmpty(txModelo_armacao.Text))
                {
                    MessageBox.Show("Campo modelo da armação vazio ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }

                if (String.IsNullOrEmpty(txDesconto_Armacao.Text))
                {
                    var result = MessageBox.Show("Campo valor desconto armação vazio , deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                if (String.IsNullOrEmpty(txFornecedor_armacao.Text))
                {

                    MessageBox.Show(x1 + "fornecedor de armação " + x2, "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }
                if (String.IsNullOrEmpty(txFornecedor_lente.Text))
                {
                    MessageBox.Show(x1 + "fornecedor de lente " + x2, "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }
                if (String.IsNullOrEmpty(txCompra_lente.Text))
                {

                    MessageBox.Show("Campo valor de compra da lente vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }
                if (String.IsNullOrEmpty(txVenda_lente.Text))
                {

                    MessageBox.Show("Campo valor de venda da lente vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }
                if (String.IsNullOrEmpty(txCompra_armacao.Text))
                {
                    MessageBox.Show("Campo valor de venda da armação vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }

                if (String.IsNullOrEmpty(txVenda_armacao.Text))
                {
                    MessageBox.Show("Campo valor de venda da armação vazio, impossivel calcular lucro ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }

                if (String.IsNullOrEmpty(txLab_Surf.Text))
                {
                    var result = MessageBox.Show("Campo valor de sufaçagem vazio , deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        labs = "0";
                    }
                    else if (result == DialogResult.No)
                    {
                        vari++;
                    }
                }

                if (String.IsNullOrEmpty(txLab_montagem.Text))
                {
                    var result = MessageBox.Show("Campo valor de montagem vazio , deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        labm = "0";
                    }
                    else if (result == DialogResult.No)
                    {
                        vari++;
                    }

                }



                if (String.IsNullOrEmpty(txModelo_armacao.Text))
                {
                    MessageBox.Show("Campo modelo da mrmação vazio ", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }

                if (String.IsNullOrEmpty(txNome_lente.Text))
                {
                    MessageBox.Show("Campo nome da lente vazio", "Salvar Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    vari++;
                }



                if (String.IsNullOrEmpty(txDesconto_Lente.Text))
                {
                    var result = MessageBox.Show("Campo valor desconto da lente vazio, deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        decl = "0";
                    }
                    else if (result == DialogResult.No)
                    {
                        vari++;
                    }

                }


                if (String.IsNullOrEmpty(txDesconto_Armacao.Text))
                {
                    var result = MessageBox.Show("Campo valor desconto da armação vazio, deseja continuar ?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

            txVenda.Text = calcSomaeSub(txVenda_lente.Text, txDesconto_Lente.Text, txVenda_armacao.Text, txDesconto_Armacao.Text);
            txLucro_lente.Text = calcSub(txVenda_lente.Text, txCompra_lente.Text, txDesconto_Lente.Text, txCusto_com_venda.Text).ToString("C");

            txGanho.Text = ganho();
            txPerda.Text = perda();

        }

        private void txVenda_armacao_TextChanged(object sender, EventArgs e)
        {
            txVenda.Text = calcSomaeSub(txVenda_lente.Text, txDesconto_Lente.Text, txVenda_armacao.Text, txDesconto_Armacao.Text);
            txDesconto_Total.Text = calcSoma(txDesconto_Lente.Text, txDesconto_Armacao.Text);
            txVenda.Text = calcSomaeSub(txVenda_lente.Text, txDesconto_Lente.Text, txVenda_armacao.Text, txDesconto_Armacao.Text);
            txLucro_armacao.Text = calcSub(txVenda_armacao.Text, txCompra_armacao.Text, txDesconto_Armacao.Text, "0").ToString("C");

            txGanho.Text = ganho();
            txPerda.Text = perda();
        }

        private void txCompra_Lente_TextChanged(object sender, EventArgs e)
        {
            txVenda.Text = calcSomaeSub(txVenda_lente.Text, txDesconto_Lente.Text, txVenda_armacao.Text, txDesconto_Armacao.Text);
            txDesconto_Total.Text = calcSoma(txDesconto_Lente.Text, txDesconto_Armacao.Text);
            txLucro_lente.Text = calcSub(txVenda_lente.Text, txCompra_lente.Text, txDesconto_Lente.Text, txCusto_com_venda.Text).ToString("C");

            txGanho.Text = ganho();
            txPerda.Text = perda(); ;
        }

        private void txLab_Surf_TextChanged(object sender, EventArgs e)
        {
            labs = txLab_Surf.Text.Replace("R$", "");
            txCusto_com_venda.Text = calcSoma(txLab_Surf.Text, txLab_montagem.Text);
            txLucro_lente.Text = calcSub(txVenda_lente.Text, txCompra_lente.Text, txDesconto_Lente.Text, txCusto_com_venda.Text).ToString("C");
            txDesconto_Total.Text = calcSoma(txDesconto_Lente.Text, txDesconto_Armacao.Text);
            txGanho.Text = ganho();
            txPerda.Text = perda();
        }

        private void txLab_montagem_TextChanged(object sender, EventArgs e)
        {
            labm = txLab_montagem.Text.Replace("R$", "");
            txCusto_com_venda.Text = calcSoma(txLab_Surf.Text, txLab_montagem.Text);
            txLucro_lente.Text = calcSub(txVenda_lente.Text, txCompra_lente.Text, txDesconto_Lente.Text, txCusto_com_venda.Text).ToString("C");
            txDesconto_Total.Text = calcSoma(txDesconto_Lente.Text, txDesconto_Armacao.Text);
            txGanho.Text = ganho();
            txPerda.Text = perda();

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
            txDesconto_Total.Text = calcSoma(txDesconto_Lente.Text, txDesconto_Armacao.Text);
            txVenda.Text = calcSomaeSub(txVenda_lente.Text, txDesconto_Lente.Text, txVenda_armacao.Text, txDesconto_Armacao.Text);
            txLucro_armacao.Text = calcSub(txVenda_armacao.Text, txCompra_armacao.Text, txDesconto_Armacao.Text, "0").ToString("C");

            txGanho.Text = ganho();
            txPerda.Text = perda();

        }

        private void Cadastro_Load(object sender, EventArgs e)
        {

            sugestao();
            if (string.IsNullOrEmpty(form1.retornaos()) || form1.retorna() == false) { }
            else if (form1.retorna())
            {
                travatudo();
                importabanco("DB", form1.retornaos(), 1);
                importabanco("Registrosip", form1.retornaos(), 2);
                btSalvar.Text = "Atualizar";
                retorna2();
            }
        }

        private void txDesconto_Lente_TextChanged(object sender, EventArgs e)
        {
            decl = txDesconto_Lente.Text.Replace("R$", "");
            txVenda.Text = calcSomaeSub(txVenda_lente.Text, txDesconto_Lente.Text, txVenda_armacao.Text, txDesconto_Armacao.Text);
            txDesconto_Total.Text = calcSoma(txDesconto_Lente.Text, txDesconto_Armacao.Text);
            txLucro_lente.Text = calcSub(txVenda_lente.Text, txCompra_lente.Text, txDesconto_Lente.Text, txCusto_com_venda.Text).ToString("C");

            txGanho.Text = ganho();
            txPerda.Text = perda();

        }

        private void txDesconto_Armacao_TextChanged(object sender, EventArgs e)
        {
            deca = txDesconto_Armacao.Text.Replace("R$", "");
            txDesconto_Total.Text = calcSoma(txDesconto_Lente.Text, txDesconto_Armacao.Text);
            txVenda.Text = calcSomaeSub(txVenda_lente.Text, txDesconto_Lente.Text, txVenda_armacao.Text, txDesconto_Armacao.Text);
            txLucro_armacao.Text = calcSub(txVenda_armacao.Text, txCompra_armacao.Text, txDesconto_Armacao.Text, "0").ToString("C");

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

        private void cbForma_de_Pagamento1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fdp1 = cbForma_de_Pagamento1.Text.Replace("R$", "");
            txGanho.Text = ganho();
            txPerda.Text = perda();
        }

        private void btPesquisar_Click(object sender, EventArgs e)
        {
            //BUASCAR POR OS
            btSalvar.Text = "Atualizar";
            travatudo();
            importabanco("DB", txPesquisa_Cadastro.Text, 1);
            importabanco("Registrosip", txPesquisa_Cadastro.Text, 2);
           if (btSalvar.Text == "Atualizar"){ 
            if (cb== "Parque (Matriz)") { cb2.Checked = true; }
                if (cb == "Boulevard (Filial)") { cb1.Checked = true; }

            }

        }

        private void btSalvar_Click(object sender, EventArgs e)
        {

            if (verificarcampos() && btSalvar.Text == "Salvar")
            {
                if (cb2.Checked == false && cb1.Checked == false) { MessageBox.Show("Por favor Escolha a Loja"); }

                else
                {
                    exportabanco("DB");
                    exportabanco("Registrosip");                    
                    sugestao();
                    MessageBox.Show("Registro Salvo");
                    
                }
            }
            else if (btSalvar.Text == "Atualizar")
            {

                db.atualizar("DB", "Registrosip", Convert.ToDateTime(dateTimePicker1.Value).ToShortDateString(),
                    txOs_Or.Text, txFornecedor_armacao.Text + " / " + txFornecedor_lente.Text,
                txCompra_lente.Text, txVenda_lente.Text, txCompra_armacao.Text, txVenda_armacao.Text, txLab_Surf.Text,
                txLab_montagem.Text, txCusto_com_venda.Text, txVenda.Text, "0", cb,
                 
                txModelo_armacao.Text, txNome_lente.Text, txLucro_armacao.Text,txLucro_lente.Text,
                txFornecedor_lente.Text, txFornecedor_armacao.Text, txLucro_total.Text, txDesconto_Total.Text
                 ,txTxcartao.Text, cbForma_de_Pagamento.Text, txDesconto_Lente.Text, txDesconto_Armacao.Text, "0", "0", "0", 
                txMarca_armacao.Text, txMarca_lente.Text, "0", txObs.Text, txOs_Or.Text);
                
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
            Form3 tela_add_servico = new Form3();
            tela_add_servico.ShowDialog();
        }

        private void txLucro_armacao_TextChanged(object sender, EventArgs e)
        {
            txLucro_total.Text = calcSoma(txLucro_lente.Text, txLucro_armacao.Text);
        }

        private void txLucro_lente_TextChanged(object sender, EventArgs e)
        {
            txLucro_total.Text = calcSoma(txLucro_lente.Text, txLucro_armacao.Text);
        }

        private void cb2_CheckedChanged(object sender, EventArgs e)
        {

            cb = cb2.Text;
        }

        private void cb1_CheckedChanged(object sender, EventArgs e)
        {
            cb = cb1.Text;
        }

    }
}

