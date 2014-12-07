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

    public List<GameObject> Levels;
    private LevelController mCurrLevel;
    private int mCurrLevelInt;
    public GameObject GO_Emitter;
    public GameObject Door;
    private Camera mCamera;

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
        mCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        mWoundedParticleSystem = GO_Emitter.GetComponent<ParticleSystem>();

        // Figure out how to do this via menus
        mCurrLevelInt = 0;
        mCurrLevel = Levels[mCurrLevelInt].GetComponent<LevelController>();
        mCurrLevel.StartLevel();

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
                Door.GetComponent<UI2DSpriteAnimation>().Play();
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

    internal bool LevelClearCheck()
    {
        return GO_WoundedRemaining.Count == 0;
    }

    internal void NextLevel()
    {
        if (Levels.Count > mCurrLevelInt + 1){
            mCurrLevel.WrapUp();
            // Next level!
            LevelController lastLevel = mCurrLevel;
            mCurrLevelInt++;
            mCurrLevel = Levels[mCurrLevelInt].GetComponent<LevelController>();
            LevelTransition(lastLevel, mCurrLevel);
        }
        else
        {
            // Done
            Debug.Log("Win");
        }
    }

    /// <summary>
    /// Delete this, or at least unhook its button
    /// </summary>
    public void Cheat()
    {
        NextLevel();
    }

    private void LevelTransition(LevelController lastLevel, LevelController mCurrLevel)
    {
        // Move them
        iTween.MoveTo(mCamera.gameObject, iTween.Hash("position", mCurrLevel.transform.position,
            "time", 1.5f, "delay", 0.75f, "easetype", iTween.EaseType.easeInOutQuad));
        // Fade in and out
        
        // Start the level
        mCurrLevel.StartLevel();
        GO_WoundedRemaining = mCurrLevel.mFallenSoldiers;
    }
}
