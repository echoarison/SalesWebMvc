using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        //Atributos
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }   //esse Id tem que existe, pois não pode ficar null
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();  //instanciando a ICollection

        //construtor
        public Seller()
        {

        }

        //construtor with arguments
        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        //methods
        public void AddSales(SalesRecord sr) 
        {
            Sales.Add(sr);  //add na list ICollection
        }

        public void RemoveSales(SalesRecord sr) 
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime inicial, DateTime final) 
        {
            return Sales.Where(sr => sr.Date >= inicial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
