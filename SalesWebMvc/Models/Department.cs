using System.Collections.Generic;
using System;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Department
    {
        //atributos
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); //instanciando o iCollection

        //Construtor
        public Department()
        {

        }

        //Construtor with arguments
        public Department(int id, string name) 
        {

            Id = id;
            Name = name;

        }

        //methods
        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime inicial, DateTime final) 
        {
            return Sellers.Sum(seller => seller.TotalSales(inicial, final));
        }

    }
}
