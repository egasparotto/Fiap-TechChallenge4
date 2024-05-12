using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.Domain.Services.Restaurantes;
using FiapReservas.Domain.Utils.Cryptography;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FiapReservas.Domain.Testes
{
    public class UserTest
    {
        [Fact(DisplayName = "Teste User: Instância Deve Ter ID Único")]
        [Trait("Categoria", "Testes de Usuários")]
        public async Task User_Instance_Should_Have_UniqueId()
        {
            // Arrange
            var user1 = new User();
            var user2 = new User();

            // Act

            // Assert
            Assert.NotEqual(user1.Id, user2.Id);
        }

        [Fact(DisplayName = "Teste User: Validar Senha Deve Retornar Verdadeiro Para Senha Correta")]
        [Trait("Categoria", "Testes de Usuários")]
        public async Task User_ValidatePassword_Should_Return_True_For_Correct_Password()
        {
            // Arrange
            var user = new User
            {
                Password = PasswordCryptography.Encrypt("password123") 
            };

            // Act
            var isValid = user.ValidatePassword("password123");

            // Assert
            Assert.True(isValid);
        }

        [Fact(DisplayName = "Teste User: Validar Senha Deve Retornar Falso Para Senha Incorreta")]
        [Trait("Categoria", "Testes de Usuários")]
        public async Task User_ValidatePassword_Should_Return_False_For_Incorrect_Password()
        {
            // Arrange
            var user = new User
            {
                Password = PasswordCryptography.Encrypt("password123")
            };

            // Act
            var isValid = user.ValidatePassword("incorrectpassword");

            // Assert
            Assert.False(isValid);
        }

        [Fact(DisplayName = "Teste UserService: Obter Por Email Deve Retornar Usuário Se Existir")]
        [Trait("Categoria", "Testes de Usuários")]
        public async Task UserService_GetByEmail_Should_Return_User_IfExists()
        {
            // Arrange
            string userEmail = "test@example.com";
            var expectedUser = new User
            {
                Id = Guid.NewGuid(),
                Nome = "Test User",
                Email = userEmail,
                Password = PasswordCryptography.Encrypt("password123")
            };

            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.GetByEmail(userEmail)).ReturnsAsync(expectedUser);

            IUserService userService = new UserService(mockRepository.Object);

            // Act
            var result = await userService.GetByEmail(userEmail);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Nome, result.Nome);
            Assert.Equal(expectedUser.Email, result.Email);
        }
    }
}
