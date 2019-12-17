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
    CLIKO = 1,
    OBACHAN,
    KUIDAORE,
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

    //public GameObject p1Cursol;
    //public GameObject p2Cursol;
    public GameObject cliko;
    public GameObject obachan;
    public GameObject kuidaore;

    public Sprite clikoNamePanel;
    public Sprite obachanNamePanel;
    public Sprite kuidaoreNamePanel;

    public Characterselect_Timer timer;

    public CharacterSelectCursol p1CursolData = new CharacterSelectCursol();
    public CharacterSelectCursol p2CursolData = new CharacterSelectCursol();

    public NamePanel p1NamePanelData = new NamePanel();
    public NamePanel p2NamePanelData = new NamePanel();

    // 各オブジェクトの辞書にデータを登録、初期化処理
    private void Awake()
    {
        // キャラクターの種類分ループ
        for (int i = 0; i < System.Enum.GetNames(typeof(ECharacterID)).Length; i++)
        {
            switch (i)
            {
                case 0:
                    p1CursolData.Add(ECharacterID.CLIKO, cliko);
                    p2CursolData.Add(ECharacterID.CLIKO, cliko);
                    p1NamePanelData.Add(ECharacterID.CLIKO, clikoNamePanel);
                    p2NamePanelData.Add(ECharacterID.CLIKO, clikoNamePanel);
                    break;
                case 1:
                    p1CursolData.Add(ECharacterID.OBACHAN, obachan);
                    p2CursolData.Add(ECharacterID.OBACHAN, obachan);
                    p1NamePanelData.Add(ECharacterID.OBACHAN, obachanNamePanel);
                    p2NamePanelData.Add(ECharacterID.OBACHAN, obachanNamePanel);
                    break;
                case 2:
                    p1CursolData.Add(ECharacterID.KUIDAORE, kuidaore);
                    p2CursolData.Add(ECharacterID.KUIDAORE, kuidaore);
                    p1NamePanelData.Add(ECharacterID.KUIDAORE, kuidaoreNamePanel);
                    p2NamePanelData.Add(ECharacterID.KUIDAORE, kuidaoreNamePanel);
                    break;
            }
        }
        // カーテンが上がるまでループ
        CanvasController_CharacterSelect.CanvasControllerInstance.UpCurtain();
        // カーテンが上がったらタイマーカウントダウンスタート
        timer.Start();

    }
    private void Update()
    {
        // ポーズ処理
        if (Mathf.Approximately(Time.timeScale, 0f)) return;

        timer.Update();
        p1CursolData.Update();
        p1NamePanelData.ChangeName(p1CursolData.currentCharacter);
    }
}
//
//
// カーソルの位置を変更するクラス
[System.Serializable]
public class CharacterSelectCursol : ICharacterSelectable<GameObject>
{
    public string controllerName;
    public int playerNumber = 0;
    public float moveCursorFrame;
    public float limitCursorFrame;
    // キャラクターのIDで選択キャラを管理
    Dictionary<ECharacterID, GameObject> selectCharacters = new Dictionary<ECharacterID, GameObject>();
    // 現在選んでいるキャラクター。inspectorから初期化しておかないと最初にエラー吐きます。
    public ECharacterID currentCharacter = ECharacterID.CLIKO;
    public GameObject cursol;
    private bool acceptFlag;
    public bool AcceptFlag { get { return acceptFlag; } set { acceptFlag = value; } }

    //
    // 辞書に登録するAddの処理
    // 第一引数：キャラクターのID 第二引数：キャラクターごとのパネルの位置
    public void Add(ECharacterID _eCharacterID, GameObject _pos)
    {
        selectCharacters.Add(_eCharacterID, _pos);
    }
    //
    //
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
    //
    //
    //
    public GameObject Getvalue(ECharacterID _ID)
    {
        return selectCharacters[_ID];
    }
    //
    //
    //
    public void CursolMove(ECharacterID _selectCharacterID)
    {
        cursol.transform.position = Getvalue(_selectCharacterID).transform.position;
    }
    //
    //
    //
    public void Update()
    {
        Vector2 tempInputDirection;
        tempInputDirection.x = Input.GetAxisRaw(string.Format("{0}Player{1}_Horizontal", controllerName, playerNumber));
        tempInputDirection.y = Input.GetAxisRaw(string.Format("{0}Player{1}_Vertical", controllerName, playerNumber));
        moveCursorFrame += Time.deltaTime;
        if(tempInputDirection != Vector2.zero && AcceptFlag == false)
        {
            if (moveCursorFrame >= limitCursorFrame)
            {
                currentCharacter = InputCursolDirection(currentCharacter, tempInputDirection);
            }
        }
    }
    //
    //
    // 第一引数：自分の選んでいるキャラ 第二引数：入力の値
    public ECharacterID InputCursolDirection(ECharacterID _selectCharacter,Vector2 _inputDir)
    {
        //左右移動（-1が左、1が右）
        _selectCharacter += (int)_inputDir.x;
        // 左端のキャラを選択しているときに、左の入力があった場合、右端のキャラにする
        if(_selectCharacter <= 0)
        {
            _selectCharacter = ECharacterID.KUIDAORE;
        }
        // 右端のキャラを選択しているときに、右の入力があった場合、左端のキャラにする
        else if(_selectCharacter == (ECharacterID)Enum.GetNames(typeof(ECharacterID)).Length+1)
        {
            _selectCharacter = ECharacterID.CLIKO;
        }
        CursolMove(_selectCharacter);
        moveCursorFrame = 0;
        return _selectCharacter;
    }
}
//
//
// キャラクターの名前パネルを変更するクラス
[System.Serializable]
public class NamePanel : ICharacterSelectable<Sprite>
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
    public void Add(ECharacterID _eCharacterID, Sprite sprite)
    {
        sprites.Add(_eCharacterID, sprite);
    }
    //
    public void ChangeName(ECharacterID _selectCharacterID)
    {
        charaNamePanel.GetComponent<Image>().sprite = Getvalue(_selectCharacterID);
    }
    // interfaceを継承してるのでおいておく
    public void Update()
    {

    }
}
//
//
// キャラクターセレクト画面で使用するオブジェクトに継承させる
public interface ICharacterSelectable<T>
{
    void Add(ECharacterID _ID, T _value);
    T Getvalue(ECharacterID _ID);
}