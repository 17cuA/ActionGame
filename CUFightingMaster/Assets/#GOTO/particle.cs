// 17CU0322　中村弘大
// -----------------------------------------
// バトル中の効果音文字の位置・色を調整する
// -----------------------------------------
// 
// 変更履歴
// 0912/19:00		効果音文字の位置を複数のパターンにわけて、変更する処理の追加
// 0913/00:30		効果音文字の色を変える処理の追加

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{
	[SerializeField] private PlayerNumber num1 = PlayerNumber.Player1;
	[SerializeField] private PlayerNumber num2 = PlayerNumber.Player2;
	public GameObject[] tagObjects;
	public int TranslateTagNum;			// タグ調整処理を行った回数
	public int ChangeCounter;			// この変数の数によって、タグの色を変化させる
	public FighterCore core1;					// FighterCore(P1)の情報を格納する変数(コンボ数の取得のため使用)
	public FighterCore core2;					// FighterCoreの()P2情報を格納する変数(コンボ数の取得のため使用)

	// Start is called before the first frame update
	void Start()
	{
		// 初期化
		TranslateTagNum = 0;
		ChangeCounter = 0;
		core1 = GameManager.Instance.GetPlayFighterCore(num1);	// P1の情報を格納
		core2 = GameManager.Instance.GetPlayFighterCore(num2);	// P2の情報を格納
	}

	// Update is called once per frame
	void Update()
	{
		CheckTag();
	}

	void CheckTag()
	{
		//tagObjects.Lengthはオブジェクトの数であれば（tagObjects 。長さ== 0 ）{ デバッグ。Log （tagname + "タグがついたオブジェクトはありません" ）; } } }
		tagObjects = GameObject.FindGameObjectsWithTag("Effects");
		//Debug.Log(tagObjects.Length);

		// 2個目のエフェクトを生成する際位置を調整する
		if (tagObjects.Length > 0)
		{
			// tagObjects(文字エフェクトの配列)に格納されたオブジェクトの位置を変更する

			int i = tagObjects.Length;			// 現在のtagObjectsの配列の要素数を取得する
			int n = (1 + TranslateTagNum % 5);	// 調整処理の起動回数ごとに6パターンに変化させるための変数

			// 要素数が奇数か偶数かで移動させる位置を変化させる
			// 奇数の場合、プラス方向に位置を調整
			if (n % 2 == 1)
			{
				tagObjects[i - 1].transform.Translate(n * 0.13f, (n * 0.15f), 0);
			}
			// 偶数の場合、マイナス方向に位置を調整
			else
			{
				tagObjects[i - 1].transform.Translate(n * 0.13f, -(n * 0.15f), 0);
			}
			// 起動回数をカウント
			++TranslateTagNum;

			// 各プレイヤーのコンボ数を比較してパーティクルの色を変える関数を呼び出す。
			if(core1.GetComboCount() > core2.GetComboCount())
			{
				ParticleColorChanger(core1.GetComboCount(), i);
			}
			else if(core2.GetComboCount() > core1.GetComboCount())
			{
				ParticleColorChanger(core2.GetComboCount(), i);
			}
			//ParticleColorChanger(ChangeCounter, i);
		}
		else
		{
			// エフェクトがなくなったのですべての値を初期値に戻す
			TranslateTagNum = 0;
			//ChangeCounter = 0;
		}
	}

	// パーティクルの色を変える関数
	void ParticleColorChanger(int ChangeCounter,int i)
	{
		ParticleSystem particle = tagObjects[i - 1].GetComponent<ParticleSystem>();

		// エフェクトが出ている間に攻撃した回数(コンボ?)に応じてエフェクトの色を変える
		switch (ChangeCounter)
		{
			case 0:
				particle.startColor = new Color(1, 1, 1);			// 白
				break;

			case 1:
				particle.startColor = new Color(1, 1, 1);			// 白
				break;

			case 2:
				particle.startColor = new Color(1, 1, 0);			// 黄色
				break;

			case 3:
				particle.startColor = new Color(1, 0.55f, 0);			// 橙色
				break;

			case 4:
				particle.startColor = new Color(1, 0, 0);			// 赤色
				break;

			default:
				particle.startColor = new Color(1, 0, 0);           // 赤色
				break;
		}

		++ChangeCounter;
	}
}
