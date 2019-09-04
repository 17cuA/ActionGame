using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tststs : MonoBehaviour
{
	// -2,-1.4,-7.7
	[SerializeField]
	Vector3 finalPosDistance;
	public GameObject finalPosObject;
	public bool posJughe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(finalPosDistance != transform.position)
		{
			DistanceJughe();
		}
		else
		{
			posJughe = true;
		}
    }
	public void  DistanceJughe()
	{
		finalPosDistance = transform.position - finalPosObject.transform.position;
		transform.position -= finalPosDistance / 2;
	}
}
