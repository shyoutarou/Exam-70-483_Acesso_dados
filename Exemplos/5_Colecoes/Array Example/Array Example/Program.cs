using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] mySet_1d = new int[4];
            mySet_1d[0] = 1;
            mySet_1d[1] = 2;
            mySet_1d[2] = 3;
            mySet_1d[3] = 4;

            int[,] mySet_2d = new int[2, 2];
            mySet_2d[0, 0] = 1;
            mySet_2d[0, 1] = 2;
            mySet_2d[1, 0] = 3;
            mySet_2d[1, 1] = 4;


            int[] arrayOfInt = new int[10];
            for (int x = 0; x < arrayOfInt.Length; x++)
            {
                arrayOfInt[x] = x;
            }
            Console.WriteLine(arrayOfInt.GetType()); //System.Int32[]

            var arrayRange = Enumerable.Range(0, 10);
            //System.Linq.Enumerable +< RangeIterator > d__113
            Console.WriteLine(arrayRange.GetType());
            Console.WriteLine(arrayRange.ToArray().GetType()); //System.Int32[]

            int[] arrayInit = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Console.WriteLine(arrayInit.GetType()); //System.Int32[]

            int[][] jaggedArray =
                    {
        new int[] {1,3,5,7,9},
        new int[] {0,2,4,6},
        new int[] {42,21}
        };

            foreach (var vector in jaggedArray)
            {
                foreach (var i in vector)
                {
                    Console.Write(i);
                }
                Console.WriteLine();
            }

            Console.WriteLine(jaggedArray[0][0]); // 1
            Console.WriteLine(jaggedArray[0][1]); // 3


            Console.ReadKey();

        }
    }
}
