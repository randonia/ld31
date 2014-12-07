using UnityEngine;
using System.Collections;

public class PlayerController : AbstractMovingGameObject {

    private Camera mMainCamera;

    private bool mSprinting = false;

    # region Touch properties 
    public GameObject GO_TouchStart;
    public GameObject GO_TouchEnd;

    private const float kJoystickRange = 100.0f;

    private Vector2 mTouchStartPos;
    private Vector2 mTouchCurrPos;
    private float mTouchMagnitude;
    private Vector3 mTouchStartWorldPos;
    private Vector3 mTouchCurrWorldPos;

    #endregion

    // Use this for initialization
    void Start()
    {
        mMainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        Initialize("Player");
        foreach (TriggerDetector td in GetComponentsInChildren<TriggerDetector>())
        {
            td.TriggerEnterDelegate = OnTriggerEnter;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DoMovement();
    }

    private void DoMovement()
    {
        mSprinting = Input.GetKey(KeyCode.LeftShift);

        Vector3 movementVector = Vector3.zero;

        if(GameController.mControlMode.Equals(GameController.ControlMode.Desktop))
        {
            if(MovementKeyDown()){
                mSpeed = Mathf.Min(mSpeed + ACCELERATION, ((mSprinting)?RUN_SPEED:WALK_SPEED));
            } else {
                mSpeed = Mathf.Max(mSpeed - ACCELERATION, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                movementVector.x += 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movementVector.x += -1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                movementVector.y += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementVector.y += -1;
            }
        }
        else if(GameController.mControlMode.Equals(GameController.ControlMode.Mobile))
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Set up all the vectors
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        // Activate the Game Objects
                        GO_TouchStart.SetActive(true);
                        GO_TouchEnd.SetActive(true);
                        // Set up the vectors
                        mTouchStartPos = touch.position;
                        mTouchStartWorldPos = mMainCamera.ScreenToWorldPoint(touch.position);
                        // Move the Game Objects
                        GO_TouchStart.transform.position = mTouchStartWorldPos;
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        mTouchCurrPos = touch.position;
                        mTouchCurrWorldPos = mMainCamera.ScreenToWorldPoint(touch.position);
                        GO_TouchEnd.transform.position = mTouchCurrWorldPos;
                        mTouchMagnitude = (mTouchCurrPos - mTouchStartPos).magnitude;
                        break;
                    case TouchPhase.Ended:
                        GO_TouchStart.SetActive(false);
                        GO_TouchEnd.SetActive(false);
                        break;
                    default:
                        break;
                }

                // Map 0 to 100 for Joystick controls to 0 to RUN_SPEED
                mSpeed = Mathf.Min(MathUtils.Map(mTouchMagnitude, 0, 100, 0, RUN_SPEED), RUN_SPEED);
                movementVector = mTouchCurrPos - mTouchStartPos;
                Debug.DrawLine(transform.position, transform.position + (mTouchCurrWorldPos - mTouchStartWorldPos), Color.yellow);
            }
            else
            {
                // Slow down since no movement is there
                mSpeed = 0.0f;
            }

            if (GO_TouchStart.activeSelf && GO_TouchEnd.activeSelf)
            {
                Debug.DrawLine(GO_TouchStart.transform.position, GO_TouchEnd.transform.position, Color.yellow);
            }
        }

        mController.Move(movementVector.normalized * Time.deltaTime * mSpeed * ((mSprinting)?2:1));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "projectile")
        {
            // Do death stuff
        }
        if (other.gameObject.tag == "target")
        {
            // Do target stuff
        }
        if (other.gameObject.tag == "goal")
        {
            if (GameController.instance.LevelClearCheck())
            {
                // Start the level end sequence
                GameController.instance.NextLevel();
            }
        }
    }

    private bool MovementKeyDown()
    {
 	    return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
    }
}
