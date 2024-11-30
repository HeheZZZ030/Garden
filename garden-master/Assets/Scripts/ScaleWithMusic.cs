using UnityEngine;

public class ScaleWithMusic : MonoBehaviour
{
    public AudioSource audioSource; // ���ֵ�AudioSource
    public Transform targetObject; // Ҫ�仯������
    public int spectrumIndex = 0;  // Ƶ�����ݵ�������ѡ��Ӱ��Ч����Ƶ��
    public float scaleMultiplier = 10f; // �Ŵ���
    public float smoothFactor = 0.5f; // ƽ������
    private float[] spectrumData = new float[64]; // ����Ƶ�����ݵ�����

    private Vector3 initialScale; // ��¼��ʼ����

    void Start()
    {
        if (targetObject == null)
        {
            targetObject = this.transform; // ���û����Ŀ�����壬Ĭ��������
        }
        initialScale = targetObject.localScale;
    }

    void Update()
    {
        // ��ȡƵ������
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Blackman);

        // ��Ƶ�������л�ȡָ��������ֵ
        float intensity = spectrumData[spectrumIndex] * scaleMultiplier;

        // ƽ�����ɵ��µ�����ֵ
        float scaleValue = Mathf.Lerp(targetObject.localScale.x, initialScale.x + intensity, smoothFactor);

        // ��������
        targetObject.localScale = new Vector3(scaleValue, scaleValue, 1.0f);
    }
}
