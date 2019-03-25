using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count_Bucket_Sort
{
    /// <summary>
    /// Abstract list class
    /// </summary>
    public abstract class DataList
    {
        protected int length;
        public int Length { get { return length; } set { length = value; } }
        public abstract int Operations { get; set; }
        public abstract int Head();
        public abstract int Next();
        public abstract int Current();
        public abstract bool Exists();
        public abstract int Min();
        public abstract int Max();
        public abstract void ChangeData(int data);
        public abstract void Put(int data);

        public abstract void Swap(int a, int b);

        public void Print(int n)
        {
            Console.WriteLine(" {0} ", Head());
            for (int i = 1; i < n; i++)
            {
                Console.WriteLine(" {0} ", Next());
            }

            Console.WriteLine();
        }
    }
}
