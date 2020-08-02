using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nsnbc.Auxiliary
{ 
    [JsonObject(MemberSerialization.Fields)]

    public class ImprovedQueue<T> 
    {
        private readonly List<T> list = new List<T>();

        public int Count => list.Count;
        public void QuickEnqueue(T @event)
        {
            list.Insert(0, @event);
        }
        public void QuickEnqueue(T[] events)
        {
            list.InsertRange(0, events);
        }

        public void Enqueue(T @event)
        {
            list.Add(@event);
        }

        public T Dequeue()
        {
            T item = list[0];
            list.RemoveAt(0);
            return item;
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Any(Func<T, bool> func)
        {
            return list.Any(func);
        }
    }
}