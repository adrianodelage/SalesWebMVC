using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sales.Models;
using Microsoft.EntityFrameworkCore;
using Sales.Services.Exceptions;

namespace Sales.Services
{
    public class SellerService
    {

        // acessando o context

        private readonly SalesContext _context;

        // criando um construtor

        public SellerService(SalesContext context)
        {
            _context = context;
        }

        // retornar uma lista com todos os vendedores do banco de dados:
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }


        // metodo para inserir os dados na base

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);

            // confirmando a inserção
           await _context.SaveChangesAsync();
        }


        // para apagar
        // retorna o vendedor que tem aquele Id
        public async Task<Seller> FindByIdAsync(int id)
        {

            // fazendo um Join com Seller e Departamento.
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                //removendo o objeto do DdSET
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Seller obj)
        {
            // Any -> verificar se existe algum vendedor com  om aquele ID
            if (! await _context.Seller.AnyAsync(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id não encontrado");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException (e.Message);

            }

        }

    }
}
