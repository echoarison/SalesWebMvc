using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        //Atributos
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]  //estou pedindo required do campo name
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")] //{0} -> pegando o nome do atributo
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]  //Display é um DataAnnotations que serve para evitar o uso dos names dos atributos na tabela, exemplo.
        [DataType(DataType.Date)]   //DataType é a formatação dos dados de como vai se demostardo o dados
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(1300.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }

        [Display(Name = "Department")]
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
