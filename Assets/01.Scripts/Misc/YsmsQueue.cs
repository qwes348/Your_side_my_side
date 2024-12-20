using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ysms
{
    public class YsmsQueue<T>
    {
        private List<T> fifoList = new List<T>();
        
        public int Count => fifoList.Count;
        public T this[int index] => fifoList[index];

        public void Enqueue(T item)
        {
            fifoList.Add(item);
        }

        public T Dequeue()
        {
            T item = fifoList.First();
            fifoList.RemoveAt(0);
            return item;
        }

        public T Peek()
        {
            return fifoList[0];
        }

        public int IndexOf(T item)
        {
            int ret = 0;
            foreach (T i in fifoList)
            {
                if (i.Equals(item))
                    return ret;
                ret++;
            }

            return ret;
        }
        
        public T Find(Predicate<T> predicate)
        {
            T item = default(T);
            item = predicate.Invoke(item) ? item : default(T);
            return item;
        }

        public IEnumerable<T> GetEnumerable()
        {
            return fifoList;
        }
    }
}