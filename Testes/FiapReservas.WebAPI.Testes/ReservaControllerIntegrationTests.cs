using FiapReservas.Domain.Entities.Reservas;
using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Enums;
using FiapReservas.Domain.Interfaces.Services.Reservas;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.WebAPI.Controllers;
using FiapReservas.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace FiapReservas.WebAPI.Testes
{
    public class ReservaControllerIntegrationTests
    {
        private readonly Mock<IReservaService> _reservaServiceMock;
        private readonly Mock<IRestauranteService> _restauranteServiceMock;
        private readonly ReservaController _controller;

        public ReservaControllerIntegrationTests()
        {
            _reservaServiceMock = new Mock<IReservaService>();
            _restauranteServiceMock = new Mock<IRestauranteService>();
            _controller = new ReservaController(_reservaServiceMock.Object, _restauranteServiceMock.Object);
        }

        [Fact(DisplayName = "Listar Deve Retornar Lista de Reservas")]
        [Trait("Categoria", "Testes de Integração Reserva")]
        public async Task Listar_Deve_Retornar_Lista_De_Reservas()
        {
            // Arrange
            var expectedReservas = new[]
            {
                new Reserva { Id = Guid.NewGuid(), DataReserva = DateTime.Now, Status = StatusReserva.Solicitada },
                new Reserva { Id = Guid.NewGuid(), DataReserva = DateTime.Now.AddDays(1), Status = StatusReserva.Confirmada }
            };

            _reservaServiceMock.Setup(x => x.List(It.IsAny<Expression<Func<Reserva, bool>>>())).ReturnsAsync(expectedReservas);

            // Act
            var result = await _controller.Listar();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedReservas.Length, ((IEnumerable<Reserva>)result).Count());
        }

        [Fact(DisplayName = "Listar Por Id Deve Retornar Reserva")]
        [Trait("Categoria", "Testes de Integração Reserva")]
        public async Task Listar_Por_Id_Deve_Retornar_Reserva()
        {
            // Arrange
            var reservaId = Guid.NewGuid();
            var expectedReserva = new Reserva { Id = reservaId, DataReserva = DateTime.Now, Status = StatusReserva.Solicitada };

            _reservaServiceMock.Setup(x => x.Get(reservaId)).ReturnsAsync(expectedReserva);

            // Act
            var result = await _controller.Listar(reservaId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedReserva.Id, result.Id);
        }

        [Fact(DisplayName = "Inserir Deve Inserir Reserva")]
        [Trait("Categoria", "Testes de Integração Reserva")]
        public async Task Inserir_Deve_Inserir_Reserva()
        {
            // Arrange
            var restauranteId = Guid.NewGuid();
            var reservaInsertDto = new ReservaInsertDTO
            {
                DataReserva = DateTime.Now,
                Status = StatusReserva.Solicitada,
                RestauranteId = restauranteId
            };

            var restaurante = new Restaurante { Id = restauranteId, Nome = "Restaurante Teste" };

            _restauranteServiceMock.Setup(x => x.Get(restauranteId)).ReturnsAsync(restaurante);
            _reservaServiceMock.Setup(x => x.Insert(It.IsAny<Reserva>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Inserir(reservaInsertDto);

            // Assert
            Assert.IsType<OkResult>(result);
            _reservaServiceMock.Verify(x => x.Insert(It.Is<Reserva>(r => r.DataReserva == reservaInsertDto.DataReserva && r.Status == reservaInsertDto.Status)), Times.Once);
        }

        [Fact(DisplayName = "Inserir Deve Retornar NotFound Quando Restaurante Não Existe")]
        [Trait("Categoria", "Testes de Integração Reserva")]
        public async Task Inserir_Deve_Retornar_NotFound_Quando_Restaurante_Nao_Existe()
        {
            // Arrange
            var restauranteId = Guid.NewGuid();
            var reservaInsertDto = new ReservaInsertDTO
            {
                DataReserva = DateTime.Now,
                Status = StatusReserva.Solicitada,
                RestauranteId = restauranteId
            };

            _restauranteServiceMock.Setup(x => x.Get(restauranteId)).ReturnsAsync((Restaurante)null);

            // Act
            var result = await _controller.Inserir(reservaInsertDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Restaurante não encontrado", notFoundResult.Value);
        }

        [Fact(DisplayName = "Atualizar Deve Retornar NotFound Quando Reserva Não Existe")]
        [Trait("Categoria", "Testes de Integração Reserva")]
        public async Task Atualizar_Deve_Retornar_NotFound_Quando_Reserva_Nao_Existe()
        {
            // Arrange
            var reservaUpdateDto = new ReservaUpdateDTO
            {
                Id = Guid.NewGuid(),
                DataReserva = DateTime.Now,
                Status = StatusReserva.Confirmada
            };

            _reservaServiceMock.Setup(x => x.Get(reservaUpdateDto.Id)).ReturnsAsync((Reserva)null);

            // Act
            var result = await _controller.Atualizar(reservaUpdateDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact(DisplayName = "Atualizar Deve Atualizar Reserva Existente")]
        [Trait("Categoria", "Testes de Integração Reserva")]
        public async Task Atualizar_Deve_Atualizar_Reserva_Existente()
        {
            // Arrange
            var reservaUpdateDto = new ReservaUpdateDTO
            {
                Id = Guid.NewGuid(),
                DataReserva = DateTime.Now,
                Status = StatusReserva.Confirmada
            };

            var existingReserva = new Reserva
            {
                Id = reservaUpdateDto.Id,
                DataReserva = DateTime.Now.AddDays(-1),
                Status = StatusReserva.Solicitada
            };

            _reservaServiceMock.Setup(x => x.Get(reservaUpdateDto.Id)).ReturnsAsync(existingReserva);
            _reservaServiceMock.Setup(x => x.Update(It.IsAny<Reserva>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Atualizar(reservaUpdateDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "Excluir Deve Retornar NotFound Quando Reserva Não Existe")]
        [Trait("Categoria", "Testes de Integração Reserva")]
        public async Task Excluir_Deve_Retornar_NotFound_Quando_Reserva_Nao_Existe()
        {
            // Arrange
            var reservaId = Guid.NewGuid();
            _reservaServiceMock.Setup(x => x.Get(reservaId)).ReturnsAsync((Reserva)null);

            // Act
            var result = await _controller.Excluir(reservaId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact(DisplayName = "Excluir Deve Deletar Reserva Existente")]
        [Trait("Categoria", "Testes de Integração Reserva")]
        public async Task Excluir_Deve_Deletar_Reserva_Existente()
        {
            // Arrange
            var reservaId = Guid.NewGuid();
            var existingReserva = new Reserva
            {
                Id = reservaId,
                DataReserva = DateTime.Now,
                Status = StatusReserva.Solicitada
            };

            _reservaServiceMock.Setup(x => x.Get(reservaId)).ReturnsAsync(existingReserva);
            _reservaServiceMock.Setup(x => x.Delete(reservaId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Excluir(reservaId);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "Reservar Deve Retornar Reserva Criada")]
        [Trait("Categoria", "Testes de Integração Reserva")]
        public async Task Reservar_Deve_Retornar_Reserva_Criada()
        {
            // Arrange
            var restauranteId = Guid.NewGuid();
            var reservarDto = new ReservarDTO
            {
                DataReserva = DateTime.Now,
                Email = "teste@teste.com",
                Nome = "Teste",
                IdRestaurante = restauranteId,
                Telefone = "123456789",
                QuantidadePessoas = 4
            };

            var restaurante = new Restaurante { Id = restauranteId, Nome = "Restaurante Teste" };
            var expectedReserva = new Reserva
            {
                Id = Guid.NewGuid(),
                DataReserva = reservarDto.DataReserva,
                Email = reservarDto.Email,
                Nome = reservarDto.Nome,
                Restaurante = restaurante,
                Status = StatusReserva.Solicitada,
                Telefone = reservarDto.Telefone,
                QuantidadePessoas = reservarDto.QuantidadePessoas
            };

            _restauranteServiceMock.Setup(x => x.Get(restauranteId)).ReturnsAsync(restaurante);
            _reservaServiceMock.Setup(x => x.Reservar(It.IsAny<Reserva>())).ReturnsAsync(expectedReserva);

            // Act
            var result = await _controller.Reservar(reservarDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var reservaResult = Assert.IsType<Reserva>(okResult.Value);
            Assert.Equal(expectedReserva.Id, reservaResult.Id);
        }
    }
}
