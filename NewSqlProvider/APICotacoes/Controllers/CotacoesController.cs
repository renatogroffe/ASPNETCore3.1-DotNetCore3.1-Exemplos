using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace APICotacoes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CotacoesController : ControllerBase
    {
        [HttpGet]
        public ContentResult Get(
            [FromServices]IConfiguration config)
        {
            using var conexao = new SqlConnection(
                config.GetConnectionString("BaseCotacoes"));

            using var cmd = conexao.CreateCommand();
            cmd.CommandText =
                "SELECT Sigla " +
                      ",NomeMoeda " +
                      ",UltimaCotacao " +
                      ",ValorComercial AS 'Cotacoes.Argo.Comercial' " +
                      ",ValorTurismo AS 'Cotacoes.Argo.Turismo' " +
                "FROM dbo.Cotacoes " +
                "ORDER BY NomeMoeda " +
                "FOR JSON PATH, ROOT('Moedas')";
            
            conexao.Open();
            string valorJSON = (string)cmd.ExecuteScalar();
            conexao.Close();
            
            return Content(valorJSON, "application/json");
        }
    }
}