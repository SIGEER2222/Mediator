using Mediator.Tests.TestTypes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;

namespace Mediator.Tests
{
    public sealed class SenderTests
    {
        [Fact]
        public async Task Test_Request_Handler()
        {
            var (_, mediator) = Fixture.GetMediator();
            ISender sender = mediator;

            var id = Guid.NewGuid();

            var response = await sender.Send(new SomeRequest(id));
            Assert.NotNull(response);
            Assert.Equal(id, response.Id);
        }

        [Fact]
        public async Task Test_RequestWithoutResponse_Handler()
        {
            var (sp, mediator) = Fixture.GetMediator();
            ISender sender = mediator;

            var id = Guid.NewGuid();

            var handler = sp.GetRequiredService<SomeRequestWithoutResponseHandler>();
            await sender.Send(new SomeRequestWithoutResponse(id));
            Assert.Equal(id, handler.Id);
        }

        [Fact]
        public async Task Test_Command_Handler()
        {
            var (_, mediator) = Fixture.GetMediator();
            ISender sender = mediator;

            var id = Guid.NewGuid();

            var response = await sender.Send(new SomeCommand(id));
            Assert.NotNull(response);
            Assert.Equal(id, response.Id);
        }

        [Fact]
        public async Task Test_CommandWithoutResponse_Handler()
        {
            var (sp, mediator) = Fixture.GetMediator();
            ISender sender = mediator;

            var id = Guid.NewGuid();

            var handler = sp.GetRequiredService<SomeCommandWithoutResponseHandler>();
            await sender.Send(new SomeCommandWithoutResponse(id));
            Assert.Equal(id, handler.Id);
        }

        [Fact]
        public async Task Test_Query_Handler()
        {
            var (_, mediator) = Fixture.GetMediator();
            ISender sender = mediator;

            var id = Guid.NewGuid();

            var response = await sender.Send(new SomeQuery(id));
            Assert.NotNull(response);
            Assert.Equal(id, response.Id);
        }

        [Fact]
        public async Task Test_Fails_On_Dynamic_Type()
        {
            var (_, mediator) = Fixture.GetMediator();
            ISender sender = mediator;

            var id = Guid.NewGuid();
            object obj = new { Id = id };

            var request = Unsafe.As<object, IRequest>(ref obj);
            await Assert.ThrowsAsync<MissingMessageHandlerException>(async () => await sender.Send(request));
        }
    }
}