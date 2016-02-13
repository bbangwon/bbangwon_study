using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public ArcherControl mArcher;

    [HideInInspector]
    public List<MonsterControl> mMonster;

    //오토 타깃이 된 몬스터를 참조합니다.
    [HideInInspector]
    public MonsterControl TargetMonster;

    //몬스터의 프리팹들을 인스턴스화 할 위치 정보입니다.
    public Transform[] mSpawnPoint;

    //던전을 탐험하는 횟수입니다.
    private int mLoopCount = 5;

    //화면에 나타난 적의 합
    private int mMonsterCount = 0;

    // 얼마만큼 뛰다가 적을 만날것인지
    private float mRunTime = 1.8f;

    //던전의 현재 스텟
    public enum Status
    {
        Idle,
        Run,
        BattleIdle,
        Battle,
        Clear,
    }

    public Status mStatus = Status.Idle;

	// Use this for initialization
	void Start () {
        //적 몬스터들이 담길 List
        mMonster = new List<MonsterControl>();
        mMonster.Clear();
        //던전 탐험 스텝을 만들어서 순서대로 순환시킵니다.
        StartCoroutine("AutoStep");
	
	}

    IEnumerator AutoStep()
    {
        while(true)
        {
            if (mStatus == Status.Idle)
            {
                //1.2초를 대기한 후 던전 탐험을 시작합니다.
                yield return new WaitForSeconds(1.2f);
                mStatus = Status.Run;
            }
            else if (mStatus == Status.Run)
            {
                //아처의 애니메이션 상태를 달리기로 설정합니다.
                mArcher.SetStatus(ArcherControl.Status.Run, 1);

                // mRunTime 후 배틀대기 상태로 돌입합니다.
                yield return new WaitForSeconds(mRunTime);
                mStatus = Status.BattleIdle;
            }
            else if(mStatus == Status.BattleIdle)
            {
                //아처를 Run에서 Idle 상태로 전환합니다.
                mArcher.SetStatus(ArcherControl.Status.Idle, 0);
                mMonster.Clear();
                for(int i = 0; i < 3; ++i)
                {
                    //3마리의 몬스터를 Spawn합니다.
                    SpawnMonster(i);
                    //0.12초 간격으로 for문을 순환합니다.
                    yield return new WaitForSeconds(0.12f);
                }

                //몬스터 3마리를 모두 Spawn하고 2초를 대기합니다.
                yield return new WaitForSeconds(2);

                //배틀상태로 돌입합니다.
                mStatus = Status.Battle;

                //아처와 몬스터의 공격을 명령합니다.
                StartCoroutine("ArcherAttack");
                StartCoroutine("MonsterAttack");
                yield break;
            }
        }
    }

    private void SpawnMonster(int idx)
    {
        //Resources 폴더로부터 Monster 프리팹(Prefab)을 로드합니다.
        Object prefab = Resources.Load("Monster");

        //참조한 프리팹을 인스턴스화합니다.(화면에 나타납니다.)
        GameObject monster = Instantiate(prefab, mSpawnPoint[idx].position, Quaternion.identity) as GameObject;
        //monster.transform.parent = mSpawnPoint[idx];

        //생성된 인스턴스에서 MonsterControl 컴포넌트를 불러내어 mMonster 리스트에 Add 시킵니다.
        mMonster.Add(monster.GetComponent<MonsterControl>());

        //생성된 몬스터만큼 카운팅됩니다.
        mMonsterCount += 1;
        mMonster[idx].idx = idx;
        mMonster[idx].RandomHp();
        monster.name = "Monster" + idx;

        //레이어 오더를 단계적으로 주어 몬스터들의 뎁스가 차례대로 겹치도록 합니다.
        monster.GetComponentInChildren<SpriteRenderer>().sortingOrder = idx + 1;
    }

    IEnumerator ArcherAttack()
    {
        //아처의 타깃이 될 몬스터를 선택합니다.
        GetAutoTarget();

        while(mStatus == Status.Battle)
        {
            //아처의 공격 애니메이션
            mArcher.SetStatus(ArcherControl.Status.Attack, 0);

            //아처의 공격속도만큼 대기 후 순환합니다.
            yield return new WaitForSeconds(mArcher.mAttackSpeed);
        }
    }

    private void GetAutoTarget()
    {
        //Hp가 가장 낮은 몬스터를 타깃팅합니다.
        TargetMonster = mMonster.Where(m => m.mHp > 0).OrderBy(m => m.mHp).First();

        //타깃은 충돌체가 준비됩니다.
        TargetMonster.SetTarget();
    }

    public void ReAutoTarget()
    {
        //타깃을 재탐색합니다.
        mMonsterCount -= 1;
        TargetMonster = null;
        if(mMonsterCount == 0)
        {
            //몬스터를 모두 클리어했습니다.
            Debug.Log("Clear");

            mLoopCount -= 1;

            //모든 공격과 스텝을 중지시킵니다.
            StopCoroutine("ArcherAttack");
            StopCoroutine("MonsterAttack");
            StopCoroutine("AutoStep");

            if(mLoopCount == 0)
            {
                //모든 스테이지가 클리어되었습니다.
                Debug.Log("Stage All Clear");
                GameOver();
                return;                
            }

            //던전 스텝을 초기화시키고 다시 순환시킵니다.
            mStatus = Status.Idle;
            StartCoroutine("AutoStep");
            return;
        }

        //타깃 재탐색
        GetAutoTarget();

    }

    IEnumerator MonsterAttack()
    {
        while(mStatus == Status.Battle)
        {
            foreach(MonsterControl monster in mMonster)
            {
                //등록된 모든 몬스터는 공격 애니메이션 상태로
                if (monster.mStatus == MonsterControl.Status.Dead) continue;
                monster.mAnimator.SetTrigger("Shoot");
                yield return new WaitForSeconds(monster.mAttackSpeed + Random.Range(0, 0.5f));
                //몬스터의 공격스피드 + 랜덤값
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        StopCoroutine("ArcherAttack");
        StopCoroutine("MonsterAttack");
        StopCoroutine("AutoStep");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
