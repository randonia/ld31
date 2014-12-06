using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public enum ControlMode {
        Mobile,
        Desktop
    }

    public static ControlMode mControlMode;

    // Use this for initialization
    void Start()
    {
        mControlMode = ControlMode.Desktop;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
