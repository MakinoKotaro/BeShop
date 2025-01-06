using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �ǂ̃X�e�[�W���I�����ꂽ���𔻒肷��X�N���v�g
/// </summary>
public class SelectUIController : MonoBehaviour
{
    [SerializeField] private GameObject[] selectedFrameImages = new GameObject[3]; //�I�����ꂽ���Ƃ������t���[���̉摜���������

    private void Start()
    {
        HideFrames();
    }
    /// <summary>
    /// �I�����ꂽ���Ƃ�\�����鏈��
    /// </summary>
    public void HideFrames()
    {
        foreach (GameObject g in selectedFrameImages)
        {
            g.SetActive(false);
        }
    }

    /// <summary>
    /// �I�������̏���
    /// </summary>
    /// <param name="stageNum"></param>
    public void ShowFrame(int stageNum)
    {
        foreach (GameObject g in selectedFrameImages)
        {
            g.SetActive(false);
        }

        selectedFrameImages[stageNum].SetActive(true);
    }
}
