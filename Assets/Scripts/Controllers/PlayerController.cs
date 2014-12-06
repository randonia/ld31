using UnityEngine;
using System.Collections;

public class PlayerController : AbstractMovingGameObject {

    private bool mSprinting = false;

    // Use this for initialization
    void Start()
    {
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
            Debug.Log("Target!");
        }
    }

    private bool MovementKeyDown()
    {
 	    return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S);
    }
}
