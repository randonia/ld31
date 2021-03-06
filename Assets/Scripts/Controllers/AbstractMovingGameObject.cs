﻿using UnityEngine;
using System.Collections;

public class AbstractMovingGameObject : MonoBehaviour {

    protected const float ACCELERATION = 0.15f;
    protected const float WALK_SPEED = 0.7f;
    protected const float RUN_SPEED = 0.85f;

    protected CharacterController mController;
    protected float mSpeed = 0.0f;
    protected Vector3 mLookDirection = Vector3.zero;

	protected void Initialize(string name){
        gameObject.name = name;
        Debug.Log("Started " + name);
        mController = GetComponent<CharacterController>();
        transform.position.Set(transform.position.x, transform.position.y, 0.0f);
    }
}
