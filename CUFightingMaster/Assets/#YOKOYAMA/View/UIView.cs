/*------------------------------------------------------------------------------------------------------------------------------------------------------
 * 作成日：	2019/05/09
 * 作成者：	横山凌
 * 
 * UIを編集する際にシーンを映すView
 ------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
public class UIView : SceneView
{
	//座標定数
	public const float UISETTING_VIEW_PLACE_X = +950.0f;            // UIViewのカメラ配置(Ｘ軸)
	public const float UISETTING_VIEW_PLACE_Y = +600.0f;            // UIViewのカメラ配置(Y軸)
	public const float UISETTING_VIEW_PLACE_Z = -1500.0f;       // UIViewのカメラ配置(Z軸)

	public static UIView Open()
	{
		var window = ScriptableObject.CreateInstance<UIView>();
		window.Show();

		// Viewのpivot(中心座標)を設定
		window.pivot = new Vector3(Camera.main.transform.position.x,
									Camera.main.transform.position.y,
									Camera.main.transform.position.z);
		window.rotation = Quaternion.identity;

		// 全体が見えるように位置を変更
		window.LookAt(new Vector3(Camera.main.transform.position.x + UISETTING_VIEW_PLACE_X,
									Camera.main.transform.position.y + UISETTING_VIEW_PLACE_Y,
									Camera.main.transform.position.z + UISETTING_VIEW_PLACE_Z),
									window.rotation, 1);

		return window;
	}
}
#endif

