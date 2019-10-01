//--------------------------------------------------------
//ファイル名：ComboUIManager.cs
//作成者　　：田嶋颯
//作成日　　：20190904
//
//コンボを表示する
//--------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUIManager : MonoBehaviour
{
	[SerializeField] private PlayerNumber num = PlayerNumber.Player1;
	Text text;
	FighterCore core;
	private int currentRemainFrame;
	public GameObject comboObj;
	public Sprite comboSprite;
	public GameObject[] comboNumObj;
	public Sprite[] comboNumSprite;
	private Vector3 comboPos;

    private float maxTime = 1;
    private float time = 0;
    private int beforeCombo = 0;

    void Start()
    {
		Init();
        time = 0;
	}

    void Update()
    {
		ComboCounter();
        time += Time.deltaTime;
	}
	private void Init()
	{
		text = GetComponent<Text>();
		core = GameManager.Instance.GetPlayFighterCore(num);
		comboPos = transform.position;
		ResetCombo();
	}
	void ResetCombo()
	{
		for (int i = 0; i < comboNumObj.Length; i++)
		{
			comboNumObj[i].SetActive(false);
		}
		comboObj.SetActive(false);
	}
	public void ComboCounter()
	{
		var comboCount = core.ComboCount;
		if (core.ComboCount > 1)
		{
			var length = comboNumObj.Length;
			var isCombo = new bool[length];
            if(beforeCombo != core.ComboCount)
            {
                time = 0;
            }
            beforeCombo = core.ComboCount;
			//スプライトを格納
			for (int i = 0; i < length; i++)
			{
				var outputComboCount = Mathf.Clamp(comboCount, 0, max:99);
				comboNumObj[i].GetComponent<Image>().sprite = comboNumSprite[(i == 0) ? outputComboCount / 10: outputComboCount % 10];
				isCombo[i] = true;
			}
			//表示するかどうか判定
			for (int i = 0; i < length; i++)
			{
				if (comboNumObj[i].GetComponent<Image>().sprite == comboNumSprite[0]) isCombo[i] = false;
				else break;
			}
			//表示する
			for (int i = 0; i < length; i++)
			{
				if ( isCombo[i])
				{
					comboNumObj[i].SetActive(true);
					Shake(0.5f,150f);
				}
			}
			comboObj.SetActive(true);
		}
		else if(time>maxTime)
		{
            time = 0;
			ResetCombo();
            beforeCombo = 0;
		}
	}
	#region 揺らす処理
	/// <summary>
	/// 揺らす値を受け取りコルーチンに投げる
	/// </summary>
	/// <param name="duration">揺れる期間</param>
	/// <param name="magnitude">揺れの大きさ</param>
	void Shake(float duration, float magnitude)
	{
		StartCoroutine(DoShake(duration, magnitude));
	}

	/// <summary>
	/// 揺らす値を受け取り揺らす
	/// </summary>
	/// <param name="duration">揺れる期間</param>
	/// <param name="magnitude">揺れの大きさ</param>
	/// <returns></returns>
	IEnumerator DoShake(float duration, float magnitude)
	{
		// 経過時間
		var elapsed = 0f;

		while (elapsed < duration)
		{
			var x = transform.position.x + Random.Range(-0.1f, 0.1f) * magnitude;
			var y = transform.position.y + Random.Range(-0.1f, 0.1f) * magnitude;

			transform.position = new Vector3(x, y, transform.position.z);

			elapsed += Time.deltaTime;

			yield return null;
			transform.position = comboPos;
		}
	}
	#endregion
}
