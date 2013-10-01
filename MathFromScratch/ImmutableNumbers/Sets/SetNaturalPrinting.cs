using System.Text;

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
         var representation = new StringBuilder("{");

         foreach (SetNatural element in value)
         {
            representation.Append(element.DetailledString());
            representation.Append(",");
         }

         // truncate last ","
         string representationString = representation.ToString();

         if (representationString.EndsWith(","))
            representationString = representationString.Remove(representation.Length - 1);

         representationString += ("}");

         return representationString;
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
         var representation = new StringBuilder("{");

         foreach (SetNatural element in value)
         {
            representation.Append(element.NumericString());
            representation.Append(",");
         }

         // truncate last ","
         string representationString = representation.ToString();

         if (representationString.EndsWith(","))
            representationString = representationString.Remove(representation.Length - 1);

         representationString += ("}");

         return representationString;
      }
   }
}