using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect_Manager : MonoBehaviour
{
	public Cursor_CharacterSelect cursor1_1;
	public Cursor_CharacterSelect cursor1_2;
	public Cursor_CharacterSelect cursor2_1;
	public Cursor_CharacterSelect cursor2_2;

	public GameObject[] CharacterNamePanels = new GameObject[4];

	public Sprite[] SelectCharacterNamePanels = new Sprite[2];

	[SerializeField]
	private bool[] CharacterSelectBool = { false, false, false, false };
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (cursor1_1.selectDir < 2)
		{
			CharacterNamePanels[0].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
			CharacterNamePanels[2].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
		}
		else
		{
			CharacterNamePanels[0].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
			CharacterNamePanels[2].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
		}

		if (cursor1_2.selectDir < 2)
		{
			CharacterNamePanels[1].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
			CharacterNamePanels[3].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
		}
		else
		{
			CharacterNamePanels[1].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
			CharacterNamePanels[3].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
		}

		CharacterSelectBool[0] = cursor1_1.Determining_decision;
		CharacterSelectBool[1] = cursor1_2.Determining_decision;
		CharacterSelectBool[2] = cursor2_1.Determining_decision;
		CharacterSelectBool[3] = cursor2_2.Determining_decision;

		if (CharacterSelectBool[0] && CharacterSelectBool[1] && CharacterSelectBool[2] && CharacterSelectBool[3])
		{
			SceneManager.LoadScene("Battle");
		}
    }
}
