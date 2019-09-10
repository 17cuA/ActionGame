using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CUEngine.Pattern;

public class CharacterSelect_Manager : SingletonMono<CharacterSelect_Manager>
{
    #region 変数宣言
    public Cursor_CharacterSelect cursor1_1;
    public Cursor_CharacterSelect cursor1_2;
    public Cursor_CharacterSelect cursor2_1;
    public Cursor_CharacterSelect cursor2_2;

    [SerializeField]
    static int charaMax = 4;

    public GameObject[] CharacterNamePanels = new GameObject[charaMax * 2];    // キャラクターの名前のパネル（２画面２プレイヤーのため、２倍生成）
    public GameObject[] previewModel = new GameObject[charaMax * 2];    // 生成したキャラクターモデルを入れておく変数（２画面２プレイヤーのため、２倍生成）
    public Animationdata[] nomalAnimationPlayers = new Animationdata[charaMax * 2];   // 生成したキャラクターモデルのアニメーション情報を入れておく変数（２画面２プレイヤーのため、２倍生成）
    public GameObject[] createCharaPos = new GameObject[charaMax]; // キャラクターモデルを生成する位置

    public Sprite[] SelectCharacterNamePanels = new Sprite[charaMax];  // キャラクターの名前のパネルを格納しておく変数

    [SerializeField]
    private bool[] characterSelectBool = { false, false, false, false };    // キャラクターを選択したかどうかの判定
    public bool[] CharacterSelectBool
    {
        get { return characterSelectBool; }
    }
    public bool sceneChangeJughe;   // シーンの変更を許可

    public FighterStatus[] currentCharacter = new FighterStatus[charaMax]; // ファイターの情報を格納する変数

    [SerializeField]
    private bool panelAnimFlag;  // キャラクターパネルのアニメーション許可
    [SerializeField]
    private int animFlagCount;  // １回だけアニメーションをさせたいので、作った変数  
    [SerializeField]
    private float fadeFrame;     // シーンを変更させるためのフレーム（時間）
    public float FadeFrame
    {
        get { return fadeFrame; }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
		// 飯塚追加-------------------------------------------
        Sound.LoadBgm("BGM_Menu", "BGM_Menu");
        Sound.PlayBgm("BGM_Menu", 0.4f, 1, true);
        // ---------------------------------------------------

        /*pplication.targetFrameRate = 60;*/
        panelAnimFlag = false;
        animFlagCount = 1;
        fadeFrame = 0;
        sceneChangeJughe = false;
        if (currentCharacter[0] == null)
        {
            return;
        }
        // キャラモデルの生成
        for (int i = 0; i < charaMax; i++)
        {
            if ((i + 1) % 2 == 1)
            {
				//Clico
                previewModel[i] = Instantiate(currentCharacter[i].PlayerModel, createCharaPos[0].transform.position, createCharaPos[0].transform.rotation);
                previewModel[i + 4] = Instantiate(currentCharacter[i].PlayerModel2, createCharaPos[1].transform.position, Quaternion.identity);
                nomalAnimationPlayers[i] = previewModel[i].GetComponent<Animationdata>();
                nomalAnimationPlayers[i + 4] = previewModel[i + 4].GetComponent<Animationdata>();
				nomalAnimationPlayers[i + 4].ScaleObject.transform.localScale = new Vector3(1, 1, -1);
				nomalAnimationPlayers[i + 4].RotationObject.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			if ((i + 1) % 2 == 0)
            {
				//Obachan
                previewModel[i] = Instantiate(currentCharacter[i].PlayerModel, createCharaPos[0].transform.position, createCharaPos[0].transform.rotation);
                previewModel[i + 4] = Instantiate(currentCharacter[i].PlayerModel2, createCharaPos[1].transform.position, Quaternion.identity);
                nomalAnimationPlayers[i] = previewModel[i].GetComponent<Animationdata>();
                nomalAnimationPlayers[i + 4] = previewModel[i + 4].GetComponent<Animationdata>();
				nomalAnimationPlayers[i + 4].ScaleObject.transform.localScale = new Vector3(1, 1, -1);
				nomalAnimationPlayers[i + 4].RotationObject.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
		}    
    }

    void Update()
    {
        #region 1Pのキャラ選択の処理
        if (cursor1_1 == null)
        {
            return;
        }
        // 1Pの選択されているキャラの設定(selectDirが0ならグリコ)
        if (cursor1_1.selectDir == 0)
        {
            // GameDataStrageの選択されているキャラをグリコにする
            GameDataStrage.Instance.fighterStatuses[0] = currentCharacter[0];
            if (SelectCharacterNamePanels[0] == null)
            {
                return;
            }
            if (CharacterNamePanels[0] != null)
            {
                // Display1の表示されているキャラ名をグリコにする
                CharacterNamePanels[0].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
            }
            if (CharacterNamePanels[2] != null)
            {
                // Display2の表示されているキャラ名をグリコにする
                CharacterNamePanels[2].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
            }
            PreviewModelsActiveSet(0, 1);
        }
        // 1の場合別グリコ
        else if (cursor1_1.selectDir == 1)
        {
            // GameDataStrageの選択されているキャラを別グリコにする
            GameDataStrage.Instance.fighterStatuses[0] = currentCharacter[1];
            if (SelectCharacterNamePanels[1] == null)
            {
                return;
            }
            if (CharacterNamePanels[0] != null)
            {
                // Display1の表示されているキャラ名を別グリコにする
                CharacterNamePanels[0].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
            }
            if (CharacterNamePanels[2] != null)
            {
                // Display2の表示されているキャラ名を別グリコにする
                CharacterNamePanels[2].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
            }
            PreviewModelsActiveSet(1, 1);
        }
        // ２ならおばちゃん
        else if (cursor1_1.selectDir == 2)
        {
            // GameDataStrageの選択されているキャラをおばちゃんにする
            GameDataStrage.Instance.fighterStatuses[0] = currentCharacter[2];
            if (SelectCharacterNamePanels[1] == null)
            {
                return;
            }
            if (CharacterNamePanels[0] != null)
            {
                // Display1の表示されているキャラ名をおばちゃんにする
                CharacterNamePanels[0].GetComponent<Image>().sprite = SelectCharacterNamePanels[2];
            }
            PreviewModelsActiveSet(2, 1);
        }
        // 3 なら別おばちゃん
        else if (cursor1_1.selectDir == 3)
        {
            // GameDataStrageの選択されているキャラを別おばちゃんにする
            GameDataStrage.Instance.fighterStatuses[0] = currentCharacter[3];
            if (SelectCharacterNamePanels[1] == null)
            {
                return;
            }
            if (CharacterNamePanels[0] != null)
            {
                // Display1の表示されているキャラ名を別おばちゃんにする
                CharacterNamePanels[0].GetComponent<Image>().sprite = SelectCharacterNamePanels[3];
            }
            if (CharacterNamePanels[2] != null)
            {
                // Display2の表示されているキャラ名を別おばちゃんにする
                CharacterNamePanels[2].GetComponent<Image>().sprite = SelectCharacterNamePanels[3];
            }
            PreviewModelsActiveSet(3, 1);
        }
        #endregion
        #region 2Pのキャラ選択の処理
        if (cursor1_2 == null)
        {
            return;
        }
        // 2Pの選択されているキャラの設定(selectDir２未満ならグリコ)
        if (cursor1_2.selectDir == 0)
        {
            // GameDataStrageの選択されているキャラをグリコにする
            GameDataStrage.Instance.fighterStatuses[1] = currentCharacter[0];
            if (SelectCharacterNamePanels[1] == null)
            {
                return;
            }
            if (CharacterNamePanels[1] != null)
            {
                // Display1の表示されているキャラ名をグリコにする
                CharacterNamePanels[1].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
            }
            if (CharacterNamePanels[3] != null)
            {
                // Display2の表示されているキャラ名をグリコにする
                CharacterNamePanels[3].GetComponent<Image>().sprite = SelectCharacterNamePanels[0];
            }
            PreviewModelsActiveSet(0, 2);
        }
        // １なら別グリコ
        else if (cursor1_2.selectDir == 1)
        {
            // GameDataStrageの選択されているキャラを別グリコにする
            GameDataStrage.Instance.fighterStatuses[1] = currentCharacter[1];
            if (SelectCharacterNamePanels[1] == null)
            {
                return;
            }
            if (CharacterNamePanels[1] != null)
            {
                // Display1の表示されているキャラ名を別グリコにする
                CharacterNamePanels[1].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
            }
            if (CharacterNamePanels[3] != null)
            {
                // Display2の表示されているキャラ名を別グリコにする
                CharacterNamePanels[3].GetComponent<Image>().sprite = SelectCharacterNamePanels[1];
            }
            // それ以外をfalse
            PreviewModelsActiveSet(1, 2);
        }
        // ２ならおばちゃん
        else if (cursor1_2.selectDir == 2)
        {
            // GameDataStrageの選択されているキャラをおばちゃんにする
            GameDataStrage.Instance.fighterStatuses[1] = currentCharacter[2];
            if (SelectCharacterNamePanels[1] == null)
            {
                return;
            }
            if (CharacterNamePanels[1] != null)
            {
                // Display1の表示されているキャラ名をおばちゃんにする
                CharacterNamePanels[1].GetComponent<Image>().sprite = SelectCharacterNamePanels[2];
            }
            if (CharacterNamePanels[3] != null)
            {
                // Display2の表示されているキャラ名をおばちゃんにする
                CharacterNamePanels[3].GetComponent<Image>().sprite = SelectCharacterNamePanels[2];
            }
            PreviewModelsActiveSet(2, 2);
        }
        // 3ならおばちゃん
        else if (cursor1_2.selectDir == 3)
        {
            // GameDataStrageの選択されているキャラを別グリコにする
            GameDataStrage.Instance.fighterStatuses[1] = currentCharacter[3];
            if (SelectCharacterNamePanels[1] == null)
            {
                return;
            }
            if (CharacterNamePanels[1] != null)
            {
                // Display1の表示されているキャラ名を別グリコにする
                CharacterNamePanels[1].GetComponent<Image>().sprite = SelectCharacterNamePanels[3];
            }
            if (CharacterNamePanels[3] != null)
            {
                // Display2の表示されているキャラ名を別グリコにする
                CharacterNamePanels[3].GetComponent<Image>().sprite = SelectCharacterNamePanels[3];
            }
            // それ以外をfalse
            PreviewModelsActiveSet(3, 2);
        }
        #endregion

        characterSelectBool[0] = cursor1_1.Determining_decision;    // Display1の1Pカーソルがキャラクターを選択しているかのフラグ
        characterSelectBool[1] = cursor1_2.Determining_decision;    // Display1の2Pカーソルがキャラクターを選択しているかのフラグ
        characterSelectBool[2] = cursor2_1.Determining_decision;    // Display2の1Pカーソルがキャラクターを選択しているかのフラグ
        characterSelectBool[3] = cursor2_2.Determining_decision;    // Display2の2Pカーソルがキャラクターを選択しているかのフラグ

        if (characterSelectBool[0] == true)
        {
            for(int i = 0;i<4;i++)
            {
                nomalAnimationPlayers[i].animFrag = true;            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                nomalAnimationPlayers[i].animFrag = false;
            }
        }
        
        if(CharacterSelectBool[1] == true)
        {
            for (int i = 4; i < 8; i++)
            {
                nomalAnimationPlayers[i].animFrag = true;
            }
        }
        else
        {
            for (int i = 4; i < 8; i++)
            {
                nomalAnimationPlayers[i].animFrag = false;
            }
        }

        // 1Pと2Pがキャラを選択したら、フラグをtrueにする
        if (characterSelectBool[0] && characterSelectBool[1] && characterSelectBool[2] && characterSelectBool[3])
        {
            panelAnimFlag = true;
		}
		else
		{
			fadeFrame = 0;
		}
        // キャラ選択した後の時間を計測、managerからシーンの変更を許可
        if (panelAnimFlag)
        {
            fadeFrame += Time.deltaTime;
            sceneChangeJughe = true;
        }
        // パネルのアニメーションをプレイ
        if (panelAnimFlag == true && animFlagCount > 0)
        {
            if (cursor1_1.characterPanels[0] == null)
            {
                return;
            }
            // １回しか再生させたくないので、カウントの減算
            animFlagCount--;
            cursor1_1.characterPanels[0].GetComponent<Animation>().Play();  // Display1の一番左のパネル
            cursor1_1.characterPanels[1].GetComponent<Animation>().Play();  // Display1の左から２番目のパネル
            cursor1_1.characterPanels[2].GetComponent<Animation>().Play();  // Display1の右から２番目のパネル
            cursor1_1.characterPanels[3].GetComponent<Animation>().Play();  // Display1の一番右のパネル

            cursor2_1.characterPanels[0].GetComponent<Animation>().Play();  // Display2の一番左のパネル
            cursor2_1.characterPanels[1].GetComponent<Animation>().Play();  // Display2の左から２番目のパネル
            cursor2_1.characterPanels[2].GetComponent<Animation>().Play();  // Display2の右から２番目のパネル
            cursor2_1.characterPanels[3].GetComponent<Animation>().Play();  // Display2の一番右のパネル
        }
    }
    // selectCharacterが１ならグリコ、２なら別グリコ、3ならおばちゃん、４なら別おばちゃん
    public void PreviewModelsActiveSet(int selectCharacterNum, int playerNum)
    {

        if (playerNum == 1)
        {
            for (int i = 0; i < charaMax; i++)
            {
                if (previewModel[i] != null && previewModel[i].active == true)
                {
					if(i != selectCharacterNum)
					{
						previewModel[i].SetActive(false);
					}
                }
            }
			if (!previewModel[selectCharacterNum].activeSelf)
			{
				previewModel[selectCharacterNum].SetActive(true);
			}
        }
        if (playerNum == 2)
        {
            for (int i = 0; i <charaMax; i++)
            {
                if (previewModel[i+4] != null && previewModel[i+4].active == true)
                {
					Debug.Log(previewModel[i + 4]);
					if(i != selectCharacterNum)
					{
						previewModel[i + 4].SetActive(false);
					}
				}
            }
			if (!previewModel[selectCharacterNum + 4].activeSelf)
			{
				previewModel[selectCharacterNum + 4].SetActive(true);
			}
        }
    }
}
