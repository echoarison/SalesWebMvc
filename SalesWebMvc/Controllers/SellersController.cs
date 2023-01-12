﻿using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Service;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using System.Collections.Generic;
using SalesWebMvc.Service.Exceptions;
using System.Diagnostics;

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
            if (!ModelState.IsValid) 
            {
                return View(seller);
            }

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
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});  //redirecionando para Action Error,dentro Redirect tem a pagina e o parametro pra ela com um obj anonimo
            }

            //pegando o obj
            var obj = _sellerService.FindById(id.Value);    //tem que usar o value, pois o id é opcional

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });  //redirecionando para Action Error,dentro Redirect tem a pagina e o parametro pra ela com um obj anonimo
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
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });  //redirecionando para Action Error,dentro Redirect tem a pagina e o parametro pra ela com um obj anonimo
            }

            //pegando o obj
            var obj = _sellerService.FindById(id.Value);    //tem que usar o value, pois o id é opcional

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });  //redirecionando para Action Error,dentro Redirect tem a pagina e o parametro pra ela com um obj anonimo
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)  //id opicional 
        {
            //vendo se é null
            if (id == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });  //redirecionando para Action Error,dentro Redirect tem a pagina e o parametro pra ela com um obj anonimo
            }

            var obj = _sellerService.FindById(id.Value);

            //buscando obj
            if (obj == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });  //redirecionando para Action Error,dentro Redirect tem a pagina e o parametro pra ela com um obj anonimo
            }

            List<Department> departments = _departmentService.FindAll(); //pegando tudo e jogando na lista

            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments }; //guardando tudo e enviando para view

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller) 
        {
            //verificando se o objeto foi validado
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();

                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

                return View(viewModel);
            }

            //testando se é diferente
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });  //redirecionando para Action Error,dentro Redirect tem a pagina e o parametro pra ela com um obj anonimo
            }

            //tentando executar o update e se não dá certo tratando Exception
            try
            {
                _sellerService.Update(seller);

                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });  //redirecionando para Action Error,dentro Redirect tem a pagina e o parametro pra ela com um obj anonimo
            }
            catch (DbConcurrencyException e) 
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });  //redirecionando para Action Error,dentro Redirect tem a pagina e o parametro pra ela com um obj anonimo
            }
        }

        //criando um erro personalizado
        public IActionResult Error(string message) 
        {
            //aqui esta criando um novo ErrorViewModel
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier //aqui na primeira interrogação é o id opicional e na segunda é a verificação de ser null
            };

            return View(viewModel);

        }

    }
}
