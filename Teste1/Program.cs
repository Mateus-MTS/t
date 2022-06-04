using System;

namespace Teste1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Despesa = ");
            string x = Console.ReadLine();
            Console.Write("Pagamento = ");
            string y = Console.ReadLine();

            Caixa caixa = new Caixa();

            // PASSA POR UM TESTE E VERIFICA SE O VALOR É NUMÉRICO.
            if ((caixa.IsNumeric(x)) == true && (caixa.IsNumeric(y)) == true)
            {
                int despesa = int.Parse(x);
                int pagamento = int.Parse(y);
                caixa.CalculaPagamento(pagamento, despesa);
            }
            else
                Console.WriteLine("O Valor inserido não é um número!");

        }
    }
}
