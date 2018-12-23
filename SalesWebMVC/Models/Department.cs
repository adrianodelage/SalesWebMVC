using System;
using System.Collections.Generic;
using System.Linq;
namespace SalesWebMVC.Models
{
    public class Department
    {

        public int ID{ get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department()
        {

        }

        public Department(int iD, string name)
        {
            ID = iD;
            Name = name;
        }


        // adicionar um vendedor

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        //total de vendas do departamento

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }


    }
}
