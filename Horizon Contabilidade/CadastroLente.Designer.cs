
namespace CadastroLente
{
    partial class CadastroLente
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buSalvar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txValorCompra = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txCod = new System.Windows.Forms.TextBox();
            this.cbTratamento = new System.Windows.Forms.ComboBox();
            this.txTipo = new System.Windows.Forms.ComboBox();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.buPesquisa = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txValordeVenda = new System.Windows.Forms.TextBox();
            this.cbFornecedor = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txName
            // 
            this.txName.Location = new System.Drawing.Point(225, 59);
            this.txName.Name = "txName";
            this.txName.Size = new System.Drawing.Size(260, 20);
            this.txName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(318, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome da Lente";
            // 
            // buSalvar
            // 
            this.buSalvar.Location = new System.Drawing.Point(277, 185);
            this.buSalvar.Name = "buSalvar";
            this.buSalvar.Size = new System.Drawing.Size(75, 23);
            this.buSalvar.TabIndex = 2;
            this.buSalvar.Text = "Salvar";
            this.buSalvar.UseVisualStyleBackColor = true;
            this.buSalvar.Click += new System.EventHandler(this.buSalvar_Click);
            this.buSalvar.Enter += new System.EventHandler(this.buSalvar_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Fornecedor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Marca";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(522, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tratamento";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Tipo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(336, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Valor de Compra";
            // 
            // txValorCompra
            // 
            this.txValorCompra.Location = new System.Drawing.Point(330, 134);
            this.txValorCompra.Name = "txValorCompra";
            this.txValorCompra.Size = new System.Drawing.Size(100, 20);
            this.txValorCompra.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Código";
            // 
            // txCod
            // 
            this.txCod.Location = new System.Drawing.Point(12, 59);
            this.txCod.Name = "txCod";
            this.txCod.Size = new System.Drawing.Size(54, 20);
            this.txCod.TabIndex = 13;
            // 
            // cbTratamento
            // 
            this.cbTratamento.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbTratamento.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTratamento.FormattingEnabled = true;
            this.cbTratamento.Location = new System.Drawing.Point(491, 59);
            this.cbTratamento.Name = "cbTratamento";
            this.cbTratamento.Size = new System.Drawing.Size(121, 21);
            this.cbTratamento.TabIndex = 15;
            // 
            // txTipo
            // 
            this.txTipo.FormattingEnabled = true;
            this.txTipo.Items.AddRange(new object[] {
            "COMUM",
            "2° PAR"});
            this.txTipo.Location = new System.Drawing.Point(217, 133);
            this.txTipo.Name = "txTipo";
            this.txTipo.Size = new System.Drawing.Size(66, 21);
            this.txTipo.TabIndex = 16;
            this.txTipo.Text = "COMUM";
            // 
            // cbMarca
            // 
            this.cbMarca.FormattingEnabled = true;
            this.cbMarca.Items.AddRange(new object[] {
            "ESSILOR",
            "HOYA",
            "RODENSTOCK",
            "SYNCHRONY",
            "VISIONSET",
            "ZEISS"});
            this.cbMarca.Location = new System.Drawing.Point(98, 58);
            this.cbMarca.Name = "cbMarca";
            this.cbMarca.Size = new System.Drawing.Size(121, 21);
            this.cbMarca.TabIndex = 17;
            this.cbMarca.SelectedIndexChanged += new System.EventHandler(this.cbMarca_SelectedIndexChanged);
            // 
            // buPesquisa
            // 
            this.buPesquisa.Location = new System.Drawing.Point(66, 58);
            this.buPesquisa.Name = "buPesquisa";
            this.buPesquisa.Size = new System.Drawing.Size(18, 23);
            this.buPesquisa.TabIndex = 18;
            this.buPesquisa.UseVisualStyleBackColor = true;
            this.buPesquisa.Click += new System.EventHandler(this.buPesquisa_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(472, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Valor de Venda";
            // 
            // txValordeVenda
            // 
            this.txValordeVenda.Location = new System.Drawing.Point(458, 134);
            this.txValordeVenda.Name = "txValordeVenda";
            this.txValordeVenda.Size = new System.Drawing.Size(100, 20);
            this.txValordeVenda.TabIndex = 19;
            // 
            // cbFornecedor
            // 
            this.cbFornecedor.FormattingEnabled = true;
            this.cbFornecedor.Items.AddRange(new object[] {
            "ESSILOR",
            "HOYA",
            "RODENSTOCK",
            "SYNCHRONY",
            "VISIONSET",
            "ZEISS"});
            this.cbFornecedor.Location = new System.Drawing.Point(77, 133);
            this.cbFornecedor.Name = "cbFornecedor";
            this.cbFornecedor.Size = new System.Drawing.Size(121, 21);
            this.cbFornecedor.TabIndex = 21;
            // 
            // CadastroLente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 234);
            this.Controls.Add(this.cbFornecedor);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txValordeVenda);
            this.Controls.Add(this.buPesquisa);
            this.Controls.Add(this.cbMarca);
            this.Controls.Add(this.txTipo);
            this.Controls.Add(this.cbTratamento);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txCod);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txValorCompra);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buSalvar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txName);
            this.Name = "CadastroLente";
            this.Text = "Cadastro de Lente";
            this.Load += new System.EventHandler(this.CadastroLente_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buSalvar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txValorCompra;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txCod;
        private System.Windows.Forms.ComboBox cbTratamento;
        private System.Windows.Forms.ComboBox txTipo;
        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Button buPesquisa;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txValordeVenda;
        private System.Windows.Forms.ComboBox cbFornecedor;
    }
}

