using System.Linq;
using System.Collections.Generic;
using APIProdutos.Models;

namespace APIProdutos.Data
{
    public static class ProdutosRepository
    {
        private static List<Produto> _DadosProdutos;

        static ProdutosRepository()
        {
            _DadosProdutos = new List<Produto>();

            Produto prod;
            for (int i = 1; i <= 100; i++)
            {
                prod = new Produto();
                prod.CodProduto = i.ToString("0000");
                prod.NomeProduto = string.Format("PRODUTO {0:0000}", i);
                prod.Preco = i / 10.0;

                _DadosProdutos.Add(prod);
            }
        }
        
        public static List<Produto> Listar()
        {
            return _DadosProdutos;
        }
    }
}