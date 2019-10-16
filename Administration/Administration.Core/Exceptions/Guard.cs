using System;
using System.Collections.Generic;
using System.Linq;

namespace Administration.Core.Exceptions
{
	public static class Guard
	{
		#region Constants

		private const string _messageValueNotAllowed = "The value is not allowed.";

		private const string _messageLess = "less";

		private const string _messageLessOrEqual = "less or equal";

		private const string _messageGreater = "greater";

		private const string _messageGreaterOrEqual = "greater or equal";

		private const string _messageValueIsNotLess = "The value should be " + _messageLess + " than {0}.";

		private const string _messageValueIsNotLessOrEqual = "The value should be " + _messageLessOrEqual + " than {0}.";

		private const string _messageValueIsNotGreater = "The value should be " + _messageGreater + " than {0}.";

		private const string _messageValueIsNotGreaterOrEqual = "The value should be " + _messageGreaterOrEqual + " than {0}.";

		private const string _messageValueIsNotBetween = "The value should be {2} than {0} and {3} than {1}.";

		#endregion

		public static void IsNotDefault<T>(T argumentValue, string argumentName)
		{
			if (argumentValue.Equals(default(T)))
			{
				throw new ArgumentException(_messageValueNotAllowed, argumentName);
			}
		}

		#region Guard From Null Methods

		public static void IsNotNull(object argumentValue, string argumentName)
		{
			if (argumentValue == null)
			{
				throw new ArgumentNullException(argumentName);
			}
		}

		public static void IsNull(object argumentValue, string argumentName)
		{
			if (argumentValue != null)
			{
				throw new ArgumentException(_messageValueNotAllowed, argumentName);
			}
		}

		#endregion

		#region Guard From Empty Methods

		public static void IsNullOrWhiteSpace(string argumentValue, string argumentName)
		{
			if (!string.IsNullOrWhiteSpace(argumentValue))
			{
				throw new ArgumentException(_messageValueNotAllowed, argumentName);
			}
		}

		public static void IsNotEmpty<TElement>(IEnumerable<TElement> argumentValue, string argumentName)
		{
			if (argumentValue != null && !argumentValue.Any())
			{
				throw new ArgumentEmptyException(argumentName);
			}
		}

		public static void IsNotNullOrEmpty<TElement>(IEnumerable<TElement> argumentValue, string argumentName)
		{
			IsNotNull(argumentValue, argumentName);
			IsNotEmpty(argumentValue, argumentName);
		}
		#endregion

		#region Guard From Out Of Range Methods

		public static void IsLess(int? argumentValue, string argumentName, int upperRange)
		{
			if (argumentValue.HasValue && argumentValue.Value >= upperRange)
			{
				throw new ArgumentOutOfRangeException(argumentName,
					string.Format(_messageValueIsNotLess, upperRange));
			}
		}

		public static void IsLessOrEqual(int? argumentValue, string argumentName, int upperRange)
		{
			if (argumentValue.HasValue && argumentValue.Value > upperRange)
			{
				throw new ArgumentOutOfRangeException(argumentName,
					string.Format(_messageValueIsNotLessOrEqual, upperRange));
			}
		}

		public static void IsGreater(int? argumentValue, string argumentName, int lowerRange)
		{
			if (argumentValue.HasValue && argumentValue.Value <= lowerRange)
			{
				throw new ArgumentOutOfRangeException(argumentName,
					string.Format(_messageValueIsNotGreater, lowerRange));
			}
		}

		public static void IsGreaterOrEqual(int? argumentValue, string argumentName, int lowerRange)
		{
			if (argumentValue.HasValue && argumentValue.Value < lowerRange)
			{
				throw new ArgumentOutOfRangeException(argumentName,
					string.Format(_messageValueIsNotGreaterOrEqual, lowerRange));
			}
		}

		public static void IsBetween(int? argumentValue, string argumentName,
			int lowerRange, int upperRange,
			bool isLowerRangeInclusive = true, bool isUpperRangeInclusive = true)
		{
			if (argumentValue.HasValue)
			{
				if ((!isLowerRangeInclusive && argumentValue <= lowerRange) || argumentValue < lowerRange ||
					(!isUpperRangeInclusive && argumentValue >= upperRange) || argumentValue > upperRange)
				{
					throw new ArgumentOutOfRangeException(argumentName,
						string.Format(_messageValueIsNotBetween, lowerRange, upperRange,
						(isLowerRangeInclusive ? _messageGreaterOrEqual : _messageGreater),
						(isUpperRangeInclusive ? _messageLessOrEqual : _messageLess)));
				}
			}
		}

		public static void IsLess(decimal? argumentValue, string argumentName, decimal upperRange)
		{
			if (argumentValue.HasValue && argumentValue.Value >= upperRange)
			{
				throw new ArgumentOutOfRangeException(argumentName,
					string.Format(_messageValueIsNotLess, upperRange));
			}
		}

		public static void IsLessOrEqual(decimal? argumentValue, string argumentName, decimal upperRange)
		{
			if (argumentValue.HasValue && argumentValue.Value > upperRange)
			{
				throw new ArgumentOutOfRangeException(argumentName,
					string.Format(_messageValueIsNotLessOrEqual, upperRange));
			}
		}

		public static void IsGreater(decimal? argumentValue, string argumentName, decimal lowerRange)
		{
			if (argumentValue.HasValue && argumentValue.Value <= lowerRange)
			{
				throw new ArgumentOutOfRangeException(argumentName,
					string.Format(_messageValueIsNotGreater, lowerRange));
			}
		}

		public static void IsGreaterOrEqual(decimal? argumentValue, string argumentName, decimal lowerRange)
		{
			if (argumentValue.HasValue && argumentValue.Value < lowerRange)
			{
				throw new ArgumentOutOfRangeException(argumentName,
					string.Format(_messageValueIsNotGreaterOrEqual, lowerRange));
			}
		}

		public static void IsBetween(decimal? argumentValue, string argumentName,
			decimal lowerRange, decimal upperRange,
			bool isLowerRangeInclusive = true, bool isUpperRangeInclusive = true)
		{
			if (argumentValue.HasValue)
			{
				if ((!isLowerRangeInclusive && argumentValue <= lowerRange) || argumentValue < lowerRange ||
					(!isUpperRangeInclusive && argumentValue >= upperRange) || argumentValue > upperRange)
				{
					throw new ArgumentOutOfRangeException(argumentName,
						string.Format(_messageValueIsNotBetween, lowerRange, upperRange,
						(isLowerRangeInclusive ? _messageGreaterOrEqual : _messageGreater),
						(isUpperRangeInclusive ? _messageLessOrEqual : _messageLess)));
				}
			}
		}

		public static void IsLess(DateTime? argumentValue, string argumentName, DateTime upperRange)
		{
			if (argumentValue.HasValue && argumentValue.Value >= upperRange)
			{
				throw new ArgumentOutOfRangeException(argumentName,
					string.Format(_messageValueIsNotLess, upperRange));
			}
		}

		public static void IsLessOrEqual(DateTime? argumentValue, string argumentName, DateTime upperRange)
		{
			if (argumentValue.HasValue && argumentValue.Value > upperRange)
			{
				throw new ArgumentOutOfRangeException(argumentName,
					string.Format(_messageValueIsNotLessOrEqual, upperRange));
			}
		}

		public static void IsGreater(DateTime? argumentValue, string argumentName, DateTime lowerRange)
		{
			if (argumentValue.HasValue && argumentValue.Value <= lowerRange)
			{
				throw new ArgumentOutOfRangeException(argumentName,
					string.Format(_messageValueIsNotGreater, lowerRange));
			}
		}

		public static void IsGreaterOrEqual(DateTime? argumentValue, string argumentName, DateTime lowerRange)
		{
			if (argumentValue.HasValue && argumentValue.Value < lowerRange)
			{
				throw new ArgumentOutOfRangeException(argumentName,
					string.Format(_messageValueIsNotGreaterOrEqual, lowerRange));
			}
		}

		public static void IsBetween(DateTime? argumentValue, string argumentName,
			DateTime lowerRange, DateTime upperRange,
			bool isLowerRangeInclusive = true, bool isUpperRangeInclusive = true)
		{
			if (argumentValue.HasValue)
			{
				if ((!isLowerRangeInclusive && argumentValue <= lowerRange) || argumentValue < lowerRange ||
					(!isUpperRangeInclusive && argumentValue >= upperRange) || argumentValue > upperRange)
				{
					throw new ArgumentOutOfRangeException(argumentName,
						string.Format(_messageValueIsNotBetween, lowerRange, upperRange,
						(isLowerRangeInclusive ? _messageGreaterOrEqual : _messageGreater),
						(isUpperRangeInclusive ? _messageLessOrEqual : _messageLess)));
				}
			}
		}

		#endregion
	}
}
