using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Count_Bucket_Sort
{
    class MyFileArray : DataArray
    {
        public MyFileArray(string fileName, int n, int seed, int range)
        {
            int[] data = new int[n];
            length = n;

            Random rand = new Random(seed);
            for(int i = 0; i < length; i++)
            {
                data[i] = rand.Next(0, range);
            }

            if (File.Exists(fileName))
                File.Delete(fileName);
            
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
                {
                    for(int j = 0; j < length; j++)
                    {
                        writer.Write(data[j]);
                    }
                }
            }
            catch(IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public FileStream fs { get; set; }

        public override int this[int index]
        {
            get
            {
                Byte[] data = new Byte[4];
                fs.Seek(4 * index, SeekOrigin.Begin);
                fs.Read(data, 0, 4);
                int result = BitConverter.ToInt32(data, 0);
                return result;
            }
            set
            {
                Byte[] data = new Byte[4];
                BitConverter.GetBytes(value).CopyTo(data, 0);
                fs.Seek(4 * index, SeekOrigin.Begin);
                fs.Write(data, 0, 4);
            }
        }

        public override int Min()
        {
            int minValue = this[0];
            for(int i = 1; i < length; i++)
            {
                if (this[i] < minValue)
                    minValue = this[i];
            }

            return minValue;
        }

        public override int Max()
        {
            int maxValue = this[0];
            for (int i = 1; i < length; i++)
            {
                if (this[i] > maxValue)
                    maxValue = this[i];
            }

            return maxValue;
        }
    }
}
