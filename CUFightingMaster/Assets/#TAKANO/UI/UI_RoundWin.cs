//---------------------------------------
// ゲーム中のラウンドカウントUI
//---------------------------------------
// 作成者:高野
// 作成日:2019.07.18
//--------------------------------------
// 更新履歴
// 2019.07.18 作成
//--------------------------------------
// 仕様 
//----------------------------------------
// MEMO 
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RoundWin : MonoBehaviour
{
    private Image currentImage;

    public void UpdateImage( Sprite sprite)
    {
        currentImage.sprite = sprite;
    }
	public void HideImage()
	{
		currentImage.enabled = false;
	}
    private void Awake()
    {
        currentImage = gameObject.GetComponent<Image>(); 
    }
}
