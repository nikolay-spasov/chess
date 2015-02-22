namespace Chess.Infrastructure.Ioc
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("DependencyContext (ServiceType: {ServiceType}, ImplementationType: {})")]
    public class DependencyContext
    {
        internal static readonly DependencyContext Root = new DependencyContext();

        internal DependencyContext(Type serviceType, Type implementationType)
        {
            this.ServiceType = serviceType;
            this.ImplementationType = implementationType;
        }

        private DependencyContext() { }

        public Type ServiceType { get; private set; }

        public Type ImplementationType { get; private set; }
    }
}
