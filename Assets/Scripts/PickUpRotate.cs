using UnityEngine;
using System.Collections;

public class PickUpRotate : MonoBehaviour {
	private GameManager gameManger;
	private float rotationSpeed = 100f;
	private float movementSpeed = 25.0f;
	private Transform myTransform;

	public Material[] material;

    private int rand; 
	// Use this for initialization
	void Start () {
        rand = Random.Range(1, 4);
        myTransform = this.transform;
		gameManger = FindObjectOfType<GameManager> ();
		GetComponent<Renderer>().material = material[rand - 1];
	}
	
	// Update is called once per frame
	void Update () {
		myTransform.position += Time.deltaTime * movementSpeed * this.transform.forward;
		transform.Rotate (0,0,rotationSpeed*Time.deltaTime);

		if (transform.position.z < (-gameManger.zBoundry - 20))
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter (Collider coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			print ("Herbalerb");

			if(coll.gameObject.GetComponent<PlayerController>() != null)
			{
				coll.gameObject.GetComponent<PlayerController>().fireMode=rand;
			}
			else 
			{
				if(coll.gameObject.GetComponent<Player2Controller>() != null)
				{
					coll.gameObject.GetComponent<Player2Controller>().fireMode=rand;
				}
			}

			Destroy (gameObject);
		}
	}
}
