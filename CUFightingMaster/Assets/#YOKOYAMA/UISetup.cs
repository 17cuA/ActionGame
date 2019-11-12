/*------------------------------------------------------------------------------------------------------------------------------------------------------
 * 作成日：	2019/05/27
 * 作成者：	横山凌
 * 
 * アクティブシーンにCanvasとEventSystemsが生成されているか確認する
 * ゲーム実行時は、ここからデータを読み込み、シーン上のUIたちを配置する
 ------------------------------------------------------------------------------------------------------------------------------------------------------*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UISetup : MonoBehaviour
{
	/// <summary>
	/// 現在アクティブなシーンからCanvasがないかチェック
	/// </summary>
	public static bool SearchCanvas()
	{
		bool isCreate = false;
		// 現在アクティブなシーンにCanvasがあるか確認する
		foreach (GameObject searchObj in SceneManager.GetActiveScene().GetRootGameObjects())
		{
			// あった場合
			if (searchObj.GetComponent<Canvas>())
			{
				// 引き続きそのCanvasを使う
				isCreate = true;
				break;
			}
		}
		return isCreate;
	}

	/// <summary>
	/// 現在アクティブなシーンに存在するCanvasを取得する
	/// </summary>
	/// <returns></returns>
	public static GameObject GetCanvas()
	{
		GameObject canvas = null;
		// 現在アクティブなシーンにCanvasがあるか確認する
		foreach (GameObject searchObj in SceneManager.GetActiveScene().GetRootGameObjects())
		{
			// あった場合
			if (searchObj.GetComponent<Canvas>())
			{
				// 引き続きそのCanvasを使う
				canvas = searchObj;
				break;
			}
		}
		return canvas;
	}

	/// <summary>
	/// Canvasを生成する
	/// </summary>
	public static Canvas CreateCanvas()
	{
		// Canvasを生成
		var canvasObject = new GameObject("Canvas");
		var canvas = canvasObject.AddComponent<Canvas>();
		canvasObject.AddComponent<GraphicRaycaster>();
		canvasObject.AddComponent<CanvasScaler>();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(GetGameWindow.GetWidth(), GetGameWindow.GetHeight());

		return canvas;
	}

	/// <summary>
	/// 現在アクティブなシーンからEventSystemを探す。
	/// </summary>
	/// <returns></returns>
	public static bool SearchEventSystem()
	{
		// EventSystemを生成
		bool isCreate = false;
		// 現在アクティブなシーンにEventSystemがあるか確認する
		foreach (GameObject searchObj in SceneManager.GetActiveScene().GetRootGameObjects())
		{
			// あった場合
			if (searchObj.GetComponent<EventSystem>())
			{
				// 引き続きそのEventSystemを使う
				isCreate = true;
				break;
			}
		}
		return isCreate;
	}

	/// <summary>
	/// EventSystemを生成する
	/// </summary>
	public static GameObject CreateEventSystem()
	{
		var eventSystem = new GameObject("EventSystem", typeof(EventSystem));
		eventSystem.AddComponent<StandaloneInputModule>();

		return eventSystem;
	}
}
