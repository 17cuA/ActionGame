using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
//
// ここですべてのオブジェクトを操作、管理する
public class CharacterselectObject:MonoBehaviour
{
    public ECharacterID currentCharacterID = ECharacterID.CLIKO;
    public int playerNumber = 0;
    public Vector2 inputDir = Vector2.zero;
    public bool acceptFlag = false;

    public GameObject clikoPanel = new GameObject();
    public GameObject obachanPanel = new GameObject();
    public GameObject kuidaorePanel = new GameObject();

    public CharacterSelectCursol p1Cursol = new CharacterSelectCursol();
    public CharacterSelectCursol p2Cursol = new CharacterSelectCursol();

    public NamePanel p1NamePanel = new NamePanel();
    public NamePanel p2NamePanel = new NamePanel();

    // 各オブジェクトの辞書にデータを登録、初期化処理
    private void Awake()
    {
        for(int i = 0; i < System.Enum.GetNames(typeof(ECharacterID)).Length;i++)
        {
            switch(i)
            {
                case 0:
                    p1Cursol.Add(ECharacterID.CLIKO, clikoPanel.transform.position);
                    p2Cursol.Add(ECharacterID.CLIKO, clikoPanel.transform.position);
                    p1NamePanel.Add(ECharacterID.CLIKO, clikoPanel.GetComponent<Sprite>());
                    p2NamePanel.Add(ECharacterID.CLIKO, clikoPanel.GetComponent<Sprite>());
                    break;
                case 1:
                    p1Cursol.Add(ECharacterID.OBACHAN, obachanPanel.transform.position);
                    p2Cursol.Add(ECharacterID.OBACHAN, obachanPanel.transform.position);
                    p1NamePanel.Add(ECharacterID.OBACHAN, obachanPanel.GetComponent<Sprite>());
                    p2NamePanel.Add(ECharacterID.OBACHAN, obachanPanel.GetComponent<Sprite>());
                    break;
                case 2:
                    p1Cursol.Add(ECharacterID.KUIDAORE, kuidaorePanel.transform.position);
                    p2Cursol.Add(ECharacterID.KUIDAORE, kuidaorePanel.transform.position);
                    p1NamePanel.Add(ECharacterID.KUIDAORE, kuidaorePanel.GetComponent<Sprite>());
                    p2NamePanel.Add(ECharacterID.KUIDAORE, kuidaorePanel.GetComponent<Sprite>());
                    break;
            }
        }
    }
    private void Update()
    {

    }
    void SelectMove()
    {
        // カーソルの移動処理
        
    }
}
//
//
// カーソルの位置を変更するクラス
public class CharacterSelectCursol: ICharacterSelectable<Vector3>
{
    // キャラクターのIDで位置を管理
    Dictionary<ECharacterID, Vector3> positions = new Dictionary<ECharacterID, Vector3>();


    public Vector3 Getvalue(ECharacterID _ID)
    {
        return positions[_ID];
    }
    public void Add(ECharacterID _eCharacterID,Vector3 _pos)
    {
        positions.Add(_eCharacterID, _pos);
    }
}
//
//
// キャラクターの名前パネルを変更するクラス
public class NamePanel:ICharacterSelectable<Sprite>
{
    // キャラクターのIDでSpriteを管理
    Dictionary<ECharacterID, Sprite> sprites = new Dictionary<ECharacterID, Sprite>();

    public Sprite Getvalue(ECharacterID _eCharacterID)
    {
        return sprites[_eCharacterID];
    }
    public void Add(ECharacterID _eCharacterID,Sprite sprite)
    {
        sprites.Add(_eCharacterID, sprite);
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