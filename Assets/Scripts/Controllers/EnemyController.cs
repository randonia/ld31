using UnityEngine;
using System.Collections;

public class EnemyController : AbstractMovingGameObject {
    public GameObject PREFAB_BULLET;

    public GameObject StartPathingNode;
    private GameObject mCurrNodeGO;
    private PathNode mCurrNode;
    private SphereCollider mPerceptionCollider;

    private const float kFireRate = 0.5f;
    private float mLastFiredTime = 0.0f;

	// Use this for initialization
	void Start () {
        Initialize("Enemy");

        foreach(TriggerDetector td in GetComponentsInChildren<TriggerDetector>()){
            td.TriggerEnterDelegate = OnTriggerEnter;
            td.TriggerStayDelegate = OnTriggerStay;
        }
        mCurrNodeGO = StartPathingNode;
        mCurrNode = mCurrNodeGO.GetComponent<PathNode>();
        mPerceptionCollider = transform.Find("EnemyPerception").GetComponent<SphereCollider>();
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

        Vector3 movementNormalized = movementVector.normalized;
        mController.Move(movementNormalized * Time.deltaTime * mSpeed);
        mLookDirection = (mLookDirection + movementNormalized).normalized;
        Debug.DrawRay(transform.position, mLookDirection * mPerceptionCollider.radius, Color.cyan);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pathnode")
        {
            StartCoroutine(WaitForSeconds(mCurrNode.nodeDelay));
        }
        if (other.gameObject.tag == "Player")
        {
            // Shoot at the player
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (CanSee(other.gameObject))
            {
                float angleBetween = Vector3.Angle((other.gameObject.transform.position - transform.position), mLookDirection);
                if (angleBetween <= 120.0 && mLastFiredTime + kFireRate < Time.time)
                {
                    ShootAt(other.gameObject);
                }
            }
        }
    }

    private bool CanSee(GameObject other)
    {
        Ray ray = new Ray(transform.position, (other.transform.position - transform.position).normalized);
        RaycastHit hitInfo;
        LayerMask mask = LayerMask.GetMask(new string[]{"Player", "Obstacle"});
        if (Physics.SphereCast(ray, 0.05f, out hitInfo, distance: mPerceptionCollider.radius, layerMask: mask))
        {
            if (hitInfo.collider.gameObject.tag.Equals("Player"))
            {
                Debug.DrawLine(transform.position, hitInfo.collider.transform.position, Color.red, 1.0f);
                return true;
            }
            else
            {
                Debug.DrawLine(transform.position, hitInfo.collider.transform.position, Color.white, 1.0f);
            }
        }
        
        return false;
    }

    private void ShootAt(GameObject gameObject)
    {
        GameObject bullet = (GameObject)GameObject.Instantiate(PREFAB_BULLET,
            transform.position, Quaternion.identity);
        bullet.name = "zbullet";
        bullet.GetComponent<BulletController>().Direction = (gameObject.transform.position - transform.position);
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

    void OnDrawGizmos()
    {
        if (mPerceptionCollider)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, mPerceptionCollider.radius);
        }
    }
}
