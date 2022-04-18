namespace ATM.API.Repositories;

public interface IRepository<T, ID>
{
    public T Find(ID id);
    public void Add(T item);
    public void Remove(T item);
}
