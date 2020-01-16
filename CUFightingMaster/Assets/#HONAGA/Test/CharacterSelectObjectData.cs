using System.Collections;
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

	/// <summary>
	/// コンストラクタ、初期化処理
	/// </summary>
	/// <param name="_name">キャラの名前</param>
	/// <param name="_charaID">キャラのID</param>
	/// <param name="_model">キャラのステータスやモデル情報</param>
	/// <param name="_namePanel">キャラの顔パネル</param>
	/// <param name="_panelPosition">顔パネルの位置</param>
	public CharacterSelectObjectData(string _name,ECharacterID _charaID, FighterStatus[] _model, Sprite _namePanel, GameObject[] _panelPosition)
	{
		name = _name;
		charaID = _charaID;
		model = _model;
		namePanel = _namePanel;
		panelPosition = _panelPosition;
	}
}
