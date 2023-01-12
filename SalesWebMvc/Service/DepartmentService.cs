using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;    //para usa o Async

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

        /*public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList(); //ordenado por nome
        }*/

        //Função Async
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync(); //usando a word awair para avisar o compilador que é uma chamada Async
        }
    }
}
