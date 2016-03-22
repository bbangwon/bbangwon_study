using UnityEngine;
using System.Collections;

public class csCard : MonoBehaviour {
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseUp(){
		GameObject gameManager = GameObject.Find("GameManager");
		
		gameManager.SendMessage("DropCard",tag,SendMessageOptions.DontRequireReceiver);
	}
}
