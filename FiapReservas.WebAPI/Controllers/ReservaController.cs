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

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<Reserva> Listar(Guid id)
        {
            return await _service.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Inserir(ReservaInsertDTO dto)
        {
            var restaurante = await _restauranteService.Get(dto.RestauranteId);
            if (restaurante == null)
            {
                return NotFound("Restaurante não encontrado");
            }            

            var reserva = new Reserva()
            {
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

        [HttpPost]
        [Route("Reservar")]
        public async Task<IActionResult> Reservar(ReservarDTO dto)
        {
            var restaurante = await _restauranteService.Get(dto.IdRestaurante);
            var reserva = await _service.Reservar(new Reserva()
            {
                DataReserva = dto.DataReserva,
                Email = dto.Email,
                Nome = dto.Nome,
                Restaurante = restaurante,
                Status = Domain.Enums.StatusReserva.Solicitada,
                Telefone = dto.Telefone,
                QuantidadePessoas = dto.QuantidadePessoas
            });
            return Ok(reserva);
        }


        [HttpPost]
        [Route("{id:Guid}/Confirmar")]
        public async Task<IActionResult> Confirmar(Guid id)
        {
            var reserva = await _service.Get(id);
            await _service.Confirmar(reserva);
            return Ok(reserva);
        }
    }
}
