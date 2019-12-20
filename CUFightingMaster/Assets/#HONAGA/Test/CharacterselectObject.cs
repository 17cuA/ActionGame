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
    //KUIDAORE,
}
//
// Singleton CharacterselectObject.CharacterselectObjectInstance.xxx の形でアクセス可能
// ここですべてのオブジェクトを操作、管理する
public class CharacterselectObject : MonoBehaviour
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
    public FighterStatus[] clicoStatus;
    public FighterStatus[] obachanStatus;
    //public FighterStatus[] kuidaoreStatus;
    public GameObject[] clicoPanel;
    public GameObject[] obachanPanel;
    public GameObject[] kuidaorePanel;
    public Sprite clikoNamePanel;
    public Sprite obachanNamePanel;
    public Sprite kuidaoreNamePanel;

    List<CharacterSelectObjectData> characterSelectObjectDatas = new List<CharacterSelectObjectData>();

    public Characterselect_Timer timer;

    public CharacterModel p1CharacterModelData = new CharacterModel();
    public CharacterModel p2CharacterModelData = new CharacterModel();

    public CharacterSelectCursol p1CursolData = new CharacterSelectCursol();
    public CharacterSelectCursol p2CursolData = new CharacterSelectCursol();

    public NamePanel p1NamePanelData = new NamePanel();
    public NamePanel p2NamePanelData = new NamePanel();

    // 各オブジェクトの辞書にデータを登録、初期化処理
    private void Awake()
    {
        // キャラクターごとに追加
        characterSelectObjectDatas.Add(new CharacterSelectObjectData("cliko", ECharacterID.CLIKO, clicoStatus, clikoNamePanel, clicoPanel));
        characterSelectObjectDatas.Add(new CharacterSelectObjectData("obachan", ECharacterID.OBACHAN, obachanStatus, obachanNamePanel, obachanPanel));
        //characterSelectObjectDatas.Add(new CharacterSelectObjectData("kuidalre",ECharacterID.KUIDAORE, kuidaoreStatus, kuidaoreNamePanel, kuidaorePanel,characterInstancePos));

        for (int i = 0; i < characterSelectObjectDatas.Count; i++)
        {
            p1CharacterModelData.CreateCharacter(characterSelectObjectDatas[i],"Player1用モデル");
            p2CharacterModelData.CreateCharacter(characterSelectObjectDatas[i],"Player2用モデル");
		}	

        CanvasController_CharacterSelect.CanvasControllerInstance.InitDownCurtain();

        timer.Start();
	}

	public void Test(params int[] nums)
	{

	}

    private void Update()
    {
        // ポーズ処理
        if (Mathf.Approximately(Time.timeScale, 0f)) return;
        if (CanvasController_CharacterSelect.CanvasControllerInstance.curtainFlag == false)
        {
            // カーテンが上がるまでループ
            CanvasController_CharacterSelect.CanvasControllerInstance.curtainFlag = CanvasController_CharacterSelect.CanvasControllerInstance.UpCurtain();
            return;
        }

        timer.Update();
        p1CursolData.Update(characterSelectObjectDatas);
        p2CursolData.Update(characterSelectObjectDatas);
        p1NamePanelData.ChangeName(characterSelectObjectDatas[(int)p1CursolData.currentCharacter].NamePanel);
        p2NamePanelData.ChangeName(characterSelectObjectDatas[(int)p2CursolData.currentCharacter].NamePanel);
        //if(p1CursolData.AcceptFlag)
        //{
        //    p1CharacterModelData.ChangeAnimation(p1CursolData.currentCharacter);
        //}
        //if (p2CursolData.AcceptFlag)
        //{
        //    p2CharacterModelData.ChangeAnimation(p2CursolData.currentCharacter);
        //}

    }
}
//
//
// カーソルの位置を変更するクラス
[System.Serializable]
public class CharacterSelectCursol
{
    public int playerNumber = 0;
    public string controllerName;
    public float moveCursorFrame;
    public float limitCursorFrame;
    // 現在選んでいるキャラクター
    public ECharacterID currentCharacter = ECharacterID.CLIKO;
    public GameObject cursol;
    private bool acceptFlag = false;
    public bool AcceptFlag { get { return acceptFlag; } set { acceptFlag = value; } }

    // コントローラーの名前をプレイヤーごとに設定
    public void ControlerSetting()
    {
        var controllerNames = Input.GetJoystickNames();
        if (playerNumber < controllerNames.Length)
        {
            if (controllerNames[playerNumber] != "")
            {
                controllerName = string.Format("{0}_", controllerNames[playerNumber]);
            }
        }
    }
    // カーソルの位置を変更する(あとでVector3.leapに変更)
    public void CursolMove(CharacterSelectObjectData _characterSelectObjectData)
    {
        cursol.transform.position = _characterSelectObjectData.PanelPosition[playerNumber].transform.position;
    }
    // Updateの処理
    public void Update(List<CharacterSelectObjectData> _characterSelectObjectDatas)
    {
        Vector2 tempInputDirection;
        tempInputDirection.x = Input.GetAxisRaw(string.Format("{0}Player{1}_Horizontal", controllerName, playerNumber));
        tempInputDirection.y = Input.GetAxisRaw(string.Format("{0}Player{1}_Vertical", controllerName, playerNumber));
        moveCursorFrame += Time.deltaTime;
        if (Input.GetButtonDown(string.Format("{0}Player{1}_Attack1", controllerName, playerNumber)))
        {
            CharacterSelect();
        }
            if (tempInputDirection != Vector2.zero && AcceptFlag == false)
        {
            if (moveCursorFrame >= limitCursorFrame)
            {
                currentCharacter = InputCursolDirection(currentCharacter, tempInputDirection);
                CursolMove(_characterSelectObjectDatas[(int)currentCharacter]);
            }
        }
    }
    // カーソル移動の核部分
    public ECharacterID InputCursolDirection(ECharacterID _selectCharacter, Vector2 _inputDir)
    {
        //左右移動（-1が左、1が右）
        _selectCharacter += (int)_inputDir.x;
        // 左端のキャラを選択しているときに、左の入力があった場合、右端のキャラにする
        if (_selectCharacter < 0)
        {
            _selectCharacter = (ECharacterID)System.Enum.GetNames(typeof(ECharacterID)).Length - 1;
        }
        // 右端のキャラを選択しているときに、右の入力があった場合、左端のキャラにする
        else if (_selectCharacter == (ECharacterID)Enum.GetNames(typeof(ECharacterID)).Length)
        {
            _selectCharacter = (ECharacterID)0;
        }
        moveCursorFrame = 0;
        return _selectCharacter;
    }
    public void CharacterSelect()
    {
        if (AcceptFlag == false)
        {
            AcceptFlag = true;
            Sound.LoadSE("Menu_Decision", "Se_menu_decision");
            Sound.PlaySE("Menu_Decision", 1, 1);
        }
        else
        {
            AcceptFlag = false;
            Sound.LoadSE("Menu_Cancel", "Se_menu_cancel");
            Sound.PlaySE("Menu_Cancel", 1, 1);
        }
    }
}
// キャラクターの名前パネルを変更するクラス
[System.Serializable]
public class NamePanel
{
    public GameObject charaNamePanel;
    //
    public void ChangeName(Sprite _selectCharacterSprite)
    {
        charaNamePanel.GetComponent<Image>().sprite = _selectCharacterSprite;
    }
}
// キャラクターモデルの生成、アニメーションの変更
[System.Serializable]
public class CharacterModel
{
	public GameObject characterInstancePos;
	public GameObject currentCharacter;
    public List<AnimationData> AnimationDatas = new List<AnimationData>();

    public void CreateCharacter(CharacterSelectObjectData _characterSelectObjectDatas,string _playerNumber)
    {
        for (int i = 0; i < _characterSelectObjectDatas.Model.Length; i++)
        {
            var temp = GameObject.Instantiate(_characterSelectObjectDatas.Model[i].PlayerModel2, characterInstancePos.transform.position, characterInstancePos.transform.rotation);
			//temp.GetComponent<AnimationData>().ScaleObject.transform.localScale = new Vector3(-1, 1, 1);
			AnimationDatas.Add(temp.GetComponent<AnimationData>());
			AnimationDatas[i].ScaleObject.transform.localScale = new Vector3(-1, 1, 1);
			temp.name = _characterSelectObjectDatas.Name + (i + 1) + "Color" + _playerNumber;
        }
    }
    //public void ChangeAnimation(ECharacterID _eCharacterID)
    //{
    //    AnimationDatas[(int)_eCharacterID].ChangeAnimation();
    //}
}