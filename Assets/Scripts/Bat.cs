using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Bat : MonoBehaviour
{
    [SerializeField]
    private Transform topWaypoint = null;
    [SerializeField]
    private Transform downWaypoint = null;
    private Transform nextPoint = null;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float waypointMinRadius = .2f;
    [SerializeField]
    private Transform batBody = null;

    private Enemy enemyBase = null;

    private void Awake()
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

    private void MoveToNextPoint()
    {
        Vector3 dir = (nextPoint.position - batBody.transform.position).normalized;
        batBody.transform.position += dir * speed * Time.deltaTime;
        if (IsCloseToNextPoint())
        {
            ChangeNextWaypoint();
        }
    }

    private void ChangeNextWaypoint()
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

    private bool IsCloseToNextPoint()
    {
        return Vector3.Distance(batBody.transform.position, nextPoint.position) <= waypointMinRadius;
    }


    public void HandleDie()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
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
