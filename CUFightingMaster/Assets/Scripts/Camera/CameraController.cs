//---------------------------------------------------------------
// カメラのコントロール
//---------------------------------------------------------------
// 作成者:三沢・金沢
// 作成日:2019.05.17
//----------------------------------------------------
// 更新履歴
// 2019.05.17 ズームインとズームアウト作成したよ
// 2019.05.20 ズームインとズームアウトを関数にしたよ
// 2019.05.23 実質ジャンプ時のカメラ移動を作ったよ
// 2019.05.24 移動処理をtransform.positionからtransform.Translateに変更、X軸移動制限調整したよ
// 2019.05.27 プレイヤージャンプ時に何故か沈んでいく現象を直したよ 処理をまるっと変えたよ
// 2019.05.30 ズームアウトのがくがくするのを直したよ 処理の順番が原因だったよ
// 2019.05.31 なんか背景と壁がなくてもできるようになってたよ
// 2019.05.31 ごり押しでズームイン・ズームアウトを同時に起きないようにしたよ
// 2019.06.04 妥協した気がする
// 2019.06.07 妥協なし
//-----------------------------------------------------
// 仕様
// プレイヤー間の距離に応じてズームイン・ズームアウトする
// プレイヤーがジャンプした時カメラのY座標をプレイヤー間の平均(Y座標)に合わせる
//-----------------------------------------------------
// MEMO
// ほぼ完壁
//----------------------------------------------------
// 現在判明しているバグ
// プレイヤーの操作がまともになったらわかるよ
//----------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
	#region 変数宣言
	//private GameObject Player1;
	//private GameObject Player2;
	private float offsetY;								// カメラのY座標の基準値
	private float speed_ZoomIn;					// カメラのズーム時の速度
	private float speed_ZoomOut;				// カメラのズームアウト時の速度
	private bool call_Once;							// 一度だけ呼び出す用

	private float distance_CamToPlayer;			// カメラからキャラまでの距離
	private float distanceOfPlayers_Start;			// ゲーム開始時のプレイヤー同士の距離
	private float distanceOfPLayers_Current;	// 現在のプレイヤー同士の距離

	private Vector3 cameraPos_Max;			// カメラの最大座標
	private Vector3 cameraPos_Min;				// カメラの最小座標

	private Vector3 pCentorPos;					// プレイヤー同士のセンターを取得

	public float stageWidth;							// ステージの横幅

	public Vector3 lBottom, rTop;					// 画面左下、右上の座標

	public static CameraController instance;

	// 統合のため追加
	public GameObject Fighter1;
	public GameObject Fighter2;
	#endregion

	#region 初期化
	private void Awake()
	{
		instance = GetComponent<CameraController>();
		//Player1 = GameObject.Find("Player01");
		//Player2 = GameObject.Find("Player02");
	}


	void Start()
	{
		offsetY = transform.position.y;
		speed_ZoomIn = 5.0f;
		speed_ZoomOut = 15.0f;
		call_Once = true;
		stageWidth = 20.0f;				// ステージの横幅
		cameraPos_Max.z = -8.5f;		// ズームアウトの最大値
		cameraPos_Min.z = -10.0f;		// ズームインの最小値
		//distanceOfPlayers_Start = Vector3.Distance(Camera.main.WorldToViewportPoint(Player1.transform.position), Camera.main.WorldToViewportPoint(Player2.transform.position));
		distanceOfPlayers_Start = 0.4f; // ゲーム開始時のプレイヤー同士の距離
	}
	#endregion


	void Update()
	{
		if (call_Once)
		{
			GetCameraPos_X();
			call_Once = false;
		}
		#region デバッグ
		if (Input.GetKeyDown(KeyCode.K))
		{
			SceneManager.LoadScene("CinemachineTest");
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("Title(仮)");
		}
		#endregion
	}

	void FixedUpdate()
	{
		CameraPos();
	}

	void LateUpdate()
	{
		TargetPos();
		// カメラの移動・ズーム
		transform.Translate(pCentorPos.x - transform.position.x, 0, ZoomIn() + ZoomOut());
		// カメラの座標を制限(現在Y軸移動はここで行っている)
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraPos_Min.x, cameraPos_Max.x), pCentorPos.y + offsetY, transform.position.z);
	}

	// ターゲットの中心を求める
	void TargetPos()
	{
		pCentorPos = (Fighter1.transform.position + Fighter2.transform.position) / 2;
		distanceOfPLayers_Current = Vector3.Distance(Camera.main.WorldToViewportPoint(Fighter1.transform.position), Camera.main.WorldToViewportPoint(Fighter2.transform.position));
	}

	// カメラに関する座標を求める
	void CameraPos()
	{
		// カメラからプレイヤーまでの距離の計算
		distance_CamToPlayer = Vector3.Distance(transform.position, pCentorPos);
		#region 座標を求める
		// カメラの左下座標を求める
		lBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance_CamToPlayer));
		// カメラの右上座標を求める
		rTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance_CamToPlayer));
		#endregion
	}

	// ステージ横幅からカメラのX座標の最大値・最小値を求める
	// Update関数内で一度だけ呼び出す
	void GetCameraPos_X()
	{
		cameraPos_Max.x = stageWidth - (rTop.x - transform.position.x);
		cameraPos_Min.x = -stageWidth + (transform.position.x - lBottom.x);
	}

	// カメラのズームイン
	float ZoomIn()
	{
		float zoomRatio = 0.0f;

		// カメラのZ座標が最大値より小さいかつプレイヤー間の距離が0.55未満の時
		if (transform.position.z < cameraPos_Max.z && distanceOfPLayers_Current < 0.55)
		{
			// プレイヤー間の距離によって速度を変更
			zoomRatio += distanceOfPlayers_Start / distanceOfPLayers_Current / speed_ZoomIn;
		}
		return zoomRatio;
	}

	// カメラのズームアウト
	float ZoomOut()
	{
		// ズームの比率
		float zoomRatio = 0.0f;

		// カメラのZ座標が最小値より大きいとき
		if (cameraPos_Min.z < transform.position.z)
		{
			// ズームインと被らないようにするため、0.6にしている
			if (distanceOfPLayers_Current > 0.6)
			{
				zoomRatio -= distanceOfPLayers_Current / distanceOfPlayers_Start / speed_ZoomOut;
			}
		}
		return zoomRatio;
	}
}
