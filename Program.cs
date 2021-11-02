using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------1----------");
            V4DataArray testArray = new V4DataArray("1", System.DateTime.Now, 2, 1, new System.Numerics.Vector2(0.3f, 0.6f), Fv2.TestVectorF);
            Console.WriteLine(testArray.ToLongString("{0:f}"));
            Console.WriteLine(String.Format("Count = {0:d}", testArray.Count));
            Console.WriteLine(String.Format("MaxFromOrigin =  {0:f3}", testArray.MaxFromOrigin));

            Console.WriteLine("--------------------");

            var testList = testArray.Transformation();
            Console.WriteLine(testList.ToLongString("{0:f}"));
            Console.WriteLine(String.Format("Count: {0:d}", testList.Count));
            Console.WriteLine(String.Format("MaxFromOrigin: {0:f3}", testList.MaxFromOrigin));

            Console.WriteLine("----------2----------");
            V4MainCollection testCollection = new V4MainCollection();
            testCollection.Add(testList);
            testCollection.Add(testArray);

            var testList2 = new V4DataList("2", System.DateTime.Now);
            testList2.AddDefaults(3, Fv2.TestVectorF);

            var testArray2 = new V4DataArray("3", System.DateTime.Now, 3, 1, new System.Numerics.Vector2(3.7f, 2.5f), Fv2.TestVectorF);

            testCollection.Add(testList2);
            testCollection.Add(testArray2);
            Console.WriteLine(testCollection.ToLongString("{0:f}"));

            Console.WriteLine("----------3----------");
            for (int i = 0; i < testCollection.Count; i++)
            {
                Console.WriteLine(String.Format("Count = {0:d}", testCollection[i].Count));
                Console.WriteLine(String.Format("MaxFromOrigin = {0:f3}", testCollection[i].MaxFromOrigin));
            }
        }
    }
}
