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
		// 飯塚追加-------------------------------------------
		Sound.LoadBgm("BGM_Menu", "BGM_Menu");
		Sound.PlayBgm("BGM_Menu", 0.4f, 1, true);
		// ---------------------------------------------------
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

		//決定(シーン移動)
		if (CharacterSelectBool[0] && CharacterSelectBool[1] && CharacterSelectBool[2] && CharacterSelectBool[3])
		{
			// 飯塚追加-------------------------------------------
			Sound.LoadSe("Menu_Decision", "Se_menu_decision");
			Sound.PlaySe("Menu_Decision", 1, 0.3f);
			// ---------------------------------------------------
			SceneManager.LoadScene("Battle");
		}
    }
}
