using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CUEngine.Pattern;

public class CharacterSelect_Manager : MonoBehaviour
{
    public Cursor_CharacterSelect cursor1_1;
    public Cursor_CharacterSelect cursor1_2;
    public Cursor_CharacterSelect cursor2_1;
    public Cursor_CharacterSelect cursor2_2;

    public GameObject[] CharacterNamePanels = new GameObject[4];
    public GameObject[] previewModel = new GameObject[4];
    public GameObject[] createCharaPos = new GameObject[2];

    public Sprite[] SelectCharacterNamePanels = new Sprite[2];

    [SerializeField]
    private bool[] CharacterSelectBool = { false, false, false, false };

    public FighterStatus[] currentCharacter = new FighterStatus[2];

    [SerializeField]
    private bool animFlag;
    [SerializeField]
    private int animFlagCount;
    [SerializeField]
    int fedeFrame;
    // Start is called before the first frame update
    void Start()
    {
        animFlag = false;
        animFlagCount = 1;
        fedeFrame = 0;
        if (currentCharacter[0] == null)
        {
            return;
        }
        previewModel[0] = Instantiate(currentCharacter[0].PlayerModel, createCharaPos[0].transform.position, createCharaPos[0].transform.rotation);
        previewModel[1] = Instantiate(currentCharacter[1].PlayerModel, createCharaPos[0].transform.position, createCharaPos[0].transform.rotation);
        previewModel[2] = Instantiate(currentCharacter[0].PlayerModel, createCharaPos[1].transform.position, createCharaPos[1].transform.rotation);
        previewModel[3] = Instantiate(currentCharacter[1].PlayerModel, createCharaPos[1].transform.position, createCharaPos[1].transform.rotation);
    }

    void Update()
    {
		if (cursor1_1 == null)
		{
            return;
        }
        if (cursor1_1.selectDir < 2)
        {
            if (SelectCharacterNamePanels[0] == null)
            {
                return;
            }
            if (CharacterNamePanels[0] != null)
            {
                CharacterNamePanels[0].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
            }
            if (CharacterNamePanels[2] != null)
            {
                CharacterNamePanels[2].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
            }
            if (previewModel[0] != null)
            {
                previewModel[0].SetActive(true);
            }
            if (previewModel[1] != null)
            {
                previewModel[1].SetActive(false);
            }
        }
        else
        {
            if (SelectCharacterNamePanels[1] == null)
            {
                return;
            }
            if (CharacterNamePanels[0] != null)
            {
                CharacterNamePanels[0].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
            }
            if (CharacterNamePanels[2] != null)
            {
                CharacterNamePanels[2].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
            }
            if (previewModel[0] != null)
            {
                previewModel[0].SetActive(false);
            }
            if (previewModel[1] != null)
            {
                previewModel[1].SetActive(true);
            }
        }

        if (cursor1_2.selectDir < 2)
        {
            if (SelectCharacterNamePanels[1] == null)
            {
                return;
            }
            if (CharacterNamePanels[1] != null)
            {
                CharacterNamePanels[1].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
            }
            if (CharacterNamePanels[3] != null)
            {
                CharacterNamePanels[3].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
            }
            if (previewModel[2] != null)
            {
                previewModel[2].SetActive(true);
            }
            if (previewModel[3] != null)
            {
                previewModel[3].SetActive(false);
            }
        }
        else
        {
            if (SelectCharacterNamePanels[1] == null)
            {
                return;
            }
            if (CharacterNamePanels[1] != null)
            {
                CharacterNamePanels[1].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
            }
            if (CharacterNamePanels[3] != null)
            {
                CharacterNamePanels[3].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
            }
            if (previewModel[2] != null)
            {
                previewModel[2].SetActive(false);
            }
            if (previewModel[3] != null)
            {
                previewModel[3].SetActive(true);
            }
        }

        CharacterSelectBool[0] = cursor1_1.Determining_decision;
        CharacterSelectBool[1] = cursor1_2.Determining_decision;
        CharacterSelectBool[2] = cursor2_1.Determining_decision;
        CharacterSelectBool[3] = cursor2_2.Determining_decision;

        if (CharacterSelectBool[0] && CharacterSelectBool[1] && CharacterSelectBool[2] && CharacterSelectBool[3])
        {
            animFlag = true;
            //SceneManager.LoadScene("Battle");
        }
        if (animFlag)
        {
            fedeFrame++;
        }
        if (animFlag == true && animFlagCount > 0)
        {
            if (cursor1_1.characterPanels[0] == null)
            {
                return;
            }
            animFlagCount--;
            cursor1_1.characterPanels[0].GetComponent<Animation>().Play();
            cursor1_1.characterPanels[1].GetComponent<Animation>().Play();
            cursor1_1.characterPanels[2].GetComponent<Animation>().Play();
            cursor1_1.characterPanels[3].GetComponent<Animation>().Play();

            cursor2_1.characterPanels[0].GetComponent<Animation>().Play();
            cursor2_1.characterPanels[1].GetComponent<Animation>().Play();
            cursor2_1.characterPanels[2].GetComponent<Animation>().Play();
            cursor2_1.characterPanels[3].GetComponent<Animation>().Play();


        }
        if (fedeFrame > 60)
        {
			SceneManager.LoadScene("Battle");
		}
    }
}
