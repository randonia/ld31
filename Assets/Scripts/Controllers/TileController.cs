using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {

    private const float ZINDEX = 0.0f;

    public GameObject PREFAB_Concrete;


	// Use this for initialization
	void Start () {
        int gridSize = 26;
        int halfGrid = (int)(gridSize * 0.5);
        for (int x = -halfGrid - 1; x < halfGrid; ++x)
        {
            for (int y = -halfGrid - 1; y < halfGrid; ++y)
            {
                GameObject newTile = (GameObject)GameObject.Instantiate(PREFAB_Concrete);
                newTile.transform.SetParent(transform);
                newTile.transform.Translate(x * 0.25f, y * 0.25f, ZINDEX);
                newTile.name = "concrete_" + x + "_" + y;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
