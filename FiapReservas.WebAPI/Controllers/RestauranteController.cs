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
                Mesas = dto.Mesas.Select(x => new Mesa()
                {
                    QuantidadePessoas = x.QuantidadePessoas,
                    Numero = x.Numero
                    
                })
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

            // Lista para armazenar as novas mesas e as mesas atualizadas
            var novasMesas = new List<Mesa>();

            // Atualiza as mesas existentes e adiciona novas mesas
            foreach (var mesaDto in dto.Mesas)
            {
                var mesaExistente = restaurante.Mesas.FirstOrDefault(m => m.Numero == mesaDto.Numero);
                if (mesaExistente != null)
                {
                    // Atualiza os dados da mesa existente
                    mesaExistente.QuantidadePessoas = mesaDto.QuantidadePessoas;
                }
                else
                {
                    // Adiciona uma nova mesa
                    novasMesas.Add(new Mesa()
                    {
                        Numero = mesaDto.Numero,
                        QuantidadePessoas = mesaDto.QuantidadePessoas
                    });
                }
            }

            // Remove as mesas que não estão presentes no DTO de atualização
            foreach (var mesaExistente in restaurante.Mesas.ToList())
            {
                if (!dto.Mesas.Any(m => m.Numero == mesaExistente.Numero))
                {
                    restaurante.Mesas = restaurante.Mesas.Where(m => m.Numero != mesaExistente.Numero);
                }
            }

            // Adiciona as novas mesas à lista de mesas do restaurante
            restaurante.Mesas = restaurante.Mesas.Concat(novasMesas);

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
