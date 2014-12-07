using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

    public List<GameObject> mEnemies;
    public List<GameObject> mFallenSoldiers;
    public Transform PlayerStartPosition;
    public GameObject mDoor;

    public int LevelIndex;
	// Use this for initialization
	void Start () {
        SleepLevel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SleepLevel()
    {
        foreach (EnemyController ec in GetComponentsInChildren<EnemyController>())
        {
            ec.GoToSleep();
        }
        foreach (FallenAllyController fac in GetComponentsInChildren<FallenAllyController>())
        {
            fac.GoToSleep();
        }
    }

    public void StartLevel()
    {
        GameObject.Find("Player").transform.position = PlayerStartPosition.position;

        foreach (EnemyController enemy in GetComponentsInChildren<EnemyController>())
        {
            enemy.GetComponent<EnemyController>().WakeUp();
        }
        foreach (FallenAllyController fac in GetComponentsInChildren<FallenAllyController>())
        {
            fac.WakeUp();
        }
        GameController.instance.Door = mDoor;
    }
}
