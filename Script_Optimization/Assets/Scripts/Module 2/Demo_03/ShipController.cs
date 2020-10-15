using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;


public class ShipController : MonoBehaviour
{
    GameObject[] targets;

    private void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Target");
    }

    void Update()
    {
        GameObject nearestTarget = null;

        float nearestDistance = float.MaxValue;

        foreach (GameObject target in targets)
        {
            Profiler.BeginSample("DISTANCE");

            float currentDistance = Vector2.Distance(transform.position, target.transform.position);

            Profiler.EndSample();

            Profiler.BeginSample("SqrMAGNITUDE");

            float currentDistance2 = Vector2.SqrMagnitude(target.transform.position - transform.position);

            Profiler.EndSample();

            if (currentDistance < nearestDistance)
            {
                nearestDistance = currentDistance;
                nearestTarget = target;
            }
        }

        Debug.DrawLine(transform.position, nearestTarget.transform.position);
    }
}

