using UnityEngine;
using System.Collections;

public class RotateSimple : MonoBehaviour {

    public float rotationSpeedX = 0;
    public float rotationSpeedY = 0;
    public float rotationSpeedZ = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //매 프레임 rotationSpeedX, rotationSpeedY, rotationSpeedZ축으로 회전시킵니다.
        transform.Rotate(new Vector3(rotationSpeedX, rotationSpeedY, rotationSpeedZ) * Time.deltaTime);
	}
}
