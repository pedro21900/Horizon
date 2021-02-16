namespace Horizon_Contabilidade
{
    internal class TF
    {
        private string txCod;
        private string txFornecedor_lente;
        private string txMarca_lente;
        private string TxNome;
        private string TxTratamento;
        private string TxTipo;
        private int chamado;

        public string TxCod { get => txCod; set => txCod = value; }
        public string TxFornecedor_lente { get => txFornecedor_lente; set => txFornecedor_lente = value; }
        public string TxMarca_lente { get => txMarca_lente; set => txMarca_lente = value; }
        public string TxNome1 { get => TxNome; set => TxNome = value; }
        public string TxTratamento1 { get => TxTratamento; set => TxTratamento = value; }
        public string TxTipo1 { get => TxTipo; set => TxTipo = value; }
        public int Chamado { get => chamado; set => chamado = value; }
    }

}
