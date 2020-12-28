using UnityEngine;
using UnityEngine.Jobs;
using Unity.Collections;
using Unity.Burst;
using Unity.Jobs;

#region Instructions
//using Unity.Mathematics;

/*
 *
 * JobTest scene runs very slow because of the repeated dummy math operation below. Implement the for loop below, using parallelized Unity jobs and Burst compiler to gain performance
 * If the 'count' is too large for your machine to handle, you can decrease it.
 * 
 */
#endregion

[BurstCompile]
public struct TestJob : IJobParallelFor {

	public NativeArray<float> values;
	public void Execute(int index)
    {       
        values[index] = Mathf.Sqrt(Mathf.Pow(values[index] + 1.75f, 2.5f + index)) * 5 + 2f;
    }
}

public class JobTest : MonoBehaviour
{
	[SerializeField]
	private bool useJob = false;

	private int count = 1000000;
	private float[] values;

	void Start()
	{
		values = new float[count];
	}  
    void Update()
	{
		if (useJob)
		{
			NativeArray<float> jobValues = new NativeArray<float>(count, Allocator.TempJob);
			TestJob job = new TestJob
			{
				values = jobValues
			};
			JobHandle jobHandle = job.Schedule(count, 1);
			jobHandle.Complete();
			for (int i = 0; i < values.Length; i++)
			{
				values[i] = jobValues[i];
			}
			jobValues.Dispose();
		}
		else
		{
			for (int i = 0; i < values.Length; i++)
			{
				values[i] = Mathf.Sqrt(Mathf.Pow(values[i] + 1.75f, 2.5f + i)) * 5 + 2f;
			}
		}
	}
}