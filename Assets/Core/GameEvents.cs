using UnityEngine;
using System.Collections;
using System;

public static class GameEvents
{
    private static EventDispatcher dispatcher = new EventDispatcher();

    public static void Subscribe<T>(Action<T> callback) where T : class
    {
        dispatcher.Subscribe(callback);
    }
    public static void Unsubscribe<T>(Action<T> callback) where T : class
    {
        dispatcher.Unsubscribe(callback);
    }

    public static void Invoke<T>(T evt) where T : class
    {
    	//Debug.Log ("Dispatching event " + typeof(T).ToString());
        dispatcher.Invoke(evt);
    }
}