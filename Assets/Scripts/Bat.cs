using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Bat : MonoBehaviour
{
    [SerializeField] Transform topWaypoint = null;
    [SerializeField] Transform downWaypoint = null;
    Transform nextPoint = null;
    [SerializeField] float speed = 2f;
    [SerializeField] float waypointMinRadius = .2f;
    [SerializeField] Transform batBody = null;

    Enemy enemyBase = null;

    void Awake()
    {
        enemyBase = GetComponent<Enemy>();
        enemyBase.OnDie += HandleDie;
    }

    void Start()
    {
        if (topWaypoint == null || downWaypoint == null)
        {
            Debug.LogError("Need to set the waypoints");
        }
        if (batBody == null)
        {
            Debug.LogError("Need to set batBody");
        }
        nextPoint = topWaypoint;
    }

    void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        Vector3 dir = (nextPoint.position - batBody.transform.position).normalized;
        batBody.transform.position += dir * speed * Time.deltaTime;
        if (IsCloseToNextPoint())
        {
            ChangeNextWaypoint();
        }
    }

    void ChangeNextWaypoint()
    {
        if (nextPoint.position == topWaypoint.position)
        {
            nextPoint = downWaypoint;
        }
        else
        {
            nextPoint = topWaypoint;
        }
    }

    bool IsCloseToNextPoint()
    {
        return Vector3.Distance(batBody.transform.position, nextPoint.position) <= waypointMinRadius;
    }

    void HandleDie()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        if (topWaypoint != null && downWaypoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(topWaypoint.position, waypointMinRadius);
            Gizmos.DrawWireSphere(downWaypoint.position, waypointMinRadius);
            Gizmos.DrawLine(topWaypoint.position, downWaypoint.position);
        }
    }

}
