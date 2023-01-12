using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore; //necessario para poder usar o include(), pois seria um innerjoin
using SalesWebMvc.Service.Exceptions;
using System.Threading.Tasks;

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

        //retornando toda lista de sellers no modelo Async
        public async Task<List<Seller>> FindAllAsync() 
        {
            //Isso é uma operação sicrona que é uma operação aonde ela fica bloqueada esperando a operaçã finalizar 
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)    //no lugar od void vc coloca o Task do Asyc
        {
            //pegando o primeiro department(assosiando ao seller)
            //obj.Department = _context.Department.First();

            //add no bancoDatas
            _context.Add(obj);

            //confirmando
            await _context.SaveChangesAsync(); //salvando as mudanças no banco
        }

        public async Task<Seller> FindByIdAsync(int id) 
        {
            //buscando o id do seller
            //return _context.Seller.FirstOrDefault(obj => obj.Id == id); //só o seller
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id); //aqui seria um innerjoin da tabela seller and department

        }

        public async Task RemoveAsync(int id) 
        {
            //pegando a exeception Db
            try
            {
                var obj = await _context.Seller.FindAsync(id); //encontrar o seller
                _context.Seller.Remove(obj);    //removi Db o obj seller

                //confirmando e salvando
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) 
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id); //fica melhor e mais certinho Async

            if (!hasAny)  //se não existe alguma coisa  no banco de dados
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);   // atualizando obj no Db
                await _context.SaveChangesAsync();

            }
            catch(DbUpdateConcurrencyException e)   //exception do update db
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
