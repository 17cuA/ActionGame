using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CUEngine.Pattern;

public class CharacterSelect_Manager : SingletonMono<CharacterSelect_Manager>
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
		previewModel[0] = Instantiate(currentCharacter[0].PlayerModel, createCharaPos[0].transform.position, createCharaPos[0].transform.rotation);
		previewModel[1] = Instantiate(currentCharacter[1].PlayerModel, createCharaPos[0].transform.position, createCharaPos[0].transform.rotation);
		previewModel[2] = Instantiate(currentCharacter[0].PlayerModel, createCharaPos[1].transform.position, createCharaPos[1].transform.rotation);
		previewModel[3] = Instantiate(currentCharacter[1].PlayerModel, createCharaPos[1].transform.position, createCharaPos[1].transform.rotation);
	}

	void Update()
    {
		if (cursor1_1.selectDir < 2)
		{
			CharacterNamePanels[0].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
			CharacterNamePanels[2].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
			previewModel[0].SetActive(true);
			previewModel[1].SetActive(false);
		}
		else
		{
			CharacterNamePanels[0].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
			CharacterNamePanels[2].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
			previewModel[0].SetActive(false);
			previewModel[1].SetActive(true);
		}

		if (cursor1_2.selectDir < 2)
		{
			CharacterNamePanels[1].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
			CharacterNamePanels[3].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
			previewModel[2].SetActive(true);
			previewModel[3].SetActive(false);
		}
		else
		{
			CharacterNamePanels[1].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
			CharacterNamePanels[3].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
			previewModel[2].SetActive(false);
			previewModel[3].SetActive(true);
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
		if(animFlag)
		{
			fedeFrame++;
		}
		if(animFlag == true && animFlagCount > 0)
		{
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
		if(fedeFrame > 60)
		{

		}
	}
}
