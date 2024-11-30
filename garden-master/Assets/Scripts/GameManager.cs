using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    private static GameManager _instance;

    public bool isLevel1Achieved;

    private void Awake()
    {
        if(_instance == null)
            _instance = this;
        DontDestroyOnLoad(this);
        isLevel1Achieved = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    LoadScene("Real");
        //}
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GetLevel1Down()
    {
        isLevel1Achieved = true;
    }
}
