using UnityEngine;
using UnityEditor; //always remember to add this
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(PatrolWaypointContainer))]
public class WaypointContainerEditor : Editor
{

    private GameObject PREFAB_NODE;
    private const string NODE_PATH = "Assets/Prefabs/EnemyPathNode.prefab";

    List<GameObject> waypoints; //the list 

    void OnEnable()
    {
        PREFAB_NODE = (GameObject)AssetDatabase.LoadAssetAtPath(NODE_PATH, typeof(GameObject));
        waypoints = (target as PatrolWaypointContainer).waypoints; //reference the list of the instance we are editing
    }

    public override void OnInspectorGUI() //this is where the inspectorgui gets handled
    {
        GUILayout.BeginVertical();
        GUILayout.Label("Hopefully your Node prefab is still: ");
        GUILayout.Label(NODE_PATH);
        GUILayout.EndVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Number of Waypoints: ");
        GUILayout.Label(waypoints.Count.ToString());
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Reset"))
        {
            foreach (GameObject wayPoint in waypoints)
            {
                if (wayPoint)
                {
                    DestroyImmediate(wayPoint.gameObject, false);
                }
            }
            waypoints.Clear();
        }
        if (GUILayout.Button("Add waypoint"))
        {
            GameObject newWaypoint = (GameObject)GameObject.Instantiate(PREFAB_NODE, 
                (target as PatrolWaypointContainer).transform.position, Quaternion.identity);
            //newWaypoint.transform.SetParent((target as PatrolWaypointContainer).transform);
            PathNode node = newWaypoint.GetComponent<PathNode>();
            newWaypoint.name = "PATH_NODE_" + waypoints.Count;
            if(waypoints.Count > 0){
                PathNode prevNode = waypoints[waypoints.Count - 1].GetComponent<PathNode>();
                node.PrevNode = prevNode;
                prevNode.NextNode = node;
            }
            waypoints.Add(newWaypoint);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
