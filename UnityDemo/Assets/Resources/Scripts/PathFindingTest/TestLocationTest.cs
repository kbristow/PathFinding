using System;
using PathFinding.Dijkstra;
namespace Test
{
	public class TestLocationTest:ILocationTest
	{
		TestGrid worldGrid;
		public String NodeType {get; set;}
		public TestLocationTest (TestGrid worldGrid, String nodeType)
		{
			NodeType = nodeType;
			this.worldGrid = worldGrid;
		}

		public bool testLocation (int x, int y){
			UnityEngine.Vector2 target = new UnityEngine.Vector2(x,y);
			if(TestBot2.testbot2Targets.Contains(target)){
				return false;
			}
			bool isCorrectNodeType = worldGrid.Grid[x,y].NodeType == NodeType;
			if(isCorrectNodeType){
				TestBot2.testbot2Targets.Add(target);
			}
			return isCorrectNodeType;
		}
	}
}