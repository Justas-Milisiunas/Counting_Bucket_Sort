using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count_Bucket_Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 10;
            int range = 10000;
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            CountingSort countSort = new CountingSort();
            countSort.TestArray_OP(n, seed, range);
            //countSort.TestList_OP(n, seed, range);
            //countSort.TestArray_D(n, seed, range, "myfile.dat");
            //countSort.TestList_D(n, seed, range, "myfile.dat");

            BucketSort bucketSort = new BucketSort();
            //bucketSort.TestArray_OP(n, seed, range);
            //bucketSort.TestList_OP(n, seed, range);
            //bucketSort.TestArray_D(n, seed, range, "myfile.dat");
            //bucketSort.TestList_D(n, seed, range, "myfile.dat");

            Console.ReadKey();
        }
    }
}
