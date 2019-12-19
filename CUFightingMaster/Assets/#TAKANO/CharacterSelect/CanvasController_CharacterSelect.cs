using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Singleton CharacterselectObject.Instance.xxx の形でアクセス可能
public class CanvasController_CharacterSelect : MonoBehaviour
{
    #region Singleton
    public static bool dontDs;
    private static CanvasController_CharacterSelect canvasControllerInstance;
    public static CanvasController_CharacterSelect CanvasControllerInstance
    {
        get
        {
            if (canvasControllerInstance == null)
            {
                Type t = typeof(CanvasController_CharacterSelect);

                canvasControllerInstance = (CanvasController_CharacterSelect)FindObjectOfType(t);
                if (canvasControllerInstance == null)
                {
                    var _ins = new GameObject();
                    _ins.name = "CharacterselectObjectInstance";
                    canvasControllerInstance = _ins.AddComponent<CanvasController_CharacterSelect>();
                }
            }

            return canvasControllerInstance;
        }
    }
    #endregion

    public bool curtainFlag = false;

    public Canvas canvas_Display1;
    public Canvas canvas_Display2;

    [SerializeField]
    private CurtainMover curtainMover_1;
    [SerializeField]
    private CurtainMover curtainMover_2;

    private void Awake()
    {
        curtainMover_1 = canvas_Display1.transform.Find("Curtain").GetComponent<CurtainMover>();
        curtainMover_2 = canvas_Display2.transform.Find("Curtain").GetComponent<CurtainMover>();
    }

    /// <summary>
    ///徐々に 幕を開ける
    /// </summary>
    public bool UpCurtain()
    {
        bool isEnd1 = curtainMover_1.UpCurtain();
        bool isEnd2 = curtainMover_2.UpCurtain();

		if (isEnd1 && isEnd2)
		{
			return true;
		}
        return false;
    }

    /// <summary>
    ///徐々に幕を下ろす
    /// </summary>
    public bool DownCurtain()
    {
        bool isEnd1 = curtainMover_1.DownCurtain();
        bool isEnd2 = curtainMover_2.DownCurtain();

		if (isEnd1 && isEnd2)
		{
			return true;
		}
		return false;
    }

    /// <summary>
    ///一気に幕を下ろす
    /// </summary>
    public void InitDownCurtain()
    {
        curtainMover_1.InitDownCurtain();
        curtainMover_2.InitDownCurtain();
    }
}
