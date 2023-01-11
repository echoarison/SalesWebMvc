using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Service;
using SalesWebMvc.Models;

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

        //method GET como default
        public IActionResult Create() 
        {
            return View();
        }

        //colocando uma anotação do method POST
        [HttpPost]
        [ValidateAntiForgeryToken]  //e evitando ataque csrf(aproveita sua sessão e envia dados maliciosos)
        public IActionResult Create(Seller seller) 
        {
            _sellerService.Insert(seller); //salvando no banco

            //redirecionado
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));
        }
    }
}
