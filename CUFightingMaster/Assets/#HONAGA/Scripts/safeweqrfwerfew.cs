using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class safeweqrfwerfew : MonoBehaviour
{
    public FighterStatus[] asdf;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            GameDataStrage.Instance.fighterStatuses[0] = asdf[0];
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            GameDataStrage.Instance.fighterStatuses[0] = asdf[1];
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            GameDataStrage.Instance.fighterStatuses[0] = asdf[2];
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Result");
        }
    }
}
