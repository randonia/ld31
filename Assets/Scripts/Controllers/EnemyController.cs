using UnityEngine;
using System.Collections;

public class EnemyController : AbstractMovingGameObject {

    public GameObject StartPathingNode;
    private GameObject mCurrNodeGO;
    private PathNode mCurrNode;

	// Use this for initialization
	void Start () {
        Initialize("Enemy");

        foreach(TriggerDetector td in GetComponentsInChildren<TriggerDetector>()){
            td.TriggerResponse = OnTriggerEnter;
        }
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
        Debug.Log("Triggered with: " + other.gameObject.tag);
        if (other.gameObject.tag == "pathnode")
        {
            StartCoroutine(WaitForSeconds(mCurrNode.nodeDelay));
        }
        if (other.gameObject.tag == "Player")
        {
            // Do some player perception here
        }
    }

    IEnumerator WaitForSeconds(float sec)
    {
        PathNode next = mCurrNode.NextNode;
        mCurrNode = null;
        mCurrNodeGO = null;
        yield return new WaitForSeconds(sec);
        mCurrNode = next;
        mCurrNodeGO = next.gameObject;
    }
}
