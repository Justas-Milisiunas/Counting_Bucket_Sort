﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Count_Bucket_Sort
{
    class MyFileArray : DataArray
    {
        public override int Operations { get; set; }

        /// <summary>
        /// Generates data and writes to file
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="n">Elements count</param>
        /// <param name="seed">Generating seed</param>
        /// <param name="range">Elements range</param>
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
                Operations += 5;
                Byte[] data = new Byte[4];
                fs.Seek(4 * index, SeekOrigin.Begin);
                fs.Read(data, 0, 4);
                int result = BitConverter.ToInt32(data, 0);
                return result;
            }
            set
            {
                Operations += 4;
                Byte[] data = new Byte[4];
                BitConverter.GetBytes(value).CopyTo(data, 0);
                fs.Seek(4 * index, SeekOrigin.Begin);
                fs.Write(data, 0, 4);
            }
        }

        /// <summary>
        /// Finds smallest value
        /// </summary>
        /// <returns>Smalles value</returns>
        public override int Min()
        {
            Operations++;
            int minValue = this[0];
            for(int i = 1; i < length; i++)
            {
                Operations++;
                if (this[i] < minValue)
                {
                    minValue = this[i];
                    Operations++;
                }
            }

            Operations++;
            return minValue;
        }

        /// <summary>
        /// Finds highest value
        /// </summary>
        /// <returns>Highest value</returns>
        public override int Max()
        {
            Operations++;
            int maxValue = this[0];

            for (int i = 1; i < length; i++)
            {
                Operations++;
                if (this[i] > maxValue)
                {
                    Operations++;
                    maxValue = this[i];
                }
            }

            Operations++;
            return maxValue;
        }
    }
}
