using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100; // Enemy health

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D otherGameObject)
    {
        // Implementing the other game object damage into damageDealer
        DamageDealer damageDealer = otherGameObject.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    // Calculating enemy health - if less or equal to 0 ~> destroy object
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
