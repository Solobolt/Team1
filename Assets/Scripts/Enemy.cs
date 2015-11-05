using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject pickUp;

    public GameObject EnemyLazor;

    private GameObject[] players;
    private GameObject closestPlayer;

    private GameManager gameManager;

    private float movementSpeed = 20.0f;

    private Transform myTransform;

    private float lazorFireTime;
    private float lazorFireRate = 2f;

    private float health = 20.0f;

    private float rotationSpeed = 2.0f;
    private float adjRotationSpeed;
    private Quaternion targetRotation;

    // Use this for initialization
    void Start()
    {
        myTransform = this.transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.enemyNumb++;
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Move();
        CheckIfDead();
        CheckIfOffScreen();
        LookAtPlayer();


        //Fires a laser at set time intervals
        if (Time.time > lazorFireTime)
        {
            Instantiate(EnemyLazor, transform.position, transform.rotation);
            lazorFireTime = Time.time + lazorFireRate;
        }
    }

    //Locates the player and turns to face them
    void LookAtPlayer()
    {
        FindClosestPlayer();
        if(closestPlayer!=null)
        {
            if (closestPlayer.transform.position.z < myTransform.position.z)
            {
                targetRotation = Quaternion.LookRotation(closestPlayer.transform.position - myTransform.position);
                adjRotationSpeed = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, adjRotationSpeed);

            }
        }
    }

    //Make the enemy take damage
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    //Simply moves the enemy in the direction it is facing.
    private void Move()
    {
        myTransform.position += Time.deltaTime * movementSpeed * this.transform.forward;

    }

    //Randomly drops a pick up
    private void RandomPickUp()
    {
        int roll = Random.Range(1, 100);
        //Chance in percent of a pick up being dropped
        if (roll <= 5)
        {
            Instantiate(pickUp, transform.position, new Quaternion(180, 0, 0, 0));
        }
    }

    //Checks if the enemy has any health left
    private void CheckIfDead()
    {
        if (health <= 0)
        {
            RemoveEnemy();
            RandomPickUp();
        }
    }

    //Deletes the object if it has traveled too far
    private void CheckIfOffScreen()
    {
        if (transform.position.z < (-gameManager.zBoundry - 20))
        {
            gameManager.enemyNumb--;
            Destroy(this.gameObject);
        }
    }

    //Removes an enemy from the total number of enemies and adds to the total number of kills
    private void RemoveEnemy()
    {
        gameManager.enemiesKilled++;
        gameManager.enemyNumb--;
        Destroy(this.gameObject);
    }

    private void FindClosestPlayer()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
            {

                if (i == 0)
                {
                    closestPlayer = players[0];
                }

                float playerDistnace = Vector3.Distance(myTransform.position, players[i].transform.position);
                float oldDistance = Vector3.Distance(myTransform.position, closestPlayer.transform.position);

                if (playerDistnace <= oldDistance)
                {
                    closestPlayer = players[i];
                }
            }
        }
    }
}
