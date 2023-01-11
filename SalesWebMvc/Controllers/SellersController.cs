using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Service;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //criando uma dependecia do SellerService, DepartmentService
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        //construtor
        public SellersController(SellerService sellerService, DepartmentService departmentService) 
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
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
            //carregando os departments
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };

            return View(viewModel); //quando entrar na pagina create já vai ta carregado os department
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
