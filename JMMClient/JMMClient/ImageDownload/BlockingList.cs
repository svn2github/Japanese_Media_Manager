﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace JMMClient.ImageDownload
{
	public class BlockingList<T> : IEnumerable<T>
	{
		private List<T> _list;
		private readonly object _syncRoot;
		private int _count = 0;
		private int _size = 0;

		public int Count
		{
			get { return _count; }
		}

		public void Add(T data)
		{
			Add(data, Timeout.Infinite);
		}

		public void Add(T data, int millisecondsTimeout)
		{
			if (data == null) throw new ArgumentNullException("data");

			lock (_syncRoot)
			{
				while (_count == _size)
				{
					try
					{
						// Monitor exited with exception.
						// Could be owner thread of monitor
						// object was terminated or timeout
						// on wait. Pulse any/all waiting
						// threads to ensure we don't get
						// any "live locked" producers.
						if (!Monitor.Wait(_syncRoot, millisecondsTimeout))
							throw new Exception("Could not add");
					}
					catch
					{
						Monitor.PulseAll(_syncRoot);
						throw;
					}
				}

				_list.Add(data);
				_count++;
				if (_count == 1)
					// could have blocking Dequeue thread(s).
					Monitor.PulseAll(_syncRoot);
			}
		}

		public void Clear()
		{
			_count = 0;
			lock (_list)
			{
				_list.Clear();
			}
		}

		public void Remove(T data)
		{
			_count--;
			lock (_list) _list.Remove(data);
		}

		public T GetNextItem()
		{
			return GetNextItem(Timeout.Infinite);
		}

		public T GetNextItem(int millisecondsTimeout)
		{
			lock (_syncRoot)
			{
				while (_count == 0)
				{
					try
					{
						if (!Monitor.Wait(_syncRoot, millisecondsTimeout))
							throw new Exception("Could not get next item");
					}
					catch
					{
						Monitor.PulseAll(_syncRoot);
						throw;
					}
				}

				if (_count == (_size - 1))
					// could have blocking Enqueue thread(s).
					Monitor.PulseAll(_syncRoot);

				return _list[0];
			}
		}

		public BlockingList(int size)
		{
			if (size <= 0) throw new ArgumentOutOfRangeException("size");

			_size = size;
			_syncRoot = new object();
			_list = new List<T>(size);
		}

		public BlockingList()
		{
			_size = int.MaxValue;
			_syncRoot = new object();
			_list = new List<T>();
		}

		public bool Contains(T data)
		{
			lock (_list) return _list.Contains(data);
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			while (true) yield return GetNextItem();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}
	}
}
