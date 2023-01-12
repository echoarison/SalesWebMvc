using System;

namespace SalesWebMvc.Service.Exceptions
    {//herdando do application exeption
    public class NotFoundException : ApplicationException
    {
        //exeception personalizada
        public NotFoundException(string message) : base(message) 
        {

        }
    }
}
