using FiapReservas.Domain.Entities.Reservas;
using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Reservas;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.WebAPI.DTOs;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiapReservas.WebAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _service;
        private readonly IRestauranteService _restauranteService;

        public ReservaController(IReservaService service, IRestauranteService restauranteService)
        {
            _service = service;
            _restauranteService = restauranteService;
        }

        [HttpGet]
        public async Task<IEnumerable<Reserva>> Listar()
        {
            return await _service.List(x => true);
        }

        [HttpPost]
        public async Task<IActionResult> Inserir(ReservaInsertDTO dto)
        {
            var restaurante = await _restauranteService.Get(dto.RestauranteId);
            if (restaurante == null)
            {
                return NotFound("Restaurante não encontrado");
            }

            var mesa = restaurante.Mesas.FirstOrDefault(m => m.Numero == dto.MesaNumero);
            if (mesa == null)
            {
                return NotFound("Mesa não encontrada");
            }

            var reserva = new Reserva()
            {
                Mesa = mesa,
                DataReserva = dto.DataReserva,
                Status = dto.Status
            };

            await _service.Insert(reserva);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(ReservaUpdateDTO dto)
        {
            var reserva = await _service.Get(dto.Id);

            if (reserva == null)
            {
                return NotFound();
            }

            reserva.Mesa = await _restauranteService.GetMesaByNumero(dto.RestauranteId, dto.MesaNumero);
            if (reserva.Mesa == null)
            {
                return NotFound("Mesa não encontrada");
            }

            reserva.DataReserva = dto.DataReserva;
            reserva.Status = dto.Status;

            await _service.Update(reserva);

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var reserva = await _service.Get(id);

            if (reserva == null)
            {
                return NotFound();
            }

            await _service.Delete(id);

            return Ok();
        }
    }
}
