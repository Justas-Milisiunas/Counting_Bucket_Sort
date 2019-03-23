using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count_Bucket_Sort
{
    class MyArray : DataArray
    {
        int[] data;

        public MyArray(int n)
        {
            data = new int[n];
        }

        public MyArray(int n, int seed, int range)
        {
            data = new int[n];
            length = n;

            Random rand = new Random(seed);
            for (int i = 0; i < length; i++)
            {
                data[i] = rand.Next(range);
            }
        }

        public MyArray(MyArray array)
        {
            for(int i = 0; i < array.length; i++)
            {
                data[i] = array.length;
            }
        }

        /// <summary>
        /// Finds smallest value
        /// </summary>
        /// <returns>Smallest value</returns>
        public override int Min()
        {
            int minValue = data[0];
            for (int i = 1; i < length; i++)
            {
                if(data[i] < minValue)
                {
                    minValue = data[i];
                }
            }

            return minValue;
        }

        /// <summary>
        /// Finds highest value
        /// </summary>
        /// <returns>Highest value</returns>
        public override int Max()
        {
            int maxValue = data[0];
            for (int i = 1; i < length; i++)
            {
                if (data[i] > maxValue)
                {
                    maxValue = data[i];
                }
            }

            return maxValue;
        }

        public override int this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }
    }
}
