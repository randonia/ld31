using UnityEngine;
using System.Collections;

public class FallenAllyController : MonoBehaviour {
    
    public GameObject GO_TIMER;
    private UI2DSpriteAnimation mTimerAnimation;

    private const float kHealTime = 2.0f;
    private float mHealStartTime;

    private ParticleSystem mParticleSystem;

	// Use this for initialization
	void Start () {
        mParticleSystem = GetComponentInChildren<ParticleSystem>();
        mParticleSystem.Stop();
        GO_TIMER.SetActive(false);
        mTimerAnimation = GO_TIMER.GetComponent<UI2DSpriteAnimation>();
        mTimerAnimation.framesPerSecond = Mathf.RoundToInt(mTimerAnimation.frames.Length / kHealTime);
	}
	
	// Update is called once per frame
	void Update () {
        // If we're doing timing
        if (GO_TIMER.activeSelf)
        {
            if (mHealStartTime + kHealTime < Time.time)
            {
                // Do the poof or whatever. Add score
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mParticleSystem.Play();
            GO_TIMER.SetActive(true);
            mTimerAnimation.ResetToBeginning();
            mTimerAnimation.Play();
            mHealStartTime = Time.time;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mParticleSystem.Stop();
            GO_TIMER.SetActive(false);
            mHealStartTime = float.MaxValue;
            mTimerAnimation.Pause();
        }
    }
}
