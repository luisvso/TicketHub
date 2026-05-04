using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using TicketHub.Contracts.Prioridade;
using TicketHub.Contracts.Setor;
using TicketHub.Exceptions;
using TicketHub.Interfaces.IServices;

namespace TicketHub.Controllers
{
    public class PrioridadeController : Controller
    {

        private readonly IPrioridadeService _service;
        private readonly ILogger<PrioridadeController> _logger;

        public PrioridadeController(ILogger<PrioridadeController> logger, IPrioridadeService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var todasPrioridades = await _service.GetAllPrioridades();
            return View(todasPrioridades);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrioridadeRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                await _service.CreatePrioridade(dto);
                TempData["Sucesso"] = "Prioridade Criada com Sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Nome", ex.Message);
                return View(dto);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var prioridade = await _service.GetPrioridadeById(id);

            if(prioridade == null) return NotFound();

            var dto = new PrioridadeUpdateDTO(prioridade.Nome, prioridade.HorasEstimadas);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PrioridadeUpdateDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.UpdatePrioridade(id, dto);

                TempData["Sucesso"] = "Prioridade Editada com Sucesso";

                return RedirectToAction(nameof(Index));
            }

            return View(dto);

        }

        public async Task<IActionResult> Delete(int id)
        {
            var prioridade = await _service.GetPrioridadeById(id);

            if(prioridade == null) return NotFound();

            return View(prioridade);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConf(int id)
        {
            try
            {
                await _service.DeletePrioridade(id);
                TempData["Sucesso"] = "Prioridade Deletada com Sucesso!";
            }catch(Exception ex)
            {
                TempData["Erro"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}