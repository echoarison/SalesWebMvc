using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public Seller FindById(int id) 
        {
            //buscando o id do seller
            //return _context.Seller.FirstOrDefault(obj => obj.Id == id); //só o seller
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id); //aqui seria um innerjoin da tabela seller and department

        }

        public void Remove(int id) 
        {
            var obj = _context.Seller.Find(id); //encontrar o seller
            _context.Seller.Remove(obj);    //removi Db o obj seller

            //confirmando e salvando
            _context.SaveChanges();

        }
    }
}
