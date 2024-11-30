using System.Collections;
using UnityEngine;

public class SpriteTransition : MonoBehaviour
{
    private enum LevelState
    {
        Ready,
        Playing,
        Success,
        End,
    }

    public Material ringMaterial; // ʹ�õĹ��ɲ���
    public float transitionSpeed = 1f; // �л��ٶ�
    public Texture correctTexture; // Correct ״̬�µ�����
    public Texture wrongTexture;   // Wrong ״̬�µ�����

    public BreathingScale breathingScale;
    public enum TransitionState
    {
        None,
        Correct,
        Wrong,
    }

    public TransitionState curState; // ��ǰ״̬
    private float blendValue = 0f; // ��ǰ blend ֵ
    private bool isTransitioning = false; // �Ƿ����ڹ���

    public Material bgMaterial; // ʹ�õĹ��ɲ���
    [Range(0,1)]
    public float dissolveAmount;

    public float correctThreshold = 2f; // ��ȷ״̬���ֶ೤ʱ���ʼ�ӷ�
    public float wrongThreshold = 2f;   // ����״̬���ֶ೤ʱ���ʼ����
    public int score = 0; // ��ǰ����

    private float correctTime = 0f; // ��ȷ״̬��ʱ��
    private float wrongTime = 0f;   // ����״̬��ʱ��


    public int maxScore = 30;
    private float easeValue = 0f; // ���յĻ���ֵ
    private float mappedValue = 0f; // ӳ���ֵ��0-1��

    private LevelState levelState;
    void Start()
    {
        levelState = LevelState.Ready;

        UpdateMaterialTexture(); // ��ʼ���²��ʵ�����

        StartCoroutine(WaitForBegin());
    }

    void Update()
    {
        if(levelState == LevelState.Ready)
        {

        }

        if(levelState == LevelState.Playing)
        {
            // cal next state
            TransitionState nextState;
            if (breathingScale.breathingState == BreathingState.Inhale)
            {
                if (!Input.GetKey(KeyCode.Space))
                {
                    nextState = TransitionState.Correct;
                }
                else
                {
                    nextState = TransitionState.Wrong;
                }
            }
            else if (breathingScale.breathingState == BreathingState.Exhale)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    nextState = TransitionState.Correct;
                }
                else
                {
                    nextState = TransitionState.Wrong;
                }
            }
            else
            {
                nextState = curState;
            }

            // state transfer timmer reset
            if (nextState == TransitionState.Correct && curState == TransitionState.Wrong)
            {
                wrongTime = 0f;
            }
            if (nextState == TransitionState.Wrong && curState == TransitionState.Correct)
            {
                correctTime = 0f;
            }

            // ring state 
            if (nextState != curState)
            {
                curState = nextState;
                ToggleState();
            }

            // ���ƹ���Ч���Ľ���
            HandleTransition();

            if (curState == TransitionState.Correct)
            {
                correctTime += Time.deltaTime; // ���� Correct ״̬��ʱ
            }
            else if (curState == TransitionState.Wrong)
            {
                wrongTime += Time.deltaTime; // ���� Wrong ״̬��ʱ
            }

            // ��� Correct ״̬���ֳ�����ֵ����ʼ�ӷ�
            if (curState == TransitionState.Correct && correctTime >= correctThreshold)
            {
                score += 1; // �ӷ�
                correctTime = 0f; // ���ü�ʱ��
            }

            // ��� Wrong ״̬���ֳ�����ֵ����ʼ����
            if (curState == TransitionState.Wrong && wrongTime >= wrongThreshold)
            {
                score -= 2; // ����
                wrongTime = 0f; // ���ü�ʱ��
            }

            score = Mathf.Clamp(score, 0, maxScore);

            // ������ֵӳ�䵽0��1�ķ�Χ
            mappedValue = Mathf.InverseLerp(0f, maxScore, score);

            // ʹ�� Mathf.SmoothStep ���� Ease In/Out ����
            easeValue = Mathf.SmoothStep(0f, 1f, mappedValue);

            dissolveAmount = easeValue;

            bgMaterial.SetFloat("_DissolveAmount", dissolveAmount);

            if(dissolveAmount >= 0.95)
            {
                levelState = LevelState.Success;
                StartCoroutine(WaitForEnd());
            }
        }
        
        if(levelState == LevelState.Success)
        {
            GameManager.Instance.GetLevel1Down();
            bgMaterial.SetFloat("_DissolveAmount", 1);
        }

        if (levelState == LevelState.End)
        {

        }
    }
    IEnumerator WaitForBegin()
    {
        yield return new WaitForSeconds(5.0f);
        levelState = LevelState.Playing;
    }
    IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(3.0f);
        levelState = LevelState.End;
        GameManager.Instance.LoadScene("REAL");
    }
    // �л�״̬
    private void ToggleState()
    {
        isTransitioning = true; // ��ʼ����
        blendValue = 0f; // ���ù���ֵ
        UpdateMaterialTexture(); // �л����ʵ�����
    }

    // �������Ч��
    private void HandleTransition()
    {
        if (isTransitioning)
        {
            // ���ݵ�ǰ״̬������ blend ֵ
            blendValue = Mathf.Clamp01(blendValue + Time.deltaTime * transitionSpeed);
            if (blendValue >= 1f)
            {
                isTransitioning = false; // ��ɹ���
            }

            // ���� Shader �е� Blend ֵ
            ringMaterial.SetFloat("_Blend", blendValue);
        }
    }

    // ���²��ʵ�����
    private void UpdateMaterialTexture()
    {
        if (curState == TransitionState.Correct)
        {
            ringMaterial.SetTexture("_MainTex", correctTexture);
            ringMaterial.SetTexture("_SecondTex", wrongTexture);
        }
        else
        {
            ringMaterial.SetTexture("_MainTex", wrongTexture);
            ringMaterial.SetTexture("_SecondTex", correctTexture);
        }
    }

    private void OnDestroy()
    {
        bgMaterial.SetFloat("_DissolveAmount", 0);
    }
}