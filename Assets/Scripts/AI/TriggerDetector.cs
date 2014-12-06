using UnityEngine;
using System.Collections;

public class TriggerDetector : MonoBehaviour {
    
    public delegate void triggerResponse(Collider other);
    public triggerResponse TriggerEnterDelegate;
    public triggerResponse TriggerStayDelegate;
    public triggerResponse TriggerExitDelegate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        // Propegate the change upwards. Because Unity
        if (TriggerEnterDelegate != null)
        {
            TriggerEnterDelegate(other);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (TriggerStayDelegate != null)
        {
            TriggerStayDelegate(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (TriggerExitDelegate != null)
        {
            TriggerExitDelegate(other);
        }
    }
}
