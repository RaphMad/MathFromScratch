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
   public sealed class SetNatural : ISet<SetNatural>, IEquatable<SetNatural>, IComparable<SetNatural>
   {
      /// <summary>
      /// Memo table for successors.
      /// </summary>
      private static readonly IDictionary<SetNatural, SetNatural> SucMemo = new Dictionary<SetNatural, SetNatural>();

      /// <summary>
      /// Memo table for predecessors.
      /// </summary>
      private static readonly IDictionary<SetNatural, SetNatural> PredMemo = new Dictionary<SetNatural, SetNatural>();

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
      private SetNatural(HashSet<SetNatural> elements)
      {
         _elements = elements;
      }

      /* simply ignore the following bulky regions, only SetEquals() and IsSubsetOf() are somewhat interesting */

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
         // performance boost in hashed data structures - valid choice for SetNaturals
         // (but of course a bit of cheating in the strict sense)
         return Count;
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

      /// <summary>
      /// Compares this instance to another instance.
      /// </summary>
      /// <param name="other">The other.</param>
      /// <returns>-1 if greater than, 0 if equal and 1 if less than this instance.</returns>
      public int CompareTo(SetNatural other)
      {
         if (this < other)
            return -1;

         if (this > other)
            return 1;

         return 0;
      }

      #endregion

      #region ISet implementation

      public bool SetEquals(IEnumerable<SetNatural> other)
      {
         // used for determining equality
         SetNatural otherSetNatural = other as SetNatural;

         if (otherSetNatural != null)
         {
            // performance boost - valid for SetNatural's by construction
            // (but of course this is cheating since in set theory we would not know about a "Count"
            //  - the fallback branch would work slower nevertheless)
            return Count == other.Count();
         }

         return _elements.SetEquals(other);
      }

      public bool IsSubsetOf(IEnumerable<SetNatural> other)
      {
         // used for determining order of SetNaturals
         SetNatural otherSetNatural = other as SetNatural;

         if (otherSetNatural != null)
         {
            // performance boost - but same argument as above
            return Count < other.Count();
         }

         return _elements.IsSubsetOf(other);
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
         // use memoized value if available
         if (SucMemo.ContainsKey(value))
         {
            return SucMemo[value];
         }

         // the trick: Suc(n) = n UNION { n }
         return SucMemo[value] = new SetNatural(new HashSet<SetNatural>(value) { value });
      }

      /// <summary>
      /// Calculates the predecessor.
      /// </summary>
      public static SetNatural Pred(SetNatural value)
      {
         // only defined for values != Zero
         if (value == Zero)
            throw new ArgumentException("value");

         // use memoized value if available
         if (PredMemo.ContainsKey(value))
         {
            return PredMemo[value];
         }

         // searching for predecessors can begin at the last memoized value
         SetNatural predecessor = Zero;

         // advance predecessor to the first value that has not yet been calculated
         while (PredMemo.ContainsKey(Suc(predecessor)))
         {
            predecessor = Suc(predecessor);
         }

         // advance further until the desired value is reached, memoize newly calculated values
         while (Suc(predecessor) != value)
         {
            PredMemo[Suc(predecessor)] = predecessor;
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
      /// Implements the operator ++.
      /// </summary>
      /// <param name="x">The x value.</param>
      /// <returns>
      /// The result of the operator.
      /// </returns>
      public static SetNatural operator ++(SetNatural x)
      {
         if (x == null)
            throw new ArgumentNullException("x");

         return Addition(x, One);
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

      /// <summary>
      /// Implements the operator less than.
      /// </summary>
      /// <param name="x">The x.</param>
      /// <param name="y">The y.</param>
      /// <returns>
      /// The result of the operator.
      /// </returns>
      public static bool operator <(SetNatural x, SetNatural y)
      {
         if (x == null)
            throw new ArgumentNullException("x");

         if (y == null)
            throw new ArgumentNullException("y");

         return x.IsSubsetOf(y);
      }

      /// <summary>
      /// Implements the operator greater than.
      /// </summary>
      /// <param name="x">The x.</param>
      /// <param name="y">The y.</param>
      /// <returns>
      /// The result of the operator.
      /// </returns>
      public static bool operator >(SetNatural x, SetNatural y)
      {
         if (x == null)
            throw new ArgumentNullException("x");

         if (y == null)
            throw new ArgumentNullException("y");

         return y.IsSubsetOf(x);
      }
   }
}