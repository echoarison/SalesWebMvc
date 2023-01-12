using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Service;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using System.Collections.Generic;
using SalesWebMvc.Service.Exceptions;

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

        public IActionResult Delete(int? id) //id é opcional
        {
            //aqui eu sei que foi feita uma solicitação fora dos parametros
            if (id == null)
            {
                return NotFound();
            }

            //pegando o obj
            var obj = _sellerService.FindById(id.Value);    //tem que usar o value, pois o id é opcional

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);  //deletando

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            //aqui eu sei que foi feita uma solicitação fora dos parametros
            if (id == null)
            {
                return NotFound();
            }

            //pegando o obj
            var obj = _sellerService.FindById(id.Value);    //tem que usar o value, pois o id é opcional

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)  //id opicional 
        {
            //vendo se é null
            if (id == null) 
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);

            //buscando obj
            if (obj == null) 
            {
                return NotFound();
            }

            List<Department> departments = _departmentService.FindAll(); //pegando tudo e jogando na lista

            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments }; //guardando tudo e enviando para view

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller) 
        {
            //testando se é diferente
            if (id != seller.Id)
            {
                return BadRequest();
            }

            //tentando executar o update e se não dá certo tratando Exception
            try
            {
                _sellerService.Update(seller);

                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException) 
            {
                return BadRequest();
            }
        }

    }
}
