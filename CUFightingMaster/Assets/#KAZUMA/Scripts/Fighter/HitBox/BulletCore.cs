using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class BulletCore : MonoBehaviour,IEventable
{
	[SerializeField]
	private BulletHitBox bulletHit = null;
	private bool attackHit = false;//技が当たってるかどうか
	private List<ComponentObjectPool<BoxCollider>.Objs> nowPlayCollider = new List<ComponentObjectPool<BoxCollider>.Objs>();
	private ComponentObjectPool<BoxCollider> hitBoxCollider;
	public int RightLeft = 1;
	public PlayerNumber playerNumber = PlayerNumber.None;

	protected int nowFrame = -1;//現在のフレーム
    protected int allFrame = -1;//全体フレーム
    protected bool isEndFrame = false;//フレームの最大数を超えた時
    protected int hitAttackNum = 0;//攻撃が当たった回数
    protected bool isDestroyFlag = false;//trueで削除
    protected bool isNotCheck = false;//当たり判定を無くす

    public virtual void Start()
	{
		ChangeSkillInit();
        GameManager.Instance.UpdateBulletList.Add(this);
        GameManager.Instance.LateUpdateBulletList.Add(this);
        nowFrame = -1;
	}

	// Update
	public virtual void UpdateGame()
	{
        nowFrame++;
        allFrame++;
        if (bulletHit.isLoop)
		{
			if (nowFrame > bulletHit.maxFrame)
			{
				nowFrame = 0;
			}
		}
		else
		{
            if (nowFrame > bulletHit.maxFrame)
            {
                isEndFrame = true;
            }
        }
        if (!isNotCheck)
        {
            CustomHitBoxes();
        }

    }
    public virtual void LateUpdateGame()
	{
        //ラウンド開始時に削除
        if (isDestroyFlag || GameManager.Instance.isEndRound)
        {
            GameManager.Instance.DeleteBulletList.Add(this);
            Destroy(gameObject);
        }
    }
    public void FixedUpdateGame() { }

    private List<FighterSkill.CustomHitBox> customs = new List<FighterSkill.CustomHitBox>();
	private List<int> startFrames = new List<int>();
	private List<int> nowPlayCustomNumber = new List<int>();

	private void ChangeSkillInit()
	{
		//ヒットボックス格納
		customs = new List<FighterSkill.CustomHitBox>(bulletHit.customHitBox);
		nowPlayCustomNumber = new List<int>();
		attackHit = false;
		foreach (FighterSkill.CustomHitBox c in customs)
		{
			c.frameHitBoxes = new List<FighterSkill.FrameHitBox>(c.frameHitBoxes);
			nowPlayCollider.Add(new ComponentObjectPool<BoxCollider>.Objs());
		}
		//移動配列のソート、フレームが近い順に並べる
		for (int i = 0; i < customs.Count; i++)
		{
			if (customs[i].frameHitBoxes.Count > 1)
			{
				customs[i].frameHitBoxes.Sort((a, b) => a.startFrame - b.startFrame);
			}
			nowPlayCustomNumber.Add(-1);
		}
		//hitboxのプール
		hitBoxCollider = new ComponentObjectPool<BoxCollider>(10, "hitMan", gameObject);
	}

	private void CustomHitBoxes()
	{
		if ((customs == null) || (customs.Count == 0)) return;
		for (int i = 0; i < customs.Count; i++)
		{
			if ((customs[i].frameHitBoxes == null) || (customs.Count == 0)) continue;
			if (customs[i].frameHitBoxes.Count > nowPlayCustomNumber[i] + 1)
			{
				//現在再生中の次フレームを越えれば
				if (customs[i].frameHitBoxes[nowPlayCustomNumber[i] + 1].startFrame <= nowFrame)
				{
					//処理
					if (nowPlayCollider[i].gameObject != null)
					{
						nowPlayCollider[i].gameObject.SetActive(false);
					}
					nowPlayCustomNumber[i]++;
					nowPlayCollider[i] = hitBoxCollider.GetObjects();
					nowPlayCollider[i].gameObject.tag = CommonConstants.Tags.GetTags(customs[i].mode);
					nowPlayCollider[i].gameObject.layer = LayerMask.NameToLayer(CommonConstants.Layers.GetPlayerNumberLayer(playerNumber));
					nowPlayCollider[i].component.size = customs[i].frameHitBoxes[nowPlayCustomNumber[i]].hitBox.size;
					attackHit = false;
				}
			}
			//ループ時
			else
			{
				//一番最初のスタートフレーム以上かつ現在のフレームが現在の再生フレーム数より小さい場合ループありの
				if (customs[i].frameHitBoxes[0].startFrame <= nowFrame && customs[i].frameHitBoxes[nowPlayCustomNumber[i]].startFrame > nowFrame)
				{
					//処理
					if (nowPlayCollider[i].gameObject != null)
					{
						nowPlayCollider[i].gameObject.SetActive(false);
					}
					nowPlayCustomNumber[i] = 0;
					//処理
					nowPlayCollider[i] = hitBoxCollider.GetObjects();
					nowPlayCollider[i].gameObject.tag = CommonConstants.Tags.GetTags(customs[i].mode);
					nowPlayCollider[i].gameObject.layer = LayerMask.NameToLayer(CommonConstants.Layers.GetPlayerNumberLayer(playerNumber));
					nowPlayCollider[i].component.size = customs[i].frameHitBoxes[nowPlayCustomNumber[i]].hitBox.size;
					attackHit = false;
				}
			}
			if (nowPlayCustomNumber[i] < 0) continue;
			if (customs[i].frameHitBoxes[nowPlayCustomNumber[i]].endFrame < nowFrame)
			{
				nowPlayCollider[i].gameObject.SetActive(false);
				return;
			}
			else
			{
				Vector3 tmp = customs[i].frameHitBoxes[nowPlayCustomNumber[i]].hitBox.localPosition;
				tmp.x *= RightLeft;
				nowPlayCollider[i].component.center = tmp;
			}
			if (attackHit == true) return;
			//ヒットボックスの当たり判定
			if (nowPlayCollider[i].gameObject.tag == CommonConstants.Tags.GetTags(HitBoxMode.Bullet))
			{
				//ダメージ判定処理
				CheckHitBox(nowPlayCollider[i].component, customs[i]);
			}
		}
	}
    //技が当たった時の処理
    private void CheckHitBox(BoxCollider _bCol, FighterSkill.CustomHitBox _cHit)
    {
        Transform t = _bCol.gameObject.transform;
        Collider[] col = Physics.OverlapBox(new Vector3(t.position.x + _bCol.center.x, t.position.y + _bCol.center.y, t.position.z + _bCol.center.z), _bCol.size / 2, Quaternion.identity, -1 - (1 << LayerMask.NameToLayer(CommonConstants.Layers.GetPlayerNumberLayer(playerNumber))));
        foreach (Collider c in col)
        {
            //相殺
            if ((c.gameObject.tag == CommonConstants.Tags.GetTags(HitBoxMode.Bullet)))
            {
                if (bulletHit.isOffset)
                {
                    foreach (var hit in bulletHit.offsetEffects)
                    {
                        Instantiate(hit.effect, hit.position + transform.position, Quaternion.identity);
                    }
                    isDestroyFlag = true;
                }
                return;
            }
            //通常攻撃
            else if ((c.gameObject.tag == CommonConstants.Tags.GetTags(HitBoxMode.HurtBox)) && (_cHit.isThrow == false))
            {
                //飛び道具無効
                if (GameManager.Instance.GetPlayFighterCore(c.gameObject.layer).NowPlaySkill.isInvincibleBullet && bulletHit.isInvincibleDis == false)
                {
                    foreach (var hit in bulletHit.offsetEffects)
                    {
                        Instantiate(hit.effect, hit.position + transform.position, Quaternion.identity);
                    }
                    isDestroyFlag = true;
                    return;
                }
                hitAttackNum++;
                FighterCore cr = GameManager.Instance.GetPlayFighterCore(c.gameObject.layer);
                //ダメージを与える
                cr.SetDamage(_cHit, _bCol);
                cr.SetEnemyNumber(playerNumber);//現在フォーカス中の敵のセット（未使用）
                                                //キャンセルなどで使用するので、遠距離では使わない
                                                //core.SetHitAttackFlag(true);//攻撃が当たったことを渡す
                attackHit = true;
                return;
            }
            else if ((c.gameObject.tag == CommonConstants.Tags.GetTags(HitBoxMode.HitBox)) && (_cHit.isThrow == false))
            {
                //飛び道具無効
                if (GameManager.Instance.GetPlayFighterCore(c.gameObject.layer).NowPlaySkill.isInvincibleBullet && bulletHit.isInvincibleDis == false)
                {
                    foreach (var hit in bulletHit.offsetEffects)
                    {
                        Instantiate(hit.effect, hit.position + transform.position, Quaternion.identity);
                    }
                    isDestroyFlag = true;
                    return;
                }
            }
            //投げ技
            else if ((c.gameObject.tag == CommonConstants.Tags.GetTags(HitBoxMode.GrabAndSqueeze)) && (_cHit.isThrow == true))
            {
                if (GameManager.Instance.GetPlayFighterCore(c.gameObject.layer).NowPlaySkill.isInvincibleBullet && bulletHit.isInvincibleDis == false)
                {
                    return;
                }
                hitAttackNum++;
                FighterCore cr = GameManager.Instance.GetPlayFighterCore(c.gameObject.layer);
                //ダメージを与える
                cr.SetDamage(_cHit, _bCol);
                cr.SetEnemyNumber(playerNumber);//現在フォーカス中の敵のセット（未使用）
                attackHit = true;
                return;
            }
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
	{
		if (EditorApplication.isPlaying)
		{
			foreach(var obj in nowPlayCollider)
			{
				if(obj.gameObject == null) return;
                if(obj.gameObject.activeSelf == true)
				{
					Gizmos.color = new Color(245f/255f, 120f/255f, 0f,0.3f);
					Gizmos.DrawCube(obj.component.center+obj.gameObject.transform.position, obj.component.size);
				}
			}
		}
	}
#endif
}

