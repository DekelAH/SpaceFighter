using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPathing : MonoBehaviour
{

    [SerializeField] List<Transform> wayPoints; // List of the enemies positions
    [SerializeField] float moveSpeed = 2f; // Speed of the enemy

    private int wayPointIndex = 0; // Start way point

    // Start is called before the first frame update
    void Start()
    {
        transform.position = wayPoints[wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveByWayPoints();
    }

    private void MoveByWayPoints()
    {
        if (wayPointIndex <= wayPoints.Count - 1) // Checking if it gone higher than the list length
        {
            // Getting the position of the current way point
            var targetPosition = wayPoints[wayPointIndex].transform.position;

            // Setting the movement speed and giving it frame independent
            float movementThisFrame = moveSpeed * Time.deltaTime;

            // Setting the target of the next way point
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);

            // If the enemy reached the target increment the way point index by 1
            if (transform.position == targetPosition)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
