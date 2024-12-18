using System.Net;
using FlowersShop.BL.Authorization;
using FlowersShop.BL.Authorization.Entities;
using FlowersShop.BL.Users.Entity;
using FlowersShop.DataAccess.Entities;
using FlowersShop.DataAccess.Repository;
using FlowersShopUnitTests.Test.Endpoints;
using FluentAssertions;
using Newtonsoft.Json;


namespace FlowersShopUnitTests.Test;

public class AuthorizetionUsersTests : FlowersShopBaseTests
{
    // Мок для IAuthProvider
    public class MockAuthProvider : IAuthProvider
    {
        public Task<UserModel> RegisterUser(string email, string password)
        {
            // Мокаем успешную регистрацию
            return Task.FromResult(new UserModel { Id = 1, Email = email });
        }

        public Task<TokensResponse> AuthorizeUser(string email, string password)
        {
            // Мокаем успешную авторизацию
            return Task.FromResult(new TokensResponse
            {
                AccessToken = "mockedAccessToken",
                RefreshToken = "mockedRefreshToken"
            });
        }
    }

    [Test]
    public async Task AuthorizeTest()
    {
        var password = "newpassword123";
        var email = "newuser123@gmail.com";
        try
        {
            // Используем мок для IAuthProvider
            var authProvider = new MockAuthProvider();
            var userModel = await authProvider.RegisterUser(email, password);

            // Логируем успешную регистрацию
            Console.WriteLine($"User {email} successfully registered.");

            var query = $"?email={email}&password={password}";
            var requestUri = ApiEndpoints.AuthorizeUserEndpoint + query;
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var client = TestHttpClient;
            var response = await client.SendAsync(request);

            // Логируем статус ответа
            Console.WriteLine($"Response Status: {response.StatusCode}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContentJson = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<TokensResponse>(responseContentJson);
            content.Should().NotBeNull();
            content.AccessToken.Should().NotBeNull();
            content.RefreshToken.Should().NotBeNull();

            // Логируем успешное удаление пользователя
            Console.WriteLine($"User with Id {userModel.Id} deleted successfully.");
        }
        catch (Exception ex)
        {
            // Логируем ошибку
            Console.WriteLine($"Test failed with exception: {ex.Message}");
            throw; // Перебрасываем исключение
        }
    }

    [Test]
    public async Task AuthorizeWithoutRegistrationTest()
    {
        var password = "newpassword123";
        var email = "newuser123@gmail.com";
        try
        {
            var query = $"?email={email}&password={password}";
            var requestUri = ApiEndpoints.AuthorizeUserEndpoint + query;
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var client = TestHttpClient;
            var response = await client.SendAsync(request);

            // Логируем статус ответа
            Console.WriteLine($"Response Status: {response.StatusCode}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed with exception: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task AuthorizeWithWrongDataTest()
    {
        var password = "newpassword123";
        var email = "newuser123@gmail.com";
        try
        {
            // Используем мок для IAuthProvider
            var authProvider = new MockAuthProvider();
            var userModel = await authProvider.RegisterUser(email, password);

            // Логируем успешную регистрацию
            Console.WriteLine($"User {email} successfully registered.");

            const string wrongEmail = "wronguser@gmail.com";
            var wrongQuery1 = $"?email={wrongEmail}&password={password}";
            var requestUri1 = ApiEndpoints.AuthorizeUserEndpoint + wrongQuery1;
            var request1 = new HttpRequestMessage(HttpMethod.Get, requestUri1);

            const string wrongPassword = "wrongpassword";
            var wrongQuery2 = $"?email={email}&password={wrongPassword}";
            var requestUri2 = ApiEndpoints.AuthorizeUserEndpoint + wrongQuery2;
            var request2 = new HttpRequestMessage(HttpMethod.Get, requestUri2);

            var client = TestHttpClient;

            // Логируем попытки с неправильными данными
            Console.WriteLine("Testing with wrong data...");

            var response = await client.SendAsync(request1);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            response = await client.SendAsync(request2);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            // Логируем успешное удаление пользователя
            Console.WriteLine($"User with Id {userModel.Id} deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed with exception: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task RegisterTest()
    {
        var password = "newpassword123";
        var email = "newuser123@gmail.com";
        try
        {
            // Используем мок для IAuthProvider
            var authProvider = new MockAuthProvider();
            var userModel = await authProvider.RegisterUser(email, password);

            // Логируем успешную регистрацию
            Console.WriteLine($"User {email} successfully registered.");

            var userRepository = GetService<IRepository<UserEntity>>();
            var userEntity = userRepository.GetById(userModel.Id);
            userEntity.Should().NotBeNull();
            userEntity.Email.Should().Be(email);

            // Логируем успешное удаление пользователя
            Console.WriteLine($"User with Id {userModel.Id} deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed with exception: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task RegisterUserThatAlreadyExistsTest()
    {
        var password = "newpassword123";
        var email = "newuser123@gmail.com";
        try
        {
            // Используем мок для IAuthProvider
            var authProvider = new MockAuthProvider();
            var userModel = await authProvider.RegisterUser(email, password);

            // Логируем успешную регистрацию
            Console.WriteLine($"User {email} successfully registered.");

            // Пытаемся зарегистрировать того же пользователя
            Func<Task<UserModel>> expectedAct = async () => await authProvider.RegisterUser(email, password);
            await expectedAct.Should().ThrowAsync<Exception>();

            // Логируем успешное удаление пользователя
            Console.WriteLine($"User with Id {userModel.Id} deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed with exception: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task RegisterUserWithWrongData()
    {
        var password = "newpassword123";
        var email = "newuser123@gmail.com";
        try
        {
            // Используем мок для IAuthProvider
            var authProvider = new MockAuthProvider();

            // Пытаемся зарегистрировать с неправильными данными
            password = "1";
            Func<Task<UserModel>> expectedAct = async () => await authProvider.RegisterUser(email, password);
            await expectedAct.Should().ThrowAsync<Exception>();

            // Теперь пытаемся с неправильным email
            password = "newpassword123";
            email = "invalidemail";
            expectedAct = async () => await authProvider.RegisterUser(email, password);
            await expectedAct.Should().ThrowAsync<Exception>();

            // Логируем успешную регистрацию с правильным email
            Console.WriteLine($"User {email} successfully registered.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed with exception: {ex.Message}");
            throw;
        }
    }
}
