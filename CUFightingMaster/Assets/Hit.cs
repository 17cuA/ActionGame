using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
	public GameObject EF;
	public GameObject SE;
	Vector3 InstantiatePosition;

    // Start is called before the first frame update
    void Start()
    {
		InstantiatePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Hit");
		Instantiate(EF, InstantiatePosition, Quaternion.identity);
		Instantiate(SE, InstantiatePosition, Quaternion.identity);
	}
}
