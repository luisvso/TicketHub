using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketHub.Contracts.Chamado;
using TicketHub.Infrastructure.Data;
using TicketHub.Interfaces.IServices;
using TicketHub.Models.Enums;

namespace TicketHub.Controllers
{
    public class ChamadoController : Controller
    {
        private readonly ILogger<ChamadoController> _logger;
        private readonly IChamadoService _service;
        private readonly IPrioridadeService _servicePrioridade;
        private readonly ISetorService _serviceSetor;

        public ChamadoController(ILogger<ChamadoController> logger, IChamadoService chamadoService, ISetorService setorService, IPrioridadeService prioridadeService)
        {
            _logger = logger;
            _service = chamadoService;
            _servicePrioridade = prioridadeService;
            _serviceSetor = setorService;
        }

        public async Task<IActionResult> Index(int? setorId, int? prioridadeId, string? status)
        {
            var chamados = await _service.GetAll();

            if (setorId.HasValue)
                chamados = chamados.Where(c => c.SetorId == setorId);

            if (prioridadeId.HasValue)
                chamados = chamados.Where(c => c.PrioridadeId == prioridadeId);

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<ChamadoStatus>(status, out var parsed))
                chamados = chamados.Where(c => c.Status == parsed);

            await CarregarViewBags();

            return View(chamados);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarViewBags();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChamadoRequestDTO request)
        {
            if (!ModelState.IsValid)
            {

                await CarregarViewBags();

                return View(request);
            }

            await _service.Create(request);

            TempData["Sucesso"] = "Chamado criado com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CheckIn(int id)
        {
            await _service.CheckIn(id);
            TempData["Sucesso"] = "Atendimento iniciado com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CheckOut(int id)
        {
            var chamado = await _service.GetById(id);
            if (chamado == null) return NotFound();

            ViewBag.ChamadoId = id;
            ViewBag.Titulo = chamado.Titulo;

            return View(new ChamadoCheckOutDTO(""));
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(int id, ChamadoCheckOutDTO dto)
        {

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _service.CheckOut(id, dto);

            TempData["Sucesso"] = "Chamado Finalizado com sucesso";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Cancelar(int id)
        {
            try
            {
                await _service.Cancelar(id);
                TempData["Success"] = "Chamado foi cancelado com sucesso";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Chamado",ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var chamado = await _service.GetById(id);

            if(chamado == null)
            {
                return NotFound();
            }

            return View(chamado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConf(int id)
        {
            await _service.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var chamado = await _service.GetById(id);

            if(chamado == null) return NotFound();

            var dto = new ChamadoUpdateDTO(chamado.Titulo, chamado.Descricao, chamado.SetorId, chamado.PrioridadeId);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChamadoUpdateDTO dto)
        {

            if (ModelState.IsValid)
            {
                await _service.Update(id, dto);

                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        private async Task CarregarViewBags()
        {
            ViewBag.Setores = new SelectList(
                await _serviceSetor.GetSetores(), "Id", "Nome");
            
            ViewBag.Prioridades = new SelectList(
                await _servicePrioridade.GetAllPrioridades(), "Id", "Nome");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}