using PathFinding.General;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class ExtendedPathNode : PathNode
    {
        public bool Walkable { get; set; }

		public GameObject NodeObject {get; set;}

		public String NodeType { get; set; }

        public ExtendedPathNode(int x, int y):base(x,y)
        {
            Walkable = true;
			NodeType = "None";
        }
    }
}