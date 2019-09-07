using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Fighting/スキル")]
public class FighterSkill : ScriptableObject
{
    #region 構造体、クラス
    //当たり判定の基礎構造体
	[System.Serializable]
	public struct HitBox_
	{
		public Vector3 size;
		public Vector3 localPosition;
	}
    //当たり判定
	[System.Serializable]
	public class FrameHitBox
	{
        public bool isInfinityFrame;//アニメーション再生終了後も継続
        public HitBox_ hitBox;
		public int startFrame;
		public int endFrame;        
    }
    [System.Serializable]
    public class HitEffects
    {
        public bool isEnemyPos;
        public bool isParant;
        public bool isEnemyParant;
        public GameObject effect;
		public GameObject guardEffect;
        public Vector3 position;
    }
    // 投げのダメージ
    [System.Serializable]
    public class ThrowDamage
    {
        public int frame;
        public int damage;
    }
    //フレームエフェクト
    [System.Serializable]
    public class FrameEffects
    {
        public int frame;
        public GameObject effect;
        public bool childFlag;
        public bool worldPositionFlag;
        public Vector3 position;
    }
	//フレームエフェクト
	[System.Serializable]
	public class FrameBullets
	{
		public int frame;
		public BulletCore bullet;
		public bool childFlag;
		public bool worldPositionFlag;
		public Vector3 position;
	}

	//当たり判定群
	[System.Serializable]
    public class CustomHitBox
    {
        public HitBoxMode mode;
        public List<FrameHitBox> frameHitBoxes = new List<FrameHitBox>();
        public int hitStop;             //ヒットストップ
        public HitPoint hitPoint;       //上段中段下段
        public HitStrength hitStrength; //弱中強
        public int damage;              //ダメージ
        public int stanDamage;          //スタン値
        public float knockBack;           //ノックバック値
        public float airKnockBack;
        public float guardKnockBack;
        public List<HitEffects> hitEffects = new List<HitEffects>();//ヒットエフェクト
        public bool isDown = false;     //ダウンするかどうか

        public bool isThrow = false;    //投げかどうか
        public bool isThrowGroundDamage = false;//投げ技の時、接地時にダメージを喰らう
        public bool isThrowGroundAnimEnd = false;//投げ技の時、接地時にアニメーション終了
		public List<Move> airDamageMovements = new List<Move>();//空中で当たった時の動き
        public int throwGroundDamage = 0;
        public FighterSkill throwSkill;//投げ技
        public FighterSkill enemyThrowSkill;
        public List<ThrowDamage> throwDamages = new List<ThrowDamage>();
		public bool isJumpCancel = false;
        //ダウン時の移動
        public bool isContinue = false;
        public List<Move> movements = new List<Move>();
        public List<GravityMove> gravityMoves = new List<GravityMove>();

        public bool isFaceDown = false; //うつ伏せかどうか
        public bool isPassiveNotPossible = false;//受け身不可
        public int hitRigor;            //ヒット硬直
        public int guardHitRigor;       //ガード硬直
        public int plusGauge;           //ゲージ増加量
        public int enemyPlusGauge;      //相手ゲージ増加量
    }
    //移動量
    [System.Serializable]
	public class Move
	{
        public bool isGravityInvaid;
        public bool isResetStartGravity;
        public bool isResetEndGravity;
        public bool isImpact;
        public bool isBound = false;//バウンド
        public List<Move> boundMovements = new List<Move>();//バウンド移動量
        public Vector3 movement;
		public int startFrame;
	}
    //重力用
	[System.Serializable]
	public class GravityMove
	{
		public Vector3 movement;
		public Vector3 limitMove;
		public int startFrame;
    }
    #endregion
    public int skillLayer = 0;//キャンセル用のレイヤー
    public AnimationClip animationClip = null;  //再生するアニメーション
    public float animationSpeed = 1;            //アニメーションの速度
    public SkillStatus status = SkillStatus.Normal;                  //Normal,Special等
    public List<FrameEffects> frameEffects = new List<FrameEffects>(); //TODO::エフェクトリスト
	public List<FrameBullets> frameBullets = new List<FrameBullets>();
    public SkillStatus cancelFrag = (SkillStatus)(1<<0);//キャンセルできるもの(ビット)
                                                        //TODO::飛び道具
    public bool barrageCancelFrag = false;      //連打キャンセル
    public List<int> cancelLayer = new List<int>();                 //キャンセルできるレイヤー
    //ブレンドするかしないか
    public bool inBlend = false;
    public bool outBlend = false;

    //当たり判定
    public bool isInvincibleBullet = false;//飛び道具無効
    public List<FrameHitBox> plusHeadHitBox = new List<FrameHitBox>();//頭
	public List<FrameHitBox> plusBodyHitBox = new List<FrameHitBox>();//体
	public List<FrameHitBox> plusFootHitBox = new List<FrameHitBox>();//足
    public List<FrameHitBox> plusGrabHitBox = new List<FrameHitBox>();//掴み
	public List<FrameHitBox> plusPushingHitBox = new List<FrameHitBox>();//押し合い

    public List<CustomHitBox> customHitBox = new List<CustomHitBox>();//カスタム

	//重力継続判定
	public bool isContinue = false;
	public bool isMoveContinue = false;
	//移動
	public List<Move> movements = new List<Move>();
    public List<GravityMove> gravityMoves = new List<GravityMove>();

    //Default当たり判定があるかないか
    public bool headFlag = true;
    public bool bodyFlag = true;
    public bool footFlag = true;
    public bool grabFlag = true;
	public bool pushingFlag = true;

    #region EDITOR_
#if UNITY_EDITOR
    [CustomEditor(typeof(FighterSkill))]
    public class FighterSkillInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("技設定画面を開く"))
            {
                PlayerSkillEditor.Open((FighterSkill)target);
            }
        }
    }
#endif
    #endregion
}
