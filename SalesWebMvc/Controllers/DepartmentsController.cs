using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using System.Collections.Generic;

namespace SalesWebMvc.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            //instanciando uma lista de Departments

            List<Department> list = new List<Department>();

            //add na list
            list.Add(new Department { Id = 1, Name = "Eletronics"});
            list.Add(new Department { Id = 2, Name = "Fashion"});

            //para enviar essa lista só colocar no method view
            return View(list);
        }
    }
}
