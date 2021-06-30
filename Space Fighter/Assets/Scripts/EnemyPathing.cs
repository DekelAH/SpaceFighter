using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPathing : MonoBehaviour
{
    List<Transform> wayPoints; // List of the enemies positions

    WaveConfig waveConfig; // Getting access to Wave Config class

    private int wayPointIndex = 0; // Starting way point

    // Start is called before the first frame update
    void Start()
    {
        wayPoints = waveConfig.GetWayPoints(); // Getting the way points from WaveConfing class into wayPoints list
        transform.position = wayPoints[wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveByWayPoints();
    }

    // Setting the recived waveConfig into the waveConfing instance
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void MoveByWayPoints()
    {
        if (wayPointIndex <= wayPoints.Count -1) // Checking if it gone higher than the list length
        {
            // Getting the position of the current way point
            var targetPosition = wayPoints[wayPointIndex].transform.position;

            // Setting the movement speed from WaveConfig class and giving it frame independent
            float movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;

            // Setting the target of the next way point
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);

            // If the enemy reached the target, increment the way point index by 1
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
