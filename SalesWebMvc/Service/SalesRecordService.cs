using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Service
{
    public class SalesRecordService
    {
        //criando uma dependecia do _context
        private readonly SalesWebMvcContext _context;

        //construtor
        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecords>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //construindo um objeto de consulta
            var result = from obj in _context.SalesRecords select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            //Fazendo um inner join com os outros objetos ou entities e ainda com Async
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

        }

        //nessa busca o resultado vai seruma List de IGroup
        public async Task<List<IGrouping<Department,SalesRecords>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            //construindo um objeto de consulta
            var result = from obj in _context.SalesRecords select obj;


            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            //Fazendo um inner join com os outros objetos ou entities e ainda com Async
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();

        }
    }
}
