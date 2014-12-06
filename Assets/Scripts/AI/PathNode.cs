﻿using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour {
    private const float kGizmoSize = 0.1f;
    public float nodeDelay;

    public GameObject prevNodeGO;
    public GameObject nextNodeGO;

    private PathNode mPrevNode;
    public PathNode PrevNode
    {
        get { return mPrevNode; }
    }

    private PathNode mNextNode;
    public PathNode NextNode
    {
        get { return mNextNode; }
    }

    private string mNodeName;

	// Use this for initialization
	void Start () {
        mNodeName = gameObject.name;
        if (nextNodeGO)
        {
            mNextNode = nextNodeGO.GetComponent<PathNode>();
        }
        if (prevNodeGO)
        {
            mPrevNode = prevNodeGO.GetComponent<PathNode>();
        }
        transform.position.Set(transform.position.x, transform.position.y, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, Vector3.one * kGizmoSize);
        TextGizmo.Instance.DrawText(transform.position, gameObject.name);
        if (prevNodeGO)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, MathUtils.HalfwayPointBetween(transform.position, prevNodeGO.transform.position));
        }
        if (nextNodeGO)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, MathUtils.HalfwayPointBetween(transform.position, nextNodeGO.transform.position));
        }
    }
}
