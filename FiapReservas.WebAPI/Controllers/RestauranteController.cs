using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.WebAPI.DTOs;

using Microsoft.AspNetCore.Mvc;

namespace FiapReservas.WebAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RestauranteController : ControllerBase
    {
        private readonly IRestauranteService _service;

        public RestauranteController(IRestauranteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Restaurante>> Listar()
        {
            return await _service.List(x => true);
        }

        [HttpPost]
        public async Task Inserir(RestauranteInsertDTO dto)
        {
            var restaurante = new Restaurante()
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Telefone = dto.Telefone                
            };

            await _service.Insert(restaurante);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(RestauranteUpdateDTO dto)
        {
            var restaurante = await _service.Get(dto.Id);

            if (restaurante == null)
            {
                return NotFound();
            }

            restaurante.Nome = dto.Nome;
            restaurante.Descricao = dto.Descricao;
            restaurante.Telefone = dto.Telefone;
            
            await _service.Update(restaurante);

            return Ok();
        }


        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var restaurante = await _service.Get(id);

            if (restaurante == null)
            {
                return NotFound();
            }

            await _service.Delete(id);

            return Ok();
        }

    }
}
