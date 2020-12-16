using System;
using System.Windows.Forms;


namespace Horizon_Contabilidade
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }

            catch (Exception ex)
            {
 
                MessageBox.Show("Erro :" + ex.Message);



                //DialogResult drResult = ofd1.ShowDialog();

               // if (drResult == System.Windows.Forms.DialogResult.OK)
                //{

                    //sDBstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
                    ////sDBstr += ofd1.FileName;
                    //Properties.Settings.Default.CaminhoDb = ofd1.FileName;
                    //Properties.Settings.Default.Save();


               // }
            }
        }
    }
}
