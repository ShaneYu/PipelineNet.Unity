using System;

using PipelineNet.MiddlewareResolver;

using Unity;

namespace PipelineNet.Unity
{
    public class UnityMiddlewareResolver : IMiddlewareResolver
    {
        private readonly IUnityContainer _unityContainer;

        public UnityMiddlewareResolver(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        /// <inheritdoc />
        public object Resolve(Type type)
        {
            return _unityContainer.Resolve(type);
        }
    }
}
