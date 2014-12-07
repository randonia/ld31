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
    public GameObject GO_Fader;
    private Camera mCamera;

    public List<GameObject> GO_WoundedRemaining;
    private ParticleSystem mWoundedParticleSystem;

    public bool Playing { get { return State == GameState.Playing; } }
    public bool GamePaused { get { return State == GameState.Paused; } }
    public bool PlayerDead { get { return State == GameState.Dead; } }
    private float mDeadFade = 0.0f;
    public float DeadFade { get { return mDeadFade; } }
    

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
        iTween.MoveTo(mCamera.gameObject, iTween.Hash("position", mCurrLevel.transform.position - 20.0f * mCurrLevel.transform.forward,
            "time", 1.5f, "delay", 1.00f, "easetype", iTween.EaseType.easeInOutQuad,
            "oncomplete", "LevelTransitionCallback", "oncompletetarget", gameObject));
        // Fade in and out
        FadeOut();
    }

    private void FadeOut()
    {
        iTween.FadeTo(GO_Fader, iTween.Hash("alpha", 1, "time", 0.5f));
        iTween.MoveTo(GO_Fader, iTween.Hash("z", 18, "time", 1.0f, "islocal", true));
    }

    private void FadeIn()
    {
        iTween.FadeTo(GO_Fader, iTween.Hash("alpha", 0, "time", 0.5f));
        iTween.MoveTo(GO_Fader, iTween.Hash("z", 31, "time", 0.5f, "islocal", true));
    }

    public void LevelTransitionCallback()
    {
        FadeIn();
        // Start the level
        mCurrLevel.StartLevel();
        GO_WoundedRemaining = mCurrLevel.mFallenSoldiers;
    }

    internal void PlayerKilled()
    {
        if (State != GameState.Dead)
        {
            iTween.ValueTo(gameObject, iTween.Hash("name", "mDeadFade", "from", 0.0f, "to", 1.0f,
                "onupdate", "SetDeathFade"));
        }
        State = GameState.Dead;
    }

    private void SetDeathFade(float val)
    {
        mDeadFade = val;
    }
}
