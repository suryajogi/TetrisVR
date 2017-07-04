using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tetrimino : MonoBehaviour {
    public Text text;
    public enum Side { left,middle,right};

    public Side side = Side.middle;

    public float fallSpeed = 1;

    //public AudioClip BGSounds;

    //We need to add offsets for each block type in their prfabs 

    public bool allowRotation = true;
    public bool limitRotation = false;

    [HideInInspector]
    public Vector3 movementVector = new Vector3(1, 0, 0);
    // Use this for initialization

    void Awake()
    {
        InvokeRepeating("Fall", 0, fallSpeed);
    }

    void Start()
    {
        text = GameObject.FindObjectOfType<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckUserInput();
	}



    void CheckUserInput()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {

            transform.position += movementVector;
           // AudioSource.PlayClipAtPoint(BGSounds, transform.position);
            if (CheckIsValidPosition())
            {

            }
            else
            {
                transform.position += movementVector * -1;
           //     AudioSource.PlayClipAtPoint(BGSounds, transform.position);
            }

        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += movementVector * -1;
         //   AudioSource.PlayClipAtPoint(BGSounds, transform.position);

            if (CheckIsValidPosition())
            {

            }
            else
            {
                transform.position += movementVector;
           //     AudioSource.PlayClipAtPoint(BGSounds, transform.position);
            }

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            CancelInvoke("Fall");
            InvokeRepeating("Fall", 0, fallSpeed / 2f);

        }
        else if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            CancelInvoke("Fall");
            InvokeRepeating("Fall", 0, fallSpeed);

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (allowRotation)
            {
                if (limitRotation)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                 //       AudioSource.PlayClipAtPoint(BGSounds, transform.position);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                  //      AudioSource.PlayClipAtPoint(BGSounds, transform.position);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                 //   AudioSource.PlayClipAtPoint(BGSounds, transform.position);
                }
                if (CheckIsValidPosition())
                {

                }
                else
                {
                    if (limitRotation)
                    {

                        if (transform.rotation.eulerAngles.z >= 90)
                        {
                            transform.Rotate(0, 0, -90);
                   //         AudioSource.PlayClipAtPoint(BGSounds, transform.position);
                        }
                        else
                        {
                            transform.Rotate(0, 0, -90);
                      //      AudioSource.PlayClipAtPoint(BGSounds, transform.position);

                        }
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                     //   AudioSource.PlayClipAtPoint(BGSounds, transform.position);
                    }
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {

            text.text = "Moving along sides have to be developed";
          //  AudioSource.PlayClipAtPoint(BGSounds, transform.position);
            /* switch (side)
            {
                case Side.left:
                    transform.position = new Vector3(-(transform.position.z)-1, transform.position.y, 0);
                    transform.Rotate(0, -90, 0);
                    side = Side.middle;
                    break;
                case Side.middle:
                    transform.position = new Vector3(10, transform.position.y, -(transform.position.x)-1);
                    transform.Rotate(0, 90, 0);
                    side = Side.right;
                    break;
                case Side.right:
                    transform.position = new Vector3(-1, transform.position.y, transform.position.z+1);
                    //transform.Rotate(0, -90, 0);
                    side = Side.left;
                    break;
            }
            */
        }
           


    }

    bool CheckIsValidPosition()
    {
        foreach(Transform mino in transform)
        {
            Vector3 pos = FindObjectOfType<Game>().Round(mino.position);
            if(FindObjectOfType<Game>().checkIsInsideGrid(pos)==false)
            {
                return false;
            }

        }
        return true;
    }

    void Fall()
    {
        CheckForCubesBelow();
        transform.position += new Vector3(0, -1, 0);
      //  AudioSource.PlayClipAtPoint(BGSounds, transform.position);
        if (CheckIsValidPosition())
        {

        }
        else
        {
            transform.position += new Vector3(0, 1, 0);
        //    AudioSource.PlayClipAtPoint(BGSounds, transform.position);
            DisableObject();
        }
    }

    void CheckForCubesBelow()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Stationary");

        foreach(Transform child in transform)
        {
            foreach(GameObject cube in cubes)
            {
                if (child.transform.position.x ==  cube.transform.position.x &&
                    child.transform.position.z == cube.transform.position.z &&
                    child.transform.position.y -2 == cube.transform.position.y )
                {

                    DisableObject();
                    return;
                }
            }
        }
    }

    void DisableObject()
    {
        //Disable the fall
        CancelInvoke("Fall");

        //Label child cubes to stationary
        foreach (Transform child in transform)
        {
            child.transform.tag = "Stationary";
        }

        //Disable the block
        enabled = false;
        
        //Spawn
        FindObjectOfType<Game>().spawnObject();
    }
}
