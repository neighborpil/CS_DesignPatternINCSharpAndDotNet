using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec82_IteratorObject
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }
    }

    public class InOrderIterator<T>
    {
        public Node<T> Current;
        public InOrderIterator(Node<T> root)
        {
            
        }

        public bool MoveNext()
        {

        }

        public void Reset()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //   1
            //  / \
            // 2   3

            // in-order: 213

            var root = new Node<int>(1, new Node<int>(2), new Node<int>(3));
        }
    }
}
