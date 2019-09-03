using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine.Pattern;

public class SelectCharacterSave : SingletonMono<SelectCharacterSave>
{
	public Cursor_CharacterSelect[] cursor_CharacterSelects = new Cursor_CharacterSelect[2];
	[SerializeField]
	private FighterStatus[] selectFighterStatuses = new FighterStatus[2];
	public FighterStatus[] fighterStatuses = new FighterStatus[2];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
        if (cursor_CharacterSelects[0] != null)
        {
            if (cursor_CharacterSelects[0].selectDir < 2)
            {
                selectFighterStatuses[0] = fighterStatuses[0];
            }
            else
            {
                selectFighterStatuses[0] = fighterStatuses[1];
            }

            if (cursor_CharacterSelects[1].selectDir < 2)
            {
                selectFighterStatuses[1] = fighterStatuses[0];
            }
            else
            {
                selectFighterStatuses[1] = fighterStatuses[1];
            }
        }
    }
}
