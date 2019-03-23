using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count_Bucket_Sort
{
    class MyList : DataList
    {
        class Node
        {
            public Node nextNode { get; set; }
            public int data { get; set; }

            public Node(int data, Node next)
            {
                this.data = data;
                this.nextNode = next;
            }
        }

        Node headNode;
        Node prevNode;
        Node currentNode;

        public MyList()
        {
            headNode = null;
            prevNode = null;
            currentNode = null;
        }

        public MyList(int n, int seed, int range)
        {
            length = n;
            Random rand = new Random(seed);

            headNode = new Node(rand.Next(0, range), null);
            currentNode = headNode;

            for(int i = 1; i < length; i++)
            {
                prevNode = currentNode;
                currentNode.nextNode = new Node(rand.Next(0, range), null);
                currentNode = currentNode.nextNode;
            }
        }

        public MyList(int[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                this.Put(data[i]);
            }
        }

        /// <summary>
        /// Adds element to list
        /// </summary>
        /// <param name="data">Element</param>
        public override void Put(int data)
        {
            Node temp = new Node(data, null);

            if(headNode == null)
            {
                headNode = temp;
                currentNode = headNode;
            }
            else
            {
                currentNode.nextNode = temp;
                currentNode = temp;
            }

            length++;
        }

        /// <summary>
        /// Changes current node's data
        /// </summary>
        /// <param name="data">Element</param>
        public override void ChangeData(int data)
        {
            if (currentNode == null)
                return;

            currentNode.data = data;
        }

        /// <summary>
        /// Finds min value
        /// </summary>
        /// <returns>Min value</returns>
        public override int Min()
        {
            int minValue = headNode.data;
            for(Node i = headNode.nextNode; i != null; i = i.nextNode)
            {
                if(i.data < minValue)
                    minValue = i.data;
            }

            return minValue;
        }

        /// <summary>
        /// Finds max value
        /// </summary>
        /// <returns>Max value</returns>
        public override int Max()
        {
            int maxValue = headNode.data;
            for (Node i = headNode.nextNode; i != null; i = i.nextNode)
            {
                if (i.data > maxValue)
                    maxValue = i.data;
            }

            return maxValue;
        }

        /// <summary>
        /// Assigns current node to first node
        /// </summary>
        /// <returns>First elemet</returns>
        public override int Head()
        {
            currentNode = headNode;
            prevNode = null;
            return currentNode.data;
        }

        /// <summary>
        /// Checks if current node exists
        /// </summary>
        /// <returns>True if exists, false if not</returns>
        public override bool Exists()
        {
            return currentNode != null;
        }

        /// <summary>
        /// Selects next node, and return current node's data
        /// </summary>
        /// <returns>Current node's data</returns>
        public override int Next()
        {
            prevNode = currentNode;
            if (currentNode.nextNode == null)
            {
                currentNode = null;
                return 0;
            }

            currentNode = currentNode.nextNode;
            return currentNode.data;
        }

        /// <summary>
        /// Returns current node's data
        /// </summary>
        /// <returns>Data</returns>
        public override int Current()
        {
            return currentNode.data;
        }

        /// <summary>
        /// Changes data
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public override void Swap(int a, int b)
        {
            prevNode.data = a;
            currentNode.data = b;
        }
    }
}
