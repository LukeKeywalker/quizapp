using System;
using System.Collections.Generic;

public class EventDispatcher
{
    private Dictionary<System.Type, Action<object>> events = new Dictionary<Type, Action<object>>();
    private Dictionary<object, Action<object>> lookupTable = new Dictionary<object, Action<object>>();

    public void Subscribe<T>(Action<T> callback) where T : class
    {
        Action<object> objCallback = (o) => callback((T)o);

        if (events.ContainsKey(typeof(T)))
            events[typeof(T)] = events[typeof(T)] + objCallback;
        else
            events.Add(typeof(T), objCallback);

        lookupTable.Add(callback, objCallback);
    }

    public void Unsubscribe<T>(Action<T> callback) where T : class
    {
        events[typeof(T)] -= lookupTable[callback];
        lookupTable.Remove(callback);
    }

    public void Invoke<T>(T evt) where T : class
    {
        Action<object> callback;

        if (events.TryGetValue(typeof(T), out callback) && callback != null)
            callback(evt);
    }
}