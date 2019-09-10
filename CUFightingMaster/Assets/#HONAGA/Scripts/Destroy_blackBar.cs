using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_blackBar : MonoBehaviour
{
    public CharacterSelect_Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<CharacterSelect_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.sceneChangeJughe == true)
        {
            Destroy(gameObject);
        }
    }
}
