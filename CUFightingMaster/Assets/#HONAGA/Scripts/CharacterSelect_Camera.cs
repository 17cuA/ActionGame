using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect_Camera : MonoBehaviour
{
    float f = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.eulerAngles.y <= -13.0f)
        {
            f *= -1.0f;
        }
        else if(transform.rotation.eulerAngles.y >= 35.0f)
        {
            f *= -1.0f;
        }
        transform.Rotate(0.0f, f, 0.0f);
    }
}
