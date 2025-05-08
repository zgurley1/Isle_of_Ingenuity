using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    public float reachDistance = 0.5f;
    private int currentWaypoint = 0;
    private bool isSailing = false;

    void Start() {
        StartSailing();
    }
    
    void Update()
    {
        if (isSailing && currentWaypoint < waypoints.Length)
        {
            Transform target = waypoints[currentWaypoint];
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.LookAt(target);

            if (Vector3.Distance(transform.position, target.position) < reachDistance)
            {
                currentWaypoint++;
            }
        }
    }

    public void StartSailing()
    {
        isSailing = true;
    }
}
