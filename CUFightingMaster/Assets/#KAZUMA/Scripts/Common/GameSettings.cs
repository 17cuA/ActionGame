using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fighting/Setting")]
public class GameSettings : ScriptableObject
{
    [SerializeField,Header("押し合いの値")]
    private float pushAmount = 0.5f;
    [SerializeField, Header("ノックバックフレーム")]
    private int knock_Back_Count = 6;

    public float PushAmount
    {
        get { return pushAmount; }
    }
    public int Knock_Back_Count
    {
        get { return knock_Back_Count; }
    }
}
