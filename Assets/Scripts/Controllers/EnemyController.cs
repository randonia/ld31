using UnityEngine;
using System.Collections;

public class EnemyController : AbstractMovingGameObject {

    public GameObject StartPathingNode;
    private GameObject mCurrNodeGO;
    private PathNode mCurrNode;
    private TriggerDetector mTriggerDetector;

	// Use this for initialization
	void Start () {
        Initialize("Enemy");
        mTriggerDetector = GetComponentInChildren<TriggerDetector>();
        mTriggerDetector.TriggerResponse = OnTriggerEnter;
        mCurrNodeGO = StartPathingNode;
        mCurrNode = mCurrNodeGO.GetComponent<PathNode>();
	}
	
	// Update is called once per frame
	void Update () {
        mSpeed = 0.5f;
        DoMovement();
	}

    void DoMovement()
    {
        Vector3 movementVector = Vector3.zero;
        if (mCurrNodeGO)
        {
            movementVector = mCurrNodeGO.transform.position - transform.position;
        }

        mController.Move(movementVector.normalized * Time.deltaTime * mSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLISION WOO");
        if (other.gameObject.tag == "pathnode")
        {
            mCurrNode = mCurrNode.NextNode;
            mCurrNodeGO = mCurrNode.gameObject;
        }
    }
}
