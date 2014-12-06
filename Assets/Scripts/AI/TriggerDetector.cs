using UnityEngine;
using System.Collections;

public class TriggerDetector : MonoBehaviour {
    
    public delegate void triggerResponse(Collider other);
    public triggerResponse TriggerResponse;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        // Propegate the change upwards. Because Unity
        TriggerResponse(other);
    }
}
