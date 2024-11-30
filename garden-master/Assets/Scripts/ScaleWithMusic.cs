using UnityEngine;

public class ScaleWithMusic : MonoBehaviour
{
    public AudioSource audioSource; // 音乐的AudioSource
    public Transform targetObject; // 要变化的物体
    public int spectrumIndex = 0;  // 频谱数据的索引，选择影响效果的频段
    public float scaleMultiplier = 10f; // 放大倍数
    public float smoothFactor = 0.5f; // 平滑因子
    private float[] spectrumData = new float[64]; // 保存频谱数据的数组

    private Vector3 initialScale; // 记录初始缩放

    void Start()
    {
        if (targetObject == null)
        {
            targetObject = this.transform; // 如果没设置目标物体，默认是自身
        }
        initialScale = targetObject.localScale;
    }

    void Update()
    {
        // 获取频谱数据
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Blackman);

        // 从频谱数据中获取指定索引的值
        float intensity = spectrumData[spectrumIndex] * scaleMultiplier;

        // 平滑过渡到新的缩放值
        float scaleValue = Mathf.Lerp(targetObject.localScale.x, initialScale.x + intensity, smoothFactor);

        // 设置缩放
        targetObject.localScale = new Vector3(scaleValue, scaleValue, 1.0f);
    }
}
