//---------------------------------------
// 画面をフェードイン、フェードアウトさせる
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
// 参照するImageを手動で設定して下さい
//
// 宮島くんのソースを参考にしています
//----------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public float fadeSpeed;

    float red, green, blue, alfa;

    //public bool iscalled;

    public Image image;

    /// <summary>
    /// 画面を徐々に暗くする、暗くなったらTrueを返す
    /// </summary>
    public bool StartFadeOut()
    {
            if (alfa > 1)
            {
				return true;
			}
            image.color = new Color(red, green, blue, alfa);
            alfa += fadeSpeed;
            return false;
    }

    /// <summary>
    /// 画面を徐々に明るくする、明るくなったらTrueを返す
    /// </summary>
    public bool StartFadeIn()
    {
            if (alfa < 0)
            {
                return true;
            }
            image.color = new Color(red, green, blue, alfa);
            alfa -= fadeSpeed;
            return false;
    }

	public void BrackOut()
	{
		alfa = 1.0f;
	}

    private void Awake()
    {
        red = gameObject.GetComponent<Image>().color.r;
		green = gameObject.GetComponent<Image>().color.g;
		blue = gameObject.GetComponent<Image>().color.b;
		alfa = gameObject.GetComponent<Image>().color.a;
    }
}
