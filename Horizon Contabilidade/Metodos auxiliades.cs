namespace Horizon_Contabilidade
{
    internal class Metodos_auxiliades
    {
        private readonly Calculo calculo = new Calculo();
        private readonly Db db = new Db();
        private readonly Form1 form1 = new Form1();

        public void calcforn()
        {
            int o = form1.db1("DB").Rows.Count;
            for (int i = 0; i <= o - 1; i++)
            {
                double valor = calculo.Recalc(form1.db1("DB").Rows[i]["Compra_da_lente"].ToString(), form1.db1("DB").Rows[i]["Venda_da_lente"].ToString(), form1.db1("DB").Rows[i]["Fornecedor"].ToString());
                if (valor == 0) { }
                else
                {
                    db.registrocadlentes(form1.db1("DB").Rows[i]["Or_os"].ToString(), valor);
                }

            }
        }
    }
}
