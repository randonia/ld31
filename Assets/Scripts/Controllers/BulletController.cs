using UnityEngine;
using System.Collections;

public class BulletController : AbstractMovingGameObject {

    private const float kLifespan = 3.0f;
    private float mBirthTime;

    private Vector3 mDirection;
    public Vector3 Direction
    {
        get { return mDirection; }
        set { mDirection = value.normalized; }
    }

	// Use this for initialization
	void Start () {
        mSpeed = 10.0f;
        mBirthTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(mDirection * mSpeed * Time.deltaTime);
        if (mBirthTime + kLifespan < Time.time)
        {
            GameObject.Destroy(gameObject);
        }
	}

}
