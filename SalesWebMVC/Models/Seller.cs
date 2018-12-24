using System;
using System.Collections.Generic;
using System.Linq;

namespace Sales.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public string Email { get; set; }
        public DateTime Birthlarya { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }

        // um Vendedor pode ter várias vendas
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthlarya, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            Birthlarya = birthlarya;
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
