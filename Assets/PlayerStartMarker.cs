using UnityEngine;
using System.Collections;

public class PlayerStartMarker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}
