﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class AutoDestructShuriken : MonoBehaviour {

    public bool OnlyDeactivate;
    //게임오브젝트가 활성화되면 함수가 호출됩니다.
    void OnEnable()
    {
        StartCoroutine("CheckIfAlive");
    }

    IEnumerator CheckIfAlive()
    {
        while(true)
        {
            //0.5초 간격으로 루프되는 코루틴
            yield return new WaitForSeconds(0.5f);
            //파티클이 살아있는지 체크
            if(!GetComponent<ParticleSystem>().IsAlive(true))
            {
                //파티클이 모두 사라지게 되면 조건에 따라 비활성화하거나 파괴합니다.
                if(OnlyDeactivate)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }

}
