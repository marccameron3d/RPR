using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public static GameObject ChunkManager;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        InitGame();
    }

    void InitGame()
    {
        ChunkManager = GameObject.FindGameObjectWithTag("ChunkManager");
    }

    //Update is called every frame.
    void Update()
    {

    }
}