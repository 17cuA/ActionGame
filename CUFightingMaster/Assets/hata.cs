using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hata : MonoBehaviour
{
	float tim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		float duralation = 1f;
		float angle = 100;
		tim += Time.deltaTime * angle / duralation;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.PingPong(tim, angle) - angle / 2);
    }
}
