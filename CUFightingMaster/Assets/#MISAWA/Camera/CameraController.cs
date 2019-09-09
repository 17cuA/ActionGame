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
using CUEngine.Pattern;

public class CameraController : SingletonMono<CameraController>
{
	#region 変数宣言
	[SerializeField]
	private float offsetY;                      // カメラのY座標の基準値
	[SerializeField]
	private float speed_ZoomIn;                 // カメラのズーム時の速度
	[SerializeField]
	private float speed_ZoomOut;				// カメラのズームアウト時の速度

	private float distance_CamToPlayer;				// カメラからキャラまでの距離
	private float distanceOfPlayers_Start;			// ゲーム開始時のプレイヤー同士の距離
	private float distanceOfPLayers_Current;		// 現在のプレイヤー同士の距離
	private float distanceOfPLayers_Current_x;  // 現在のプレイヤー同士の距離のXの差
	private float distanceOfPLayers_Current_y;  // 現在のプレイヤー同士の距離のYの差
	private float distanceOfPLayers_Current_z;  // 現在のプレイヤー同士の距離のZの差

	[SerializeField]
	private Vector3 cameraPos_Max;          // カメラの最大座標
	[SerializeField]
	public Vector3 cameraPos_Min;				// カメラの最小座標

	private Vector3 pCentorPos;					// プレイヤー同士のセンターを取得

	public float stageWidth;							// ステージの横幅

	public Vector3 lBottom, rTop;					// 画面左下、右上の座標

	public static CameraController instance;

	// 統合のため追加
	public GameObject Fighter1;
	public GameObject Fighter2;

	// 仮で作成(07/29)
	public GameObject Collider1;
	public GameObject Collider2;
    public BoxCollider boxCollider1;
    public BoxCollider boxCollider2;

    public CinemaController cinemaController;

	[SerializeField]
	private bool zoomFlag_Z = false;
    #endregion

    #region 初期化
    private void Awake()
    {
        instance = GetComponent<CameraController>();
        boxCollider1 = Collider1.GetComponent<BoxCollider>();
        boxCollider2 = Collider2.GetComponent<BoxCollider>();
    }


    void Start()
	{
		Fighter1 = GameManager.Instance.Player_one.gameObject;
		Fighter2 = GameManager.Instance.Player_two.gameObject;
		offsetY = transform.position.y;
		speed_ZoomIn = 8.0f;
		speed_ZoomOut = 40.0f;
		stageWidth = 20.0f;								// ステージの横幅
		cameraPos_Max = new Vector3(28.0f,0, -9.5f);	// ズームアウトの最大値
		cameraPos_Min = new Vector3(-28.0f,0,-12.0f);	// ズームインの最小値
		distanceOfPlayers_Start = 0.4f; // ゲーム開始時のプレイヤー同士の距離
    }
	#endregion

	#region Update
	void Update()
	{
		#region デバック用
		if (Input.GetKeyDown(KeyCode.N))
		{
			Shake(0.25f, 0.1f);
		}
		if (Input.GetKeyDown(KeyCode.M))
		{
			Shake(0.25f, 5f);
		}
		#endregion
	}

	void LateUpdate()
	{
        if (cinemaController != null)
        {
            if (cinemaController.isPlay) return;
        }
        CameraUpdate();
		Debug.Log(Zoom());
	}
	#endregion

	void CameraUpdate()
	{
		CameraPos();
		TargetPos();
		// カメラの移動・ズーム
		transform.Translate(pCentorPos.x - transform.position.x, 0, Zoom());
		// カメラの座標を制限(現在Y軸移動はここで行っている)
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraPos_Min.x, cameraPos_Max.x), pCentorPos.y + offsetY, transform.position.z);
	}

	#region カメラの座標
	/// <summary>
	/// ターゲットの中心を求める
	/// </summary>
	void TargetPos()
	{
		pCentorPos = (Fighter1.transform.position + Fighter2.transform.position) / 2;
		distanceOfPLayers_Current = Vector3.Distance(Camera.main.WorldToViewportPoint(Fighter1.transform.position), Camera.main.WorldToViewportPoint(Fighter2.transform.position));
		distanceOfPLayers_Current_x = Camera.main.WorldToViewportPoint(Fighter1.transform.position).x - Camera.main.WorldToViewportPoint(Fighter2.transform.position).x;
		distanceOfPLayers_Current_y = Camera.main.WorldToViewportPoint(Fighter1.transform.position).y - Camera.main.WorldToViewportPoint(Fighter2.transform.position).y;
		distanceOfPLayers_Current_z = Camera.main.WorldToViewportPoint(Fighter1.transform.position).z - Camera.main.WorldToViewportPoint(Fighter2.transform.position).z;
	}

	/// <summary>
	/// カメラに関する座標を求める
	/// </summary>
	void CameraPos()
	{
		// カメラからプレイヤーまでの距離の計算
		distance_CamToPlayer = Vector3.Distance(transform.position, pCentorPos);
		#region 座標を求める
		// カメラの左下座標を求める
		lBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance_CamToPlayer));
		Collider1.transform.position = new Vector3(lBottom.x, transform.position.y, 0);
		// カメラの右上座標を求める
		rTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance_CamToPlayer));
		Collider2.transform.position = new Vector3(rTop.x, transform.position.y, 0);
		#endregion
	}

	/// <summary>
	/// ステージ横幅からカメラのX座標の最大値・最小値を求める
	/// Update関数内で一度だけ呼び出す
	/// </summary>
	void GetCameraPos_X()
	{
		cameraPos_Max.x = stageWidth - (rTop.x - transform.position.x);
		cameraPos_Min.x = -stageWidth + (transform.position.x - lBottom.x);
	}
	#endregion

	#region ズーム処理
	/// <summary>
	/// カメラのズームイン、ズームアウトの処理
	/// </summary>
	/// <returns></returns>
	float Zoom()
	{
		// ズームの比率
		float zoomRatio = 0.0f;

		//if ((Fighter1.transform.position.y >= 6.0 || Fighter2.transform.position.y >= 6.0) && transform.position.z > -15.0f)
		//{
		//	zoomRatio -= 1.0f;
		//}

		// ズームの比率を計算
		// カメラのZ座標が最大値より小さいかつプレイヤー間の距離が0.55未満の時
		if (transform.position.z < cameraPos_Max.z && distanceOfPLayers_Current < 0.55)
		{
			// プレイヤー間の距離によって速度を変更
			zoomRatio += distanceOfPlayers_Start / distanceOfPLayers_Current / speed_ZoomIn;
		}
		// カメラのZ座標が最小値より大きいかつプレイヤー間の距離が0.6より大きい時
		if (cameraPos_Min.z < transform.position.z && distanceOfPLayers_Current > 0.6)
		{
			// プレイヤー間の距離によって速度を変更
			zoomRatio -= distanceOfPLayers_Current / distanceOfPlayers_Start / speed_ZoomOut;
		}

		//// プレイヤーがジャンプして一定の高さになったら
		//if ((Fighter1.transform.position.y >= 6.0 || Fighter2.transform.position.y >= 6.0) && transform.position.z > -15.5f)
		//{
		//	// Zのズームを許可する
		//	zoomFlag_Z = true;
		//}
		//// ズーム(カメラの距離が一定に戻るまで)
		//if (transform.position.z <= -13.0f)
		//{
		//	// Zのズームの許可を出さない
		//	zoomFlag_Z = false;
		//}
		//// ズームが許可されたら
		//if (zoomFlag_Z)
		//{
		//	// ジャンプ用のZのズーム、今までのズームの距離を伸ばしただけ
		//	zoomRatio -= 0.1f;
		//}
		return zoomRatio;
	}
	#endregion

	#region カメラを揺らす処理
	/// <summary>
	/// カメラを揺らす値を受け取りコルーチンに投げる
	/// </summary>
	/// <param name="duration">揺れる期間</param>
	/// <param name="magnitude">揺れの大きさ</param>
	void Shake(float duration, float magnitude)
	{
		StartCoroutine(DoShake(duration, magnitude));
	}

	/// <summary>
	/// カメラを揺らす値を受け取り揺らす
	/// </summary>
	/// <param name="duration">揺れる期間</param>
	/// <param name="magnitude">揺れの大きさ</param>
	/// <returns></returns>
	IEnumerator DoShake(float duration, float magnitude)
	{
		// 経過時間
		var elapsed = 0f;

		while(elapsed < duration)
		{
			var x = transform.position.x + Random.Range(-0.1f, 0.1f) * magnitude;
			var y = transform.position.y + Random.Range(-0.1f, 0.1f) * magnitude;

			transform.position = new Vector3(x, y, transform.position.z);

			elapsed += Time.deltaTime;

			yield return null;
		}
	}
	#endregion
}
