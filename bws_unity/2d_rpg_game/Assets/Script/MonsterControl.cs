using UnityEngine;
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
    public void Hit()
    {
        GameObject archer = GameObject.Find("Archer");
        ArcherControl archercontrol = archer.GetComponent<ArcherControl>();
        mHp -= archercontrol.GetRandomDamage();
        mAnimator.SetTrigger("Damage");

        //사망처리
        if(mHp <= 0)
        {
            mStatus = Status.Dead;
            mHp = 0;
            mCollider.enabled = false;
            mAnimator.SetTrigger("Die");
            mGameManager.ReAutoTarget();
            Destroy(gameObject, 1f);
        }
    }

    //파이어볼 프리팹을 인스턴스(Instance)화해서 사용합니다.
    private void ShootFire()
    {
        //파이어볼 프리팹을 씬에 인스턴스화하는 과정을 작성하게 됩니다.
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
