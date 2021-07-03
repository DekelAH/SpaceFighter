using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 100;

    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 0f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] GameObject laserPrefab; // Copy of the player laser game object

    Coroutine firingCoroutine; // variable to implement the coroutine and to stop it when needed

    private float xMin, xMax; // X axis boundries variables
    private float yMin, yMax; // Y axis boundries variables



    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D otherGameObject)
    {
        // Implementing the other game object damage into damageDealer
        DamageDealer damageDealer = otherGameObject.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) { return; } // Prevent Null exception

        ProcessHit(damageDealer);
    }

    // Calculating player health - if less or equal to 0 ~> destroy object
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit(); // Destroying the projetile on hit

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Fire()
    {
        // While hold the space key the coroutine - FireContinuously() starts
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        // When leaving the space key the coroutine stops
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            // Creating the player laser from player position and using the default rotation and setting it as game object
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);

            // Getting the rigidbody2D component and setting the velocity to the laser at the Y axis
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            // Setting the rate of fire (wait X secs between projectiles)
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    // Setting up the movement of the player
    private void Move()
    {
        // Variable gets access to input object function(GetAxis) who sets the horizontal movement - right to positive x, left to negetive x *
        // time.deltaTime that gives frame rate independent * moveSpeed variable type float to determine the speed.
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        // Variable gets access to input object function(GetAxis) who sets the vertical movement - up to positive y, down to negetive y *
        // time.deltaTime that gives frame rate independent * moveSpeed variable type float to determine the speed.
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        // Clamping up the new position of the X axis by setting the new position + direction & speed, minimum X position and max position
        float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        // Clamping up the new position of the Y axis by setting the new position + direction & speed, minimum Y position and max position
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        // Setting the new position of the player in game by this mentioned variables
        transform.position = new Vector2(newXPos, newYPos);
    }

    // Setting up boundries for the player movement in game
    private void SetUpMoveBoundaries()
    {
        // Implementing the main camera in the scene to this variable
        Camera gameCamera = Camera.main;

        // Setting the minimun X position of the world space value of the X axis +
        // padding - to prevent from the player sprite to be half out side of the view because of the pivot point
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;

        // Setting the maximun X position of the world space value of the X axis + padding (as mentioned)
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        // Setting the minimum Y position of the world space value of the Y axis + padding (as mentioned)
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;

        // Setting the maximum Y position of the world space value of the Y axis + padding (as mentioned)
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
