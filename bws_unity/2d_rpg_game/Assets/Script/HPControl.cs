﻿using UnityEngine;
using System.Collections;

public class HPControl : MonoBehaviour {
    private float mTotalHp;
    private float mNowHp;
    public Transform mBar;
    public TextMesh mHpLabel;
    
    public void SetHp(int hp)
    {
        //체력을 외부로부터 입력받아 두 변수에 담아 둡니다.
        mNowHp = mTotalHp = hp;
        //Hp바의 상태를 초기화 합니다.
        mBar.transform.localScale = new Vector3(1, 1, 1);
        //텍스트로 현재 체력을 표시합니다.
        mHpLabel.text = mNowHp.ToString();
    }

    public void Hit(int damage)
    {
        //현재 체력에서 데미지 만큼씩 뺍니다.
        mNowHp -= damage;

        //체력이 0 이하면 Invisible 함수를 0.1초 후에 호출합니다.
        if(mNowHp <= 0)
        {
            mNowHp = 0;
            Invoke("Invisible", 0.1f);
        }

        //원래 체력과 현재 데미지 입은 체력 간의 비율로 mBar를 스케일링 합니다.
        mBar.transform.localScale = new Vector3(mNowHp / mTotalHp, 1, 1);
        mHpLabel.text = mNowHp.ToString();
    }

    public void Invisible()
    {
        //이 게임오브젝트가 사라집니다.(비활성화)
        gameObject.SetActive(false);
    }
}
