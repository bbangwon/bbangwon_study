﻿using UnityEngine;
using System.Collections;

public class MonsterControl : MonoBehaviour {
    public Animator mAnimator; //자신의 애니메이터를 참조할 변수

    //생성될 몬스터의 인덱스, 체력, 공격력, 공격속도
    [HideInInspector]
    public int idx;
    public int mHp;
    public int mAttack;
    public float mAttackSpeed;

    //몬스터가 발사할 파이어볼의 발사지점
    public Transform mFireShootSpot;

    //몬스터의 피격 설정을 위한 콜라이더
    public Collider2D mCollider;

    //몬스터가 사용할 파이어 볼 프리팹
    public GameObject mFirePrefab;

    public HPControl mHpControl;

    //몬스터의 상태
    public enum Status
    {
        Alive,
        Dead
    }

    [HideInInspector]
    public Status mStatus = Status.Alive;

    private GameManager mGameManager;

	// Use this for initialization
	void Start () {
        //참조해야 할 객체나 스크립트들을 여기서 설정하게 될 것입니다.
        mGameManager = GameObject.FindObjectOfType<GameManager>();
        mFirePrefab = Resources.Load("FireBall") as GameObject;
        mHpControl.SetHp(mHp);
    }

    //생성될 몬스터들은 현재 체력 +-10의 랜덤 체력을 가지게 됩니다.
    public void RandomHp()
    {
        mHp += Random.Range(-10, 10);
    }

    //몬스터들이 오토타깃팅이 될 경우만 콜라이더를 설정하게 됩니다.
    public void SetTarget()
    {
        mCollider.enabled = true;
    }

    //피격당할 경우 데미지 처리와 애니메이션 처리
    public void Hit(Vector3 hitPos)
    {
        GameObject archer = GameObject.Find("Archer");
        ArcherControl archercontrol = archer.GetComponent<ArcherControl>();

        int damage;
        archercontrol.isCritical();
        if (archercontrol.IsCritical)
        {
            damage = archercontrol.GetRandomDamage() * 2;
        }
        else
        {
            damage = archercontrol.GetRandomDamage();
        }
        mHp -= damage;
        mHpControl.Hit(damage);

        MakeEffect("Eff_Hit", hitPos + new Vector3(0.4f, 0.2f, 0), transform);
        MakeEffect("Sound_Hit", Vector3.zero, transform);

        HudText(damage, transform.position + new Vector3(0, .7f, 0), archercontrol.IsCritical);

        mAnimator.SetTrigger("Damage");

        //사망처리
        if(mHp <= 0)
        {
            mStatus = Status.Dead;
            mHp = 0;
            mCollider.enabled = false;
            mAnimator.SetTrigger("Die");
            mGameManager.SetEXP();
            mGameManager.ReAutoTarget();
            MakeEffect("Eff_Blood", transform.position + new Vector3(0, -1.5f, 0), GameObject.Find("FG_Depth0").transform);
            Destroy(gameObject, 1f);
        }
    }


    //Resources폴더로부터 경로(path)에 있는 프리팹을 로드한 후 해당 포지션(pos)에 인스턴스화(Instantiate)시키고
    // _parent 게임오브젝트에 자식으로 추가합니다.
    private void MakeEffect(string path, Vector3 pos, Transform _parent)
    {
        GameObject prefab = Resources.Load(path) as GameObject;
        GameObject eff = Instantiate(prefab) as GameObject;
        eff.transform.position = pos;
        eff.transform.parent = _parent;
    }

    //파이어볼 프리팹을 인스턴스(Instance)화해서 사용합니다.
    private void ShootFire()
    {
        //파이어볼 프리팹을 씬에 인스턴스화하는 과정을 작성하게 됩니다.
        GameObject fire = Instantiate(mFirePrefab, mFireShootSpot.position, Quaternion.identity) as GameObject;
        fire.SendMessage("Shoot", this);
        MakeEffect("Sound_Fireball", Vector3.zero, transform); 
    }
	
    private void HudText(int damage, Vector3 pos, bool isCritical)
    {
        GameObject prefab = Resources.Load("HudText") as GameObject;
        GameObject hudtext = Instantiate(prefab, pos, Quaternion.identity) as GameObject;

        if(isCritical)
        {
            hudtext.GetComponent<HudText>().SetHudText("Critical!!\n" + damage, new Color(255, 216, 0, 255), 35);
        }
        else
        {
            hudtext.GetComponent<HudText>().SetHudText(damage.ToString(), new Color(255, 255, 255, 255), 30);
        }
    }
}
