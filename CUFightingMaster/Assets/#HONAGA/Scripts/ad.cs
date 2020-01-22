using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ad : MonoBehaviour
{
    GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        game = gameObject;
        Adc(ref game);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Adc(ref GameObject _ga)
    {
        Adc2(ref _ga);
    }
    void Adc2(ref GameObject _ga)
    {
        _ga.AddComponent<Image>();
    }
}
