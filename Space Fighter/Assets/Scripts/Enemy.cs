using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] float health = 100; // Enemy health

    [Header("Projectile")]
    [SerializeField] float shotCounter; // After X of shots, restart the counter to make the shots more random
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float enemyProjectileSpeed = 10f;
    [SerializeField] GameObject EnemeyLaserPrefab;

    [Header("Sound Effects")]
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.75f; // Range of volume between 0 - 1
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f; // Range of volume between 0 - 1
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shootSound;

    [Header("Visual Effects")]
    [SerializeField] float durationOfExplosion = 0.1f;
    [SerializeField] GameObject particleExplosion;


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots); // Random range between the time between shots
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime; // Making the counter frame independent 
        if (shotCounter <= 0f) // if the shot counter reaching 0 or less call Fire()
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots); // Reseting the shot counter
        }
    }

    private void Fire()
    {
        // Creating the enemy laser to be fired from the enemy position + default rotation
        GameObject enemyLaser = Instantiate(EnemeyLaserPrefab, transform.position, Quaternion.identity);

        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);

        // Getting the rigidbody velocity component from the laser game object and setting the speed on negetive Y axis
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyProjectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D otherGameObject)
    {
        // Implementing the other game object damage into damageDealer
        DamageDealer damageDealer = otherGameObject.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) { return; } // Prevent Null exception

        ProcessHit(damageDealer);
    }

    // Calculating enemy health - if less or equal to 0 ~> destroy object
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit(); // Destroying the projetile on hit

        if (health <= 0)
        {
            EnemyDies();
        }
    }

    private void EnemyDies()
    {
        Destroy(gameObject);

        // Creating the explosion VFX by the game object itself + location + rotation
        GameObject explosion = Instantiate(particleExplosion, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion); // Destroying the explosion game object after X seconds
        // Placing the audio sound at the main camera + volume range
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);

    }
}
