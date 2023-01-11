using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Service
{
    public class DepartmentService
    {
        //criando uma dependecia do _context
        private readonly SalesWebMvcContext _context;

        //construtor
        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList(); //ordenado por nome
        }
    }
}
