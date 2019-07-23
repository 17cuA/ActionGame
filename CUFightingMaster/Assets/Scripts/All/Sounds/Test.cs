//---------------------------------------------------------------
// サウンドの鳴らし方の例(テスト)
//---------------------------------------------------------------
// 作成者:飯塚
// 作成日:2019.06.26
//---------------------------------------------------------------
// 音源データは　Assets/Resources/Sounds でこの階層でファイルを作り入れてください
// Sound.AllSoundLod	全サウンドのロード
// Sound.LoadBgm		BGM個別ロード
// Sound.LoadBgm		SE個別ロード
// Sound.PlayBgm		BGM再生
// Sound.StopBgm		BGM停止
// Sound.PlaySe			SE再生
//
// Load関数で読み込んでして、Play関数で再生できます。
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    bool flag = false;
    void Start()
	{
		// サウンドのロード
		// SEロード
		Sound.LoadSe("HitW", "Se_hit_weak");
		Sound.LoadSe("HitM", "Se_hit_medium");
		Sound.LoadSe("HitS", "Se_hit_strong");
		Sound.LoadSe("GuardWM", "Se_guard_weak_medium");
		Sound.LoadSe("GuardS", "Se_guard_strong");
		Sound.LoadSe("PunchWM", "Se_punch_weak_medium");
		Sound.LoadSe("PunchS", "Se_punch_strong"); 
		Sound.LoadSe("Jump", "Se_jump");
		Sound.LoadSe("down", "Se_down"); 
		Sound.LoadSe("Step", "Se_step");
        Sound.LoadSe("GetUp", "Se_GetUp");
        Sound.LoadSe("Menu_MoveCursor", "Se_Menu_MoveCursor");
        Sound.LoadSe("Menu_Cancel", "Se_Menu_Cancel"); 
        Sound.LoadSe("Menu_Decision", "Se_Menu_Decision");
        // BGMロード
        Sound.LoadBgm("Bgm01", "Bgm01");

		// 一括ロード(SoundManagerのAllSoundLodに使うものを前もって記載する必要あり。)
		//Sound.AllSoundLod();

		// BGM再生　(アクセスキー, ボリューム, ピッチ)
		Sound.PlayBgm("Bgm01", 0.4f, 1);
	}

	void Update()
	{
		// SE再生 (アクセスキー, チャンネル, ボリューム, ピッチ)
		if (Input.GetKeyDown(KeyCode.Alpha1))
			Sound.PlaySe("HitW", 1, 1, 1);
		if (Input.GetKeyDown(KeyCode.Alpha2))
			Sound.PlaySe("HitM", 1, 1, 1);
		if (Input.GetKeyDown(KeyCode.Alpha3))
			Sound.PlaySe("HitS", 1, 1, 1);
		if (Input.GetKeyDown(KeyCode.Alpha4))
			Sound.PlaySe("GuardWM", 2, 1, 1);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            Sound.PlaySe("GuardWM", 2, 2, 1);
		if (Input.GetKeyDown(KeyCode.Alpha6))
			Sound.PlaySe("GuardS", 2, 2, 1);
		if (Input.GetKeyDown(KeyCode.Alpha7))
			Sound.PlaySe("PunchWM", 3, 1, 1);
		if (Input.GetKeyDown(KeyCode.Alpha8))
			Sound.PlaySe("PunchWM", 3, 1, 1);
		if (Input.GetKeyDown(KeyCode.Alpha9))
			Sound.PlaySe("PunchS", 3, 1, 1);
		if (Input.GetKeyDown(KeyCode.Q))
			Sound.PlaySe("Jump", 3, 1, 1);
		if (Input.GetKeyDown(KeyCode.W))
			Sound.PlaySe("down", 3, 1, 1);
		if (Input.GetKeyDown(KeyCode.E))
			Sound.PlaySe("Step", 3, 1, 1);
        if (Input.GetKeyDown(KeyCode.R))
            Sound.PlaySe("GetUp", 3, 1, 1);
        if (Input.GetKeyDown(KeyCode.T))
            Sound.PlaySe("Menu_MoveCursor", 3, 1, 1);
        if (Input.GetKeyDown(KeyCode.Y))
            Sound.PlaySe("Menu_Cancel", 3, 1, 1); 
        if (Input.GetKeyDown(KeyCode.U))
            Sound.PlaySe("Menu_Decision", 3, 1, 1);

        // BGMのオン・オフ
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!flag)
            {
                // BGM停止
                Sound.StopBgm();
                flag = true;
            }
            else
            {
                Sound.PlayBgm("Bgm01", 0.4f, 1);
                flag = false;
            }
        }
    }
}
