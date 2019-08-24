using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : BulletCore
{
    private FighterCore core;
    public override void Start() 
    {
        base.Start();
        core = GameManager.Instance.GetPlayFighterCore(playerNumber);
    }
    public override void UpdateGame()
    {
        base.UpdateGame();
        transform.Translate(0.1f * RightLeft, 0, 0);
        if(hitAttackNum>0)
        {
            isDestroyFlag = true;
        }
        if(nowFrame>180)
        {
            isDestroyFlag = true;
        }
    }
}
