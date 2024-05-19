using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.WebAPI.Controllers;
using FiapReservas.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace FiapReservas.WebAPI.Testes
{
    public class RestauranteControllerIntegrationTests
    {
        private readonly Mock<IRestauranteService> _restauranteServiceMock;
        private readonly RestauranteController _controller;

        public RestauranteControllerIntegrationTests()
        {
            _restauranteServiceMock = new Mock<IRestauranteService>();
            _controller = new RestauranteController(_restauranteServiceMock.Object);
        }

        [Fact(DisplayName = "Listar Deve Retornar Lista de Restaurantes")]
        [Trait("Categoria", "Testes de Integração Restaurante")]
        public async Task Listar_Deve_Retornar_Lista_De_Restaurantes()
        {
            // Arrange
            var expectedRestaurantes = new[]
            {
                new Restaurante { Id = Guid.NewGuid(), Nome = "Restaurante 1", Descricao = "Descricao 1", Telefone = "123456789" },
                new Restaurante { Id = Guid.NewGuid(), Nome = "Restaurante 2", Descricao = "Descricao 2", Telefone = "987654321" }
            };

            _restauranteServiceMock.Setup(x => x.List(It.IsAny<Expression<Func<Restaurante, bool>>>())).ReturnsAsync(expectedRestaurantes);

            // Act
            var result = await _controller.Listar();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedRestaurantes.Length, ((IEnumerable<Restaurante>)result).Count());
        }

        [Fact(DisplayName = "Inserir Deve Inserir Restaurante")]
        [Trait("Categoria", "Testes de Integração Restaurante")]
        public async Task Inserir_Deve_Inserir_Restaurante()
        {
            // Arrange
            var restauranteInsertDto = new RestauranteInsertDTO
            {
                Nome = "Novo Restaurante",
                Descricao = "Nova Descricao",
                Telefone = "123456789"
            };

            var restaurante = new Restaurante
            {
                Id = Guid.NewGuid(),
                Nome = restauranteInsertDto.Nome,
                Descricao = restauranteInsertDto.Descricao,
                Telefone = restauranteInsertDto.Telefone
            };

            _restauranteServiceMock.Setup(x => x.Insert(It.IsAny<Restaurante>())).Returns(Task.CompletedTask);

            // Act
            await _controller.Inserir(restauranteInsertDto);

            // Assert
            _restauranteServiceMock.Verify(x => x.Insert(It.Is<Restaurante>(r => r.Nome == restaurante.Nome && r.Descricao == restaurante.Descricao && r.Telefone == restaurante.Telefone)), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Deve Retornar NotFound Quando Restaurante Não Existe")]
        [Trait("Categoria", "Testes de Integração Restaurante")]
        public async Task Atualizar_Deve_Retornar_NotFound_Quando_Restaurante_Nao_Existe()
        {
            // Arrange
            var restauranteUpdateDto = new RestauranteUpdateDTO
            {
                Id = Guid.NewGuid(),
                Nome = "Restaurante Atualizado",
                Descricao = "Descricao Atualizada",
                Telefone = "123456789"
            };

            _restauranteServiceMock.Setup(x => x.Get(restauranteUpdateDto.Id)).ReturnsAsync((Restaurante)null);

            // Act
            var result = await _controller.Atualizar(restauranteUpdateDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact(DisplayName = "Atualizar Deve Atualizar Restaurante Existente")]
        [Trait("Categoria", "Testes de Integração Restaurante")]
        public async Task Atualizar_Deve_Atualizar_Restaurante_Existente()
        {
            // Arrange
            var restauranteUpdateDto = new RestauranteUpdateDTO
            {
                Id = Guid.NewGuid(),
                Nome = "Restaurante Atualizado",
                Descricao = "Descricao Atualizada",
                Telefone = "123456789"
            };

            var existingRestaurante = new Restaurante
            {
                Id = restauranteUpdateDto.Id,
                Nome = "Restaurante Antigo",
                Descricao = "Descricao Antiga",
                Telefone = "987654321"
            };

            _restauranteServiceMock.Setup(x => x.Get(restauranteUpdateDto.Id)).ReturnsAsync(existingRestaurante);
            _restauranteServiceMock.Setup(x => x.Update(It.IsAny<Restaurante>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Atualizar(restauranteUpdateDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "Excluir Deve Retornar NotFound Quando Restaurante Não Existe")]
        [Trait("Categoria", "Testes de Integração Restaurante")]
        public async Task Excluir_Deve_Retornar_NotFound_Quando_Restaurante_Nao_Existe()
        {
            // Arrange
            var restauranteId = Guid.NewGuid();
            _restauranteServiceMock.Setup(x => x.Get(restauranteId)).ReturnsAsync((Restaurante)null);

            // Act
            var result = await _controller.Excluir(restauranteId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact(DisplayName = "Excluir Deve Deletar Restaurante Existente")]
        [Trait("Categoria", "Testes de Integração Restaurante")]
        public async Task Excluir_Deve_Deletar_Restaurante_Existente()
        {
            // Arrange
            var restauranteId = Guid.NewGuid();
            var existingRestaurante = new Restaurante
            {
                Id = restauranteId,
                Nome = "Restaurante para Deletar",
                Descricao = "Descricao",
                Telefone = "123456789"
            };

            _restauranteServiceMock.Setup(x => x.Get(restauranteId)).ReturnsAsync(existingRestaurante);
            _restauranteServiceMock.Setup(x => x.Delete(restauranteId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Excluir(restauranteId);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
