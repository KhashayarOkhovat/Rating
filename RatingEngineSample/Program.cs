using System;

namespace RatingEngineSample
{
    class Program
    {
        static void Main(string[] args)
        {
            #region [ Old Program ]
            //Console.WriteLine("Insurance Rating System Starting...");

            //var engine = new RatingEngine();
            //engine.Rate();

            //if (engine.Rating > 0)
            //{
            //    Console.WriteLine($"Rating: {engine.Rating}");
            //}
            //else
            //{
            //    Console.WriteLine("No rating produced.");
            //} 
            #endregion

            Console.WriteLine("Insurance Rating System Starting...");

            var engine = new RatingEngine(new LifePolice());
            var result = engine.Rateing();

            if (result.IsSuccess && Convert.ToDecimal(result.Result) > 0)
            {
                Console.WriteLine($"Rating: {result.Result}");
            }
            else
            {
                Console.WriteLine($"{result.Message}");
            }
            Console.ReadKey();
        }
    }
}
