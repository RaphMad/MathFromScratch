namespace ImmutableNumbers
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
   using EqualityExtensions;
   using PrintingExtensions;

   /// <summary>
   /// A set describing a  natural number.
   /// </summary>
   public sealed class SetNatural : IEquatable<SetNatural>, ISet<SetNatural>
   {
      /// <summary>
      /// Empty set - represents Zero.
      /// </summary>
      public static readonly SetNatural Zero = new SetNatural(new HashSet<SetNatural>());

      /// <summary>
      /// Set containing the empty set - One.
      /// </summary>
      public static readonly SetNatural One = Suc(Zero);

      /// <summary>
      /// Gets the elements contained in this SetNatural.
      /// </summary>
      private readonly ISet<SetNatural> _elements;

      /// <summary>
      /// Prevents a default instance of the <see cref="SetNatural"/> class from being created.
      /// </summary>
      /// <param name="elements">The elements.</param>
      private SetNatural(IEnumerable<SetNatural> elements)
      {
         // accept IEnumerable since LINQ's Union() is nicer to work with than ISet's UnionWith()
         _elements = new HashSet<SetNatural>(elements);
      }

      /* simply ignore the following bulky regions, only SetEquals() is somewhat interesting */

      #region printing and equality

      /// <summary>
      /// Returns a <see cref="string"/> that represents this instance.
      /// </summary>
      /// <returns>
      /// A <see cref="string"/> that represents this instance.
      /// </returns>
      public override string ToString()
      {
         return this.ShortString();
      }

      /// <summary>
      /// Determines equality.
      /// </summary>
      public bool Equals(SetNatural other)
      {
         return this.TypedEquals(other);
      }

      /// <summary>
      /// Determines equality.
      /// </summary>
      public override bool Equals(object other)
      {
         return this.UntypedEquals(other);
      }

      /// <summary>
      /// Returns a hash code for this instance.
      /// </summary>
      /// <returns>
      /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
      /// </returns>
      public override int GetHashCode()
      {
         // valid choice for SetNaturals
         return _elements.Count;
      }

      /// <summary>
      /// Implements the operator ==.
      /// </summary>
      /// <param name="first">The first.</param>
      /// <param name="second">The second.</param>
      /// <returns>
      /// The result of the operator.
      /// </returns>
      public static bool operator ==(SetNatural first, SetNatural second)
      {
         return first.TypedEquals(second);
      }

      /// <summary>
      /// Implements the operator !=.
      /// </summary>
      /// <param name="first">The first.</param>
      /// <param name="second">The second.</param>
      /// <returns>
      /// The result of the operator.
      /// </returns>
      public static bool operator !=(SetNatural first, SetNatural second)
      {
         return !(first == second);
      }

      #endregion

      #region ISet implementation
      public bool SetEquals(IEnumerable<SetNatural> other)
      {
         SetNatural otherSetNatural = other as SetNatural;

         if (otherSetNatural != null)
         {
            // performance boost - valid for SetNatural's by construction
            return _elements.Count == other.Count();
         }

         return _elements.SetEquals(other);
      }

      public IEnumerator<SetNatural> GetEnumerator()
      {
         return _elements.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return _elements.GetEnumerator();
      }

      public void Add(SetNatural item)
      {
         _elements.Add(item);
      }

      bool ISet<SetNatural>.Add(SetNatural item)
      {
         return _elements.Add(item);
      }

      public void UnionWith(IEnumerable<SetNatural> other)
      {
         _elements.UnionWith(other);
      }

      public void IntersectWith(IEnumerable<SetNatural> other)
      {
         _elements.IntersectWith(other);
      }

      public void ExceptWith(IEnumerable<SetNatural> other)
      {
         _elements.ExceptWith(other);
      }

      public void SymmetricExceptWith(IEnumerable<SetNatural> other)
      {
         _elements.SymmetricExceptWith(other);
      }

      public bool IsSubsetOf(IEnumerable<SetNatural> other)
      {
         return _elements.IsSubsetOf(other);
      }

      public bool IsSupersetOf(IEnumerable<SetNatural> other)
      {
         return _elements.IsSupersetOf(other);
      }

      public bool IsProperSupersetOf(IEnumerable<SetNatural> other)
      {
         return _elements.IsProperSupersetOf(other);
      }

      public bool IsProperSubsetOf(IEnumerable<SetNatural> other)
      {
         return _elements.IsProperSubsetOf(other);
      }

      public bool Overlaps(IEnumerable<SetNatural> other)
      {
         return _elements.Overlaps(other);
      }

      public void Clear()
      {
         _elements.Clear();
      }

      public bool Contains(SetNatural item)
      {
         return _elements.Contains(item);
      }

      public void CopyTo(SetNatural[] array, int arrayIndex)
      {
         _elements.CopyTo(array, arrayIndex);
      }

      public bool Remove(SetNatural item)
      {
         return _elements.Remove(item);
      }

      public int Count
      {
         get
         {
            return _elements.Count;
         }
      }

      public bool IsReadOnly
      {
         get
         {
            return _elements.IsReadOnly;
         }
      }

      #endregion

      /// <summary>
      /// Calculates the successor of this instance.
      /// </summary>
      /// <returns>The successor.</returns>
      public static SetNatural Suc(SetNatural value)
      {
         // the trick: Suc(n) = n UNION { n }
         return new SetNatural(value.Union(InSet(value)));
      }

      /// <summary>
      /// Encloses the given set in an additional set.
      /// </summary>
      private static ISet<SetNatural> InSet(SetNatural value)
      {
         return new HashSet<SetNatural> { value };
      }

      /// <summary>
      /// Calculates the predecessor.
      /// </summary>
      public static SetNatural Pred(SetNatural value)
      {
         // only defined for values != Zero
         if (value == Zero)
            throw new ArgumentException("value");

         SetNatural predecessor = Zero;

         // to check: better approach for Pred(), is this even allowed?
         while (Suc(predecessor) != value)
         {
            predecessor = Suc(predecessor);
         }

         return predecessor;
      }

      /// <summary>
      /// Implements the operator +.
      /// </summary>
      /// <param name="x">The x value.</param>
      /// <param name="y">The y value.</param>
      /// <returns>
      /// The result of the operator.
      /// </returns>
      public static SetNatural operator +(SetNatural x, SetNatural y)
      {
         if (x == null)
            throw new ArgumentNullException("x");

         if (y == null)
            throw new ArgumentNullException("y");

         return Addition(x, y);
      }

      /// <summary>
      /// Calculates x + y.
      /// </summary>
      private static SetNatural Addition(SetNatural x, SetNatural y)
      {
         // base case: x + 0 = x
         if (y == Zero)
            return x;

         // recursive case: x + y = Suc(x + Pred(y))
         return Suc(x + Pred(y));
      }

      /// <summary>
      /// Implements the operator *.
      /// </summary>
      /// <param name="x">The x value.</param>
      /// <param name="y">The y value.</param>
      /// <returns>
      /// The result of the operator.
      /// </returns>
      public static SetNatural operator *(SetNatural x, SetNatural y)
      {
         if (x == null)
            throw new ArgumentNullException("x");

         if (y == null)
            throw new ArgumentNullException("y");

         return Mult(x, y);
      }

      /// <summary>
      /// Calculates x * y.
      /// </summary>
      private static SetNatural Mult(SetNatural x, SetNatural y)
      {
         // base case: x * 0 = 0
         if (y == Zero)
            return Zero;

         // recursive case: x * y = x * Pred(y) + x
         return x * Pred(y) + x;
      }

      /// <summary>
      /// Implements the operator ^.
      /// </summary>
      /// <param name="baseValue">The base value.</param>
      /// <param name="exponent">The exponent.</param>
      /// <returns>
      /// The result of the operator.
      /// </returns>
      public static SetNatural operator ^(SetNatural baseValue, SetNatural exponent)
      {
         if (baseValue == null)
            throw new ArgumentNullException("baseValue");

         if (exponent == null)
            throw new ArgumentNullException("exponent");

         return Power(baseValue, exponent);
      }

      /// <summary>
      /// Calculates x ^ y.
      /// </summary>
      private static SetNatural Power(SetNatural x, SetNatural y)
      {
         // base case: x ^ 0 = 1
         if (y == Zero)
            return One;

         // recursive case: x ^ y = (x ^ Pred(y)) * x
         return (x ^ Pred(y)) * x;
      }
   }
}