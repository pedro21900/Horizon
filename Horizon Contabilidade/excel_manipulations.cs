using ClosedXML.Excel;
using System.Data;
using System.Linq;
using System.Windows;

namespace Horizon_Contabilidade
{
    class excel_manipulations
    {
        
        static invalid_character ic=  new invalid_character();
        static Db1 Db1 = new Db1();
        private static string[] colummrelfat = new string[] { "Forma Pgto.", "Valor", "Desconto", "Faturado", "Cliente", "NFCe/SAT/Cupom", "Código Fatura", "DataFaturamento", "Tipo" };
        static public string[] colummxml = new string[] {"Emitente","Destinatário","N°. Nota","Emissão","Valor"};
        static public string[] columnsXml = new string[] {"B","E", "F","I","K" };
        static public string[] columnsRelfat = new string[] { "B", "C", "D","E", "G", "H", "I", "J", "K" };
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
            var linha = 1;
            int max = 0;
            string[] columns = new string[] { };
            string[] columnsLetter = new string[] { };
            var Reading = true;
            if (colummxml[Qtdcolumn] == planilha.Cell("B" + linha.ToString()).Value.ToString())
            {
                columns = colummxml;
                columnsLetter = columnsXml;
                max = colummxml.Length;
            }
            else if (colummrelfat[Qtdcolumn] == planilha.Cell("B" + linha.ToString()).Value.ToString())
            { 
                columns = colummrelfat;
                columnsLetter = columnsRelfat;
                max = colummrelfat.Length;
            } 
            while (Reading == true)
                {

                    if (columns[qtdcolumn] == planilha.Cell(columnsLetter[qtdcolumn] + linha.ToString()).Value.ToString())
                    {
                    
                    qtdcolumn++;
                    if (qtdcolumn >= max-1) { break;}
                }
                    else
                {
                    break;
                }
                }
            //2ºverificação aqui ele verifica se as tebelas retornaram a tabela parcial
            if (ic.TratarTermoComCaracteresEspeciais(string.Join(",", listNameColumns(Db1.importTable())))
                == ic.TratarTermoComCaracteresEspeciais(string.Join(",", Colummrelfat)))
            {
                return true;
            }
           
            else if (ic.TratarTermoComCaracteresEspeciais(string.Join(",", listNameColumns(Db1.importTable())))
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
