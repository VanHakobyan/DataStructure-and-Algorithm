using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackAndQueue
{
    public class QueueStructure
    {
        private Queue queue;

        public QueueStructure(int value)
        {
            queue.items = new int[value];
        }

        public void Demo()
        {
            queue.Front = 0;
            queue.Rear = -1;
            var items = Convert.ToInt16(Console.ReadLine());
            for (int i = 0; i < queue.items.Length; i++)
            {
                Console.Write("Enter the " + i.ToString() + " Value :");
                Insert(Convert.ToInt16(Console.ReadLine()));
            }
            Display();
            Delete();
            Display();
            Delete();
            Display();
            Delete();
            Display();
            Console.ReadLine();
        }

        public void Display()
        {
            Console.WriteLine("The items are");
            if (queue.Rear < queue.Front)
            {
                Console.WriteLine("Queue is Empty");
            }
            else
            {
                for (int i = queue.Front; i <= queue.Rear; i++)
                {
                    Console.WriteLine(queue.items[i]);
                }
            }
        }
        public int Delete()
        {

            if (queue.Front > queue.Rear)
                Console.WriteLine("Queue is empty");
            else
                queue.Front++;
            return -1;
        }

        public void Insert(int value)
        {
            queue.Rear++;
            queue.items[queue.Rear] = value;
        }
    }
}
