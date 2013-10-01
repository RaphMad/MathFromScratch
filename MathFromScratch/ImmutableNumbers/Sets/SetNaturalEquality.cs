namespace ImmutableNumbers.EqualityExtensions
{
   /// <summary>
   /// Contains methods w.r.t. to equality of SetNaturals.
   /// </summary>
   public static class SetNaturalEquality
   {
      /// <summary>
      /// Determines whether this instance is equal to another SetNatural.
      /// </summary>
      public static bool TypedEquals(this SetNatural value, SetNatural other)
      {
         // handle null instances
         if (ReferenceEquals(value, null) && ReferenceEquals(other, null))
               return true;
         if (ReferenceEquals(value, null) || ReferenceEquals(other, null))
               return false;

         // perform an element-wise comparison for non-null instances
         return other.SetEquals(value);
      }

      /// <summary>
      /// Determines whether this instance is equal to another SetNatural - untyped version.
      /// </summary>
      public static bool UntypedEquals(this SetNatural value, object other)
      {
         return value.TypedEquals(other as SetNatural);
      }
   }
}