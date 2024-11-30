using UnityEngine;

public enum BreathingState
{
    Exhale, // ����
    Inhale, // ����
    Transition,
}

public class BreathingScale : MonoBehaviour
{
    
    public float minScale = 0.8f; // ��С����ֵ���������ʱ������
    public float maxScale = 1.2f; // �������ֵ����������ʱ������
    public float speed = 1f; // ����������ٶȣ����������ͺ����Ŀ���

    private float _scaleValue;

    private Vector3 initialScale;

    public BreathingState breathingState;

    public float threadHold = 0.1f;

    void Start()
    {
        // ��¼�����ʼ������ֵ
        initialScale = transform.localScale;
    }

    void Update()
    {
        // ʹ�� PingPong ������һ��ƽ�������ر仯Ч����ģ������Ľ���
        float curScaleValue = Mathf.PingPong(Time.time * speed, 1); // PingPong ����0��1֮�����ر仯

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


        // �� PingPong �Ľ��ӳ�䵽 minScale �� maxScale ֮��
        float newScale = Mathf.Lerp(minScale, maxScale, curScaleValue);

        // �������������
        transform.localScale = new Vector3(initialScale.x * newScale, initialScale.y * newScale, 1.0f);
    }
}
