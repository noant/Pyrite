using System;
using System.Collections.Generic;

namespace PyriteCore
{
    public class SkeddedList<T> : List<T>
    {
        public new void Add(T item)
        {
            if (CanAdd(item))
                base.Add(item);
        }

        public new void Remove(T item)
        {
            if (CanRemove(item))
                base.Remove(item);
        }

        public new void Insert(int index, T item)
        {
            if (CanAdd(item))
                base.Insert(index, item);
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                this.Add(item);
        }

        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            foreach (var item in collection)
                this.Insert(index++, item);
        }

#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required
        public new void RemoveRange(IEnumerable<T> collection)
#pragma warning restore CS0109 // Member does not hide an inherited member; new keyword is not required
        {
            foreach (var item in collection)
                Remove(item);
        }

        public event ItemWork<T> ItemAdd;
        public event ItemWork<T> ItemRemove;

        public bool CanAdd(T item)
        {
            if (ItemAdd == null)
                return true;
            else
            {
                var args = new ItemWorkEventArgs<T>(item);
                ItemAdd(this, args);
                return !args.Cancel;
            }
        }

        public bool CanRemove(T item)
        {
            if (ItemRemove == null)
                return true;
            else
            {
                var args = new ItemWorkEventArgs<T>(item);
                ItemRemove(this, args);
                return !args.Cancel;
            }
        }

        public static SkeddedList<T> Create(IEnumerable<T> list)
        {
            var sList = new SkeddedList<T>();
            sList.AddRange(list);
            return sList;
        }
    }

    public delegate void ItemWork<T>(object sender, ItemWorkEventArgs<T> e);

    public class ItemWorkEventArgs<T> : EventArgs
    {
        public ItemWorkEventArgs(T item)
        {
            Item = item;
        }
        public bool Cancel { get; set; }
        public T Item { get; private set; }
    }
}
