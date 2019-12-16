using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine.Pattern;
using System;


public class CharacterSelecterControl:MonoBehaviour
{
    public enum ECharacterID
    {
        CLIKO = 0,
        OBACHAN,
        KUIDAORE,
    }
    #region Singleton
    public static bool dontDs;
    private static CharacterSelecterControl characterSelectControlInstance;
    public static CharacterSelecterControl CharacterSelectControlInstance
    {
        get
        {
            if (characterSelectControlInstance == null)
            {
                Type t = typeof(CharacterSelecterControl);

                characterSelectControlInstance = (CharacterSelecterControl)FindObjectOfType(t);
                if (characterSelectControlInstance == null)
                {
                    var _ins = new GameObject();
                    _ins.name = "CharacterSelectInstance";
                    characterSelectControlInstance = _ins.AddComponent<CharacterSelecterControl>();
                }
            }

            return characterSelectControlInstance;
        }
    }
    #endregion
    public int charaMax;
    // ウィンドウ最大数
    public int windowMax;
    // プレイヤー最大数
    public int playerMax;
    // 現在選択されているキャラクターのID
    public int characterID;
    // アクセサー

    // インスペクタ－からFighterStatusを設定する
    public FighterStatus[] fighterDataStrage;
        
    private Dictionary<int, FighterStatus> FighterDatas = new Dictionary<int, FighterStatus>();

    private void Awake()
    {
        characterID = fighterDataStrage[0].PlayerID;
        for (int i = 0; i < fighterDataStrage.Length; i++)
        {
            FighterDatas.Add(fighterDataStrage[i].PlayerID, fighterDataStrage[i]);
        }
    }
    // 現在選択されているキャラクターのデータを返す
    public FighterStatus GetSelectFighterStatus()
    {
        return FighterDatas[characterID];
    }
    public int GetCharacterID()
    {
        return characterID;
    }
}
