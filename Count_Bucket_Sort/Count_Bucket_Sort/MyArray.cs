using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count_Bucket_Sort
{
    class MyArray : DataArray
    {
        public override int Operations { get; set; }
        int[] data;

        public MyArray(int n)
        {
            data = new int[n];
        }

        public MyArray(int n, int seed, int range)
        {
            this.Operations = 0;
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
            this.Operations = 0;

            for (int i = 0; i < array.length; i++)
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
            Operations += 2;

            for (int i = 1; i < length; i++)
            {
                Operations++;
                if(data[i] < minValue)
                {
                    Operations++;
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
            Operations += 2;

            for (int i = 1; i < length; i++)
            {
                Operations++;
                if (data[i] > maxValue)
                {
                    maxValue = data[i];
                    Operations++;
                }
            }

            Operations++;
            return maxValue;
        }

        public override int this[int index]
        {
            set
            {
                Operations++;
                data[index] = value;
            }
            get
            {
                Operations++;
                return data[index];
            }
        }
    }
}
