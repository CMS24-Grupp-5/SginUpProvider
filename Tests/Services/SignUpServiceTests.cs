using System.Threading.Tasks;
using Moq;

using Xunit;
using Grpc.Core;
using Grpc.Core.Testing;
using Xunit.Sdk;
using Presentation;
using WebApplication1.Models;
using WebApplication1.Services;

namespace Tests.Services
{
    public class SignUpServiceTests
    {
        [Fact]
        public async Task SignUpAsync_ShouldReturnSuccess_WhenGrpcReturnsSuccess()
        {
            // Arrange  
            var mockClient = new Mock<AccountGrpcService.AccountGrpcServiceClient>();

            var response = new CreateAccountReply
            {
                Succeeded = true,
                Message = "Account created successfully",
                UserId ="1"
            };
            // Chat gpt4o
            var asyncUnaryCall = TestCalls.AsyncUnaryCall(
                Task.FromResult(response),
                Task.FromResult(new Metadata()),
                () => new Status(StatusCode.OK, string.Empty),
                () => [],
                () => { }
            );

            mockClient
                .Setup(client => client.CreateAccountAsync(
                    It.IsAny<CreateAccountRequest>(), null, null, default))
                .Returns(asyncUnaryCall);

            var service = new SignUpService(mockClient.Object);

            var formData = new SignUpFormData
            {
                Email = "Yaarub.nasser@yahoo.com",
                Password = "Test.123"
            };

            // Act  
            var result = await service.SignUpAsync(formData);

            // Assert  
            Assert.True(result.Success);
            Assert.Equal("Account created successfully", result.Message);
            Assert.Equal("1", result.UserId);
        }

        [Fact]
        public async Task SignUpAsync_ShouldReturnFailure_WhenGrpcThrows()
        {
            // Arrange  
            var mockClient = new Mock<AccountGrpcService.AccountGrpcServiceClient>();
            //chat Gpt4o
            mockClient
                .Setup(client => client.CreateAccountAsync(
                    It.IsAny<CreateAccountRequest>(), null, null, default))
                .Throws(new RpcException(new Status(StatusCode.Internal, "gRPC error")));

            var service = new SignUpService(mockClient.Object);

            var formData = new SignUpFormData
            {
                Email = "Yaarub.Nassr@yahoo.com",
                Password = "Test.123"
            };

            // Act  
            var result = await service.SignUpAsync(formData);

            // Assert  
            Assert.False(result.Success);
            Assert.Equal("An error occurred while processing your request.", result.Message);
        }
    }
}