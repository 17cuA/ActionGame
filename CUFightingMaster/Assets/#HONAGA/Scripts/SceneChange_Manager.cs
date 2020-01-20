using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange_Manager : MonoBehaviour
{
    public CanvasController_CharacterSelect canvasController_CharacterSelect;
    public CharacterSelect_Manager CharacterSelectManager;

    [SerializeField]
    private AsyncOperation async;
    void Start()
    {
        canvasController_CharacterSelect.InitDownCurtain();
		CharacterSelectManager = GameObject.Find("CharacterSelectManager").GetComponent<CharacterSelect_Manager>();
        async = SceneManager.LoadSceneAsync("Result");
        async.allowSceneActivation = false;
    }

    void Update()
    {
        if (CharacterSelectManager.sceneChangeJughe == false)
        {
			canvasController_CharacterSelect.UpCurtain();
		}
        if (CharacterSelectManager.sceneChangeJughe == true && CharacterSelectManager.FadeFrame >= 2.0f)
        {
            if (canvasController_CharacterSelect.DownCurtain())
            {
                async.allowSceneActivation = true;
            }
        }
    }
}
