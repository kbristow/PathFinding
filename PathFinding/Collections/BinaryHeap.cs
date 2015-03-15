using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinding.Collections
{

    //Implementation of the Data Structures used in AStar and Dijkstra searches
    //TODO: why did I do this T generic?
    public class BinaryHeap<T>
    {
        List<BinaryNode<T>> tree;

        public List<BinaryNode<T>> Tree { get { return tree; } }

        public BinaryHeap()
        {
            tree = new List<BinaryNode<T>>();
        }

        public void insertNode(BinaryNode<T> newNode)
        {
            tree.Add(newNode);
            trickleUp(tree.Count - 1);
        }


        public void trickleUp(int currentIndex)
        {
            bool correctOrder = false;
            int parentIndex = getParentIndex(currentIndex);
            float nodeValue = tree[currentIndex].Value;
            while (!correctOrder && parentIndex != -1)
            {
                if (tree[parentIndex].Value > nodeValue)
                {
                    swapNodes(parentIndex, currentIndex);
                    currentIndex = parentIndex;
                    parentIndex = getParentIndex(currentIndex);
                }
                else
                {
                    correctOrder = true;
                }
            }
        }

        public void trickleDown(int currentIndex)
        {
            bool correctOrder = false;
            float nodeValue = tree[currentIndex].Value;
            while (!correctOrder)
            {
                int leftChild = getChildLeftIndex(currentIndex);
                int rightChild = getChildRightIndex(currentIndex);
                int minIndex = currentIndex;
                float minValue = nodeValue;

                if (leftChild < tree.Count && minValue > tree[leftChild].Value)
                {
                    minIndex = leftChild;
                    minValue = tree[leftChild].Value;
                }

                if (rightChild < tree.Count && minValue > tree[rightChild].Value)
                {
                    minIndex = rightChild;
                }

                if (minIndex == currentIndex)
                {
                    correctOrder = true;
                }
                else
                {
                    swapNodes(currentIndex, minIndex);
                    currentIndex = minIndex;
                }
            }
        }


        public BinaryNode<T> peek()
        {
            BinaryNode<T> returnNode = null;
            int treeCount = tree.Count;
            if (treeCount >= 1)
            {
                returnNode = tree[0];
            }
            return returnNode;
        }


        public BinaryNode<T> pop()
        {
            BinaryNode<T> returnNode = null;
            int treeCount = tree.Count;
            if (treeCount > 1)
            {
                returnNode = tree[0];
                tree[0] = tree[treeCount - 1];
                tree.RemoveAt(treeCount - 1);
                trickleDown(0);
            }
            else if (treeCount == 1)
            {
                returnNode = tree[0];
                tree.Clear();
            }
            return returnNode;
        }


        public void adjustPriority(int index, float newValue)
        {
            if (index < tree.Count)
            {
                float oldValue = tree[index].Value;
                tree[index].Value = newValue;
                if (newValue < oldValue)
                {
                    trickleUp(index);
                }
                else
                {
                    trickleDown(index);
                }
            }
        }



        public int getParentIndex(int nodeInd)
        {
            if (nodeInd == 0)
            {
                return -1;
            }
            return (nodeInd - 1) / 2;
        }

        public int getChildRightIndex(int nodeInd)
        {
            return (2 * nodeInd + 2);
        }

        public int getChildLeftIndex(int nodeInd)
        {
            return (2 * nodeInd + 1);
        }

        public void swapNodes(int i1, int i2)
        {
            BinaryNode<T> temp = tree[i1];
            tree[i1] = tree[i2];
            tree[i2] = temp;
        }

        public String toString()
        {
            String ret = "";
            for (int i = 0; i < tree.Count; i++)
            {
                ret += tree[i].Value + " ";
            }

            return ret;
        }
    }
}