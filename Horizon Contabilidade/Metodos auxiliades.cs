using FastMember;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Horizon_Contabilidade
{
    internal class Metodos_auxiliades
    {
        private readonly Calculo calculo = new Calculo();
        private static readonly Db db = new Db();
        private static DataTable DB12 = db.TableDb("Select * From DB");
        private static DataTable DB;
        //private readonly Form1 form1 = new Form1();

        public void calcforn()
        {
          //  int o = form1.db1("DB").Rows.Count;
          //  for (int i = 0; i <= o - 1; i++)
          //  {
             //   double valor = calculo.Recalc(form1.db1("DB").Rows[i]["Compra_da_Lente"].ToString(), form1.db1("DB").Rows[i]["Venda_da_Lente"].ToString(), form1.db1("DB").Rows[i]["Fornecedor"].ToString());
              //  if (valor == 0) { }
            //    else
             //   {
            //        db.registrocadlentes(form1.db1("DB").Rows[i]["Or_os"].ToString(), valor);
             //   }

          //  }
        }
        public  void DatabaseMemory(DateTimePicker dptData,ComboBox comboBox2, ComboBox comboBox1)
        {
            //DB.Reset();
            DateTime data = dptData.Value;
           // var xc="";
            var xc2 = "";
            
            if (comboBox1.Text == "Mês / Ano" && comboBox2.Text != "Todas as Lojas") 
            {
               
                DB = DB12.AsEnumerable().Where(DB => DB.Field<string>("Data").Contains(data.Month.ToString() + "/" + data.Year.ToString())).
                    Where(DB => DB.Field<string>("Loja").Contains(comboBox2.Text)).CopyToDataTable().Copy();
                

            }
            else if (comboBox1.Text == "Dia" & comboBox2.Text != "Todas as Lojas") 
            {
                DB = DB12.AsEnumerable().Where(DB => DB.Field<string>("Data").Contains(dptData.Value.ToString("d"))).
                   Where(DB => DB.Field<string>("Loja").Contains(comboBox2.Text)).CopyToDataTable().Copy();
            }
            else if (comboBox1.Text == "Mês / Ano") 
            
            {
               
              //  DB = DB12.AsEnumerable().Where(DB => DB.Field<string>("Data").Contains(data.Month.ToString() + "/" + data.Year.ToString())).
               //     CopyToDataTable().Copy();
            }
            else if (comboBox1.Text == "Dia")
            {
                //DB = DB12.AsEnumerable().Where(DB => DB.Field<string>("Data").Contains(dptData.Value.ToString("d"))).
                  //   CopyToDataTable().Copy();
            }
            else if (comboBox1.Text == "Ano" & comboBox2.Text != "Todas as Lojas") 
            {
                DB = DB12.AsEnumerable().Where(DB => DB.Field<string>("Data").Contains(data.Year.ToString())).
                   Where(DB => DB.Field<string>("Loja").Contains(comboBox2.Text)).CopyToDataTable().Copy();
            }
            else 
            {
                DB = DB12.AsEnumerable().Where(DB => DB.Field<string>("Data").Contains(data.Year.ToString())).
                    CopyToDataTable().Copy();

            }

            int x = 0;
            

           
        }
    }
}
