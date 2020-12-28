using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelObject levelObject1;
    [SerializeField] private LevelObject levelObject2;
    void Start()
    {
        GameObject newLevelObject1 = Instantiate(levelObject1.levelPrefab);
        Renderer lvlObjRenderer = newLevelObject1.GetComponent<Renderer>();
        lvlObjRenderer.material.mainTexture = Utility.GenerateTexture(20,20,new Color(0,0,1)); //testing Utility.GenerateTexture function

        GameObject newLevelObject2 = Instantiate(levelObject2.levelPrefab); //created bunch of cubes to make sure we got the correct result for line 20 - testing Utility.GetObjectsWithName function
        GameObject newLevelObject3 = Instantiate(levelObject2.levelPrefab);
        GameObject newLevelObject4 = Instantiate(levelObject2.levelPrefab);

        GameObject[] cubeGameObjects = Utility.GetObjectsWithName("Cube(Clone)");

        foreach (var go in cubeGameObjects) Debug.Log(go.name); //testing Utility.GetObjectsWithName function

    }
}
