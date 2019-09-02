using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUISetting : MonoBehaviour
{
    public GameObject[] gameObjects;

    void Start()
    {
        for (int i = 0;i < gameObjects.Length;i++)
        {
            gameObjects[i].GetComponent<AnimationUIManager>().Init();
        }
    }
}
