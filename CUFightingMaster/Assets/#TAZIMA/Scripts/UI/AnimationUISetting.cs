
using UnityEngine;

public class AnimationUISetting : MonoBehaviour
{
    public GameObject[] gameObjects;

    void Awake()
    {
        for (int i = 0;i < gameObjects.Length;i++)
        {
			var animUIManagers = gameObjects[i].GetComponents<AnimationUIManager>();
			for (int j = 0;j < animUIManagers.Length; j++)
			{
				animUIManagers[j].Init();
			}
		}
    }
}
