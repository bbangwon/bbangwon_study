using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	void Start () {
        if (isLocalPlayer)
            name = "Player Me";
        else
            name = "Player " + GetComponent<NetworkIdentity>().netId.Value.ToString();
	}
}
