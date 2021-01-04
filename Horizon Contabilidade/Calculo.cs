﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Horizon_Contabilidade
{
    class Calculo
    {
        public double perda(string div1, string div2, string divs)
        {
            double div12 = 1;
            double div22 = 1;
            double divs2 = 1;
            double porcento = 0;

            if (div1 == "" || string.IsNullOrEmpty(div1)) { div12 = 0; }
            if (div2 == "" || string.IsNullOrEmpty(div2)) { div22 = 0; }
            if (divs == "" || string.IsNullOrEmpty(divs)) { divs2 = 0; }
            if (div1.Contains("-") || div2.Contains("-") || divs.Contains("-"))
            {

                if (div12 == 1) { div1 = div1.Replace("-", ""); }
                if (div22 == 1) { div2 = div2.Replace("-", ""); }
                if (divs2 == 1) { divs = divs.Replace("-", ""); }
            }
            if (div12 == 1) { div12 = Convert.ToDouble(div1.Replace("R$ ", "")); }
            if (div22 == 1) { div22 = Convert.ToDouble(div2.Replace("R$ ", "")); }
            if (divs2 == 1) { divs2 = Convert.ToDouble(divs.Replace("R$ ", "")); }


            if ((div12 + div22) < 0) { porcento = 0; }
            else
            {
                double calc1 = div12 + div22;

                porcento = ((calc1 / divs2));
            }
            return porcento;

        }
        public double ganho(string div1, string divs)
        {
            double div12 = 1;

            double divs2 = 1;
            double porcento = 0;

            if (div1 == "" || string.IsNullOrEmpty(div1)) { div12 = 0; }

            if (divs == "" || string.IsNullOrEmpty(divs)) { divs2 = 0; }

            if (div1.Contains("-") || divs.Contains("-"))
            {

                // if (div12 == 1) { div1 = div1.Replace("-", ""); }

                if (divs2 == 1) { divs = divs.Replace("-", ""); }
            }
            if (div12 == 1) { div12 = Convert.ToDouble(div1.Replace("R$ ", "")); }

            if (divs2 == 1) { divs2 = Convert.ToDouble(divs.Replace("R$ ", "")); }

            if ((div12) < 0) { porcento = 0; }
            else
            {
                porcento = ((div12 / divs2));
            }
            return porcento;

            /*    if (txLucro_total.Text.Contains("-"))
                {
                    calc1 = Convert.ToDouble(txLucro_total.Text.Replace("-R$", ""));
                    txGanho.Enabled = false;
                    porcento = "";
                }
                else
                {
                    calc1 = Convert.ToDouble(txLucro_total.Text.Replace("R$", ""));
                    calc2 = Convert.ToDouble(calcSoma(txVenda_armacao, txVenda_lente).Replace("R$", ""));
                    porcento = ((calc1 / calc2) * 100).ToString("F") + "%";
                }


                return porcento;*/
        }
        public double calcSub(string txVenda_lente, string txCompra_Lente, string txDesconto, string txCusto_com_venda)
        {
            double txVenda_lente1 = 1;
            double txCompra_Lente1 = 1;
            double txDesconto1 = 1;
            double txCusto_com_venda1 = 1;
            double txLucro_lente = 0;
 
                if (txVenda_lente == "" || string.IsNullOrEmpty(txVenda_lente)) { txVenda_lente1 = 0; }
                if (txCompra_Lente == "" || string.IsNullOrEmpty(txCompra_Lente)) { txCompra_Lente1 = 0; }
                if (txDesconto == "" || string.IsNullOrEmpty(txDesconto)) { txDesconto1 = 0; }
                if (txCusto_com_venda == "" || string.IsNullOrEmpty(txCusto_com_venda)) { txCusto_com_venda1 = 0; }

                if (txVenda_lente1 == 1) { txVenda_lente1 = Convert.ToDouble(txVenda_lente.Replace("R$ ", "")); }
                if (txCompra_Lente1 == 1) { txCompra_Lente1 = Convert.ToDouble(txCompra_Lente.Replace("R$ ", "")); }
                if (txDesconto1 == 1) { txDesconto1 = Convert.ToDouble(txDesconto.Replace("R$ ", "")); }
                if (txCusto_com_venda1 == 1) { txCusto_com_venda1 = Convert.ToDouble(txCusto_com_venda.Replace("R$ ", "")); }

                txLucro_lente = (txVenda_lente1 - txCompra_Lente1 - txDesconto1 - txCusto_com_venda1);
            
            return txLucro_lente;
        }
        public string calcSoma(string tx1, string tx2)
        {
            string txVenda = "0";
            double tx12 = 1;
            double tx22 = 1;

            if (tx1 == "" || string.IsNullOrEmpty(tx1)) { tx12 = 0; }
            if (tx2 == "" || string.IsNullOrEmpty(tx2)) { tx22 = 0; }

            if (tx12 == 1) { tx12 = Convert.ToDouble(tx1.Replace("R$ ", "")); }
            if (tx22 == 1) { tx22 = Convert.ToDouble(tx2.Replace("R$ ", "")); }

            txVenda = (tx12 + tx22).ToString("C");

            return txVenda;
        }
        public string calcSomaeSub(string txVenda_lente, string txDesconto_Lente, string txVenda_armacao, string txDesconto_Armacao)
        {
            string retorno = "0";
            double txVenda_lente1 = 1;
            double txDesconto_Lente1 = 1;
            double txVenda_armacao1 = 1;
            double txDesconto_Armacao1 = 1;

            if (txVenda_lente == "" || string.IsNullOrEmpty(txVenda_lente)) { txVenda_lente1 = 0; }
            if (txDesconto_Lente == "" || string.IsNullOrEmpty(txDesconto_Lente)) { txDesconto_Lente1 = 0; }
            if (txVenda_armacao == "" || string.IsNullOrEmpty(txVenda_armacao)) { txVenda_armacao1 = 0; }
            if (txDesconto_Armacao == "" || string.IsNullOrEmpty(txDesconto_Armacao)) { txDesconto_Armacao1 = 0; }

            if (txVenda_lente1 == 1) { txVenda_lente1 = Convert.ToDouble(txVenda_lente.Replace("R$ ", "")); }
            if (txDesconto_Lente1 == 1) { txDesconto_Lente1 = Convert.ToDouble(txDesconto_Lente.Replace("R$ ", "")); }
            if (txVenda_armacao1 == 1) { txVenda_armacao1 = Convert.ToDouble(txVenda_armacao.Replace("R$ ", "")); }
            if (txDesconto_Armacao1 == 1) { txDesconto_Armacao1 = Convert.ToDouble(txDesconto_Armacao.Replace("R$ ", "")); }
            retorno = (txVenda_lente1 - txDesconto_Lente1 + txVenda_armacao1 - txDesconto_Armacao1).ToString("C");


            return retorno;
        }
        
        public double carne()
        {
            per
            double index = 0;
            try
            {

                int count1 = Carne.Tables[0].Columns.Count;
                int qtdlinha = Carne.Tables[0].Rows.Count;

                for (int i = 0; i <= qtdlinha - 1; i++)
                {
                    index += Convert.ToDouble(Carne.Tables[0].Rows[i]["Valor"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
            return index;
        }
        public double carnelastmonth()
        {
            double index = 0;
            try
            {

                int count1 = Carne.Tables[0].Columns.Count;
                int qtdlinha = Carne.Tables[0].Rows.Count;

                for (int i = 0; i <= qtdlinha - 1; i++)
                {
                    DateTime datasale = Convert.ToDateTime(Carne.Tables[0].Rows[i]["Data_de_Venda"].ToString());
                    DateTime datafat = Convert.ToDateTime(Carne.Tables[0].Rows[i]["Data"].ToString());
                    DateTime data = dptData.Value;
                    if (comboBox1.Text == "Mês / Ano" && datafat.Month == data.Month && datasale.Month < data.Month && datafat.Year.ToString() == data.Year.ToString())
                    {
                        index += Convert.ToDouble(Carne.Tables[0].Rows[i]["Valor"].ToString());

                    }
                    else if (comboBox1.Text == "Ano" && datafat.Year == data.Year && datasale.Year < data.Year)
                    {
                        index += Convert.ToDouble(Carne.Tables[0].Rows[i]["Valor"].ToString());

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);


            }
            return index;
        }
        public double liquido(string tabela, string coluna)
        {
            double index = 0;
            try
            {
                DataTable oDs = db1(tabela);
                int qtdlinha = oDs.Rows.Count;

                for (int i = 0; i <= qtdlinha - 1; i++)
                {
                    if (String.IsNullOrEmpty(oDs.Rows[i][coluna].ToString())) { }

                    else
                    {
                        double index2 = Convert.ToDouble(oDs.Rows[i][coluna].ToString());
                        index += index2;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);
            }
            return index;
        }
    }
}
