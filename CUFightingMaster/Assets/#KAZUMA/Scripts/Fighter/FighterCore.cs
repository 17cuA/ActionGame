using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/*
 * 9.13　中村コンボカウントを取得するために変更いたしました 264行目
 */

public class FighterCore : MonoBehaviour
{
    [SerializeField] private GameObject playerModel = null;//プレイヤーモデル
    [SerializeField] private PlayerNumber playerNumber; //プレイヤー番号
    [SerializeField] private FightingAnimationPlayer animationPlayer = null;//アニメーション再生クラス
    [SerializeField] private FighterStatus status = null;
    [SerializeField] private PlayerDirection direction;
	[SerializeField, Header("メッシュ")] private List<SkinnedMeshRenderer> mesh = null;
    [Header("スケール変更オブジェクト")] public GameObject scaleChangeObject = null;
    [Header("ローテーション変更オブジェクト")] public GameObject rotationChangeObject = null;
	private List<Material> mainMaterial = new List<Material>();
    private FighterMover mover = null;
    private HitBoxJudgement hitJudgement = null;
    [SerializeField] private FighterSkill nextAnimation = null;//ここにいれればアニメーションが再生される
    [SerializeField] private FighterSkill nowPlaySkill = null;
    [SerializeField] private FighterSkill.CustomHitBox applyDamageSkill = null;//ダメージを食らった時に入る
    [SerializeField] private BoxCollider applyDamageCollider = null;
    private PlayerNumber enemyNumber;
    [SerializeField] private bool isCrouching = false;
    private int changeWeightFrame = 0;
    public bool changeSkill { get; private set; }//技が入れ替わったかどうか
    public int HP = 100;
    private int comboCount = 0;
	public int StanGauge = 0;
	public int SpecialGauge = 0;

    private bool isHitAttack = false;//攻撃が当たったかどうか
    public bool isBound = false;
    public int BoundCount = 0;//何回バウンドしたか
    private FighterSkill.CustomHitBox hitAttackBox = null;
    //現在のプレイヤーの移動の状況、状態
    private PlayerMoveState playerMoveState = PlayerMoveState.Idle;
    #region Getter
    public List<SkinnedMeshRenderer> Meshes
    {
        get { return mesh; }
    }
    public GameObject PlayerModel
    {
        get { return playerModel; }
    }
    public FightingAnimationPlayer AnimationPlayerCompornent
    {
        get { return animationPlayer; }
    }
    public FighterSkill NowPlaySkill
    {
        get { return nowPlaySkill; }
    }

	public List<Material> MainMaterial
	{
		get { return mainMaterial; }
	}
    public PlayerNumber PlayerNumber
    {
        get { return playerNumber; }
    }
    public FighterStatus Status
    {
        get { return status; }
    }
    public PlayerDirection Direction
    {
        get { return direction; }
    }
    public FighterSkill.CustomHitBox GetDamage
    {
        get { return applyDamageSkill; }
    }
    public BoxCollider GetDamageCollider
    {
        get { return applyDamageCollider; }
    }
    public PlayerNumber EnemyNumber
    {
        get { return enemyNumber; }
    }
    public PlayerMoveState PlayerMoveStates
    {
        get { return playerMoveState; }
    }
    public bool IsHitAttack
    {
        get { return isHitAttack; }
    }
	public FighterSkill.CustomHitBox HitAattackBox
	{
		get { return hitAttackBox; }
	}
    public HitBoxJudgement GetBoxJudgement
    {
        get { return hitJudgement; }
    }
    public int ComboCount
    {
        get { return comboCount; }
    }
	public FighterMover Mover
	{
		get { return mover; }
	}
    #endregion
    private void Start()
    {
        //アタッチエラーチェック
        if (InitErrorCheck())
        {
			for(int i = 0;i<mesh.Count;i++)
			{
                if (mesh[i] != null)
                {
                    if (mesh[i].material != null)
                    {
                        mainMaterial.Add(mesh[i].material);
                    }
                }
            }
            HP = status.HP;
			StanGauge = 0;
			SpecialGauge = 110; //一時的に１００に変更
            //アニメーションプレイヤーの取得
            animationPlayer = playerModel.GetComponent<FightingAnimationPlayer>();
            mover = new FighterMover(this);
            hitJudgement = new HitBoxJudgement(this);
            comboCount = 0;
            if (scaleChangeObject == null) scaleChangeObject = AnimationPlayerCompornent.gameObject;
            if (rotationChangeObject == null) rotationChangeObject = AnimationPlayerCompornent.gameObject;
        }
    }
    public void UpdateGame()
    {
        //技の入れ替え
        if (nextAnimation != null)
        {
            animationPlayer.SetSkillAnimation(nextAnimation, changeWeightFrame);
            nextAnimation = null;
            changeSkill = true;
            changeWeightFrame = 0;
        }
        if(changeSkill)
        {
            if(playerNumber == PlayerNumber.Player1)
            {
                GameManager.Instance.isTimeStop_Two = false;
            }
            else if (playerNumber == PlayerNumber.Player2)
            {
                GameManager.Instance.isTimeStop_One = false;
            }
        }
        //技が入れ替わってから動作させたいのでanimationのアップデートは後
        animationPlayer.UpdateGame();
        //アニメーションが入れ替わってから入れ替わったかどうかチェック
        CheckNowPlaySkill();
        //移動のアップデート
        mover.UpdateGame();
        //当たり判定のアップデート
        hitJudgement.UpdateGame();
		mover.UpdateEffects();
		if(SpecialGauge>status.SpecialGuage)
		{
			SpecialGauge = status.SpecialGuage;
		}
        //終了
        UpdateEnd();
    }
    public void HitStopUpdate()
    {
        //当たり判定のアップデート
        hitJudgement.UpdateGame();
    }
    public void KnockBackUpdate()
    {
        hitJudgement.KnockBackPushing();
    }

    #region publid メソッド
    //技の設定
    public void SetSkill(FighterSkill _skill, int _weightFrame)
    {
        nextAnimation = _skill;
        changeWeightFrame = _weightFrame;
    }
    //地面判定
    public bool GroundCheck()
    {
        return hitJudgement.isGround;
    }
    public void SetIsGround(bool _f)
    {
        hitJudgement.SetGround(_f);
    }
    public void SetDamage(FighterSkill.CustomHitBox _s, BoxCollider _col)
    {
        applyDamageSkill = _s;
        applyDamageCollider = _col;
    }
    public void SetIsCrouching(bool _f)
    {
        isCrouching = _f;
    }
    public void SetEnemyNumber(PlayerNumber _num)
    {
        enemyNumber = _num;
    }
    public void SetDirection(PlayerDirection _dir)
    {
        direction = _dir;
    }
    public void SetPlayerMoveState(PlayerMoveState _state)
    {
        playerMoveState = _state;
    }
	public void SetPlayerNumber(PlayerNumber _num)
	{
		playerNumber = _num;
	}

    public void SetKnockBack(bool isBullet, float _backCount, PlayerNumber _number, PlayerDirection _dir, bool isEnKnock = true, int? _count = null)
    {
        if(_count == null)
        {
            hitJudgement.SetKnockBack(isBullet, _backCount, _number, _dir,isEnKnock);
            return;
        }
        hitJudgement.SetKnockBack(isBullet, _backCount, _number, _dir, isEnKnock, _count.Value);
    }
    public void SetHitAttackFlag(bool _hitFlag,FighterSkill.CustomHitBox _box)
    {
        isHitAttack = _hitFlag;
		hitAttackBox = _box;
    }
    public void PlusComboCount(int _count)
    {
        //ファイターの数が2体以上ならコンボしない
        if(GameManager.Instance.fighterAmount>2)
        {
            return;
        }
        comboCount += _count;
    }
    public void SetComboCount(int _count)
    {
        comboCount = _count;
        if(comboCount == 0)
        {
            isBound = false;
            BoundCount = 0;
        }
    }
	/////////////////////////
	//// コンボカウントの受け渡し 中村変更
	public int GetComboCount()
	{
		return comboCount;
	}
	////////////////////////
	public void SetMaterial(Material[] _material)
	{
		for(int i = 0;i<_material.Length;i++) 
		{
            if (_material[i] != null)
            {
                mesh[i].material = _material[i];
            }
        }
	}
    public void DirectionChangeMaterial()
    {
        int numberP = 1;
		if(PlayerNumber==PlayerNumber.Player1)
		{
			numberP = 0;
		}
        if (Direction == PlayerDirection.Right)
        {
            if (PlayerNumber == PlayerNumber.Player1)
            {
                Transform t = AnimationPlayerCompornent.gameObject.transform;
                if (GameManager.Instance.GetPlayFighterCore(PlayerNumber.Player2).gameObject.transform.position.x < transform.position.x)
                {
                    SetDirection(PlayerDirection.Left);
                    scaleChangeObject.transform.localScale = new Vector3(1, 1, -1);
                    rotationChangeObject.transform.rotation = Quaternion.Euler(0, 0, 0);
					SetMaterial(Status.playerMaterials[numberP].inversionMaterial);
                }
            }
            else if (PlayerNumber == PlayerNumber.Player2)
            {
                if (GameManager.Instance.GetPlayFighterCore(PlayerNumber.Player1).gameObject.transform.position.x < transform.position.x)
                {
                    Transform t = AnimationPlayerCompornent.gameObject.transform;
                    SetDirection(PlayerDirection.Left);
                    scaleChangeObject.transform.localScale = new Vector3(1, 1, -1);
                    rotationChangeObject.transform.rotation = Quaternion.Euler(0, 0, 0);
					SetMaterial(Status.playerMaterials[numberP].inversionMaterial);
				}
			}
        }
        else if (Direction == PlayerDirection.Left)
        {
            if (PlayerNumber == PlayerNumber.Player1)
            {
                Transform t = AnimationPlayerCompornent.gameObject.transform;
                if (GameManager.Instance.GetPlayFighterCore(PlayerNumber.Player2).gameObject.transform.position.x > transform.position.x)
                {
                    SetDirection(PlayerDirection.Right);
                    scaleChangeObject.transform.localScale = new Vector3(1, 1, 1);
                    rotationChangeObject.transform.rotation = Quaternion.Euler(0, 180, 0);
					SetMaterial(Status.playerMaterials[numberP].nomalMaterial);
				}
			}
            else if (PlayerNumber == PlayerNumber.Player2)
            {
                Transform t = AnimationPlayerCompornent.gameObject.transform;
                if (GameManager.Instance.GetPlayFighterCore(PlayerNumber.Player1).gameObject.transform.position.x > transform.position.x)
                {
                    SetDirection(PlayerDirection.Right);
                    scaleChangeObject.transform.localScale = new Vector3(1, 1, 1);
                    rotationChangeObject.transform.rotation = Quaternion.Euler(0, 180, 0);
					if(Status.playerMaterials[numberP].nomalMaterial != null)
					{
						SetMaterial(Status.playerMaterials[numberP].nomalMaterial);
					}
				}
			}
        }

    }
    #endregion

    #region 初期化時エラーチェック
    private bool InitErrorCheck()
    {
        if (playerModel == null)
        {
            Debug.LogError("PlayerModelがありません");
            return false;
        }
        else
        {
            if (playerModel.GetComponent(typeof(FightingAnimationPlayer)) == null)
            {
                Debug.LogError("FightingAnimationPlayerがついていません");
                return false;
            }
        }
        if (status == null)
        {
            Debug.LogError("FighterStatusがついていません");
            return false;
        }
        return true;
    }
    #endregion
    #region 現在のスキルチェック
    private void CheckNowPlaySkill()
    {
        //技の取得
        if (nowPlaySkill != animationPlayer.NowPlaySkill)
        {
            changeSkill = true;
            nowPlaySkill = animationPlayer.NowPlaySkill;
        }
    }
    #endregion
    #region Update終了時処理
    private void UpdateEnd()
    {
        changeSkill = false;
    }
    #endregion
    #region ギズモ
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //足元
        Gizmos.color = Color.black;
        Vector3 vector3 = new Vector3(1, 1, 1);
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + (vector3.y / 2), transform.position.z), vector3);
        if (status == null) return;
        int dir = 1;
        if (direction == PlayerDirection.Left)
        {
            dir = -1;
        }
        #region 未再生
        if (!EditorApplication.isPlaying)
        {
            //エディタが開かれているとき
            if (PlayerSkillEditorParameter.instance.window != null)
            {
                //エディタにアタッチされているGameObjectがこのObjectだったら
                if (PlayerSkillEditorParameter.instance.window.previewCharacter == animationPlayer.gameObject)
                {
                    Gizmos.color = Color.green;
                    Vector3 pos; Vector3 size;
                    //技情報のキャッシュ
                    var skill = PlayerSkillEditorParameter.instance.window.playerSkill;
                    #region Head
                    //TODO 関数化
                    if (skill.headFlag)
                    {
                        Vector3 lp = status.headHitBox.localPosition;
                        lp.x *= dir;
                        pos = transform.position + lp;
                        size = status.headHitBox.size;
                        //フレームごとの移動
                        for (int i = 0; i < skill.plusHeadHitBox.Count; i++)
                        {
                            if ((skill.plusHeadHitBox[i].startFrame <= PlayerSkillEditorParameter.instance.window.value) && skill.plusHeadHitBox[i].endFrame >= PlayerSkillEditorParameter.instance.window.value)
                            {
                                Vector3 lPos = status.headHitBox.localPosition;
                                Vector3 plusLpos = skill.plusHeadHitBox[i].hitBox.localPosition;
                                lPos.x *= dir;
                                plusLpos.x *= dir;
                                pos = transform.position + lPos + plusLpos;
                                size = status.headHitBox.size + skill.plusHeadHitBox[i].hitBox.size;
                            }
                        }
                        Gizmos.DrawWireCube(pos, size);
                    }
                    #endregion
                    #region Body
                    if (skill.bodyFlag)
                    {
                        Vector3 lp = status.bodyHitBox.localPosition;
                        lp.x *= dir;
                        pos = transform.position + lp;
                        size = status.bodyHitBox.size;
                        for (int i = 0; i < skill.plusBodyHitBox.Count; i++)
                        {
                            if ((skill.plusBodyHitBox[i].startFrame <= PlayerSkillEditorParameter.instance.window.value) && skill.plusBodyHitBox[i].endFrame >= PlayerSkillEditorParameter.instance.window.value)
                            {
                                Vector3 lPos = status.bodyHitBox.localPosition;
                                Vector3 plusLpos = skill.plusBodyHitBox[i].hitBox.localPosition;
                                lPos.x *= dir;
                                plusLpos.x *= dir;
                                pos = transform.position + lPos + plusLpos;
                                size = status.bodyHitBox.size + skill.plusBodyHitBox[i].hitBox.size;
                            }
                        }
                        Gizmos.DrawWireCube(pos, size);
                    }
                    #endregion
                    #region Foot
                    if (skill.footFlag)
                    {
                        Vector3 lp = status.footHitBox.localPosition;
                        lp.x *= dir;
                        pos = transform.position + lp;
                        size = status.footHitBox.size;
                        for (int i = 0; i < skill.plusFootHitBox.Count; i++)
                        {
                            if ((skill.plusFootHitBox[i].startFrame <= PlayerSkillEditorParameter.instance.window.value) && skill.plusFootHitBox[i].endFrame >= PlayerSkillEditorParameter.instance.window.value)
                            {
                                Vector3 lPos = status.footHitBox.localPosition;
                                Vector3 plusLpos = skill.plusFootHitBox[i].hitBox.localPosition;
                                lPos.x *= dir;
                                plusLpos.x *= dir;
                                pos = transform.position + lPos + plusLpos;
                                size = status.footHitBox.size + skill.plusFootHitBox[i].hitBox.size;
                            }
                        }
                        Gizmos.DrawWireCube(pos, size);
                    }
                    #endregion
                    #region Grab
                    if (skill.grabFlag)
                    {
                        Gizmos.color = Color.blue;
                        Vector3 lp = status.grabHitBox.localPosition;
                        lp.x *= dir;
                        pos = transform.position + lp;
                        size = status.grabHitBox.size;
                        for (int i = 0; i < skill.plusGrabHitBox.Count; i++)
                        {
                            if ((skill.plusGrabHitBox[i].startFrame <= PlayerSkillEditorParameter.instance.window.value) && skill.plusGrabHitBox[i].endFrame >= PlayerSkillEditorParameter.instance.window.value)
                            {
                                Vector3 lPos = status.grabHitBox.localPosition;
                                Vector3 plusLpos = skill.plusGrabHitBox[i].hitBox.localPosition;
                                lPos.x *= dir;
                                plusLpos.x *= dir;
                                pos = transform.position + lPos + plusLpos;
                                size = status.grabHitBox.size + skill.plusGrabHitBox[i].hitBox.size;
                            }
                        }
                        Gizmos.DrawWireCube(pos, size);
                    }
                    #endregion
                    #region Pushing
                    if (skill.pushingFlag)
                    {
                        Gizmos.color = new Color(1, 0.92f, 0.016f, 0.5f);
                        Vector3 lp = status.pushingHitBox.localPosition;
                        lp.x *= dir;
                        pos = transform.position + lp;
                        size = status.pushingHitBox.size;
                        for (int i = 0; i < skill.plusPushingHitBox.Count; i++)
                        {
                            if ((skill.plusPushingHitBox[i].startFrame <= PlayerSkillEditorParameter.instance.window.value) && skill.plusPushingHitBox[i].endFrame >= PlayerSkillEditorParameter.instance.window.value)
                            {
                                Vector3 lPos = status.pushingHitBox.localPosition;
                                Vector3 plusLpos = skill.plusPushingHitBox[i].hitBox.localPosition;
                                lPos.x *= dir;
                                plusLpos.x *= dir;
                                pos = transform.position + lPos + plusLpos;
                                size = status.pushingHitBox.size + skill.plusPushingHitBox[i].hitBox.size;
                            }
                        }
                        Gizmos.DrawCube(pos, size);
                    }
                    #endregion

                    #region Custom
                    for (int i = 0; i < skill.customHitBox.Count; i++)
                    {
                        pos = transform.position;
                        size = Vector3.zero;
                        for (int j = 0; j < skill.customHitBox[i].frameHitBoxes.Count; j++)
                        {
                            if ((skill.customHitBox[i].frameHitBoxes[j].startFrame <= PlayerSkillEditorParameter.instance.window.value) &&
                                (skill.customHitBox[i].frameHitBoxes[j].endFrame >= PlayerSkillEditorParameter.instance.window.value))
                            {
                                Vector3 lPos = skill.customHitBox[i].frameHitBoxes[j].hitBox.localPosition;
                                lPos.x *= dir;
                                pos = transform.position + lPos;
                                size = skill.customHitBox[i].frameHitBoxes[j].hitBox.size;
                            }
                        }
                        switch (skill.customHitBox[i].mode)
                        {
                            case HitBoxMode.HitBox:
                                Gizmos.color = new Color(1, 0, 0, 0.5f);
                                Gizmos.DrawCube(pos, size);
                                break;
                            case HitBoxMode.HurtBox:
                                Gizmos.color = Color.green;
                                Gizmos.DrawWireCube(pos, size);
                                break;
                            case HitBoxMode.GrabAndSqueeze:
                                Gizmos.color = new Color(1, 0.92f, 0.016f, 0.5f);
                                Gizmos.DrawCube(pos, size);
                                break;
                        }
                    }
                    #endregion
                }
                else
                {
                    DefaultHitBoxDraw();
                }
            }
            else
            {
                DefaultHitBoxDraw();
            }
        }
        #endregion
        #region 再生時
        else
        {
            Gizmos.color = Color.green;
            Vector3 pos; Vector3 size;
            if (hitJudgement.Head.gameObject.activeSelf == true)
            {
                Gizmos.DrawWireCube(hitJudgement.Head.transform.position + hitJudgement.Head.center, hitJudgement.Head.size);
            }
            if (hitJudgement.Body.gameObject.activeSelf == true)
            {
                Gizmos.DrawWireCube(hitJudgement.Body.transform.position + hitJudgement.Body.center, hitJudgement.Body.size);
            }
            if (hitJudgement.Foot.gameObject.activeSelf == true)
            {
                Gizmos.DrawWireCube(hitJudgement.Foot.transform.position + hitJudgement.Foot.center, hitJudgement.Foot.size);
            }
            if (hitJudgement.Grab.gameObject.activeSelf == true)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(hitJudgement.Grab.transform.position + hitJudgement.Grab.center, hitJudgement.Grab.size);
            }
            if (hitJudgement.Push.gameObject.activeSelf == true)
            {
                Gizmos.color = new Color(1, 0.92f, 0.016f, 0.5f);
                Gizmos.DrawCube(hitJudgement.Push.transform.position + hitJudgement.Push.center, hitJudgement.Push.size);
            }
            foreach(var _c in hitJudgement.Custom)
            {
                if(_c.gameObject.activeSelf == true)
                {
                    if(_c.gameObject.tag == CommonConstants.Tags.HurtBox)
                    {
                        Gizmos.color = Color.green;
                    }
                    else if(_c.gameObject.tag == CommonConstants.Tags.Pushing)
                    {
                        Gizmos.color = new Color(1, 0.92f, 0.016f, 0.5f);
                        Gizmos.DrawCube(_c.transform.position + _c.center, _c.size);
                        continue;
                    }
                    else if(_c.gameObject.tag == CommonConstants.Tags.HitBox)
                    {
                        Gizmos.color = new Color(1, 0, 0, 0.5f);
                        Gizmos.DrawCube(_c.transform.position + _c.center, _c.size);
                        continue;
                    }
                    else if (_c.gameObject.tag == CommonConstants.Tags.Grab)
                    {
                        Gizmos.color = Color.blue;
                    }                    
                    Gizmos.DrawWireCube(_c.transform.position + _c.center, _c.size);
                }
            }

            //     if (nowPlaySkill != null)
            //     {
            //         #region Head
            //         // if (nowPlaySkill.headFlag)
            //         // {
            //         //     Vector3 lp = status.headHitBox.localPosition;
            //         //     lp.x *= dir;
            //         //     pos = transform.position + lp;
            //         //     size = status.headHitBox.size;
            //         //     for (int i = 0; i < nowPlaySkill.plusHeadHitBox.Count; i++)
            //         //     {
            //         //         if ((nowPlaySkill.plusHeadHitBox[i].startFrame <= animationPlayer.NowFrame) && nowPlaySkill.plusHeadHitBox[i].endFrame >= animationPlayer.NowFrame)
            //         //         {
            //         //             Vector3 lPos = status.headHitBox.localPosition;
            //         //             Vector3 plusLpos = nowPlaySkill.plusHeadHitBox[i].hitBox.localPosition;
            //         //             lPos.x *= dir;
            //         //             plusLpos.x *= dir;
            //         //             pos = transform.position + lPos + plusLpos;
            //         //             size = status.headHitBox.size + nowPlaySkill.plusHeadHitBox[i].hitBox.size;
            //         //         }
            //         //     }
            //         //     Gizmos.DrawWireCube(pos, size);
            //         // }
            //         // #endregion
            //         // #region Body
            //         // if (nowPlaySkill.bodyFlag)
            //         // {
            //         //     Vector3 lp = status.bodyHitBox.localPosition;
            //         //     lp.x *= dir;
            //         //     pos = transform.position + lp;
            //         //     size = status.bodyHitBox.size;
            //         //     for (int i = 0; i < nowPlaySkill.plusBodyHitBox.Count; i++)
            //         //     {
            //         //         if ((nowPlaySkill.plusBodyHitBox[i].startFrame <= animationPlayer.NowFrame) && nowPlaySkill.plusBodyHitBox[i].endFrame >= animationPlayer.NowFrame)
            //         //         {
            //         //             Vector3 lPos = status.bodyHitBox.localPosition;
            //         //             Vector3 plusLpos = nowPlaySkill.plusBodyHitBox[i].hitBox.localPosition;
            //         //             lPos.x *= dir;
            //         //             plusLpos.x *= dir;
            //         //             pos = transform.position + lPos + plusLpos;
            //         //             size = status.bodyHitBox.size + nowPlaySkill.plusBodyHitBox[i].hitBox.size;
            //         //         }
            //         //     }
            //         //     Gizmos.DrawWireCube(pos, size);
            //         // }
            //         // #endregion
            //         // #region Foot
            //         // if (nowPlaySkill.footFlag)
            //         // {
            //         //     Vector3 lp = status.footHitBox.localPosition;
            //         //     lp.x *= dir;
            //         //     pos = transform.position + lp;
            //         //     size = status.footHitBox.size;
            //         //     for (int i = 0; i < nowPlaySkill.plusFootHitBox.Count; i++)
            //         //     {
            //         //         if ((nowPlaySkill.plusFootHitBox[i].startFrame <= animationPlayer.NowFrame) && nowPlaySkill.plusFootHitBox[i].endFrame >= animationPlayer.NowFrame)
            //         //         {
            //         //             Vector3 lPos = status.footHitBox.localPosition;
            //         //             Vector3 plusLpos = nowPlaySkill.plusFootHitBox[i].hitBox.localPosition;
            //         //             lPos.x *= dir;
            //         //             plusLpos.x *= dir;
            //         //             pos = transform.position + lPos + plusLpos;
            //         //             size = status.footHitBox.size + nowPlaySkill.plusFootHitBox[i].hitBox.size;
            //         //         }
            //         //     }
            //         //     Gizmos.DrawWireCube(pos, size);
            //         // }
            //         // #endregion
            //         // #region Grab
            //         // if (nowPlaySkill.grabFlag)
            //         // {
            //         //     Gizmos.color = Color.blue;
            //         //     Vector3 lp = status.grabHitBox.localPosition;
            //         //     lp.x *= dir;
            //         //     pos = transform.position + lp;
            //         //     size = status.grabHitBox.size;
            //         //     for (int i = 0; i < nowPlaySkill.plusGrabHitBox.Count; i++)
            //         //     {
            //         //         if ((nowPlaySkill.plusGrabHitBox[i].startFrame <= animationPlayer.NowFrame) && nowPlaySkill.plusGrabHitBox[i].endFrame >= animationPlayer.NowFrame)
            //         //         {
            //         //             Vector3 lPos = status.grabHitBox.localPosition;
            //         //             Vector3 plusLpos = nowPlaySkill.plusGrabHitBox[i].hitBox.localPosition;
            //         //             lPos.x *= dir;
            //         //             plusLpos.x *= dir;
            //         //             pos = transform.position + lPos + plusLpos;
            //         //             size = status.grabHitBox.size + nowPlaySkill.plusGrabHitBox[i].hitBox.size;
            //         //         }
            //         //     }
            //         //     Gizmos.DrawWireCube(pos, size);
            //         // }
            //         // #endregion
            //         // #region Pushing
            //         // if (nowPlaySkill.pushingFlag)
            //         // {
            //         //     Gizmos.color = new Color(1, 0.92f, 0.016f, 0.5f);
            //         //     Vector3 lp = status.pushingHitBox.localPosition;
            //         //     lp.x *= dir;
            //         //     pos = transform.position + lp;
            //         //     size = status.pushingHitBox.size;
            //         //     for (int i = 0; i < nowPlaySkill.plusPushingHitBox.Count; i++)
            //         //     {
            //         //         if ((nowPlaySkill.plusPushingHitBox[i].startFrame <= animationPlayer.NowFrame) && nowPlaySkill.plusPushingHitBox[i].endFrame >= animationPlayer.NowFrame)
            //         //         {
            //         //             Vector3 lPos = status.pushingHitBox.localPosition;
            //         //             Vector3 plusLpos = nowPlaySkill.plusPushingHitBox[i].hitBox.localPosition;
            //         //             lPos.x *= dir;
            //         //             plusLpos.x *= dir;
            //         //             pos = transform.position + lPos + plusLpos;
            //         //             size = status.pushingHitBox.size + nowPlaySkill.plusPushingHitBox[i].hitBox.size;
            //         //         }
            //         //     }
            //         //     Gizmos.DrawCube(pos, size);
            //         // }
            //         // #endregion
            //         // #region Custom
            //         // for (int i = 0; i < nowPlaySkill.customHitBox.Count; i++)
            //         // {
            //         //     pos = transform.position;
            //         //     size = Vector3.zero;
            //         //     for (int j = 0; j < nowPlaySkill.customHitBox[i].frameHitBoxes.Count; j++)
            //         //     {
            //         //         if ((nowPlaySkill.customHitBox[i].frameHitBoxes[j].startFrame <= animationPlayer.NowFrame) &&
            //         //             (nowPlaySkill.customHitBox[i].frameHitBoxes[j].endFrame >= animationPlayer.NowFrame))
            //         //         {
            //         //             Vector3 lPos = nowPlaySkill.customHitBox[i].frameHitBoxes[j].hitBox.localPosition;
            //         //             lPos.x *= dir;
            //         //             pos = transform.position + lPos;
            //         //             size = nowPlaySkill.customHitBox[i].frameHitBoxes[j].hitBox.size;
            //         //         }
            //         //     }
            //         //     switch (nowPlaySkill.customHitBox[i].mode)
            //         //     {
            //         //         case HitBoxMode.HitBox:
            //         //             Gizmos.color = new Color(1, 0, 0, 0.5f);
            //         //             Gizmos.DrawCube(pos, size);
            //         //             break;
            //         //         case HitBoxMode.HurtBox:
            //         //             Gizmos.color = Color.green;
            //         //             Gizmos.DrawWireCube(pos, size);
            //         //             break;
            //         //         case HitBoxMode.GrabAndSqueeze:
            //         //             Gizmos.color = new Color(1, 0.92f, 0.016f, 0.5f);
            //         //             Gizmos.DrawCube(pos, size);
            //         //             break;
            //         //     }
            //         // }
            //         #endregion
            //     }

            //     else
            //     {
            //         DefaultHitBoxDraw();
            //     }
            // }
            #endregion
        }
    }
    //デフォルトの当たり判定の表示
    private void DefaultHitBoxDraw()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + status.headHitBox.localPosition, status.headHitBox.size);
        Gizmos.DrawWireCube(transform.position + status.bodyHitBox.localPosition, status.bodyHitBox.size);
        Gizmos.DrawWireCube(transform.position + status.footHitBox.localPosition, status.footHitBox.size);
        Gizmos.color = new Color(1, 0.92f, 0.016f, 0.5f);
        Gizmos.DrawCube(transform.position + status.pushingHitBox.localPosition, status.pushingHitBox.size);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + status.grabHitBox.localPosition, status.grabHitBox.size);
    }
#endif
    #endregion
}
