using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : MonoBehaviour
{
    public List<Transform> nodes = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Transform[] path = GetComponentsInChildren<Transform>();

        nodes = new List<Transform>();
        for(int i= 1; i<path.Length; i++)
        {
            nodes.Add(path[i]);
        }

        for(int  i=0; i< nodes.Count; i++)
        {
            Vector3 currentWaypoint = nodes[i].position;
            Vector3 previousWay = Vector3.zero;

            if (i != 0)
            {
                previousWay = nodes[i - 1].position;
            }
            
            Gizmos.DrawLine(previousWay,currentWaypoint);
        }
    }
}
