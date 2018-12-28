using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sales.Services;
using Sales.Models.ViewsModels;
using Sales.Models;
using Sales.Services.Exceptions;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Sales.Controllers
{using System.Threading.Tasks;

    public class SellersController : Controller
    {
        // criando dependencia com SellerService

        private readonly SellerService _SellerService;
        private readonly DepartmentService _departmentService;

        // Construtor

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _SellerService = sellerService;
            _departmentService = departmentService;
        }


        public async Task<IActionResult> Index()
        {

            // retornando a lista
            var list = await _SellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }


        //recebendo os dados para inserir
        [HttpPost]
        // segurança para evitar ataques
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Seller seller)
        {
            // se não for validado volta pra tela
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await _SellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        // tela para confirmar o delete.
        public async Task<IActionResult> Delete(int? id)
        {
            // se o objeto nao chegou
            if (id == null)
            {
                return NotFound();
                //return RedirectToAction(nameof(Error), new { message = "Id não fornecido."});
            }

            // se o que chegou nao existe
            var obj = await _SellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return NotFound();
                //return RedirectToAction(nameof(Error), new { message = "Id não existe." });
            }

            return View(obj);
        }


        // tela para confirmar o delete.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _SellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message }); }

            }

        public async Task<IActionResult> Details(int? id)
        {
            // se o objeto nao chegou
            if (id == null)
            {
                return NotFound();
                //return RedirectToAction(nameof(Error), new { message = "Id não fornecido." });
            }

            // se o que chegou nao existe
            var obj = await _SellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return NotFound();
                //return RedirectToAction(nameof(Error), new { message = "Id não existe." });
            }

            return View(obj);

        }

        public async Task<IActionResult> Edit(int? id)
        {

            // se o objeto nao chegou
            if (id == null)
            {
                return NotFound();
                //return RedirectToAction(nameof(Error), new { message = "Id não fornecido." });
            }

            // se o que chegou nao existe
            var obj = await _SellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return NotFound();
                //return RedirectToAction(nameof(Error), new { message = "Id não existe." });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }



        // tela para confirmar o delete.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            // se não for validado volta pra tela
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }


            if (id != seller.Id)
            {
                return BadRequest();
                //return RedirectToAction(nameof(Error), new { message = "Id não corresponde." });
            }

            try
            {
                await _SellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return BadRequest();
                //return RedirectToAction(nameof(Error), new { message = e.Message });

            }

        }


        // criando uma ação de erro.
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }


    }
}