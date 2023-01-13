using SalesWebMvc.Models.Enums;
using System;

namespace SalesWebMvc.Models
{
    public class SalesRecords
    {
        //atributos
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public SaleStatus Status { get; set; }
        public Seller Seller { get; set; }

        //Construtor
        public SalesRecords() 
        {

        }

        //Construtor com arguments
        public SalesRecords(int id, DateTime date, double amount, SaleStatus status, Seller seller) 
        {

            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;

        }

        public string FormatedDate()
        {
            return Date.ToString("dd/MM/yyyy");
        }

        public string FormatedAmout(string coin)
        {
            var fixedAmout = this.Amount.ToString("F2");
            return string.Format("{0} {1}", coin, fixedAmout);
        }
    }
}
