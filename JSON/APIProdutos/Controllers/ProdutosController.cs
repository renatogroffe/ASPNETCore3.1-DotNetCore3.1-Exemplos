using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using APIProdutos.Models;
using APIProdutos.Data;

namespace APIProdutos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Produto> Get()
        {
            return ProdutosRepository.Listar();
        }
    }
}