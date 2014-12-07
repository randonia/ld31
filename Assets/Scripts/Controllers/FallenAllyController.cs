using UnityEngine;
using System.Collections;

public class FallenAllyController : MonoBehaviour {

    private enum FallenState
    {
        Sleep,
        Active
    }

    private FallenState mState = FallenState.Sleep;
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
        switch (mState)
        {
            case FallenState.Sleep:
                return;
            case FallenState.Active:
                // If we're doing timing
                if (GO_TIMER.activeSelf)
                {
                    if (mHealStartTime + kHealTime < Time.time)
                    {
                        mParticleSystem.Emit(15);
                        GameController.instance.HealSoldier(gameObject);
                    }
                }
                break;
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

    internal void WakeUp()
    {
        mState = FallenState.Active;
        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            collider.enabled = true;
        }
    }

    internal void GoToSleep()
    {
        mState = FallenState.Sleep;
        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
        }
    }
}
