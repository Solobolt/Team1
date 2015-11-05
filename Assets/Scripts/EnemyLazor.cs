using UnityEngine;
using System.Collections;

public class EnemyLazor : MonoBehaviour {

	private GameManager gameManager;
	
	private float projectileSpeed = 50.0f;
	
	private Transform myTransform;
	
	private float lifeTime;
	private float lifeTimeDuration = 10f;	
	
	// Use this for initialization
	void Start () {
		myTransform = this.transform;
		lifeTime = Time.time + lifeTimeDuration;
		gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		myTransform.position += Time.deltaTime * projectileSpeed * this.transform.forward;
		
		//Kill projectile at the end of its life
		if(Time.time > lifeTime || transform.position.z < -gameManager.zBoundry)
		{
			Destroy(this.gameObject);
		}
	}
	
	private void OnTriggerEnter(Collider otherObject)
	{
		if(otherObject.tag == "Player")
		{
			Destroy (this.gameObject);
		}
	}
}
