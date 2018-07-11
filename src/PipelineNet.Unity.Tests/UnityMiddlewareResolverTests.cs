using FluentAssertions;

using Unity;
using Unity.Exceptions;

using Xunit;

namespace PipelineNet.Unity.Tests
{
    public class UnityMiddlewareResolverShould
    {
        [Fact]
        public void SuccessfullyResolvePipelineMiddleware()
        {
            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<IFakeService, FakeService>();

            var unityMiddlewareResolver = new UnityMiddlewareResolver(unityContainer);
            var middleware = unityMiddlewareResolver.Resolve(typeof(FakePipelineMiddleware));

            middleware.Should().NotBeNull().And.BeOfType<FakePipelineMiddleware>();

            var initialEntity = new FakeEntity { Number = 2 };
            ((FakePipelineMiddleware) middleware).Run(initialEntity, finalEntity => { finalEntity.Number.Should().Be(4); });
        }

        [Fact]
        public void SuccessfullyResolveChainOfResponsibilityMiddleware()
        {
            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<IFakeService, FakeService>();

            var unityMiddlewareResolver = new UnityMiddlewareResolver(unityContainer);
            var middleware = unityMiddlewareResolver.Resolve(typeof(FakeChainOfResponsibilityMiddleware));

            middleware.Should().NotBeNull().And.BeOfType<FakeChainOfResponsibilityMiddleware>();

            var initialEntity = new FakeEntity { Number = 2 };
            var result = ((FakeChainOfResponsibilityMiddleware) middleware).Run(initialEntity, entity => entity.Number);
            result.Should().Be(4);
        }

        [Fact]
        public void ThrowExceptionWhenPipelineMiddlewareCannotBeResolved()
        {
            var unityContainer = new UnityContainer();
            var unityMiddlewareResolver = new UnityMiddlewareResolver(unityContainer);

            unityMiddlewareResolver.Invoking(x => x.Resolve(typeof(FakePipelineMiddleware)))
                .Should().ThrowExactly<ResolutionFailedException>()
                .Which.Message.Should()
                .ContainAll(
                    "Resolution of the dependency failed",
                    "PipelineNet.Unity.Tests.FakePipelineMiddleware",
                    "PipelineNet.Unity.Tests.IFakeService");
        }

        [Fact]
        public void ThrowExceptionWhenChainOfResponsibilityMiddlewareCannotBeResolved()
        {
            var unityContainer = new UnityContainer();
            var unityMiddlewareResolver = new UnityMiddlewareResolver(unityContainer);

            unityMiddlewareResolver
                .Invoking(resolver => resolver.Resolve(typeof(FakeChainOfResponsibilityMiddleware)))
                .Should().ThrowExactly<ResolutionFailedException>()
                .Which.Message.Should()
                .ContainAll(
                    "Resolution of the dependency failed",
                    "PipelineNet.Unity.Tests.FakeChainOfResponsibilityMiddleware",
                    "PipelineNet.Unity.Tests.IFakeService");
        }
    }
}
