using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadREAL", 28.5f);
    }

    // Update is called once per frame
    void LoadREAL()
    {
        SceneManager.LoadScene("REAL");
    }
}
