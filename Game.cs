using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public static int xRange = 9;
    public static int yRange = 15;
    public static int zRange = 0;
   
    public GameObject[] Blocks;
    public Transform[] spawnLocations;
    // Use this for initialization
	void Start () {
        spawnObject();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool checkIsInsideGrid(Vector3 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x <= xRange && (int)pos.y >= 1 && (int)pos.y <= yRange && (int)pos.z >= -10 && (int)pos.z <= zRange);
    }

    public Vector3 Round (Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
    }


    public void spawnObject()
    {

        int i = Random.Range(0, spawnLocations.Length);

        int b = Random.Range(0, Blocks.Length);
        GameObject block =Instantiate(Blocks[b], spawnLocations[i].position, Quaternion.identity) as GameObject;

        block.GetComponent<Tetrimino>().side = (Tetrimino.Side)i;

        if (i != 1)
        {
            block.transform.Rotate(0,90,0);
            block.GetComponent<Tetrimino>().movementVector = new Vector3(0,0,1);
        }

    }


  
}
