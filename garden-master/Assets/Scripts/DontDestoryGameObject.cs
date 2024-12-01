using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestoryGameObject : MonoBehaviour
{
    public static DontDestoryGameObject Instance { get { return _instance; } }
    private static DontDestoryGameObject _instance;

    public AnimationCurve showCurve;
    public AnimationCurve hideCurve;
    public float animationSpeed;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    StartCoroutine(ShowPanel());
        //}
        //if(Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    StartCoroutine(HidePanel());
        //}
    }
    public IEnumerator ShowPanel(string sceneName)
    {
        float timer = 0;
        while (timer <= 1)
        {
            transform.localScale = Vector3.one * showCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
        yield return new WaitUntil(() => timer > 1);
        GameManager.Instance.LoadScene(sceneName);
    }

    public IEnumerator HidePanel()
    {
        float timer = 0;
        while (timer <= 1)
        {
            transform.localScale = Vector3.one * hideCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
        transform.localScale = new Vector3(0, 0, 0);
    }
}
