//---------------------------------------
// Gameシーンでのキャラクターオブジェクトの生成
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.14
//--------------------------------------
// 更新履歴
// 2019.07.14 作成
//--------------------------------------
// 仕様
//----------------------------------------
// MEMO
//----0715----
// キャラクターセレクトシーンから、選ばれたキャラクターを受け取る必要がある
//
//このクラスから、InGameManagerへプレイヤーのhpなどがあるクラスの参照を渡すと良いかも
//現在は、InGameManagerがGameObject.Findしている
//----------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreater : MonoBehaviour
{
    private GameObject Player1;
    private GameObject Player2;

    public Vector3 Player1CreatePosition;
    public Vector3 Player2CreatePosition;
    
    public void CreatePlayer()
    {
        Instantiate(Player1, Player1CreatePosition, Quaternion.identity);
        Instantiate(Player2, Player1CreatePosition, Quaternion.identity);
    }
}
