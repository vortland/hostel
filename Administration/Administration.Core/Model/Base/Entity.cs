
using System;

namespace Administration.Core.Model.Base
{
	public abstract class Entity : IEquatable<Entity>
	{
		public int Id { get; protected set; }


		protected Entity(int id)
		{
			if (Equals(id, default(int)))
			{
				throw new ArgumentException("The ID cannot be the type's default value.", nameof(id));
			}

			Id = id;
		}
		protected Entity()
		{
		}

		public bool Equals(Entity other)
		{
			return other != null && Id.Equals(other.Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == this.GetType() && Equals((Entity) obj);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static bool operator ==(Entity left, Entity right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Entity left, Entity right)
		{
			return !Equals(left, right);
		}
	}
}
