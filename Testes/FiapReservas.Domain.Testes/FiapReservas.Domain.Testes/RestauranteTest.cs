using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.Domain.Services.Restaurantes;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace FiapReservas.Domain.Testes
{
    public class RestauranteTest
    {
        private readonly Mock<IRestauranteRepository> _restauranteRepositoryMock;
        private readonly IRestauranteService _restauranteService;

        public RestauranteTest()
        {
            _restauranteRepositoryMock = new Mock<IRestauranteRepository>();
            _restauranteService = new RestauranteService(_restauranteRepositoryMock.Object);
        }

        [Fact(DisplayName = "Teste Restaurante: Instância Deve Ter ID Único")]
        [Trait("Categoria", "Testes de Restaurantes")]
        public void Restaurante_Instance_Should_Have_UniqueId()
        {
            // Arrange
            var restaurante1 = new Restaurante();
            var restaurante2 = new Restaurante();

            // Act

            // Assert
            Assert.NotEqual(restaurante1.Id, restaurante2.Id);
        }

        [Fact(DisplayName = "Teste Restaurante: Propriedades Devem Ser Inicializadas Corretamente")]
        [Trait("Categoria", "Testes de Restaurantes")]
        public void Restaurante_Properties_Should_Be_Set_Correctly()
        {
            // Arrange
            var nome = "Restaurante Teste";
            var descricao = "Descrição Teste";
            var telefone = "123456789";

            // Act
            var restaurante = new Restaurante
            {
                Nome = nome,
                Descricao = descricao,
                Telefone = telefone
            };

            // Assert
            Assert.Equal(nome, restaurante.Nome);
            Assert.Equal(descricao, restaurante.Descricao);
            Assert.Equal(telefone, restaurante.Telefone);
        }

        [Fact(DisplayName = "Teste RestauranteService: Obter Por ID Deve Retornar Restaurante Se Existir")]
        [Trait("Categoria", "Testes de Serviços de Restaurantes")]
        public async Task RestauranteService_GetById_Should_Return_Restaurante_IfExists()
        {
            // Arrange
            var restauranteId = Guid.NewGuid();
            var expectedRestaurante = new Restaurante
            {
                Id = restauranteId,
                Nome = "Restaurante Teste",
                Descricao = "Descrição Teste",
                Telefone = "123456789"
            };

            _restauranteRepositoryMock.Setup(repo => repo.Get(restauranteId)).ReturnsAsync(expectedRestaurante);

            // Act
            var result = await _restauranteService.Get(restauranteId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedRestaurante.Id, result.Id);
            Assert.Equal(expectedRestaurante.Nome, result.Nome);
            Assert.Equal(expectedRestaurante.Descricao, result.Descricao);
            Assert.Equal(expectedRestaurante.Telefone, result.Telefone);
        }

        [Fact(DisplayName = "Teste RestauranteService: Inserir Restaurante Deve Chamar Repositório")]
        [Trait("Categoria", "Testes de Serviços de Restaurantes")]
        public async Task RestauranteService_Insert_Should_Call_Repository()
        {
            // Arrange
            var restaurante = new Restaurante
            {
                Nome = "Restaurante Teste",
                Descricao = "Descrição Teste",
                Telefone = "123456789"
            };

            _restauranteRepositoryMock.Setup(repo => repo.Insert(restaurante)).Returns(Task.CompletedTask);

            // Act
            await _restauranteService.Insert(restaurante);

            // Assert
            _restauranteRepositoryMock.Verify(repo => repo.Insert(restaurante), Times.Once);
        }

        [Fact(DisplayName = "Teste RestauranteService: Atualizar Restaurante Deve Chamar Repositório")]
        [Trait("Categoria", "Testes de Serviços de Restaurantes")]
        public async Task RestauranteService_Update_Should_Call_Repository()
        {
            // Arrange
            var restaurante = new Restaurante
            {
                Id = Guid.NewGuid(),
                Nome = "Restaurante Atualizado",
                Descricao = "Descrição Atualizada",
                Telefone = "987654321"
            };

            _restauranteRepositoryMock.Setup(repo => repo.Update(restaurante)).Returns(Task.CompletedTask);

            // Act
            await _restauranteService.Update(restaurante);

            // Assert
            _restauranteRepositoryMock.Verify(repo => repo.Update(restaurante), Times.Once);
        }

        [Fact(DisplayName = "Teste RestauranteService: Deletar Restaurante Deve Chamar Repositório")]
        [Trait("Categoria", "Testes de Serviços de Restaurantes")]
        public async Task RestauranteService_Delete_Should_Call_Repository()
        {
            // Arrange
            var restauranteId = Guid.NewGuid();
            _restauranteRepositoryMock.Setup(repo => repo.Delete(restauranteId)).Returns(Task.CompletedTask);

            // Act
            await _restauranteService.Delete(restauranteId);

            // Assert
            _restauranteRepositoryMock.Verify(repo => repo.Delete(restauranteId), Times.Once);
        }

        [Fact(DisplayName = "Teste RestauranteService: Listar Restaurantes Deve Retornar Todos os Restaurantes")]
        [Trait("Categoria", "Testes de Serviços de Restaurantes")]
        public async Task RestauranteService_List_Should_Return_All_Restaurantes()
        {
            // Arrange
            var expectedRestaurantes = new List<Restaurante>
            {
                new Restaurante { Id = Guid.NewGuid(), Nome = "Restaurante 1", Descricao = "Descrição 1", Telefone = "123456789" },
                new Restaurante { Id = Guid.NewGuid(), Nome = "Restaurante 2", Descricao = "Descrição 2", Telefone = "987654321" }
            };

            _restauranteRepositoryMock.Setup(repo => repo.List(It.IsAny<Expression<Func<Restaurante, bool>>>()))
                                      .ReturnsAsync(expectedRestaurantes);

            // Act
            var result = await _restauranteService.List(x => true);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedRestaurantes.Count, result.Count());
        }
    }
}
