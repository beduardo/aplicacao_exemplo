using System.Linq;
using aplicacao_exemplo.Dados;
using Microsoft.AspNetCore.Mvc;

namespace aplicacao_exemplo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : Controller
    {
        private readonly ContextoTreinamento _contexto;

        public ProdutosController(ContextoTreinamento contexto)
        {
            _contexto = contexto;
        }
        
        [HttpGet]
        public JsonResult Index()
        {
            var todos = _contexto.Produtos.ToList();
            return Json(todos);
        }
    }
}