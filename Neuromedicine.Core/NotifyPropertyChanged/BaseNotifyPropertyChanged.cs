using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Neuromedicine.Core.NotifyPropertyChanged
{
	public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		///  Вызывает событие PropertyChanged изменения свойства экземпляра класса
		/// </summary>
		/// <param name="propertyName">название свойства</param>
		protected void SendPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}


		protected void SendPropertyChanged<T>(Expression<Func<T>> expression)
		{
			SendPropertyChanged(GetPropertyName(expression));
		}

		/// <summary>
		/// Возвращает имя свойства
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="P"></typeparam>
		/// <param name="propertyId">Свойство</param>
		/// <returns>Возвращает имя свойства</returns>
		private static string GetPropertyName<T>(Expression<Func<T>> propertyId)
		{
			var memberExpression = propertyId.Body as MemberExpression;

			if (null != memberExpression)
			{
				return memberExpression.Member.Name;
			}

			return null;
		}
	}
}
