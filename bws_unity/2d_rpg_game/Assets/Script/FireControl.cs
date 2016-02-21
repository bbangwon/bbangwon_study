using UnityEngine;
using System.Collections;

public class FireControl : MonoBehaviour {

    private GameObject mArcher;
    private MonsterControl mMonster;

    public void Shoot(MonsterControl monster)
    {
        mMonster = monster;
        //계층 뷰(Hierachy View)에서 Archer 게임오브젝트 Find
        mArcher = GameObject.Find("Archer").gameObject;

        Vector2 randomPos = Random.insideUnitCircle * 0.3f;
        iTween.MoveTo(gameObject, iTween.Hash("Position", mArcher.transform.position + new Vector3(randomPos.x, 1.5f + randomPos.y, 0),
            "easetype", iTween.EaseType.easeOutCubic, "time", 0.5f));

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Archer")
        {
            int damage = mMonster.mAttack;
            //mArcher 게임오브젝트에 있는 모든 컴포넌트에 있는 함수 중 Hit 함수 호출
            mArcher.SendMessage("Hit", damage);
            Destroy(gameObject, 0.07f);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
