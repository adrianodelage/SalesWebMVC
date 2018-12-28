using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace Sales.Models
{
    public class Seller
    {
        public int Id { get; set; }

        // campo OBRIGATORIO
        [Required(ErrorMessage = "{0} Campo obrigatorio.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage ="Entre 03 e 60")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatorio.")]
        [EmailAddress(ErrorMessage = "Email invalido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatorio.")]
        [Display (Name = "Data aux")]
        // Formatando como se fosse data apenas sem hora
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} Campo obrigatorio.")]
        [Range(100.0,50000.0, ErrorMessage ="{0} Aceito apenas de {1} a {2}")]
        [Display(Name = "Salario")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }


        public Department Department { get; set; }
        public int DepartmentId { get; set; }

        // um Vendedor pode ter várias vendas
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        // adicinoar vendedas
        public void AddSales(SalesRecord sr) {
            Sales.Add(sr);
        }


        // adicinoar vendedor
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }


        public double TotalSales(DateTime initial, DateTime final)
        {
            // Soma das vendas.
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }


    }
}
