using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// HP��1/3�̂Ƃ��ɕ\������UI
/// </summary>
public class WarningUIProcessing : MonoBehaviour
{
    private float minValue = 0.3f;
    private float maxValue = 1.0f;
    [SerializeField] private float speed = 1.0f;
    private float currentValue = 0.0f;

    [SerializeField] private PostProcessVolume volume;

    private Vignette vignette;

    private bool canShowWarningUI = false;

    public bool CanShowWarningUI { get => canShowWarningUI; set => canShowWarningUI = value; }

    private void Start()
    {
        //PostProcessing���̃r�l�b�g���擾
        volume.profile.TryGetSettings(out vignette);
        if(vignette == null)
        {
            Debug.Log("vignette is null");
        }
    }
    void Update()
    {
        //�r�l�b�g�̏���
        if (canShowWarningUI)
        {
            float pingPongValue = Mathf.PingPong(Time.time * speed, 1.0f); // Time.time���g���Ď��ԂƂƂ��ɑ���
            currentValue = Mathf.Lerp(minValue, maxValue, pingPongValue);

            vignette.smoothness.value = currentValue;
        }
    }
}
