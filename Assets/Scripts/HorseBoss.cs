using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HorseBoss : Enemy
{
    List<BossWaypoint> waypoints;
    // Update is called once per frame

    int currentWaypointIndex;
    WaypointState state = WaypointState.Tracking;
    float arrivedDuration;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        waypoints = FindObjectsByType<BossWaypoint>(FindObjectsSortMode.InstanceID).ToList();

        transform.position = waypoints[0].transform.position;
    }
    void FixedUpdate()
    {
        if (state == WaypointState.Tracking)
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector3.Lerp(GetComponent<Rigidbody2D>().linearVelocity, (waypoints[currentWaypointIndex].transform.position - transform.position).normalized * speed, Time.fixedDeltaTime);
            if (Vector3.Distance(GetComponent<Rigidbody2D>().transform.position, waypoints[currentWaypointIndex].transform.position) < 5)
            {
                state = WaypointState.Arrived;
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Count)
                {
                    currentWaypointIndex = 0;
                }

                Helper.Wait(5f, () => state = WaypointState.Tracking);
            }

        }
        else
        {
            var newX = Mathf.Cos(Time.time);
            var newY = Mathf.Sin(Time.time);
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.Lerp(GetComponent<Rigidbody2D>().linearVelocity, new Vector2(newX, newY) * 2f, Time.fixedDeltaTime * 5f);
        }
    }

    enum WaypointState
    {
        Tracking,
        Arrived
    }
}
