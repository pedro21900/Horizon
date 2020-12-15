using ClosedXML.Excel;
using System.Data;
using System.Linq;
using System.Windows;

namespace Horizon_Contabilidade
{
    class excel_manipulations
    {
        
        static invalid_character ic=  new invalid_character();
        static Db Db = new Db();
        private static string[] colummrelfat = new string[] { "Forma Pgto.", "Valor", "Desconto", "Faturado", "Cliente", "NFCe/SAT/Cupom", "Código Fatura", "DataFaturamento", "Tipo" };
        static public string[] colummxml = new string[] {"Emitente","Destinatário","N°. Nota","Emissão","Valor"};
        static public string[] columnsXml = new string[] {"A","D", "E","H","J" };
        static public string[] columnsRelfat = new string[] { "A", "B", "C","D", "F", "G", "H", "I", "J" };
        static private int qtdcolumn = 0;
        //Getters e Setters
        public string[] Colummrelfat { get => colummrelfat; set => colummrelfat = value; }
        public int Qtdcolumn { get => qtdcolumn;}

        // Lista nome de colunas e retorna em um objeto
        public string[] listNameColumns(DataSet tables)
        {
            string[] listaNameColumns = tables.Tables[0].Columns.OfType<DataColumn>().Select(x => x.ColumnName).ToArray();

            return listaNameColumns;
        }
        //Verrifica se a tabela é uma de relatorio de faturamento ou xml
        public bool check_table(string sourcetable)
        {

            //1º verifição aqui ele verifica se há a existencia das colunas
            qtdcolumn = 0;
            var wb = new XLWorkbook(sourcetable);
            var planilha = wb.Worksheet(1);
            var Cell = "A";
            int max = 0;
            string[] columns = new string[] { };
            string[] columnsLetter = new string[] { };
            var Reading = true;
            if (string.IsNullOrEmpty(planilha.Cell("B" + 2).Value.ToString())) {
                planilha.Rows(2, 2).Delete();
                wb.Save();
            }
            string lol = planilha.Cell(Cell + 1).Value.ToString();
            if (colummxml[Qtdcolumn] == planilha.Cell(Cell + 1).Value.ToString())
            {
                columns = colummxml;
                columnsLetter = columnsXml;
                max = colummxml.Length;
            }
            else if (colummrelfat[Qtdcolumn] == planilha.Cell(Cell + 1).Value.ToString())
            { 
                columns = colummrelfat;
                columnsLetter = columnsRelfat;
                max = colummrelfat.Length;
            } 
            while (Reading == true)
                {

                if (columns[qtdcolumn] == planilha.Cell(columnsLetter[qtdcolumn] + 1).Value.ToString())
                    {
                    qtdcolumn++;
                    if (qtdcolumn >= max-1) { break;}
                }
                    else
                {
                    MessageBox.Show("Erro :Layout da planilha não reconhecida" );
                    break;
                }
                }

            //2ºverificação aqui ele verifica se as tebelas retornaram a tabela parcial
            if (ic.TratarTermoComCaracteresEspeciais(string.Join(",", listNameColumns(Db.importTable())))
                == ic.TratarTermoComCaracteresEspeciais(string.Join(",", Colummrelfat)))
            {
                return true;
            }
           
            else if (ic.TratarTermoComCaracteresEspeciais(string.Join(",", listNameColumns(Db.importTable())))
                == ic.TratarTermoComCaracteresEspeciais(string.Join(",", colummxml)))
            {
               return true;
             }
            else
            {
            
                //mensagem dizendo que o arquivo não é compativél
                MessageBox.Show("Erro : Planilha não compatível");               
                return false;
            }

        }
        //Metodos gets e settes
        
        

    }
}
