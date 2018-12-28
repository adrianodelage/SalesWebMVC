using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sales.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sales.Services
{
    public class DepartmentService
    {

        // acessando o context
        private readonly SalesContext _context;

        // criando um construtor
        public DepartmentService(SalesContext context)
        {
            _context = context;
        }

        // retornar uma lista com todos os vendedores do banco de dados:
        // Async e await vai melhorar a performance, pois não vai bloquear a aplicação
        // São requisições assincornas

        public async Task<List<Department>> FindAllAsync()
        {
            // ToList() -> Bloquea a aplicação, então mudamaos
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }


    }
}
