using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Service
{
    public class SellerService
    {
        //criando uma dependecia do _context
        private readonly SalesWebMvcContext _context;

        //construtor
        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //retornando toda lista de sellers
        public List<Seller> FindAll() 
        {
            //Isso é uma operação sicrona que é uma operação aonde ela fica bloqueada esperando a operaçã finalizar 
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            //pegando o primeiro department(assosiando ao seller)
            //obj.Department = _context.Department.First();

            //add no bancoDatas
            _context.Add(obj);

            //confirmando
            _context.SaveChanges(); //salvando as mudanças no banco
        }
    }
}
