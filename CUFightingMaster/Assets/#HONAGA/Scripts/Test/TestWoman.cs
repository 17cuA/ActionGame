using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWoman : MonoBehaviour
{
	public GameObject[] gameObjects;

	public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		float unko = (Time.time * speed) / Unko();
		transform.position = Vector3.Lerp(gameObjects[0].transform.position, gameObjects[1].transform.position, unko);
    }
	public float Unko()
	{
		return Vector3.Distance(gameObjects[0].transform.position, gameObjects[1].transform.position);
	}
}
