using System;

using PipelineNet.Middleware;

namespace PipelineNet.Unity.Tests
{
    public class FakeChainOfResponsibilityMiddleware : IMiddleware<FakeEntity, int>
    {
        private readonly IFakeService _fakeService;

        public FakeChainOfResponsibilityMiddleware(IFakeService fakeService)
        {
            _fakeService = fakeService;
        }

        /// <inheritdoc />
        public int Run(FakeEntity entity, Func<FakeEntity, int> next)
        {
            _fakeService.DoubleNumber(entity);
            return next?.Invoke(entity) ?? entity.Number;
        }
    }
}
