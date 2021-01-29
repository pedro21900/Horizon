using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Horizon_Contabilidade
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private static readonly string sDBstr = Properties.Settings.Default.Folder_Way;
        private static string cb;
        public AutoCompleteStringCollection Caixadesusgestaoos(string coluna, string DB)
        {
            Form1 form1 = new Form1();
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

        public void bloqueiatudo()
        {

            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            txQdtdias.Enabled = false;
            txValor.Enabled = false;

        }
        public void liberatudo()
        {
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
            txQdtdias.Enabled = true;
            txValor.Enabled = true;
        }

        public void exportabanco(string tabela)
        {
            Form1 form1 = new Form1();
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

                //Preencher o datarow com valores

                oDR["Or_os"] = txOs_Or.Text;
                oDR["Data_de_venda"] = dateTimePicker1.Text;
                oDR["Data"] = dateTimePicker2.Text;
                oDR["qtddias"] = txQdtdias.Text;
                oDR["Valor"] = txValor.Text.Replace("R$", "");
                oDR["Loja"] = cb;


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
        public void importabanco(string tabela, string pesquisa)
        {
            Form1 form1 = new Form1();
            try
            {


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


                    if (indexx == "Or_os") { txOs_Or.Text = index; }
                    else if (indexx == "Data_de_venda") { dateTimePicker1.Text = index; }
                    else if (indexx == "Data_de_faturamento") { dateTimePicker2.Text = index; }
                    else if (indexx == "qtddias") { txQdtdias.Text = index; }
                    else if (indexx == "Valor") { txValor.Text = index; }



                }


                //Incluir um datarow ao dataset
                //oDs.Tables[tabela].Rows.Add(oDR);
                //Usar o objeto Command Bulder para gerar o Comandop Insert
                // OleDbCommandBuilder oCB = new OleDbCommandBuilder(oDA);
                //Atualizar o BD com valores do Dataset
                oDA.Update(oDs, tabela);
                //liberar o data adapter , o dataset , o comandbuilder e a conexao
                oDA.Dispose(); oDs.Dispose(); //oCB.Dispose();
                oCn.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            TimeSpan date = Convert.ToDateTime(dateTimePicker2.Value) - Convert.ToDateTime(dateTimePicker1.Value);
            txQdtdias.Text = date.Days.ToString();

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            txOs_Or.AutoCompleteCustomSource = Caixadesusgestaoos("Or_os", "Carne");
            // txOs_Or.AutoCompleteCustomSource += Caixadesusgestaoos("Or_os", "DB");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cb) == false)
            {
                exportabanco("Carne");
            }
            else
            {
                MessageBox.Show("Loja não selecionada");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            importabanco("Carne", txOs_Or.Text);
            TimeSpan date = Convert.ToDateTime(dateTimePicker2.Value) - Convert.ToDateTime(dateTimePicker1.Value);
            txQdtdias.Text = date.Days.ToString();
            bloqueiatudo();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan date = Convert.ToDateTime(dateTimePicker2.Value) - Convert.ToDateTime(dateTimePicker1.Value);
            txQdtdias.Text = date.Days.ToString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan date = Convert.ToDateTime(dateTimePicker2.Value) - Convert.ToDateTime(dateTimePicker1.Value);
            txQdtdias.Text = date.Days.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            liberatudo();
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
