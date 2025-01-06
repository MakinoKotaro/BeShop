using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// どのステージが選択されたかを判定するスクリプト
/// </summary>
public class SelectUIController : MonoBehaviour
{
    [SerializeField] private GameObject[] selectedFrameImages = new GameObject[3]; //選択されたことを示すフレームの画像を入れるもの

    private void Start()
    {
        HideFrames();
    }
    /// <summary>
    /// 選択されたことを表示する処理
    /// </summary>
    public void HideFrames()
    {
        foreach (GameObject g in selectedFrameImages)
        {
            g.SetActive(false);
        }
    }

    /// <summary>
    /// 選択解除の処理
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
