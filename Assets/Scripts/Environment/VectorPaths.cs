using UnityEngine;
using System.Collections.Generic;

public static class VectorPaths
{	
	private static List<Vector3[]> _allPaths = new List<Vector3[]>();
	
	public static void GenerateLinearPath(Vector3 start, Vector3 end, bool reverse)
	{
		Vector3[] path = new Vector3[2];
		
		path[0] = start;
		path[1] = end;
		
		if(reverse)
			System.Array.Reverse(path);
		
		_allPaths.Add(path);
	}
	
	public static void GenerateSinePath(int points, float length, float periods,
										float amplitude, float shiftX,
										float shiftY, bool reverse)
	{
		Vector3[] path = new Vector3[points];
		float period = length / periods;
		float b = 2*Mathf.PI / period;
		float step = length/(points-1);
		
		for(int i=0; i<points; ++i)
		{
			path[i] = new Vector3(i*step,
								amplitude*Mathf.Sin(b*(i*step-shiftX))+shiftY,
								0.0f);
		}
		
		if(reverse)
			System.Array.Reverse(path);
		
		_allPaths.Add(path);
	}
	
	public static void GeneratePath(Vector3[] vectorArray)
	{
		_allPaths.Add(vectorArray);
	}
	
	public static Vector3[] PathAtIndex(int i)
	{
		if(i >= 0 && i<_allPaths.Count)
			return _allPaths[i];
		else
			return null;
	}
	
	public static List<Vector3[]> AllPaths
	{
		get { return _allPaths; }
	}
}