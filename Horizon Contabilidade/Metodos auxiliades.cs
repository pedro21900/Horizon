using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon_Contabilidade
{
    class Metodos_auxiliades
    {
        Calculo calculo = new Calculo();
        Db db = new Db();
        Form1 form1 = new Form1();

        public void calcforn()
        {
            int o = form1.db1("DB").Rows.Count;
            for (int i=0;i<= o-1; i++)
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
