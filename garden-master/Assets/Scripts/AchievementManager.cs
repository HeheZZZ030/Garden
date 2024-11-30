using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public GameObject AchievementLevel1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isLevel1Achieved)
        {
            AchievementLevel1.SetActive(true);
        }
        else
        {
            AchievementLevel1.SetActive(false);
        }
    }
}
