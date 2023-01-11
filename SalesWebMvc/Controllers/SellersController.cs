using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Service;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //criando uma dependecia do SellerService
        private readonly SellerService _sellerService;

        //construtor
        public SellersController(SellerService sellerService) 
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            //uma variavel que vai receber a lista de sellers
            var list = _sellerService.FindAll();

            return View(list);
        }
    }
}
