using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class LayerSort : MonoBehaviour {
    public enum Layers
    {
        Background,
        Foreground,
        Effect,
        UI
    }
    //레이어 이름
    public Layers mLayerName;
    //레이어 오더
    public int mOrderNumber;


	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().sortingLayerName = mLayerName.ToString();
        GetComponent<Renderer>().sortingOrder = mOrderNumber;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
