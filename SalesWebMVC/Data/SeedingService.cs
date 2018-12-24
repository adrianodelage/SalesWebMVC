using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sales.Models;

namespace Sales.Data
{
    public class SeedingService
    {
        private SalesContext _context;

        public SeedingService(SalesContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            /// testando se existe dados na tabela
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
            {
                return;
            }

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Eletronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            Seller s1 = new Seller(1, "Adriano", "aa@hotmail.com", new DateTime(1983, 03, 17), 1300, d1);
            Seller s2 = new Seller(2, "Adrianodfdf", "aa@hotmail.com", new DateTime(1983, 03, 17), 1300, d2);
            Seller s3 = new Seller(3, "Adriandfdfdfo", "aa@hotmail.com", new DateTime(1983, 03, 17), 1300, d3);
            Seller s4 = new Seller(4, "Adriandfdfdo", "aa@hotmail.com", new DateTime(1983, 03, 17), 1300, d4);


            SalesRecord sr1 = new SalesRecord(1, new DateTime(2018,05,10), 1111, Models.Enums.SalesStatus.Billed, s1 );
            SalesRecord sr2 = new SalesRecord(2, new DateTime(2018, 05, 10), 1111, Models.Enums.SalesStatus.Billed, s2);
            SalesRecord sr3 = new SalesRecord(3, new DateTime(2018, 05, 10), 1111, Models.Enums.SalesStatus.Billed, s3);

            _context.Department.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3, s4);
            _context.SalesRecord.AddRange(sr1, sr2, sr3);

            _context.SaveChanges();    


        }

    }
}
