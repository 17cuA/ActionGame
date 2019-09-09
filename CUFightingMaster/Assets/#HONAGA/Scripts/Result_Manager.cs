using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result_Manager : MonoBehaviour
{
    public AnimationUIManager[] animationUIManagers = new AnimationUIManager[2];
    // Start is called before the first frame update
    void Start()
    {
        animationUIManagers[0].isStart = true;
        animationUIManagers[1].isStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
