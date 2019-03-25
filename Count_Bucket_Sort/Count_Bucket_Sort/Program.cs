using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count_Bucket_Sort
{
    class Program
    {
        public static readonly int[] KIEKIAI = new int[] { 1000, 5000, 10000, 20_000, 50_000, 250_000, 1_000_000, 4_000_000  };

        static void Main(string[] args)
        {
            int n = 500000;
            int range = 10000;
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            CountingSort countSort = new CountingSort();
            BucketSort bucketSort = new BucketSort();

            while (true)
            {
                Console.WriteLine("Ka norite istestuoti?");
                Console.WriteLine("count_op count_d bucket_op bucket_d all exit");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "count_op":
                        countSort.TestArray_OP(seed, range);
                        countSort.TestList_OP(seed, range);
                        break;
                    case "count_d":
                        countSort.TestArray_D(seed, range, "myfile.dat");
                        countSort.TestList_D(seed, range, "myfile.dat");
                        break;
                    case "bucket_op":
                        bucketSort.TestArray_OP(seed, range);
                        bucketSort.TestList_OP(seed, range);
                        break;
                    case "bucket_d":
                        bucketSort.TestArray_D(seed, range, "myfile.dat");
                        bucketSort.TestList_D(seed, range, "myfile.dat");
                        break;
                    case "all":
                        countSort.TestArray_OP(seed, range);
                        countSort.TestList_OP(seed, range);
                        countSort.TestArray_D(seed, range, "myfile.dat");
                        countSort.TestList_D(seed, range, "myfile.dat");
                        bucketSort.TestArray_OP(seed, range);
                        bucketSort.TestList_OP(seed, range);
                        bucketSort.TestArray_D(seed, range, "myfile.dat");
                        bucketSort.TestList_D(seed, range, "myfile.dat");
                        break;
                    case "exit":
                        goto ExitMainLoop;
                    default:
                        Console.WriteLine("Tokio pasirinkimo nera");
                        break;
                }
            }

            ExitMainLoop:
            Console.WriteLine("Programa isijungia");
        }
    }
}
