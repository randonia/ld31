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
        iTween.Init(gameObject);
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
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }
        mDoor.GetComponent<UI2DSpriteAnimation>().Pause();
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
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = true;
        }

        GameController.instance.Door = mDoor;
    }

    internal void WrapUp()
    {
        // Do extra cleanup that's not done by Sleep
        SleepLevel();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.1f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + transform.up * 1.438f, new Vector3(10.0f, 0.15f, 1.0f));
        Gizmos.DrawWireCube(transform.position + transform.up * -1.562f, new Vector3(10.0f, 0.15f, 1.0f));
        Gizmos.DrawWireCube(transform.position + transform.right * 2.625f, new Vector3(0.15f, 10.0f, 1.0f));
        Gizmos.DrawWireCube(transform.position + transform.right * -2.688f, new Vector3(0.15f, 10.0f, 1.0f));
    }
}
