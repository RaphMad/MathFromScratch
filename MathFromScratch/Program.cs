namespace MathFromScratch
{
   using System;
   using System.Diagnostics;
   using ImmutableNumbers;
   using ImmutableNumbers.PrintingExtensions;

   /// <summary>
   /// Main program.
   /// </summary>
   internal class Program
   {
      /// <summary>
      /// Main entry point.
      /// </summary>
      private static void Main()
      {
         var sn0 = SetNatural.Zero;

         var sn1 = SetNatural.Suc(sn0);
         var sn2 = SetNatural.Suc(sn1);
         var sn3 = SetNatural.Suc(sn2);

         Console.WriteLine(sn0.DetailledString());
         Console.WriteLine(sn0);
         Console.WriteLine(sn0.NumericString());
         Console.WriteLine();

         Console.WriteLine(sn1.DetailledString());
         Console.WriteLine(sn1);
         Console.WriteLine(sn1.NumericString());
         Console.WriteLine();

         Console.WriteLine(sn2.DetailledString());
         Console.WriteLine(sn2);
         Console.WriteLine(sn2.NumericString());
         Console.WriteLine();

         Console.WriteLine(sn3.DetailledString());
         Console.WriteLine(sn3);
         Console.WriteLine(sn3.NumericString());
         Console.WriteLine();

         var sni3 = sn1 + sn2;

         Console.WriteLine(sn3 == sni3);
         Console.WriteLine(sn3.Equals(sni3));
         Console.WriteLine(sn3.Equals((object)sni3));
         Console.WriteLine(sn3.GetHashCode() == sni3.GetHashCode());
         Console.WriteLine();

         Console.WriteLine(sn2 == sn3);
         Console.WriteLine(sn2.Equals(sni3));
         Console.WriteLine(sn2.Equals((object)sni3));
         Console.WriteLine(sn2.GetHashCode() == sni3.GetHashCode());
         Console.WriteLine();

         Console.WriteLine(SetNatural.Suc(sn1) == SetNatural.Pred(sn3));
         Console.WriteLine();

         Console.WriteLine((sn3 + sn2 + sn3 + sn3 + sn1 + sn0 + sn0).NumericString());
         Console.WriteLine();

         // note that overloaded operators keep their precedence
         var sn10 = sn1 + sn3 * sn3;

         Console.WriteLine(sn10.NumericString());
         Console.WriteLine();

         var stopwatch = new Stopwatch();
         stopwatch.Start();

         // do some number crunching, dont even try to imagine the full set representation of this number ;)
         var sn10000 = sn10 ^ sn2 ^ sn2;

         stopwatch.Stop();

         Console.WriteLine(sn10000.NumericString() + ", Time: " + stopwatch.ElapsedMilliseconds + "ms");
         Console.WriteLine();

         Console.WriteLine(sn10000 < sn10);
         Console.WriteLine(sn10000 > sn10);

         Console.WriteLine(sn1 < SetNatural.Suc(sn1));
         Console.WriteLine(sn1 > SetNatural.Pred(sn10000));
         Console.WriteLine(sn3 < sn3);
         Console.WriteLine(sn2 > sn2);

         Console.ReadKey();
      }
   }
}