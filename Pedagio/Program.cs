using System;

namespace Pedagio
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("PROGRAMA PARA USUÁRIOS FREQUÊNTES DE PEDÁGIO!");

            Entidades entidade = new Entidades();
            Console.Write("Proprietário: ");
            entidade.Proprietario = Console.ReadLine();
            Console.Write("Veículo: ");
            entidade.Veiculo = Console.ReadLine();
            Console.Write("Placa: ");
            entidade.Placa = Console.ReadLine();

            entidade.Gravar(entidade);

        }
    }
}
