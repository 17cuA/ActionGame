//---------------------------------------------------------------
// SE, BGM等サウンドを鳴らす
//---------------------------------------------------------------
// 作成者:飯塚
// 作成日:2019.06.26
// 更新日:2019.08.30 後藤 BGMを新しく追加(Title,CharaSele,Batlle)
// 更新日:2019.08.30 後藤 SEを新しく追加
//---------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// サウンド管理
public class Sound
{
    /// SEチャンネル数
    const int SE_CHANNEL = 20;

    /// サウンド種別
    enum eType
    {
        BGM, // BGM
        SE,  // SE
    }

    // シングルトン
    static Sound _singleton = null;
    // インスタンス取得
    public static Sound GetInstance()
    {
        return _singleton ?? (_singleton = new Sound());
    }

    // サウンド再生のためのゲームオブジェクト
    GameObject _object = null;
    // サウンドリソース
    AudioSource _sourceBGM = null; // BGM
    AudioSource _sourceSEDefault = null; // SE (デフォルト)
    AudioSource[] _sourceSEArray; // SE (チャンネル)
    // BGMにアクセスするためのテーブル
    Dictionary<string, _Data> _poolBGM = new Dictionary<string, _Data>();
    // SEにアクセスするためのテーブル 
    Dictionary<string, _Data> _poolSE = new Dictionary<string, _Data>();

    /// 保持するデータ
    class _Data
    {
        /// アクセス用のキー
        public string Key;
        /// リソース名
        public string ResName;
        /// AudioClip
        public AudioClip Clip;

        /// コンストラクタ
        public _Data(string key, string res)
        {
            Key = key;
            ResName = "Sounds/" + res;
            // AudioClipの取得
            Clip = Resources.Load(ResName) as AudioClip;
        }
    }

    /// コンストラクタ
    public Sound()
    {
        // チャンネル確保
        _sourceSEArray = new AudioSource[SE_CHANNEL];
    }

    /// AudioSourceを取得する
    AudioSource _GetAudioSource(eType type, int channel = -1)
    {
        if (_object == null)
        {
            // GameObjectがなければ作る
            _object = new GameObject("Sounds");
            // 破棄しないようにする
            GameObject.DontDestroyOnLoad(_object);
            // AudioSourceを作成
            _sourceBGM = _object.AddComponent<AudioSource>();
            _sourceSEDefault = _object.AddComponent<AudioSource>();
            for (int i = 0; i < SE_CHANNEL; i++)
            {
                _sourceSEArray[i] = _object.AddComponent<AudioSource>();
            }
        }

        if (type == eType.BGM)
        {
            // BGM
            return _sourceBGM;
        }
        else
        {
            // SE
            if (0 <= channel && channel < SE_CHANNEL)
            {
                // チャンネル指定
                return _sourceSEArray[channel];
            }
            else
            {
                // デフォルト
                return _sourceSEDefault;
            }
        }
    }

    /// <summary>
    /// ※サウンドデータはResources/Soundsフォルダに配置すること
    /// サウンドの一括ロード（1度だけ呼び出す）
    /// </summary>
    public static void AllSoundLod()
    {
        // サウンドのロード
        // BGMロード
        Sound.LoadBGM("BGM_Battle", "BGM_Battle");
        Sound.LoadBGM("BGM_Menu", "BGM_Menu");
        Sound.LoadBGM("BGM_Title", "BGM_Title");

        // SEロード
        Sound.LoadSE("HitL", "kakuge_SE1_6");
        Sound.LoadSE("HitM", "kakuge_SE1_8");
        Sound.LoadSE("HitS", "kakuge_SE1_6");
        Sound.LoadSE("GuardL", "Se_guard_light");
        Sound.LoadSE("GuardM", "Se_guard_medium");
        Sound.LoadSE("GuardS", "Se_guard_strong");
        Sound.LoadSE("JabL", "Se_jab_light");
        Sound.LoadSE("JabM", "Se_jab_medium");
        Sound.LoadSE("JabS", "Se_jab_strong");
        Sound.LoadSE("Squat", "Se_squat");
        Sound.LoadSE("Jump", "Se_jump");
        Sound.LoadSE("Step", "Se_step");
        Sound.LoadSE("Down", "Se_down");
        Sound.LoadSE("DownCancel", "Se_downCancel");
        Sound.LoadSE("GetUp", "Se_getUp");
        Sound.LoadSE("Sutan", "Se_sutan");
        Sound.LoadSE("Special1", "Se_special1_hadouken");
        Sound.LoadSE("Special1_Hit", "Se_special1_hadouken_Hit");
        Sound.LoadSE("Ca_Hit", "Se_ca_Hit");
        Sound.LoadSE("Menu_MoveCursor", "Se_menu_moveCursor");
        Sound.LoadSE("Menu_Cancel", "Se_menu_cancel");
        Sound.LoadSE("Menu_Decision", "Se_menu_decision");

        //新しく入れたデータ　8.30
        Sound.LoadSE("PlayerOneWin", "Voice_Player1_Win");
        Sound.LoadSE("PlayerTwoWin", "Voice_Player2_Win");
        Sound.LoadSE("RoundOne", "Voice_Round1");
        Sound.LoadSE("RoundTwo", "Voice_Round2");
        Sound.LoadSE("RoundThree", "Voice_Final_Round");
        Sound.LoadSE("Fight", "Voice_Fight");
        Sound.LoadSE("Ko", "Voice_K.O.");
        Sound.LoadSE("Draw", "Voice_Draw");

        //まだ未使用組
        Sound.LoadSE("Ko2", "Voice_K.O.2");
        Sound.LoadSE("ADrunk", "CharaVoice_ADrunk");
        Sound.LoadSE("Clico", "CharaVoice_Clico");
        Sound.LoadSE("Kuidaore", "CharaVoice_Kuidaore");
        Sound.LoadSE("Obachan", "CharaVoice_Oba");

        // キャラボイスクリコ
        Sound.LoadSE("Clico_CharacterSelect", "Cv_Clico_CharacterSelect");
        Sound.LoadSE("Clico_GetUp_DownCancel", "Cv_Clico_GetUp_DownCancel");
        Sound.LoadSE("Clico_HitL", "Cv_Clico_HitL");
        Sound.LoadSE("Clico_HitM", "Cv_Clico_HitM");
        Sound.LoadSE("Clico_HitS_Down", "Cv_Clico_HitS_Down");
        Sound.LoadSE("Clico_JobL", "Cv_Clico_JobL");
        Sound.LoadSE("Clico_JobM_Throw", "Cv_Clico_JobM_Throw");
        Sound.LoadSE("Clico_JobS", "Cv_Clico_JobS");
        Sound.LoadSE("Clico_Jump_Step", "Cv_Clico_Jump_Step");
        Sound.LoadSE("Clico_RoundDraw", "Cv_Clico_RoundDraw");
        Sound.LoadSE("Clico_RoundLoss", "Cv_Clico_RoundLoss");
        Sound.LoadSE("Clico_RoundWin", "Cv_Clico_RoundWin");
        Sound.LoadSE("Clico_Special1", "Cv_Clico_Special1");
        Sound.LoadSE("Clico_Special2", "Cv_Clico_Special2");
        Sound.LoadSE("Clico_Special3", "Cv_Clico_Special3");
        Sound.LoadSE("Clico_Special4", "Cv_Clico_Special4");
        Sound.LoadSE("Clico_Thrown", "Cv_Clico_Thrown");
        Sound.LoadSE("Clico_RoundStart", "Cv_Clico_RoundStart");
		Sound.LoadSE("Clico_Tackle", "CV_Clico_Tackle");

		// キャラボイスオバチャン
		Sound.LoadSE("Oba_CharacterSelect", "Cv_Oba_CharacterSelect");
        Sound.LoadSE("Oba_GetUp_DownCancel", "Cv_Oba_GetUp_DownCancel");
        Sound.LoadSE("Oba_HitL", "Cv_Oba_HitL");
        Sound.LoadSE("Oba_HitM", "Cv_Oba_HitM");
        Sound.LoadSE("Oba_HitS_Down", "Cv_Oba_HitS_Down");
        Sound.LoadSE("Oba_JobL", "Cv_Oba_JobL");
        Sound.LoadSE("Oba_JobM_Throw", "Cv_Oba_JobM_Throw");
        Sound.LoadSE("Oba_JobS", "Cv_Oba_JobS");
        Sound.LoadSE("Oba_Jump_Step", "Cv_Oba_Jump_Step");
        Sound.LoadSE("Oba_RoundDraw", "Cv_Oba_RoundDraw");
        Sound.LoadSE("Oba_RoundLoss", "Cv_Oba_RoundLoss");
        Sound.LoadSE("Oba_RoundWin", "Cv_Oba_RoundWin");
        Sound.LoadSE("Oba_Special1", "Cv_Oba_Special1");
        Sound.LoadSE("Oba_Special2", "Cv_Oba_Special2");
        Sound.LoadSE("Oba_Special3", "Cv_Oba_Special3");
        Sound.LoadSE("Oba_Special4", "Cv_Oba_Special4");
        Sound.LoadSE("Oba_Thrown", "Cv_Oba_Thrown");
        Sound.LoadSE("Oba_RoundStart", "Cv_Oba_RoundStart");

        Sound.LoadSE("SE_Beams", "SE_Beams");
        Sound.LoadSE("SV_Nice", "SV_Nice");
        Sound.LoadSE("SV_Great", "SV_Great");
        Sound.LoadSE("SV_Excellent", "SV_Excellent");

		Sound.LoadSE("ATField", "ATField");
    }

    /// <summary>
    /// ※サウンドデータはResources/Soundsフォルダに配置すること
    /// 引数 : 第一引数アクセスキー(好きに命名可能), 第二引数リソース名(サウンドデータに名前を合わせる)
    /// BGMサウンド個別ロード（1度だけ呼び出す）
    /// </summary>
    /// <param name="key"></param>
    /// <param name="resName"></param>
    public static void LoadBGM(string key, string resName)
    {
        GetInstance()._LoadBgm(key, resName);
    }

    /// <summary>
    /// ※サウンドデータはResources/Soundsフォルダに配置すること
    /// 引数 : 第一引数アクセスキー(好きに命名可能), 第二引数リソース名(サウンドデータに名前を合わせる)
    /// SEサウンド個別ロード（1度だけ呼び出す）
    /// </summary>
    /// <param name="key"></param>
    /// <param name="resName"></param>
    public static void LoadSE(string key, string resName)
    {
        GetInstance()._LoadSe(key, resName);
    }

    void _LoadBgm(string key, string resName)
    {
        if (_poolBGM.ContainsKey(key))
        {
            // すでに登録済みなのでいったん消す
            _poolBGM.Remove(key);
        }
        _poolBGM.Add(key, new _Data(key, resName));
    }
    void _LoadSe(string key, string resName)
    {
        if (_poolSE.ContainsKey(key))
        {
            // すでに登録済みなのでいったん消す
            _poolSE.Remove(key);
        }
        _poolSE.Add(key, new _Data(key, resName));
    }

    /// <summary>
    /// ※事前に音源をLoadBgmでロードしておくこと
    /// 引数 : 第一引数アクセスキー, 第二引数音量(0~1), 第三引数ピッチ(-で逆再生), 第四引数ループの有無
    /// BGMの再生
    /// </summary>
    /// <param name="key"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    /// <param name="loop"></param>
    /// <returns></returns>
    public static bool PlayBGM(string key, float volume = 1, float pitch = 1, bool loop = false)
    {
        return GetInstance()._PlayBgm(key, volume, pitch, loop);
    }
    bool _PlayBgm(string key, float volume = 1, float pitch = 1, bool loop = false)
    {
        if (_poolBGM.ContainsKey(key) == false)
        {
            // 対応するキーがない
            return false;
        }

        // いったん止める
        _StopBgm();

        // リソースの取得
        var _data = _poolBGM[key];

        // 再生
        var source = _GetAudioSource(eType.BGM);
        source.loop = loop;
        source.clip = _data.Clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
        return true;
    }

    /// <summary>
    /// BGMの停止。
    /// </summary>
    /// <returns></returns>
    public static bool StopBGM()
    {
        return GetInstance()._StopBgm();
    }
    bool _StopBgm(bool fadeOutFlag = false)
    {
        _GetAudioSource(eType.BGM).Stop();
        return true;
    }
    /// <summary>
    /// ※事前に音源をLoadSeでロードしておくこと
    /// 引数 :  第一引数アクセスキー, 第二引数使うチャンネル, 第三引数音量(0~1), 第四引数ピッチ(-で逆再生)
    /// SEの再生
    /// 音を合成させないで連続で鳴らしたいときは固定のチャンネルを使う。
    /// </summary>
    /// <param name="key"></param>
    /// <param name="channel"></param>
    /// <param name="volume"></param>
    /// <returns></returns>
    public static bool PlaySE(string key, int channel = 1, float volume = 1, float pitch = 1)
    {
        return GetInstance()._PlaySe(key, channel, volume, pitch);
    }
    bool _PlaySe(string key, int channel = 1, float volume = 1, float pitch = 1)
    {
        if (_poolSE.ContainsKey(key) == false)
        {
            // 対応するキーがない
            return false;
        }

        // リソースの取得
        var _data = _poolSE[key];

        if (0 <= channel && channel < SE_CHANNEL)
        {
            // チャンネル指定
            var source = _GetAudioSource(eType.SE, channel);
            source.clip = _data.Clip;
            source.volume = volume;
            source.pitch = pitch;
            source.Play();
        }
        else
        {
            // デフォルトで再生
            var source = _GetAudioSource(eType.SE);
            source.volume = volume;
            source.pitch = pitch;
            source.PlayOneShot(_data.Clip);
        }
        return true;
    }
}