using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result_Manager : MonoBehaviour
{
    public GameObject[] animationUIManagers = new GameObject[2];
    public GameObject[] winOrlose = new GameObject[2];
    public GameObject[] targetPos = new GameObject[2];
    public AnimationClip[] animationClips;

    public Sprite win;
    public Sprite lose;
    // Start is called before the first frame update
    void Start()
    {
        //var obj = Instantiate(GameDataStrage.Instance.fighterStatuses[0].fighter, targetPos[0].transform.position, Quaternion.identity);
        //obj.gameObject.layer = LayerMask.NameToLayer(CommonConstants.Layers.Player_One);
        //var obj2 = Instantiate(GameDataStrage.Instance.fighterStatuses[1].fighter, targetPos[1].transform.position, Quaternion.identity);
        //obj.gameObject.layer = LayerMask.NameToLayer(CommonConstants.Layers.Player_Two);

        if (GameDataStrage.Instance.winFlag_PlayerOne)
        {
            winOrlose[0].GetComponent<Image>().sprite = win;
            winOrlose[1].GetComponent<Image>().sprite = lose;
            winOrlose[0].GetComponent<NomalAnimationPlayer>().SetPlayAnimation(animationClips[0], 1.0f, 0);
            winOrlose[1].GetComponent<NomalAnimationPlayer>().SetPlayAnimation(animationClips[1], 1.0f, 0);
        }
        else if(GameDataStrage.Instance.winFlag_PlayerTwo)
        {
            winOrlose[1].GetComponent<Image>().sprite = win;
            winOrlose[0].GetComponent<Image>().sprite = lose;
            winOrlose[0].GetComponent<NomalAnimationPlayer>().SetPlayAnimation(animationClips[1], 1.0f, 0);
            winOrlose[1].GetComponent<NomalAnimationPlayer>().SetPlayAnimation(animationClips[0], 1.0f, 0);
        }
        animationUIManagers[0].GetComponent<AnimationUIManager>().isStart = true;
        animationUIManagers[1].GetComponent<AnimationUIManager>().isStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.F1) && !Input.GetKeyDown(KeyCode.F2) && !Input.GetKeyDown(KeyCode.F3) && !Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("JECLogo");
        }
    }
}
