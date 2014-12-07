using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public enum ControlMode {
        Mobile,
        Desktop
    }

    public static ControlMode mControlMode = ControlMode.Desktop;

    // Use this for initialization
    void Start()
    {
        //mControlMode = ControlMode.Desktop;
        mControlMode = ControlMode.Mobile;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
