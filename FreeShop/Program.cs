using System;

namespace FreeShop
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             ALGORITMO CONVERTE VALOR PAGO DE REAIS PARA DÓLAR E RETORNA TROCO EM DÓLAR.

             ESTE ALGORITMO FOI DESENVOLVIDO PENSANDO EM UMA COMPRA DE UM PRODUTO QUE O VALOR ESTÁ SETADO COMO 500,55 DÓLARES
            */

            Caixa produto = new Caixa();

            Console.WriteLine("Preço do produto = $" + produto.Preco.ToString() + " dólares.");
            double preco = produto.ConversorDolarReal(produto.Preco);
            Console.WriteLine("Preço do produto em reais: R$" + preco.ToString("F2"));
            Console.WriteLine("---------*******---------");

            Console.Write("Pagamento = R$ ");
            string x = Console.ReadLine();
            

            // PASSA POR UM TESTE DE VALIDAÇÃO E VERIFICA SE O VALOR É NUMÉRICO.
            if ((produto.IsNumeric(x)) == true)
            {
                produto.Pagamento = double.Parse(x);
                produto.CalculaPagamento(produto.Pagamento, produto.Preco);
            }
            else
                Console.WriteLine("O Valor inserido não é um número!");
        }
    }
}
