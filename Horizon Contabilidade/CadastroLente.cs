using System;
using System.Windows.Forms;

namespace CadastroLente
{
    public partial class CadastroLente : Form
    {
        public CadastroLente()
        {
            InitializeComponent();
        }

        static Horizon_Contabilidade.Db db = new Horizon_Contabilidade.Db();
        public void retunrtratamento(string[] Tratamentos)
        {
            cbTratamento.Items.Clear();
            cbTratamento.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbTratamento.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cbTratamento.AutoCompleteCustomSource.AddRange(Tratamentos);
            cbTratamento.Items.AddRange(Tratamentos);
        }
        private void cbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] TratamentoEssilor = new string[] { "SEM AR", "OPTIFOG", "C EASY", "C FORTE", "C SAPHIRE" };
            string[] TratamentoHoya = new string[] { "SEM AR", "CLEAN EXTRA", "NO-RISK", "NO-RISK +  BC", "BCONTROL/ LONGLIFE" };
            string[] TratamentoRodenstock = new string[] { "SEM AR", "SOLITARE 2", "SOL.HIDROCOAT", "SOL.DIG BLUE", "X-TRA CLEAN" };
            string[] TratamentoSynchrony = new string[] { "SEM AR", "VISIONSET", "Dv CHROME" };
            string[] TratamentoVisionset = new string[] { "SEM AR", "VISIONSET", "VISIONSET BLUE" };
            string[] TratamentoZeiss = new string[] { "SEM AR", "DV CHROME", "DV SILVER", "DV PLATINUM", "DV BLUE PROTECT" };

            if (cbMarca.Text == "ESSILOR")
            {
                retunrtratamento(TratamentoEssilor);

            }
            else if (cbMarca.Text == "HOYA")
            {
                retunrtratamento(TratamentoHoya);
            }
            else if (cbMarca.Text == "RODENSTOCK")
            {
                retunrtratamento(TratamentoRodenstock);
            }
            else if (cbMarca.Text == "VISIONSET")
            {
                retunrtratamento(TratamentoVisionset);
            }
            else if (cbMarca.Text == "ZEISS")
            {
                retunrtratamento(TratamentoZeiss);
            }
            else if (cbMarca.Text == "SYNCHRONY")
            {
                retunrtratamento(TratamentoSynchrony);
            }
        }

        private void buPesquisa_Click(object sender, EventArgs e)
        {
            db.importalente(txCod,txName,cbFornecedor,cbMarca,cbTratamento,txTipo,txValorCompra,txValordeVenda);       
        }

        private void buSalvar_Click(object sender, EventArgs e)
        {
            db.exportalente(txCod, txName, cbFornecedor, cbMarca, cbTratamento, txTipo, txValorCompra, txValordeVenda);
        }

        private void buSalvar_Enter(object sender, EventArgs e)
        {
            db.exportalente(txCod, txName, cbFornecedor, cbMarca, cbTratamento, txTipo, txValorCompra, txValordeVenda);
        }
    

        private void CadastroLente_Load(object sender, EventArgs e)
        {
            string[] Fornecedor = new string[] { "TRI-LAB", "ICOPA", "LABOOTICA", "OPTIPRIME", "HOYA","RODENSTOCK","ZEISS BELEM","COMPROL" };
            cbFornecedor.Items.Clear();
            cbFornecedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbFornecedor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cbFornecedor.AutoCompleteCustomSource.AddRange(Fornecedor);
            cbFornecedor.Items.AddRange(Fornecedor);            
        }
    }
}
