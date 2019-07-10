﻿//---------------------------------------------------------------
// SE, BGM等サウンドを鳴らす
//---------------------------------------------------------------
// 作成者:飯塚
// 作成日:2019.06.26
//---------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// サウンド管理
public class Sound
{
	/// SEチャンネル数
	const int SE_CHANNEL = 10;

	/// サウンド種別
	enum eType
	{
		Bgm, // BGM
		Se,  // SE
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
	AudioSource _sourceBgm = null; // BGM
	AudioSource _sourceSeDefault = null; // SE (デフォルト)
	AudioSource[] _sourceSeArray; // SE (チャンネル)
	// BGMにアクセスするためのテーブル
	Dictionary<string, _Data> _poolBgm = new Dictionary<string, _Data>();
	// SEにアクセスするためのテーブル 
	Dictionary<string, _Data> _poolSe = new Dictionary<string, _Data>();

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
		_sourceSeArray = new AudioSource[SE_CHANNEL];
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
			_sourceBgm = _object.AddComponent<AudioSource>();
			_sourceSeDefault = _object.AddComponent<AudioSource>();
			for (int i = 0; i < SE_CHANNEL; i++)
			{
				_sourceSeArray[i] = _object.AddComponent<AudioSource>();
			}
		}

		if (type == eType.Bgm)
		{
			// BGM
			return _sourceBgm;
		}
		else
		{
			// SE
			if (0 <= channel && channel < SE_CHANNEL)
			{
				// チャンネル指定
				return _sourceSeArray[channel];
			}
			else
			{
				// デフォルト
				return _sourceSeDefault;
			}
		}
	}

	/// <summary>
	/// ※サウンドデータはResources/Soundsフォルダに配置すること
	/// サウンドの一括ロード（1度だけ呼び出す）
	/// </summary>
	public static void AllSoundLod()
	{
		// サウンドをロード
		// ゲームで使うサウンドを前もって全て記載しておく
		Sound.LoadBgm("Bgm01", "Bgm01");
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
    }

    /// <summary>
    /// ※サウンドデータはResources/Soundsフォルダに配置すること
    /// 引数 : 第一引数アクセスキー(好きに命名可能), 第二引数リソース名(サウンドデータに名前を合わせる)
    /// BGMサウンド個別ロード（1度だけ呼び出す）
    /// </summary>
    /// <param name="key"></param>
    /// <param name="resName"></param>
    public static void LoadBgm(string key, string resName)
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
	public static void LoadSe(string key, string resName)
	{
		GetInstance()._LoadSe(key, resName);
	}

	void _LoadBgm(string key, string resName)
	{
		if (_poolBgm.ContainsKey(key))
		{
			// すでに登録済みなのでいったん消す
			_poolBgm.Remove(key);
		}
		_poolBgm.Add(key, new _Data(key, resName));
	}
	void _LoadSe(string key, string resName)
	{
		if (_poolSe.ContainsKey(key))
		{
			// すでに登録済みなのでいったん消す
			_poolSe.Remove(key);
		}
		_poolSe.Add(key, new _Data(key, resName));
	}

    /// <summary>
    /// ※事前に音源をLoadBgmでロードしておくこと
    /// 引数 : 第一引数アクセスキー, 第二引数音量(0~1), 第四引数ピッチ(-で逆再生)
    /// BGMの再生
    /// </summary>
    /// <param name="key"></param>
    /// <param name="volume"></param>
    /// <returns></returns>
    public static bool PlayBgm(string key, float volume = 1, float pitch = 1)
	{
		return GetInstance()._PlayBgm(key, volume, pitch);
	}
	bool _PlayBgm(string key, float volume = 1, float pitch = 1)
	{
		if (_poolBgm.ContainsKey(key) == false)
		{
			// 対応するキーがない
			return false;
		}

		// いったん止める
		_StopBgm();

		// リソースの取得
		var _data = _poolBgm[key];

		// 再生
		var source = _GetAudioSource(eType.Bgm);
		source.loop = true;
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
	public static bool StopBgm()
	{
		return GetInstance()._StopBgm();
	}
	bool _StopBgm()
	{
		_GetAudioSource(eType.Bgm).Stop();
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
	public static bool PlaySe(string key, int channel = -1, float volume = 1, float pitch = 1)
	{
		return GetInstance()._PlaySe(key, channel, volume, pitch);
	}
	bool _PlaySe(string key, int channel = -1, float volume = 1, float pitch = 1)
	{
		if (_poolSe.ContainsKey(key) == false)
		{
			// 対応するキーがない
			return false;
		}

		// リソースの取得
		var _data = _poolSe[key];

		if (0 <= channel && channel < SE_CHANNEL)
		{
			// チャンネル指定
			var source = _GetAudioSource(eType.Se, channel);
			source.clip = _data.Clip;
			source.volume = volume;
			source.pitch = pitch;
			source.Play();
		}
		else
		{
			// デフォルトで再生
			var source = _GetAudioSource(eType.Se);
			source.volume = volume;
			source.pitch = pitch;
			source.PlayOneShot(_data.Clip);
		}
		return true;
	}
}