using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketHub.Contracts.Setor;
using TicketHub.Exceptions;
using TicketHub.Interfaces.IServices;

namespace TicketHub.Controllers
{
    public class SetorController : Controller
    {
        private readonly ILogger<SetorController> _logger;
        private readonly ISetorService _setorService;

        public SetorController(ILogger<SetorController> logger, ISetorService setorService)
        {
            _logger = logger;
            _setorService = setorService;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var setores = await _setorService.GetSetores();
            return View(setores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SetorRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                await _setorService.CreateSetor(dto);

                TempData["Sucesso"] = "Setor Criado com Sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (DuplicateResourceException ex)
            {
                ModelState.AddModelError("Nome", ex.Message);
                return View(dto);
            }


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var setor = await _setorService.GetSetorById(id);

            if(setor == null) return NotFound();


            var dto = new SetorUpdateDTO(setor.Nome, setor.Descricao);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SetorUpdateDTO dto)
        {
            if(ModelState.IsValid)
            {
                var result = await _setorService.UpdateSetor(id, dto);
                TempData["Sucesso"] = "Setor Atualizado com Sucesso!";

                return RedirectToAction(nameof(Index));
            }

            return View(dto);

        }
        public async Task<IActionResult> Delete(int id)
        {
            var setor = await _setorService.GetSetorById(id);

            if (setor == null)
            {
                return NotFound();
            }

            return View(setor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConf(int id)
        {
            try
            {
                await _setorService.Delete(id);
                TempData["Sucesso"] = "Setor Deletado com Sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            if(string.IsNullOrWhiteSpace(searchString)) return RedirectToAction(nameof(Index));

            var results = await _setorService.SearchByName(searchString);

            ViewData["CurrentFilter"] = searchString;

            return View("Index", results);
        }


    }
}