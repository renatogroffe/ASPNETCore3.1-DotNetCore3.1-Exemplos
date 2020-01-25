namespace ExemploDataBreakpoints
{
    public static class Reajuste
    {
        public static void AplicarPercentual(
            Produto produto, double percentual)
        {
            produto.Preco += produto.Preco * percentual / 100;
        }
    }
}