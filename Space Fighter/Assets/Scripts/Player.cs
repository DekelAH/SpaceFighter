using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;

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
}
