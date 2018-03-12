using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace OpenCGSS.Director.Modules.SldProject.Core {
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
        public InternalList([NotNull, ItemCanBeNull] IEnumerable<T> collection) {
            _list = new List<T>(collection);
        }

        [DebuggerStepThrough]
        [NotNull, ItemCanBeNull]
        public IReadOnlyList<T> AsReadOnly() {
            return _list.AsReadOnly();
        }

        [DebuggerStepThrough]
        public int BinarySearch([NotNull] T item) {
            return _list.BinarySearch(item);
        }

        [DebuggerStepThrough]
        public int BinarySearch([NotNull] T item, [NotNull] IComparer<T> comparer) {
            return _list.BinarySearch(item, comparer);
        }

        [DebuggerStepThrough]
        public int BinarySearch(int index, int count, T item, [NotNull] IComparer<T> comparer) {
            return _list.BinarySearch(index, count, item, comparer);
        }

        [DebuggerStepThrough]
        [NotNull, ItemCanBeNull]
        public List<TOutput> ConvertAll<TOutput>([NotNull] Converter<T, TOutput> converter) {
            return _list.ConvertAll(converter);
        }

        [DebuggerStepThrough]
        public bool Exists([NotNull] Predicate<T> match) {
            return _list.Exists(match);
        }

        [DebuggerStepThrough]
        [CanBeNull]
        public T Find([NotNull] Predicate<T> match) {
            return _list.Find(match);
        }

        [DebuggerStepThrough]
        [NotNull, ItemCanBeNull]
        public IList<T> FindAll([NotNull] Predicate<T> match) {
            return _list.FindAll(match);
        }

        [DebuggerStepThrough]
        public int FindIndex([NotNull] Predicate<T> match) {
            return _list.FindIndex(match);
        }

        [DebuggerStepThrough]
        public int FindIndex(int startIndex, [NotNull] Predicate<T> match) {
            return _list.FindIndex(startIndex, match);
        }

        [DebuggerStepThrough]
        public int FindIndex(int startIndex, int count, [NotNull] Predicate<T> match) {
            return _list.FindIndex(startIndex, count, match);
        }

        [DebuggerStepThrough]
        [CanBeNull]
        public T FindLast([NotNull] Predicate<T> match) {
            return _list.FindLast(match);
        }

        [DebuggerStepThrough]
        public int FindLastIndex([NotNull] Predicate<T> match) {
            return _list.FindLastIndex(match);
        }

        [DebuggerStepThrough]
        public int FindLastIndex(int startIndex, [NotNull] Predicate<T> match) {
            return _list.FindLastIndex(startIndex, match);
        }

        [DebuggerStepThrough]
        public int FindLastIndex(int startIndex, int count, [NotNull] Predicate<T> match) {
            return _list.FindLastIndex(startIndex, count, match);
        }

        [DebuggerStepThrough]
        public void ForEach([NotNull] Action<T> action) {
            _list.ForEach(action);
        }

        [DebuggerStepThrough]
        [NotNull, ItemCanBeNull]
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
        public void InsertRange(int index, [NotNull, ItemCanBeNull] IEnumerable<T> collection) {
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
        public int RemoveAll([NotNull] Predicate<T> match) {
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
        public void Sort([NotNull] Comparison<T> comparison) {
            _list.Sort(comparison);
        }

        [DebuggerStepThrough]
        public void Sort([NotNull] IComparer<T> comparer) {
            _list.Sort(comparer);
        }

        [DebuggerStepThrough]
        public void Sort(int index, int count, [NotNull] IComparer<T> comparer) {
            _list.Sort(index, count, comparer);
        }

        [DebuggerStepThrough]
        [NotNull, ItemCanBeNull]
        public T[] ToArray() {
            return _list.ToArray();
        }

        [DebuggerStepThrough]
        public void TrimExcess() {
            _list.TrimExcess();
        }

        [DebuggerStepThrough]
        public bool TrueForAll([NotNull] Predicate<T> match) {
            return _list.TrueForAll(match);
        }

        [DebuggerStepThrough]
        public bool Contains([CanBeNull] T item) {
            return _list.Contains(item);
        }

        [DebuggerStepThrough]
        public void CopyTo([NotNull, ItemCanBeNull] T[] array) {
            _list.CopyTo(array);
        }

        [DebuggerStepThrough]
        public void CopyTo([NotNull, ItemCanBeNull] T[] array, int arrayIndex) {
            _list.CopyTo(array, arrayIndex);
        }

        [DebuggerStepThrough]
        public void CopyTo(int index, [NotNull, ItemCanBeNull] T[] array, int arrayIndex, int count) {
            _list.CopyTo(index, array, arrayIndex, count);
        }

        public int Count {
            [DebuggerStepThrough]
            get => _list.Count;
        }

        public bool IsReadOnly {
            [DebuggerStepThrough]
            get => false;
        }

        [DebuggerStepThrough]
        public int IndexOf(T item) {
            return _list.IndexOf(item);
        }

        [CanBeNull]
        public T this[int index] {
            [DebuggerStepThrough]
            get => _list[index];
            [DebuggerStepThrough]
            internal set => _list[index] = value;
        }

        [DebuggerStepThrough]
        public IEnumerator<T> GetEnumerator() {
            return _list.GetEnumerator();
        }

        [DebuggerStepThrough]
        internal void Add([CanBeNull] T item) {
            _list.Add(item);
        }

        [DebuggerStepThrough]
        internal void AddRange([NotNull, ItemCanBeNull] IEnumerable<T> collection) {
            _list.AddRange(collection);
        }

        [DebuggerStepThrough]
        internal void Clear() {
            _list.Clear();
        }

        [DebuggerStepThrough]
        internal bool Remove([CanBeNull] T item) {
            return _list.Remove(item);
        }

        [DebuggerStepThrough]
        internal void Insert(int index, [CanBeNull] T item) {
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

