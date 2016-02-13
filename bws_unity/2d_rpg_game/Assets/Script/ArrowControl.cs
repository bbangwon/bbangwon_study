using UnityEngine;
using System.Collections;

public class ArrowControl : MonoBehaviour {

    private MonsterControl mMonster;
    public BoxCollider2D mCollider;
	// Use this for initialization
	void Start () {
        //Arrow오브젝트의 Box Collider를 가져옵니다.
        mCollider = gameObject.GetComponent<BoxCollider2D>();	
	}

    public void Shoot(MonsterControl monster)
    {
        mMonster = monster;
        Vector2 randomPos = Random.insideUnitCircle * 0.2f;
        iTween.MoveTo(gameObject, iTween.Hash("position", monster.transform.position + new Vector3(randomPos.x, randomPos.y, 0), "easetype", iTween.EaseType.easeOutCubic, "time", 0.3f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //몬스터 충돌체(collider)와 충돌 시 충돌정보가 전달됩니다.
        if(other.name.Substring(0,7) == "Monster")
        {
            mCollider.enabled = false;
            mMonster.Hit();

            //화살 오브젝트를 0.07초 후 파괴합니다.
            Destroy(gameObject, 0.07f);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
