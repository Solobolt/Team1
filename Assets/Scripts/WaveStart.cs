using UnityEngine;
using System.Collections;

public class WaveStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.DetachChildren ();
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject);
	}
}
