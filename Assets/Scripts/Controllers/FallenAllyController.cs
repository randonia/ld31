using UnityEngine;
using System.Collections;

public class FallenAllyController : MonoBehaviour {
    
    private ParticleSystem mParticleSystem;

	// Use this for initialization
	void Start () {
        mParticleSystem = GetComponentInChildren<ParticleSystem>();
        mParticleSystem.Stop();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mParticleSystem.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mParticleSystem.Stop();
        }
    }
}
