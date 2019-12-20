﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectObjectData
{
	private string name;
	public string Name { get { return name; } }

	private ECharacterID charaID;
	public ECharacterID CharaID { get { return charaID; } }

	private FighterStatus[] model;
	public FighterStatus[] Model { get { return model; } }

	private Sprite namePanel;
	public Sprite NamePanel { get { return namePanel; } }

	private GameObject[] panelPosition;
	public GameObject[] PanelPosition { get { return panelPosition; } }


	public CharacterSelectObjectData(string _name,ECharacterID _charaID, FighterStatus[] _model, Sprite _namePanel, GameObject[] _panelPosition)
	{
		name = _name;
		charaID = _charaID;
		model = _model;
		namePanel = _namePanel;
		panelPosition = _panelPosition;
	}
}
