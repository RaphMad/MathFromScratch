namespace ImmutableNumbers.PrintingExtensions
{
   /// <summary>
   /// Contains functions that allow printing of SetNaturals.
   /// </summary>
   public static class SetNaturalPrinting
   {
      /// <summary>
      /// Creates a string with the complete internal set representation of a SetNatural.
      /// </summary>
      public static string DetailledString(this SetNatural value)
      {
         string representation = "{";

         foreach (SetNatural element in value)
         {
            representation += element.DetailledString();
            representation += ",";
         }

         // truncate last ","
         if (representation.EndsWith(","))
            representation = representation.Remove(representation.Length - 1);

         representation += "}";

         return representation;
      }

      /// <summary>
      /// Creates a string with the numeric representation of a SetNatural.
      /// </summary>
      public static string NumericString(this SetNatural value)
      {
         return value.Count.ToString();
      }

      /// <summary>
      /// Creates a string that uses set notation on the "outermost" level and numeric representation on the inner levels.
      /// </summary>
      public static string ShortString(this SetNatural value)
      {
         string representation = "{";

         foreach (SetNatural element in value)
         {
            representation += element.NumericString();
            representation += ",";
         }

         // truncate last ","
         if (representation.EndsWith(","))
            representation = representation.Remove(representation.Length - 1);

         representation += "}";

         return representation;
      }
   }
}