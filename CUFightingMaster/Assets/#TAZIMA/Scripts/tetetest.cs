using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class tetetest : MonoBehaviour
{
	private int xx = -1;
    // Start is called before the first frame update
    void Start()
    {
		Profiler.BeginSample("#### System.Math.Abs ####");
		for (int i = 0; i < 10000; i++)
		{
			System.Math.Abs(xx);
		}
		Profiler.EndSample();

		Profiler.BeginSample("#### UnityEngine.Mathf.Abs ####");
		for (int i = 0; i < 10000; i++)
		{
			Mathf.Abs(xx);
		}
		Profiler.EndSample();
	}
}
