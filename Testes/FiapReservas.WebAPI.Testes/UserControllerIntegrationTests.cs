using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.Domain.Services.Restaurantes;
using FiapReservas.Domain.Utils.Cryptography;
using FiapReservas.WebAPI.Controllers;
using FiapReservas.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace FiapReservas.WebAPI.Testes
{
    public class UserControllerIntegrationTests
    {
        [Fact(DisplayName = "Listar Deve Retornar Lista de Usuários")]
        [Trait("Categoria", "Testes de Integração User")]
        public async Task Listar_Deve_Retornar_Lista_De_Usuarios()
        {
            Mock<IUserService> _userServiceMock = new Mock<IUserService>();            

            // Arrange
            var expectedUsers = new[] {
                new User { Id = Guid.NewGuid(), Nome = "Usuário 1", Email = "usuario1@example.com" },
                new User { Id = Guid.NewGuid(), Nome = "Usuário 2", Email = "usuario2@example.com" }
            };

            _userServiceMock.Setup(x => x.List(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(expectedUsers);
            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.Listar();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<User[]>(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "Inserir Deve Retornar Usuário Criado")]
        [Trait("Categoria", "Testes de Integração User")]
        public async Task Inserir_Deve_Retornar_Usuario_Criado()
        {
            Mock<IUserService> _userServiceMock = new Mock<IUserService>();

            // Arrange
            var newUserDto = new UserInsertDTO { Nome = "Novo Usuário", Email = "novo_usuario2@example.com", Password = "senha123" };
            var createdUser = new User { Id = Guid.NewGuid(), Nome = newUserDto.Nome, Email = newUserDto.Email, Password = PasswordCryptography.Encrypt(newUserDto.Password) };
            _userServiceMock.Setup(x => x.Insert(It.IsAny<User>())).Returns(Task.CompletedTask);
            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.Inserir(newUserDto);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var user = Assert.IsType<User>(okResult.Value);
            var isValidPassword = createdUser.ValidatePassword(newUserDto.Password);

            // Assert
            Assert.Equal(createdUser.Nome, user.Nome);
            Assert.Equal(createdUser.Email, user.Email);
            Assert.True(isValidPassword);
        }

        [Fact(DisplayName = "Update Deve Retornar Usuário Atualizado")]
        [Trait("Categoria", "Testes de Integração User")]
        public async Task Update_Deve_Retornar_Usuario_Atualizado()
        {
            Mock<IUserService> _userServiceMock = new Mock<IUserService>();

            // Arrange
            var updateUserDto = new UserUpdateDTO { Id = Guid.NewGuid(), Nome = "Usuário Atualizado", Email = "atualizado@example.com", Password = "novasenha123" };
            var existingUser = new User { Id = updateUserDto.Id, Nome = "Usuário Antigo", Email = "antigo@example.com", Password = "senha123" };
            _userServiceMock.Setup(x => x.Get(updateUserDto.Id)).ReturnsAsync(existingUser);
            _userServiceMock.Setup(x => x.Update(It.IsAny<User>())).Returns(Task.CompletedTask);
            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.Update(updateUserDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var user = Assert.IsType<User>(okResult.Value);
            Assert.Equal(updateUserDto.Id, user.Id);
            Assert.Equal(updateUserDto.Nome, user.Nome);
            Assert.Equal(updateUserDto.Email, user.Email);
        }

        [Fact(DisplayName = "Delete Deve Retornar NotFound Quando Usuário Não Existe")]
        [Trait("Categoria", "Testes de Integração User")]
        public async Task Delete_Deve_Retornar_NotFound_Quando_Usuario_Nao_Existe()
        {
            Mock<IUserService> _userServiceMock = new Mock<IUserService>();

            // Arrange
            var userId = Guid.NewGuid();
            _userServiceMock.Setup(x => x.Get(userId)).ReturnsAsync((User)null);
            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.Delete(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}
