using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectInputManager : MonoBehaviour
{
   public  CharacterSelectManager characterSelectManager_P1;
    public CharacterSelectManager characterSelectManager_P2;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            characterSelectManager_P1.SelectLeft();
        }
        else if (Input.GetKeyDown("s"))
        {
            characterSelectManager_P1.SelectRight();
        }
        //else if(Input.GetKeyDown("z"))
        //{
        //    characterSelectManager_P2.SelectLeft();
        //}
        //else if (Input.GetKeyDown("x"))
        //{
        //    characterSelectManager_P2.SelectRight();
        //}
        else if(Input.GetKeyDown("b"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
