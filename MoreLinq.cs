using System;
using System.Collections.Generic;

namespace Cabinet
{
	public static class MoreLinq
	{
		public static int LastIndexFor<T>(this IEnumerable<T> items, Predicate<T> pred)
		{
			var index = -1;
			foreach (var item in items)
			{
				if (!pred(item))
					return index;
				++index;
			}
			return -1;
		}
	}
}
