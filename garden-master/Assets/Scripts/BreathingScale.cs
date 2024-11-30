using UnityEngine;

public enum BreathingState
{
    Exhale, // 呼气
    Inhale, // 吸气
    Transition,
}

public class BreathingScale : MonoBehaviour
{
    
    public float minScale = 0.8f; // 最小缩放值，代表呼气时的缩放
    public float maxScale = 1.2f; // 最大缩放值，代表吸气时的缩放
    public float speed = 1f; // 呼吸节奏的速度，控制吸气和呼气的快慢

    private float _scaleValue;

    private Vector3 initialScale;

    public BreathingState breathingState;

    public float threadHold = 0.1f;

    void Start()
    {
        // 记录物体初始的缩放值
        initialScale = transform.localScale;
    }

    void Update()
    {
        // 使用 PingPong 来创建一个平滑的来回变化效果，模拟呼吸的节奏
        float curScaleValue = Mathf.PingPong(Time.time * speed, 1); // PingPong 会在0到1之间来回变化

        if(_scaleValue > curScaleValue && curScaleValue > threadHold)
        {
            breathingState = BreathingState.Inhale;
        }
        else if (_scaleValue < curScaleValue && curScaleValue < 1.0f - threadHold)
        {
            breathingState = BreathingState.Exhale;
        }
        else
        {
            breathingState = BreathingState.Transition;
        }

        _scaleValue = curScaleValue;


        // 将 PingPong 的结果映射到 minScale 到 maxScale 之间
        float newScale = Mathf.Lerp(minScale, maxScale, curScaleValue);

        // 设置物体的缩放
        transform.localScale = new Vector3(initialScale.x * newScale, initialScale.y * newScale, 1.0f);
    }
}
