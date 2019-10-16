using System;
using System.Collections.Generic;
using System.Linq;

namespace Administration.Core.Model.Base
{
	public abstract class ValueObject<T> : IEquatable<T> where T : ValueObject<T>
	{
		public bool Equals(T other)
		{
			return other != null && Equals(other);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != GetType())
			{
				return false;
			}

			var other = (ValueObject<T>)obj;
			var thisValues = GetAtomicValues().GetEnumerator();
			var otherValues = other.GetAtomicValues().GetEnumerator();

			while (thisValues.MoveNext() && otherValues.MoveNext())
			{
				if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
				{
					return false;
				}
				if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
				{
					return false;
				}
			}
			return !thisValues.MoveNext() && !otherValues.MoveNext();
		}

		protected abstract IEnumerable<object> GetAtomicValues();

		public override int GetHashCode()
		{
			return GetAtomicValues()
				.Select(x => x != null ? x.GetHashCode() : 0)
				.Aggregate((x, y) => x ^ y);
		}

		public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
		{
			return !Equals(left, right);
		}
	}
}
