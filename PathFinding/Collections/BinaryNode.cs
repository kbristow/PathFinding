using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinding.Collections
{
    //Stores a binary node of type T
    public class BinaryNode<T>
    {
        public T Node { get; set; }
        public float Value { get; set; }

        public BinaryNode(T node, float value)
        {
            this.Node = node;
            this.Value = value;
        }
    }
}