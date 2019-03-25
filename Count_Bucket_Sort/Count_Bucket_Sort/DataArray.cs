using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count_Bucket_Sort
{
    /// <summary>
    /// Abstract array class
    /// </summary>
    public abstract class DataArray
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract int Min();
        public abstract int Max();
        public abstract int this[int index] { get; set; }
        public abstract int Operations { get; set; }

        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(" {0} ", this[i]);
            }

            Console.WriteLine();
        }
    }
}
