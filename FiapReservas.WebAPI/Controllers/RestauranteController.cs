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
        public async Task Inserir(RestauranteDTO dto)
        {
            var restaurante = new Restaurante()
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Mesas = dto.Mesas.Select(x => new Mesa()
                {
                    QuantidadePessoas = x.QuantidadePessoas
                })
            };

            await _service.Insert(restaurante);
        }
    }
}
