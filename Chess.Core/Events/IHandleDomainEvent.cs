namespace Chess.Core.Events
{
    public interface IHandleDomainEvent<T> where T : IDomainEvents
    {
        void Handle(T args);
    }
}
