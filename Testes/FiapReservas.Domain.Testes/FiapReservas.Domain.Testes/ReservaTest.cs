using FiapReservas.Domain.Entities.Reservas;
using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Enums;
using FiapReservas.Domain.Interfaces.Repositories.Reservas;
using FiapReservas.Domain.Interfaces.Services.Reservas;
using FiapReservas.Domain.Interfaces.Services.SMS;
using FiapReservas.Domain.Services.Reservas;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace FiapReservas.Domain.Testes
{
    public class ReservaTest
    {
        private readonly Mock<IReservaRepository> _reservaRepositoryMock;
        private readonly Mock<ISmsService> _smsServiceMock;
        private readonly IReservaService _reservaService;

        public ReservaTest()
        {
            _reservaRepositoryMock = new Mock<IReservaRepository>();
            _smsServiceMock = new Mock<ISmsService>();
            _reservaService = new ReservaService(_reservaRepositoryMock.Object, _smsServiceMock.Object);
        }

        [Fact(DisplayName = "Teste Reserva: Instância Deve Ter ID Único")]
        [Trait("Categoria", "Testes de Reservas")]
        public void Reserva_Instance_Should_Have_UniqueId()
        {
            // Arrange
            var reserva1 = new Reserva();
            var reserva2 = new Reserva();

            // Act

            // Assert
            Assert.NotEqual(reserva1.Id, reserva2.Id);
        }

        [Fact(DisplayName = "Teste ReservaService: Reservar Deve Inserir Reserva e Enviar SMS")]
        [Trait("Categoria", "Testes de Serviços de Reservas")]
        public async Task ReservaService_Reservar_Should_Insert_Reserva_And_Send_SMS()
        {
            // Arrange
            var restaurante = new Restaurante
            {
                Id = Guid.NewGuid(),
                Nome = "Restaurante Teste",
                Telefone = "123456789"
            };

            var reserva = new Reserva
            {
                Restaurante = restaurante,
                DataReserva = DateTimeOffset.UtcNow,
                Status = StatusReserva.Solicitada,
                Nome = "Cliente Teste",
                Telefone = "987654321",
                Email = "cliente@teste.com",
                QuantidadePessoas = 4
            };

            _reservaRepositoryMock.Setup(repo => repo.Insert(reserva)).Returns(Task.CompletedTask);

            // Act
            var result = await _reservaService.Reservar(reserva);

            // Assert
            _reservaRepositoryMock.Verify(repo => repo.Insert(reserva), Times.Once);
            _smsServiceMock.Verify(sms => sms.EnviarMensagem(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            Assert.NotNull(result);
            Assert.Equal(reserva.Id, result.Id);
        }

        [Fact(DisplayName = "Teste ReservaService: Confirmar Deve Atualizar Reserva e Enviar SMS")]
        [Trait("Categoria", "Testes de Serviços de Reservas")]
        public async Task ReservaService_Confirmar_Should_Update_Reserva_And_Send_SMS()
        {
            // Arrange
            var restaurante = new Restaurante
            {
                Id = Guid.NewGuid(),
                Nome = "Restaurante Teste",
                Telefone = "123456789"
            };

            var reserva = new Reserva
            {
                Restaurante = restaurante,
                DataReserva = DateTimeOffset.UtcNow,
                Status = StatusReserva.Solicitada,
                Nome = "Cliente Teste",
                Telefone = "987654321",
                Email = "cliente@teste.com",
                QuantidadePessoas = 4
            };

            _reservaRepositoryMock.Setup(repo => repo.Update(reserva)).Returns(Task.CompletedTask);

            // Act
            await _reservaService.Confirmar(reserva);

            // Assert
            _reservaRepositoryMock.Verify(repo => repo.Update(reserva), Times.Once);
            _smsServiceMock.Verify(sms => sms.EnviarMensagem(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.Equal(StatusReserva.Confirmada, reserva.Status);
        }

        [Fact(DisplayName = "Teste ReservaService: Inserir Deve Chamar Repositório")]
        [Trait("Categoria", "Testes de Serviços de Reservas")]
        public async Task ReservaService_Insert_Should_Call_Repository()
        {
            // Arrange
            var reserva = new Reserva
            {
                Restaurante = new Restaurante { Id = Guid.NewGuid(), Nome = "Restaurante Teste" },
                DataReserva = DateTimeOffset.UtcNow,
                Status = StatusReserva.Solicitada,
                Nome = "Cliente Teste",
                Telefone = "987654321",
                Email = "cliente@teste.com",
                QuantidadePessoas = 4
            };

            _reservaRepositoryMock.Setup(repo => repo.Insert(reserva)).Returns(Task.CompletedTask);

            // Act
            await _reservaService.Insert(reserva);

            // Assert
            _reservaRepositoryMock.Verify(repo => repo.Insert(reserva), Times.Once);
        }

        [Fact(DisplayName = "Teste ReservaService: Atualizar Deve Chamar Repositório")]
        [Trait("Categoria", "Testes de Serviços de Reservas")]
        public async Task ReservaService_Update_Should_Call_Repository()
        {
            // Arrange
            var reserva = new Reserva
            {
                Id = Guid.NewGuid(),
                Restaurante = new Restaurante { Id = Guid.NewGuid(), Nome = "Restaurante Teste" },
                DataReserva = DateTimeOffset.UtcNow,
                Status = StatusReserva.Solicitada,
                Nome = "Cliente Teste",
                Telefone = "987654321",
                Email = "cliente@teste.com",
                QuantidadePessoas = 4
            };

            _reservaRepositoryMock.Setup(repo => repo.Update(reserva)).Returns(Task.CompletedTask);

            // Act
            await _reservaService.Update(reserva);

            // Assert
            _reservaRepositoryMock.Verify(repo => repo.Update(reserva), Times.Once);
        }

        [Fact(DisplayName = "Teste ReservaService: Deletar Deve Chamar Repositório")]
        [Trait("Categoria", "Testes de Serviços de Reservas")]
        public async Task ReservaService_Delete_Should_Call_Repository()
        {
            // Arrange
            var reservaId = Guid.NewGuid();
            _reservaRepositoryMock.Setup(repo => repo.Delete(reservaId)).Returns(Task.CompletedTask);

            // Act
            await _reservaService.Delete(reservaId);

            // Assert
            _reservaRepositoryMock.Verify(repo => repo.Delete(reservaId), Times.Once);
        }

        [Fact(DisplayName = "Teste ReservaService: Listar Reservas Deve Retornar Todas as Reservas")]
        [Trait("Categoria", "Testes de Serviços de Reservas")]
        public async Task ReservaService_List_Should_Return_All_Reservas()
        {
            // Arrange
            var expectedReservas = new List<Reserva>
            {
                new Reserva { Id = Guid.NewGuid(), Nome = "Cliente 1", Telefone = "123456789", Email = "cliente1@teste.com", QuantidadePessoas = 2, DataReserva = DateTimeOffset.UtcNow, Restaurante = new Restaurante { Id = Guid.NewGuid(), Nome = "Restaurante 1" }, Status = StatusReserva.Solicitada },
                new Reserva { Id = Guid.NewGuid(), Nome = "Cliente 2", Telefone = "987654321", Email = "cliente2@teste.com", QuantidadePessoas = 4, DataReserva = DateTimeOffset.UtcNow, Restaurante = new Restaurante { Id = Guid.NewGuid(), Nome = "Restaurante 2" }, Status = StatusReserva.Confirmada }
            };

            _reservaRepositoryMock.Setup(repo => repo.List(It.IsAny<Expression<Func<Reserva, bool>>>())).ReturnsAsync(expectedReservas);

            // Act
            var result = await _reservaService.List(x => true);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedReservas.Count, result.Count());
        }
    }
}
