using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneChange_Manager : MonoBehaviour
{
    public CharacterSelect_Manager Manager;
    public Image Canvas_One;
    public Image Canvas_Two;

    private bool isBirght = false;

    private AsyncOperation async;
    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("Manager").GetComponent<CharacterSelect_Manager>();
        async = SceneManager.LoadSceneAsync("Battle");
        async.allowSceneActivation = false;

        Canvas_One.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        Canvas_Two.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        isBirght = false;

    }

    // Update is called once per frame
    void Update()
    {

        if(Manager.sceneChangeJughe == true && Manager.FadeFrame >= 1.0f && isBirght == true)
        {
            Canvas_One.color += new Color(0.0f,0.0f,0.0f,0.02f);
            Canvas_Two.color += new Color(0.0f, 0.0f, 0.0f, 0.02f);
        }
         if (Canvas_One.color.a >= 1.0f && Canvas_Two.color.a >= 1.0f && isBirght == true)
        {
            async.allowSceneActivation = true;
        }
        if (isBirght == false)
        {
            Canvas_One.color -= new Color(0.0f, 0.0f, 0.0f, 0.02f);
            Canvas_Two.color -= new Color(0.0f, 0.0f, 0.0f, 0.02f);
            if (Canvas_One.color.a <= 0.0f && Canvas_Two.color.a <= 0.0f )
                isBirght = true;
        }
    }
}
