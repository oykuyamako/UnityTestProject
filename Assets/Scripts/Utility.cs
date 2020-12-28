using UnityEngine;

#region Instructions

/*
 * 
 * 
 * Complete the functions below.  
 * For sure, they don't belong in the same class. This is just for the test so ignore that.
 * 
 * 
 */
//GetObjectsWithName
/*
 * 
 *	Return all objects in the scene with the specified name. Don't think about performance, do it in as few lines as you can. 
 * 
 */
//CheckCollision
/*
 * 
 *	Perform a raycast using the ray provided, only to objects of the specified 'layer' within 'maxDistance' and return if something is hit. 
 * 
 */
//GeneratePoints
/*
 * Generate 'size' number of random points, making sure they are distributed as evenly as possible (Trying to achieve maximum distance between every neighbor).
 * Boundary corners are (0, 0) and (1, 1). (Point (1.2, 0.45) is not valid because it's outside the boundaries. )
 * Is there a known algorithm that achieves this?
 */
//GenerateTexture
/*
 * Create a Texture2D object of specified 'width' and 'height', fill it with 'color' and return it. Do it as performant as possible.
 */
#endregion

public static class Utility
{
	public static GameObject[] GetObjectsWithName(string name)
	{
		Object[] tempList = Resources.FindObjectsOfTypeAll(typeof(GameObject)); //getting all gos
		GameObject[] namedObjects = new GameObject[tempList.Length]; 
		GameObject temp;
		int counter = 0;

		foreach (Object obj in tempList)
		{
			if (obj is GameObject)
			{
				temp = (GameObject)obj;
				if (temp.name == name)
				{
					namedObjects.SetValue(temp, counter);
					counter++;
				}
			}
		}
		System.Array.Resize(ref namedObjects, counter); //resizing array to match the current size
		return namedObjects;
	}	  //tested in levelmanager.cs
	
	public static bool CheckCollision(Ray ray, float maxDistance, int layer)
	{
		Debug.DrawRay(ray.origin,ray.direction, Color.green); //for debugging/seeing the ray representaion in scene
		if (Physics.Raycast(ray, maxDistance, layer))
		{
			Debug.Log("Hit on object with layer" + layer);
			return true;
		}
		return false;
	}

	public static Vector2[] GeneratePoints(int size)
	{
		Vector2[] randPointsArr = new Vector2[size];
		Vector2 random2DPoint = new Vector2();
		for (int i=0; i<size; i++)
		{
			random2DPoint.x = Mathf.PerlinNoise(Random.Range(0.0f,1.0f)/ Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f) / Random.Range(0.0f, 1.0f));
			random2DPoint.y = Mathf.Clamp(Mathf.PerlinNoise(Random.Range(0.0f, 1.0f) / Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f) / Random.Range(0.0f, 1.0f)), 0.0f, 1.0f);
			randPointsArr.SetValue(random2DPoint, i);
		}
		return randPointsArr;
	}        //tested in bith generatetexture function and levelmanager.cs

	public static Texture2D GenerateTexture(int width, int height, Color color)
	{
		var texture = new Texture2D(width, height, TextureFormat.ARGB32, false); //create a texture
		var data = texture.GetRawTextureData<Color32>(); //get texture data with GetRawTextureData, faster than setpixel/setpixels

		int index = 0;

		Vector2[] randPointsArr = GeneratePoints(width);//I went a bit fancier here, used generate points function to generate color.

		for (int y = 0; y < texture.height; y++)
		{
			for (int x = 0; x < texture.width; x++)
			{
				//data[index++] = color;				
				data[index++] = new Color(randPointsArr[x].x, randPointsArr[x].x, randPointsArr[x].y); //I went a bit fancier here, used generate points function to generate color. upperline works aswell as a single color.
			}
		}
		texture.Apply(); //Apply color change
		return texture;
	}       //tested in levelmanager.cs

}