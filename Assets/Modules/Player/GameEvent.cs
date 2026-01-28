using System;
public class GameEvent{
    // null-safe
    private event Action guardador = delegate{};

    public void Add(Action subs)
    {
        guardador += subs;
    }

    public void Remove(Action subs)
    {
        guardador -= subs;
    }

    public void Publish()
    {
        guardador?.Invoke();
    }
}