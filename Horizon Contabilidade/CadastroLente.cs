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
        private readonly Horizon_Contabilidade.TF tf = new Horizon_Contabilidade.TF();
        private static readonly Horizon_Contabilidade.Db db = new Horizon_Contabilidade.Db();
        private static readonly Horizon_Contabilidade.Cadastro cadastro = new Horizon_Contabilidade.Cadastro();
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
            string[] TratamentoEssilor = new string[] { "SEM AR", "PROPRIO", "OPTIFOG", "C EASY", "C FORTE", "C SAPHIRE" };
            string[] TratamentoHoya = new string[] { "SEM AR", "PROPRIO", "CLEAN EXTRA", "NO-RISK", "NO-RISK +  BC", "BCONTROL/ LONGLIFE" };
            string[] TratamentoRodenstock = new string[] { "SEM AR", "PROPRIO", "SOLITARE 2", "SOL.HIDROCOAT", "SOL.DIG BLUE", "X-TRA CLEAN" };
            string[] TratamentoSynchrony = new string[] { "SEM AR", "PROPRIO", "VISIONSET", "Dv CHROME" };
            string[] TratamentoVisionset = new string[] { "SEM AR", "PROPRIO", "VISIONSET", "VISIONSET BLUE" };
            string[] TratamentoZeiss = new string[] { "SEM AR", "PROPRIO", "DV CHROME", "DV SILVER", "DV PLATINUM", "DV BLUE PROTECT" };

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
        private void buSalvar_Click(object sender, EventArgs e)
        {
            db.exportalente(txCod, txName, cbFornecedor, cbMarca, cbTratamento, cbTipo, txValorCompra, txValordeVenda);
        }
        private void CadastroLente_Load(object sender, EventArgs e)
        {
            if (cadastro.tf1()[6] == "1")
            {
                txCod.Text = cadastro.tf1()[0];
                cbFornecedor.Text = cadastro.tf1()[1];
                cbMarca.Text = cadastro.tf1()[2];
                txName.Text = cadastro.tf1()[3];
                cbTratamento.Text = cadastro.tf1()[4];
                cbTipo.Text = cadastro.tf1()[5];
                txValordeVenda.Text = cadastro.tf1()[7];
            }
            string[] Fornecedor = new string[] { "TRI-LAB", "ICOPA", "LABOOTICA", "OPTIPRIME", "HOYA", "RODENSTOCK", "ZEISS BELEM", "COMPROL" };
            cbFornecedor.Items.Clear();
            cbFornecedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbFornecedor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cbFornecedor.AutoCompleteCustomSource.AddRange(Fornecedor);
            cbFornecedor.Items.AddRange(Fornecedor);
        }
        private void txCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // aqui ele reconhece que foi apertado o ENTER, isso sei que está funcionando
            {
                db.importalente(txCod, txName, cbFornecedor, cbMarca, cbTratamento, cbTipo, txValorCompra, txValordeVenda);
            }
        }
    }
}
