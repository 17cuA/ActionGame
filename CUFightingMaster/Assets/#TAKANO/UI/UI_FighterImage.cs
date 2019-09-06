//---------------------------------------
// キャライメージUI
//---------------------------------------
// 作成者:高野
// 作成日:2019.09.06
//--------------------------------------
// 更新履歴
//--------------------------------------
// 仕様 
// GameDataStrageから情報を貰ってイメージを表示する
//----------------------------------------
//----------------------------------------
using UnityEngine;
using UnityEngine.UI;

public class UI_FighterImage : MonoBehaviour
{
	public PlayerType playerType = PlayerType.P1;

	private Image displayImage;
	[SerializeField]private Sprite[] fighterImage = new Sprite[2];	//0:Clico 1:Oba
	public PlayerNumber playerNumber;

	private void Awake()
	{
		displayImage = gameObject.GetComponent<Image>();
	}
	private void Start()
	{
		if (playerType == PlayerType.P1)
		{
			playerNumber = GameDataStrage.Instance.fighterStatuses[0].fighter.PlayerNumber;
			displayImage.sprite = GameDataStrage.Instance.fighterStatuses[0].fighter.Status.characterImage;
		}
		else
		{
			playerNumber = GameDataStrage.Instance.fighterStatuses[1].fighter.PlayerNumber;
			displayImage.sprite = GameDataStrage.Instance.fighterStatuses[1].fighter.Status.characterImage;
		}
		DisplayPlayerImage();
	}

	public void DisplayPlayerImage()
	{
		//P1の場合
		if(playerType == PlayerType.P1)
		{
			//赤にする
			displayImage.color = new Color(1.0f, 0, 0);
		}
		//P2の場合
		else
		{
			//青にする
			displayImage.color = new Color(0, 0.4f, 0.7f);
			//反転する
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}
	}

}
