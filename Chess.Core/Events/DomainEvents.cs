namespace Chess.Core.Events
{
    using System;

    using SimpleInjector;

    public static class DomainEvents
    {
        private static Container Container { get; set; }

        public static void SetContainer(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            Container = container;
        }

        public static void Raise<T>(T eventArg) where T : IDomainEvents
        {
            foreach (var handler in Container.GetAllInstances<IHandleDomainEvent<T>>())
            {
                handler.Handle(eventArg);
            }
        }
    }
}
