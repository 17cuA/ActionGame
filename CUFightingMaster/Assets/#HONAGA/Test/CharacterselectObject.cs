using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//
//
// キャラクターにIDを持たせて管理
public enum ECharacterID
{
    CLIKO,
    OBACHAN,
    KUIDAORE,
}
//
// Singleton CharacterselectObject.CharacterselectObjectInstance.xxx の形でアクセス可能
// ここですべてのオブジェクトを操作、管理する
public class CharacterselectObject:MonoBehaviour
{
	#region Singleton
    public static bool dontDs;
    private static CharacterselectObject characterSelectControlInstance;
    public static CharacterselectObject CharacterSelectControlInstance
    {
        get
        {
            if (characterSelectControlInstance == null)
            {
                Type t = typeof(CharacterselectObject);

                characterSelectControlInstance = (CharacterselectObject)FindObjectOfType(t);
                if (characterSelectControlInstance == null)
                {
                    var _ins = new GameObject();
                    _ins.name = "CharacterselectObjectInstance";
                    characterSelectControlInstance = _ins.AddComponent<CharacterselectObject>();
                }
            }

            return characterSelectControlInstance;
        }
    }
    #endregion
	private ECharacterID currentCharacterID = ECharacterID.CLIKO;

    public int playerNumber = 0;
    public Vector2 inputDir = Vector2.zero;
	//public GameObject p1Cursol;
	//public GameObject p2Cursol;
    public GameObject clikoPanel;
    public GameObject obachanPanel;
    public GameObject kuidaorePanel;

	public Characterselect_Timer timer = new Characterselect_Timer();

    public CharacterSelectCursol p1CursolData = new CharacterSelectCursol();
    public CharacterSelectCursol p2CursolData = new CharacterSelectCursol();

    public NamePanel p1NamePanelData = new NamePanel();
    public NamePanel p2NamePanelData = new NamePanel();

    // 各オブジェクトの辞書にデータを登録、初期化処理
    private void Awake()
    {
		timer.Start();
		// キャラクターの種類分ループ
        for(int i = 0; i < System.Enum.GetNames(typeof(ECharacterID)).Length;i++)
        {
            switch(i)
            {
                case 0:
                    p1CursolData.Add(ECharacterID.CLIKO, clikoPanel.transform.position);
                    p2CursolData.Add(ECharacterID.CLIKO, clikoPanel.transform.position);
                    p1NamePanelData.Add(ECharacterID.CLIKO, clikoPanel.GetComponent<Sprite>());
                    p2NamePanelData.Add(ECharacterID.CLIKO, clikoPanel.GetComponent<Sprite>());
                    break;
                case 1:
                    p1CursolData.Add(ECharacterID.OBACHAN, obachanPanel.transform.position);
                    p2CursolData.Add(ECharacterID.OBACHAN, obachanPanel.transform.position);
                    p1NamePanelData.Add(ECharacterID.OBACHAN, obachanPanel.GetComponent<Sprite>());
                    p2NamePanelData.Add(ECharacterID.OBACHAN, obachanPanel.GetComponent<Sprite>());
                    break;
                case 2:
                    p1CursolData.Add(ECharacterID.KUIDAORE, kuidaorePanel.transform.position);
                    p2CursolData.Add(ECharacterID.KUIDAORE, kuidaorePanel.transform.position);
                    p1NamePanelData.Add(ECharacterID.KUIDAORE, kuidaorePanel.GetComponent<Sprite>());
                    p2NamePanelData.Add(ECharacterID.KUIDAORE, kuidaorePanel.GetComponent<Sprite>());
                    break;
            }
        }
    }
    private void Update()
    {
		if (Mathf.Approximately(Time.timeScale, 0f)) return;
		timer.Update();

    }
}
//
//
// カーソルの位置を変更するクラス
[System.Serializable]
public class CharacterSelectCursol: ICharacterSelectable<Vector3>
{
    // キャラクターのIDで位置を管理
    Dictionary<ECharacterID, Vector3> positions = new Dictionary<ECharacterID, Vector3>();
	public GameObject cursol;
	private bool acceptFlag;
	public bool AcceptFlag { get { return acceptFlag; } set { acceptFlag = value; } }

    public Vector3 Getvalue(ECharacterID _ID)
    {
        return positions[_ID];
    }
	//
	// 辞書に登録するAddの処理
	// 第一引数：キャラクターのID 第二引数：キャラクターごとのパネルの位置
    public void Add(ECharacterID _eCharacterID,Vector3 _pos)
    {
        positions.Add(_eCharacterID, _pos);
    }
	//
	public void SelectMove()
    {
        // カーソルの移動処理
    }
	//
	public void CharacterSelectUpdate()
	{

	}
}
//
//
// キャラクターの名前パネルを変更するクラス
[System.Serializable]
public class NamePanel:ICharacterSelectable<Sprite>
{
    // キャラクターのIDでSpriteを管理
    Dictionary<ECharacterID, Sprite> sprites = new Dictionary<ECharacterID, Sprite>();
	public GameObject charaNamePanel;

    public Sprite Getvalue(ECharacterID _eCharacterID)
    {
        return sprites[_eCharacterID];
    }
	//
	// 辞書に登録するAddの処理
	// 第一引数：キャラクターのID 第二引数：キャラクターごとのパネルの画像
    public void Add(ECharacterID _eCharacterID,Sprite sprite)
    {
        sprites.Add(_eCharacterID, sprite);
    }
	//
	public void ChangeName(Sprite _newSprite)
	{
		charaNamePanel.GetComponent<Image>().sprite = _newSprite;
	}
	//
	public void CharacterSelectUpdate()
	{

	}
}
//
//
// キャラクターセレクト画面で使用するオブジェクトに継承させる
public interface ICharacterSelectable<T>
{
	void CharacterSelectUpdate();
    void Add(ECharacterID _ID, T _value);
    T Getvalue(ECharacterID _ID);
}