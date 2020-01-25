namespace APIProdutos.Models
{
    public class Produto
    {
        public string CodProduto { get; set; }
        public string NomeProduto { get; set; }
        public double Preco { get; set; }
        public double? Teste1 { get; set; }
        public double? Teste2 { get; set; }
        public double? Teste3 { get; set; }
        public double? Teste4 { get; set; }
        
        public bool TesteSomenteLeitura
        {
            get => true;
        }
    }
}