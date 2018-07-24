namespace PipelineNet.Unity.Tests
{
    public sealed class FakeService : IFakeService
    {
        /// <inheritdoc />
        public void DoubleNumber(FakeEntity entity)
        {
            entity.Number *= 2;
        }
    }
}