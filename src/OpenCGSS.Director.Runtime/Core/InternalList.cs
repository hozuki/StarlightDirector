using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace OpenCGSS.Director.Core {
    public class InternalList<T> : IReadOnlyList<T> {

        [DebuggerStepThrough]
        public InternalList() {
            _list = new List<T>();
        }

        [DebuggerStepThrough]
        public InternalList(int capacity) {
            _list = new List<T>(capacity);
        }

        [DebuggerStepThrough]
        public InternalList(IEnumerable<T> collection) {
            _list = new List<T>(collection);
        }

        [DebuggerStepThrough]
        public IReadOnlyList<T> AsReadOnly() {
            return _list.AsReadOnly();
        }

        [DebuggerStepThrough]
        public int BinarySearch(T item) {
            return _list.BinarySearch(item);
        }

        [DebuggerStepThrough]
        public int BinarySearch(T item, IComparer<T> comparer) {
            return _list.BinarySearch(item, comparer);
        }

        [DebuggerStepThrough]
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer) {
            return _list.BinarySearch(index, count, item, comparer);
        }

        [DebuggerStepThrough]
        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) {
            return _list.ConvertAll(converter);
        }

        [DebuggerStepThrough]
        public bool Exists(Predicate<T> match) {
            return _list.Exists(match);
        }

        [DebuggerStepThrough]
        public T Find(Predicate<T> match) {
            return _list.Find(match);
        }

        [DebuggerStepThrough]
        public IList<T> FindAll(Predicate<T> match) {
            return _list.FindAll(match);
        }

        [DebuggerStepThrough]
        public int FindIndex(Predicate<T> match) {
            return _list.FindIndex(match);
        }

        [DebuggerStepThrough]
        public int FindIndex(int startIndex, Predicate<T> match) {
            return _list.FindIndex(startIndex, match);
        }

        [DebuggerStepThrough]
        public int FindIndex(int startIndex, int count, Predicate<T> match) {
            return _list.FindIndex(startIndex, count, match);
        }

        [DebuggerStepThrough]
        public T FindLast(Predicate<T> match) {
            return _list.FindLast(match);
        }

        [DebuggerStepThrough]
        public int FindLastIndex(Predicate<T> match) {
            return _list.FindLastIndex(match);
        }

        [DebuggerStepThrough]
        public int FindLastIndex(int startIndex, Predicate<T> match) {
            return _list.FindLastIndex(startIndex, match);
        }

        [DebuggerStepThrough]
        public int FindLastIndex(int startIndex, int count, Predicate<T> match) {
            return _list.FindLastIndex(startIndex, count, match);
        }

        [DebuggerStepThrough]
        public void ForEach(Action<T> action) {
            _list.ForEach(action);
        }

        [DebuggerStepThrough]
        public IList<T> GetRange(int index, int count) {
            return _list.GetRange(index, count);
        }

        [DebuggerStepThrough]
        public int IndexOf(T item, int index) {
            return _list.IndexOf(item, index);
        }

        [DebuggerStepThrough]
        public int IndexOf(T item, int index, int count) {
            return _list.IndexOf(item, index, count);
        }

        [DebuggerStepThrough]
        public void InsertRange(int index, IEnumerable<T> collection) {
            _list.InsertRange(index, collection);
        }

        [DebuggerStepThrough]
        public int LastIndexOf(T item) {
            return _list.LastIndexOf(item);
        }

        [DebuggerStepThrough]
        public int LastIndexOf(T item, int index) {
            return _list.LastIndexOf(item, index);
        }

        [DebuggerStepThrough]
        public int LastIndexOf(T item, int index, int count) {
            return _list.LastIndexOf(item, index, count);
        }

        [DebuggerStepThrough]
        public int RemoveAll(Predicate<T> match) {
            return _list.RemoveAll(match);
        }

        [DebuggerStepThrough]
        public void RemoveRange(int index, int count) {
            _list.RemoveRange(index, count);
        }

        [DebuggerStepThrough]
        public void Reverse() {
            _list.Reverse();
        }

        [DebuggerStepThrough]
        public void Reverse(int index, int count) {
            _list.Reverse(index, count);
        }

        [DebuggerStepThrough]
        public void Sort() {
            _list.Sort();
        }

        [DebuggerStepThrough]
        public void Sort(Comparison<T> comparison) {
            _list.Sort(comparison);
        }

        [DebuggerStepThrough]
        public void Sort(IComparer<T> comparer) {
            _list.Sort(comparer);
        }

        [DebuggerStepThrough]
        public void Sort(int index, int count, IComparer<T> comparer) {
            _list.Sort(index, count, comparer);
        }

        [DebuggerStepThrough]
        public T[] ToArray() {
            return _list.ToArray();
        }

        [DebuggerStepThrough]
        public void TrimExcess() {
            _list.TrimExcess();
        }

        [DebuggerStepThrough]
        public bool TrueForAll(Predicate<T> match) {
            return _list.TrueForAll(match);
        }

        [DebuggerStepThrough]
        public bool Contains(T item) {
            return _list.Contains(item);
        }

        [DebuggerStepThrough]
        public void CopyTo(T[] array) {
            _list.CopyTo(array);
        }

        [DebuggerStepThrough]
        public void CopyTo(T[] array, int arrayIndex) {
            _list.CopyTo(array, arrayIndex);
        }

        [DebuggerStepThrough]
        public void CopyTo(int index, T[] array, int arrayIndex, int count) {
            _list.CopyTo(index, array, arrayIndex, count);
        }

        public int Count {
            [DebuggerStepThrough]
            get { return _list.Count; }
        }

        public bool IsReadOnly {
            [DebuggerStepThrough]
            get { return false; }
        }

        [DebuggerStepThrough]
        public int IndexOf(T item) {
            return _list.IndexOf(item);
        }

        public T this[int index] {
            [DebuggerStepThrough]
            get { return _list[index]; }
            [DebuggerStepThrough]
            internal set { _list[index] = value; }
        }

        [DebuggerStepThrough]
        public IEnumerator<T> GetEnumerator() {
            return _list.GetEnumerator();
        }

        [DebuggerStepThrough]
        internal void Add(T item) {
            _list.Add(item);
        }

        [DebuggerStepThrough]
        internal void AddRange(IEnumerable<T> collection) {
            _list.AddRange(collection);
        }

        [DebuggerStepThrough]
        internal void Clear() {
            _list.Clear();
        }

        [DebuggerStepThrough]
        internal bool Remove(T item) {
            return _list.Remove(item);
        }

        [DebuggerStepThrough]
        internal void Insert(int index, T item) {
            _list.Insert(index, item);
        }

        [DebuggerStepThrough]
        internal void RemoveAt(int index) {
            _list.RemoveAt(index);
        }

        [DebuggerStepThrough]
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        private readonly List<T> _list;

    }
}

