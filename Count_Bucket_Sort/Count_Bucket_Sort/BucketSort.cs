using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Count_Bucket_Sort
{
    class BucketSort
    {
        /// <summary>
        /// Tests bucket sort performance with array in operative memory
        /// </summary>
        /// <param name="n">Elements count</param>
        /// <param name="seed">Generating seed</param>
        /// <param name="range">Elements range</param>
        public void TestArray_OP(int seed, int range)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("BUCKET SORT OP");
            builder.AppendLine("===================================================");
            builder.AppendLine("| Number of elements |        |    Runtime time   |");
            builder.AppendLine("========================ARRAY======================");

            foreach(int count in Program.KIEKIAI)
            {
                //Array sorting
                DataArray data = new MyArray(count, seed, range);
                //data.Print(data.Length);

                Stopwatch t1 = new Stopwatch();
                t1.Start();
                Sort(data);
                t1.Stop();

                //data.Print(data.Length);
                builder.AppendLine(string.Format("|{0,-20}|        |{1} ms|", count, t1.Elapsed.ToString()));

                //Clears memory
                data = null;
                System.GC.Collect();
            }

            Console.Write(builder.ToString());
        }

        /// <summary>
        /// Tests bucket sort performance with linked list in operative memory
        /// </summary>
        /// <param name="n">Elements count</param>
        /// <param name="seed">Generating seed</param>
        /// <param name="range">Elements range</param>
        public void TestList_OP(int seed, int range)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("========================List=======================");

            foreach (int count in Program.KIEKIAI)
            {
                //Linked list sorting
                DataList listData = new MyList(count, seed, range);
                //listData.Print(listData.Length);

                Stopwatch t2 = new Stopwatch();
                t2.Start();
                Sort(listData);
                t2.Stop();

                //listData.Print(listData.Length);
                builder.AppendLine(string.Format("|{0,-20}|        |{1} ms|", count, t2.Elapsed.ToString()));

                //Clears memory
                listData = null;
                System.GC.Collect();
            }

            builder.AppendLine("===================================================");
            Console.Write(builder.ToString());
        }

        /// <summary>
        /// Tests bucket sort performance with array in disk memory
        /// </summary>
        /// <param name="n">Elements count</param>
        /// <param name="seed">Generating seed</param>
        /// <param name="range">Elements range</param>
        /// <param name="fileName">File name</param>
        public void TestArray_D(int seed, int range, string fileName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("BUCKET SORT D");
            builder.AppendLine("===================================================");
            builder.AppendLine("| Number of elements |        |    Runtime time   |");
            builder.AppendLine("========================ARRAY======================");

            foreach (int count in Program.KIEKIAI)
            {
                //Array sorting
                MyFileArray array = new MyFileArray(fileName, count, seed, range);
                using (array.fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    //array.Print(n);

                    Stopwatch t1 = new Stopwatch();
                    t1.Start();
                    Sort(array);
                    t1.Stop();

                    //array.Print(n);
                    builder.AppendLine(string.Format("|{0,-20}|        |{1} ms|", count, t1.Elapsed.ToString()));
                }

                //Clears memory
                array = null;
                System.GC.Collect();
            }

            Console.Write(builder.ToString());
        }

        /// <summary>
        /// Tests bucket sort performance with linked list in disk memory
        /// </summary>
        /// <param name="n">Elements count</param>
        /// <param name="seed">Generating seed</param>
        /// <param name="range">Elements range</param>
        /// <param name="fileName">File name</param>
        public void TestList_D(int seed, int range, string fileName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("========================List=======================");

            foreach (int count in Program.KIEKIAI)
            {
                MyFileList dataList = new MyFileList(fileName, count, seed, range);
                using (dataList.fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    //dataList.Print(n);

                    Stopwatch t2 = new Stopwatch();
                    t2.Start();
                    Sort(dataList);
                    t2.Stop();

                    //dataList.Print(n);
                    builder.AppendLine(string.Format("|{0,-20}|        |{1} ms|", count, t2.Elapsed.ToString()));
                }
            }

            builder.AppendLine("===================================================");
            Console.Write(builder.ToString());
        }

        /// <summary>
        /// Bucket sort for array
        /// </summary>
        /// <param name="array">Array</param>
        public void Sort(DataArray array)
        {
            int minValue = (int)array.Min();
            int maxValue = (int)array.Max();

            List<int> result = new List<int>();
            List<int>[] buckets = new List<int>[maxValue - minValue + 1];

            //Initializing list for each bucket
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<int>();
            }

            //Scatters elements to buckets
            for(int i = 0; i < array.Length; i++)
            {
                int index = (int)(array[i] - minValue);
                buckets[index].Add(array[i]);
            }

            //Sorts each bucket using bubblesort
            for(int i = 0; i < buckets.Length; i++)
            {
                for(int j = 0; j < buckets[i].Count; j++)
                {
                    for(int k = 0; k < buckets[i].Count; k++)
                    {
                        if(buckets[i][j] < buckets[i][k])
                        {
                            int temp = buckets[i][j];
                            buckets[i][j] = buckets[i][k];
                            buckets[i][k] = temp;
                        }
                    }
                }

                result.AddRange(buckets[i].ToArray());
            }


            //Copies result array to data array
            for(int i = 0; i < result.Count; i++)
            {
                array[i] = result[i];
            }
        }

        /// <summary>
        /// Bucket sort for linked list
        /// </summary>
        /// <param name="list">List</param>
        public void Sort(DataList list)
        {
            int minValue = (int)list.Min();
            int maxValue = (int)list.Max();

            List<int> result = new List<int>();
            List<int>[] buckets = new List<int>[maxValue - minValue + 1];

            //Initializing list for each bucket
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<int>();
            }

            //Scatters elements to buckets
            for(list.Head(); list.Exists(); list.Next())
            {
                int index = (int)(list.Current() - minValue);
                buckets[index].Add(list.Current());
            }

            //Sorts each bucket using bubblesort
            for (int i = 0; i < buckets.Length; i++)
            {
                for (int j = 0; j < buckets[i].Count; j++)
                {
                    for (int k = 0; k < buckets[i].Count; k++)
                    {
                        if (buckets[i][j] < buckets[i][k])
                        {
                            int temp = buckets[i][j];
                            buckets[i][j] = buckets[i][k];
                            buckets[i][k] = temp;
                        }
                    }
                }

                result.AddRange(buckets[i].ToArray());
            }


            //Copies result array to data array
            list.Head();
            for (int i = 0; i < result.Count; i++)
            {
                list.ChangeData(result[i]);
                list.Next();
            }
        }
    }
}
