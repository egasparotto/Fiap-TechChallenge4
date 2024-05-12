using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
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
        private readonly Mock<IUserService> _userServiceMock;

        public UserControllerIntegrationTests()
        {
            _userServiceMock = new Mock<IUserService>();
        }

        [Fact(DisplayName = "Listar Deve Retornar Lista de Usu�rios")]
        [Trait("Categoria", "Testes de Integra��o")]
        public async Task Listar_Deve_Retornar_Lista_De_Usuarios()
        {
            // Arrange
            var expectedUsers = new[] {
                new User { Id = Guid.NewGuid(), Nome = "Usu�rio 1", Email = "usuario1@example.com" },
                new User { Id = Guid.NewGuid(), Nome = "Usu�rio 2", Email = "usuario2@example.com" }
            };

            _userServiceMock.Setup(x => x.List(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(expectedUsers);
            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.Listar();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<User[]>(okResult.Value);
            Assert.Equal(expectedUsers, users);
        }

        [Fact(DisplayName = "Inserir Deve Retornar Usu�rio Criado")]
        [Trait("Categoria", "Testes de Integra��o")]
        public async Task Inserir_Deve_Retornar_Usuario_Criado()
        {
            // Arrange
            var newUserDto = new UserInsertDTO { Nome = "Novo Usu�rio", Email = "novo_usuario2@example.com", Password = "senha123" };
            var createdUser = new User { Id = Guid.NewGuid(), Nome = newUserDto.Nome, Email = newUserDto.Email, Password = PasswordCryptography.Encrypt(newUserDto.Password) };
            _userServiceMock.Setup(x => x.Insert(It.IsAny<User>())).Returns(Task.CompletedTask);
            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.Inserir(newUserDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var user = Assert.IsType<User>(okResult.Value);
            var isValidPassword = createdUser.ValidatePassword(newUserDto.Password);
            
            Assert.Equal(createdUser.Nome, user.Nome);
            Assert.Equal(createdUser.Email, user.Email);
            Assert.True(isValidPassword);
        }

        [Fact(DisplayName = "Update Deve Retornar Usu�rio Atualizado")]
        [Trait("Categoria", "Testes de Integra��o")]
        public async Task Update_Deve_Retornar_Usuario_Atualizado()
        {
            // Arrange
            var updateUserDto = new UserUpdateDTO { Id = Guid.NewGuid(), Nome = "Usu�rio Atualizado", Email = "atualizado@example.com", Password = "novasenha123" };
            var existingUser = new User { Id = updateUserDto.Id, Nome = "Usu�rio Antigo", Email = "antigo@example.com", Password = "senha123" };
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

        [Fact(DisplayName = "Delete Deve Retornar StatusCode 200")]
        [Trait("Categoria", "Testes de Integra��o")]
        public async Task Delete_Deve_Retornar_StatusCode_200()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userServiceMock.Setup(x => x.Get(userId)).ReturnsAsync((User)null);
            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.Delete(userId);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "Login Deve Retornar Token JWT V�lido")]
        [Trait("Categoria", "Testes de Integra��o")]
        public async Task Login_Deve_Retornar_Token_JWT_Valido()
        {
            // Arrange
            var userLoginDto = new UserLoginDTO { Email = "usuario@example.com", Password = "senha123" };
            var user = new User { Email = userLoginDto.Email, Password = PasswordCryptography.Encrypt(userLoginDto.Password) };
            _userServiceMock.Setup(x => x.GetByEmail(userLoginDto.Email)).ReturnsAsync(user);
            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.Login(userLoginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var token = Assert.IsType<string>(okResult.Value);
            Assert.NotNull(token);
        }
    }
}
