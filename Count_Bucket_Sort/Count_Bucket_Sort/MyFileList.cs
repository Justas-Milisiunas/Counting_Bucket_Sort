using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Count_Bucket_Sort
{
    class MyFileList : DataList
    {
        int prevNode;
        int currentNode;
        int nextNode;

        public MyFileList(string fileName, int n, int seed, int range)
        {
            length = n;
            Random rand = new Random(seed);
            if (File.Exists(fileName))
                File.Delete(fileName);

            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
                {
                    writer.Write(4);
                    for (int j = 0; j < length; j++)
                    {
                        int sudas = rand.Next(0, range);
                        writer.Write(sudas);
                        if (j - 1 == length)
                            writer.Write(-1);
                        else
                            writer.Write((j + 1) * 8 + 4);
                    }
                }

            }

            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public FileStream fs { get; set; }

        public override void ChangeData(int element)
        {
            Byte[] data = BitConverter.GetBytes(element);
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Write(data, 0, 4);
        }

        public override int Current()
        {
            Byte[] data = new Byte[4];
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            return BitConverter.ToInt32(data, 0);
        }

        public override int Head()
        {
            Byte[] data = new Byte[8];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            currentNode = BitConverter.ToInt32(data, 0);
            prevNode = -1;
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            int result = BitConverter.ToInt32(data, 0);
            nextNode = BitConverter.ToInt32(data, 4);
            return result;
        }

        public override int Max()
        {
            Byte[] data = new Byte[4];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            int maxValue = BitConverter.ToInt32(data, 0);

            for(Head(); Exists(); Next())
            {
                if(Current() > maxValue)
                {
                    maxValue = Current();
                }
            }

            return maxValue;
        }

        public override int Min()
        {
            Byte[] data = new Byte[4];
            fs.Seek(4, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            int minValue = BitConverter.ToInt32(data, 0);

            for (Head(); Exists(); Next())
            {
                if (Current() < minValue)
                {
                    minValue = Current();
                }
            }

            return minValue;
        }

        public override int Next()
        {
            Byte[] data = new Byte[8];
            fs.Seek(nextNode, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            prevNode = currentNode;
            currentNode = nextNode;
            int result = BitConverter.ToInt32(data, 0);
            nextNode = BitConverter.ToInt32(data, 4);

            if(nextNode == 0)
            {
                currentNode = -1;
                return 0;
            }

            return result;
        }

        public override bool Exists()
        {
            if (currentNode == -1)
                return false;

            Byte[] data = new Byte[4];
            fs.Seek(currentNode+4, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            if (BitConverter.ToInt32(data, 0) == -1)
                return false;

            return true;
        }

        public override void Put(int element)
        {
            Byte[] data = BitConverter.GetBytes(element);
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Write(data, 0, 4);
        }

        public override void Swap(int a, int b)
        {
            Byte[] data;
            fs.Seek(prevNode, SeekOrigin.Begin);
            data = BitConverter.GetBytes(a);
            fs.Write(data, 0, 4);
            fs.Seek(currentNode, SeekOrigin.Begin);
            data = BitConverter.GetBytes(b);
            fs.Write(data, 0, 4);
        }
    }
}
