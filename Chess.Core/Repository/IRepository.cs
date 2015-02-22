namespace Chess.Core.Repository
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetById();
    }
}
