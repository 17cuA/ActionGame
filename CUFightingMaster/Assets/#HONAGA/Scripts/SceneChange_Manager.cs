using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneChange_Manager : MonoBehaviour
{
    public CanvasController_CharacterSelect canvasController_CharacterSelect;
    public CharacterSelect_Manager Manager;

    [SerializeField]
    private AsyncOperation async;
    // Start is called before the first frame update
    void Start()
    {
        canvasController_CharacterSelect.InitDownCurtain();
		Manager = GameObject.Find("Manager").GetComponent<CharacterSelect_Manager>();
        async = SceneManager.LoadSceneAsync("Battle");
        async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.sceneChangeJughe == false)
        {
			canvasController_CharacterSelect.UpCurtain();

		}
        if (Manager.sceneChangeJughe == true && Manager.FadeFrame >= 2.0f)
        {
            if (canvasController_CharacterSelect.DownCurtain())
            {
                async.allowSceneActivation = true;
            }
        }
    }
}
