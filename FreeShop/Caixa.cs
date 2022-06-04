using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeShop
{
    class Caixa
    {
        #region PROPRIEDADES

        private double _Preco = 500.55;
        public double Preco
        {
            get
            {
                return this._Preco;
            }
            private set
            {
                this._Preco = value;
            }
        }

        public double Pagamento { get; set; }
        public double Troco { get; set; }

        #endregion

        //VALOR DO DÓLAR ESTARÁ SETADO COM O VALOR ATUAL DO DIA 22/05/2022 QUE ESTÁ VALENDO R$4,88
        double dolar = 4.88;

        #region PROCESSAMENTO
        public void CalculaPagamento(double pagamento, double preco)
        {
            double copiaPagamento = pagamento;

            // CONVERTE O PAGAMENTO DE REAL PARA DÓLAR E APÓS CÁLCULA O TROCO EM DÓLAR
            Pagamento = pagamento / dolar;
            Troco = Pagamento - Preco;
            Troco = Convert.ToDouble(Troco.ToString("F2"));

            if (Troco.ToString("F2").Substring(0, 5) == "-0,00")
            {
                Troco *= (-1);
            }


            if (Troco > 0)
            {
                Console.WriteLine("Troco: $" + Troco.ToString("F2") + " dólares.");
            }
            else if (Troco < 0)
            {
                Troco *= (-1);

                double precoEmReal = this.ConversorDolarReal(Preco);

                double trocoEmReal = copiaPagamento - precoEmReal;
                trocoEmReal = Convert.ToDouble(trocoEmReal.ToString("F2"));

                if (trocoEmReal < 0)
                {
                    trocoEmReal *= -1;
                }

                Console.WriteLine("Faltam: $" + Troco.ToString("F2") + " dólares." +
                  " Ou R$" + trocoEmReal.ToString("F2") + " reais.");

            }
            else
                Console.WriteLine("Troco: $" + Troco.ToString("F2") + " dólares.");
        }

        public double ConversorDolarReal(double preco)
        {
            return preco *= dolar;
        }
        #endregion

        #region VALIDAÇÃO
        public bool IsNumeric(string valor)
        {
            double resultado;
            if (double.TryParse(valor, out resultado))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
