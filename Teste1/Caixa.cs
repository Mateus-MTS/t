using System;
using System.Collections.Generic;

namespace Teste1
{
    class Caixa
    {
        #region PROPRIEDADES

        public int Despesa { get; private set; }
        public int Pagamento { get; set; }
        public int Troco { get; set; }

        #endregion

        #region PROCESSAMENTO
        public void CalculaPagamento(int pagamento, int despesa)
        {

            Pagamento = pagamento;
            Despesa = despesa;

            Troco = Pagamento - Despesa;

            if (Troco > 0)
            {
                Console.WriteLine("Troco: R$" + Troco.ToString() + " reais.");
                Devolver(Troco);
            }
            else if (Troco < 0)
            {
                Troco *= (-1);
                Console.WriteLine("Faltam: R$" + Troco.ToString() + " reais.");
            }
            else
                Console.WriteLine("Troco: R$" + Troco.ToString() + " reais.");

        }

        static void Devolver(int Troco)
        {

            int[] div = new int[8] { 200, 100, 50, 20, 10, 5, 2, 1 };
            int[] mod = new int[8] { 200, 100, 50, 20, 10, 5, 2, 1 };

            int[] divCopia = new int[div.Length];
            // Copiando os elementos com o método CopyTo:
            div.CopyTo(divCopia, 0);

            int nota = Troco;
            string comando = "";
            for (int i = 0; i < div.Length; i++)
            {

                div[i] = nota / div[i];
                mod[i] = nota % divCopia[i];
                nota = mod[i];

                if (div[i] != 0)
                {

                    if (divCopia[i] == 1)
                    {
                        comando += string.Concat(div[i] + " moeda de R$" + divCopia[i]);
                    }
                    else
                        comando += string.Concat(div[i] + " nota de R$" + divCopia[i] +", ");
                }
            }
            Console.WriteLine(string.Concat("Resultado algoritmo: (" + comando + ")."));

            #region COMENTÁRIO ----LÓGICA DO CÁLCULO----
            /* Lógica do cálculo abaixo para saber que notas deverão ser entregues
               foi feito 2 cálculos um que mostra o dividendo e outro com 
               o resto da divisão se o valor do dividendo der inteiros e diferente de 0 é a quantidade de notas que deverá ser entregue.
             
            int n200, n100, n50, n20, n10, n5, n2, n1;
            int r200, r100, r50, r20, r10, r5, r2, r1;

            200
            n200 = Troco / 200;
            r200 = Troco % 200;

            //100
            n100 = r200 / 100;
            r100 = r200 % 100;

            //50
            n50 = r100 / 50;
            r50 = r100 % 50;

            //20
            n20 = r50 / 20;
            r20 = r50 % 20;

            //10
            n10 = r20 / 10;
            r10 = r20 % 10;

            //5
            n5 = r10 / 5;
            r5 = r10 % 5;

            //2
            n2 = r5 / 2;
            r2 = r5 % 2;

            //1
            n1 = r2 / 1;
            r1 = r2 % 1;

            Console.WriteLine("Cédulas de 200 " + n200.ToString());
            Console.WriteLine("Cédulas de 100 " + n100.ToString());
            Console.WriteLine("Cédulas de 50 " + n50.ToString());
            Console.WriteLine("Cédulas de 20 " + n20.ToString());
            Console.WriteLine("Cédulas de 10 " + n10.ToString());
            Console.WriteLine("Cédulas de 5 " + n5.ToString());
            Console.WriteLine("Cédulas de 2 " + n2.ToString());
            Console.WriteLine("Moeda de 1 " + n1.ToString());
            */
            #endregion
        }

        #endregion

        #region VALIDAÇÃO
        public bool IsNumeric(string valor)
        {
            int resultado;
            if (int.TryParse(valor, out resultado))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
