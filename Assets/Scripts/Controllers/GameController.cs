using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public enum ControlMode {
        Mobile,
        Desktop
    }

    public enum GameState
    {
        Playing,
        Paused,
        Dead
    }

    public GameObject GO_Door;
    public GameObject GO_Emitter;

    public List<GameObject> GO_WoundedRemaining;
    private ParticleSystem mWoundedParticleSystem;

    public bool GamePaused { get { return State == GameState.Paused; } }

    private int mScore;
    public int Score { get { return mScore; } }
    public string ScoreString { get { return mScore.ToString().PadLeft(8, '0'); } }

    private static GameController _instance;
    public static GameController instance { get { return _instance; } }

    public static GameState State = GameState.Playing;

    public static ControlMode mControlMode = ControlMode.Desktop;

    // Use this for initialization
    void Start()
    {
        _instance = this;
        mWoundedParticleSystem = GO_Emitter.GetComponent<ParticleSystem>();
        GO_Door.GetComponent<UI2DSpriteAnimation>().Pause();

        mControlMode = ControlMode.Desktop;
        //mControlMode = ControlMode.Mobile;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameController.State)
        {
            case GameState.Paused:
                break;
            case GameState.Playing:
                break;
            case GameState.Dead:
                break;
        }
    }

    public void HealSoldier(GameObject wounded)
    {
        if (GO_WoundedRemaining.IndexOf(wounded) != -1)
        {
            GO_WoundedRemaining.Remove(wounded);
            mScore++;
            mWoundedParticleSystem.gameObject.transform.position = wounded.transform.position;
            mWoundedParticleSystem.Play();
            GameObject.Destroy(wounded);
            
            if (GO_WoundedRemaining.Count == 0)
            {
                GO_Door.GetComponent<UI2DSpriteAnimation>().Play();
            }
        }
    }

    public void TogglePausedUnpaused(){
        if (State.Equals(GameState.Paused)){
            ResumeGame();
        } else {
            PauseGame();
        }
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
        State = GameState.Paused;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
        State = GameState.Playing;
    }
}
