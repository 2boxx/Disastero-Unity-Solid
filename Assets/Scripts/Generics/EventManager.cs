using System.Collections;
using System.Collections.Generic;

public class EventManager {

	public delegate void EventCallback(params object[] parameters);
	private static Dictionary<string, EventCallback> EventDictionary;

	public static void Subscribe(string subscribedEvent, EventCallback function) {
        if (EventDictionary == null)
			EventDictionary = new Dictionary<string, EventCallback>();

		if (!EventDictionary.ContainsKey(subscribedEvent))
			EventDictionary.Add(subscribedEvent, null);
		EventDictionary[subscribedEvent] += function;

	}

	public static void Unsubscribe(string subscribedEvent, EventCallback function) {

        if (EventDictionary == null)
			return;

		if (!EventDictionary.ContainsKey(subscribedEvent))
			return;

		EventDictionary[subscribedEvent] -= function;
	}

	public static void CallEvent(string eventName, params object[] parameters) {

        if (EventDictionary == null) return;

		if (EventDictionary.ContainsKey(eventName))
			EventDictionary[eventName](parameters);
	}
}