using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BusinessLayer.Logic.Extensions
{
	public static class CollectionHelper
	{
		public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> source)
		{
			if (null == source) return null;

			return new ObservableCollection<T>(source);
		}
	}
}
