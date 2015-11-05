using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour {

    public GameObject pickUp;

    public GameObject EnemyLazor;

    private GameObject player;

    private GameManager gameManager;

    private float movementSpeed = 20.0f;

    private Transform myTransform;

    private float lazorFireTime;
    private float lazorFireRate = 2f;

    private float health = 20.0f;

    private float rotationSpeed = 2.0f;
    private float adjRotationSpeed;
    private Quaternion targetRotation;

    private int numberOfShots = 1;
    private float angleRange = 45.0f;

    // Use this for initialization
    void Start()
    {
        myTransform = this.transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.enemyNumb++;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckIfDead();
        checkIfOffScreen();

        if (player.transform.position.z < myTransform.position.z)
        {
            targetRotation = Quaternion.LookRotation(player.transform.position - myTransform.position);
            adjRotationSpeed = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, adjRotationSpeed);

        }


        if (Time.time > lazorFireTime)
        {
            float fireAngle = angleRange / (numberOfShots - 1);
            for (int i = 0; i < numberOfShots; i++)
            {
                print("Ping");
                Instantiate(EnemyLazor, transform.position, Quaternion.Euler(0, 0, (fireAngle * i)));
            }
            
            lazorFireTime = Time.time + lazorFireRate;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void Move()
    {
        myTransform.position += Time.deltaTime * movementSpeed * this.transform.forward;

    }

    private void randomPickUp()
    {
        int roll = Random.Range(1, 100);
        if (roll <= 5)
        {
            Instantiate(pickUp, transform.position, new Quaternion(180, 0, 0, 0));
        }
    }

    private void CheckIfDead()
    {
        if (health <= 0)
        {
            removeEnemy();
            randomPickUp();
        }
    }

    private void checkIfOffScreen()
    {
        if (transform.position.z < (-gameManager.zBoundry - 20))
        {
            removeEnemy();
        }
    }

    private void removeEnemy()
    {
        gameManager.enemiesKilled++;
        gameManager.enemyNumb--;
        Destroy(this.gameObject);
    }
}
