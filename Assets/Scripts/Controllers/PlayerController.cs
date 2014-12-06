using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private const float ACCELERATION = 0.15f;
    private const float WALK_SPEED = 0.75f;
    private const float RUN_SPEED = 1.5f;

    private CharacterController mController;

    private float mSpeed = 0.0f;
    private bool mSprinting = false;

    // Use this for initialization
    void Start()
    {
        mController = GetComponent<CharacterController>();
        Debug.Log("Started Player!");
    }

    // Update is called once per frame
    void Update()
    {
        DoMovement();
    }

    private void DoMovement()
    {
        mSprinting = Input.GetKey(KeyCode.LeftShift);

        Vector3 movementVector = new Vector3();
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

private bool MovementKeyDown()
{
 	return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S);
}
}
