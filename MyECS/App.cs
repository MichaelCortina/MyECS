namespace MyECS;

public sealed class App
{
    private readonly CircularList<Action<App>> _systems = new();
    private readonly List<object> _entities = new();

    public App AddEntity(object entity)
    {
        _entities.Add(entity);
        return this;
    }

    public App AddSystem(Action<App> system)
    {
        _systems.Push(system);
        return this;
    }

    public void Run()
    {
        using var eventLoop = _systems.GetEnumerator();
        while (eventLoop.MoveNext())
        {
            eventLoop.Current.Invoke(this);
        }
    }

    public IEnumerable<T> Query<T>() =>
        from entity in _entities
        where entity is T
        select (T) entity;
}