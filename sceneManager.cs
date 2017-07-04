using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour {

    static sceneManager instance;
    void Start()
    {
        if(instance!=null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(gameObject);
            instance = this;

        }
    }
    void Update()
    {

    }
}
