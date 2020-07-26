using System.Collections.Generic;
using Newtonsoft.Json;
using Nsnbc.PostSharp;

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
    }
}