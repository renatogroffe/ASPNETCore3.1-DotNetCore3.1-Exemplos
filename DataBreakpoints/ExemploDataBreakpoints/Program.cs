using System;

namespace ExemploDataBreakpoints
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Instanciando produto...");
            var prod = new Produto();
            prod.Codigo = "00001";
            prod.Nome = "TESTE PRODUTO 1";
            prod.Preco = 100;

            Console.WriteLine("Alterando preço...");
            prod.Preco += 1;

            Console.WriteLine("Atribuindo mesmo valor...");
            prod.Preco = prod.Preco;

            Console.WriteLine("Aplicando percentual...");
            Reajuste.AplicarPercentual(prod, 10);
        }
    }
}