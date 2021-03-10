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
        private static DataTable DB1 = db.TableDb("Select * From Carne");
        private static int count;
        private static int ini;
        private static DataTable DB;
        private static DataTable DB17;
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
   
        public void filtroTable(string filtroData,int maisF,ComboBox comboBox2)
        {
            var teste = new DataTable().AsEnumerable();
             
            if (count == 0) { DB17 = DB12; }
            else if(count == 2) {  DB17 = DB1; }
            if (maisF == 1)
            {
                 teste = DB17.AsEnumerable().Where(DB => DB.Field<string>("Data").Contains(filtroData));
            }
            else
            {
                 teste = DB17.AsEnumerable().Where(DB => DB.Field<string>("Data").Contains(filtroData)).
                   Where(DB => DB.Field<string>("Loja").Contains(comboBox2.Text));
            }

            if (teste.Any())
            {
                DB = teste.CopyToDataTable().Copy();
            }
            else
            {
                DB = null;
            }
            // DB17.Clear();
        }
        public DataTable DatabaseMemory(DateTimePicker dptData, ComboBox comboBox2, ComboBox comboBox1)
        {
            DateTime data = dptData.Value;
            if (comboBox1.Text == "Mês / Ano" && comboBox2.Text != "Todas as Lojas")
            {
                filtroTable(data.Month.ToString() + "/" + data.Year.ToString(), 2, comboBox2);
            }
            else if (comboBox1.Text == "Dia" & comboBox2.Text != "Todas as Lojas")
            {
                filtroTable(dptData.Value.ToString("d"), 2, comboBox2);
            }
            else if (comboBox1.Text == "Mês / Ano")

            {
                filtroTable(data.Month.ToString() + "/" + data.Year.ToString(), 1, comboBox2);                                     
            }
            else if (comboBox1.Text == "Dia")
            {
                filtroTable(dptData.Value.ToString("d"), 1, comboBox2);
            }
            else if (comboBox1.Text == "Ano" & comboBox2.Text != "Todas as Lojas")
            {
                filtroTable(data.Year.ToString(), 2, comboBox2);                
            }
            else
            {
                filtroTable(data.Year.ToString(), 1, comboBox2);
            }
            if (DB == null) { 
                DB = DB17.Clone(); 
            }
            return DB;
        }
        public DataSet Database(DateTimePicker dptData, ComboBox comboBox2, ComboBox comboBox1) {
            DataSet dataSet =new DataSet();
            dataSet.Tables.Add(DatabaseMemory( dptData,  comboBox2,  comboBox1).Copy());
            count = 2;
            
            dataSet.Tables.Add(DatabaseMemory(dptData, comboBox2, comboBox1).Copy());
            count = 0;
            
            return dataSet;
        }
    }
}
