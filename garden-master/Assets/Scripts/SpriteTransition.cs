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

    public Material ringMaterial; // 使用的过渡材质
    public float transitionSpeed = 1f; // 切换速度
    public Texture correctTexture; // Correct 状态下的纹理
    public Texture wrongTexture;   // Wrong 状态下的纹理

    public BreathingScale breathingScale;
    public enum TransitionState
    {
        None,
        Correct,
        Wrong,
    }

    public TransitionState curState; // 当前状态
    private float blendValue = 0f; // 当前 blend 值
    private bool isTransitioning = false; // 是否正在过渡

    public Material bgMaterial; // 使用的过渡材质
    [Range(0,1)]
    public float dissolveAmount;

    public float correctThreshold = 2f; // 正确状态保持多长时间后开始加分
    public float wrongThreshold = 2f;   // 错误状态保持多长时间后开始减分
    public int score = 0; // 当前分数

    private float correctTime = 0f; // 正确状态计时器
    private float wrongTime = 0f;   // 错误状态计时器


    public int maxScore = 30;
    private float easeValue = 0f; // 最终的缓动值
    private float mappedValue = 0f; // 映射的值（0-1）

    private LevelState levelState;
    void Start()
    {
        levelState = LevelState.Ready;

        UpdateMaterialTexture(); // 初始更新材质的纹理

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

            // 控制过渡效果的进行
            HandleTransition();

            if (curState == TransitionState.Correct)
            {
                correctTime += Time.deltaTime; // 增加 Correct 状态计时
            }
            else if (curState == TransitionState.Wrong)
            {
                wrongTime += Time.deltaTime; // 增加 Wrong 状态计时
            }

            // 如果 Correct 状态保持超过阈值，开始加分
            if (curState == TransitionState.Correct && correctTime >= correctThreshold)
            {
                score += 1; // 加分
                correctTime = 0f; // 重置计时器
            }

            // 如果 Wrong 状态保持超过阈值，开始减分
            if (curState == TransitionState.Wrong && wrongTime >= wrongThreshold)
            {
                score -= 2; // 减分
                wrongTime = 0f; // 重置计时器
            }

            score = Mathf.Clamp(score, 0, maxScore);

            // 将整数值映射到0到1的范围
            mappedValue = Mathf.InverseLerp(0f, maxScore, score);

            // 使用 Mathf.SmoothStep 进行 Ease In/Out 缓动
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
    // 切换状态
    private void ToggleState()
    {
        isTransitioning = true; // 开始过渡
        blendValue = 0f; // 重置过渡值
        UpdateMaterialTexture(); // 切换材质的纹理
    }

    // 处理过渡效果
    private void HandleTransition()
    {
        if (isTransitioning)
        {
            // 根据当前状态，调整 blend 值
            blendValue = Mathf.Clamp01(blendValue + Time.deltaTime * transitionSpeed);
            if (blendValue >= 1f)
            {
                isTransitioning = false; // 完成过渡
            }

            // 设置 Shader 中的 Blend 值
            ringMaterial.SetFloat("_Blend", blendValue);
        }
    }

    // 更新材质的纹理
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