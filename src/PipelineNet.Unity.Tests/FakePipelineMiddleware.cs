using System;

using PipelineNet.Middleware;

namespace PipelineNet.Unity.Tests
{
    public class FakePipelineMiddleware : IMiddleware<FakeEntity>
    {
        private readonly IFakeService _fakeService;

        public FakePipelineMiddleware(IFakeService fakeService)
        {
            _fakeService = fakeService;
        }

        /// <inheritdoc />
        public void Run(FakeEntity entity, Action<FakeEntity> next)
        {
            _fakeService.DoubleNumber(entity);
            next?.Invoke(entity);
        }
    }
}
