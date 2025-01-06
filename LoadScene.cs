using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
/// <summary>
/// �V�[�����[�h�̏������܂Ƃ߂��X�N���v�g
/// </summary>
public class LoadScene : MonoBehaviour
{
    private static string sceneName; //�J�ڐ�̃V�[���� �Ȃ�static������K�v������H
    private Scene nowScene;
    private static string currentScene;
    private static string beforeScene;

    private static Vector3 player_enter_shop_position;

    MouseCursorController mouseCursorController;

    GameObject player;

    private float popPressAnyKeyDelay = 2.0f;

    [SerializeField] private GameObject sFXManagerObj;
    public string CurrentScene
    {
        get { return currentScene; }
    }

    void Start()
    {
        //���݂̃V�[����Shop��SpellShop�̏ꍇ�A���������̏ꏊ�ɖ߂��Ă���悤�ɂ���
        player = GameObject.FindWithTag("Player");
        if (beforeScene == "ShopScene" || beforeScene == "SpellShopScene")
        {
            player.transform.position = player_enter_shop_position;
        }
        nowScene = SceneManager.GetActiveScene();
        currentScene = nowScene.name;
    }


    void Update()
    {
        //Debug.Log(sceneName);
        if(currentScene == "ResultScene")
        {
            if(Keyboard.current.anyKey.wasPressedThisFrame == true)
            {
                GetBeforeSceneName();
                sceneName = "HomeScene";
                OnLoadScene();
            }
        }

        else if (currentScene == "TitleScene")
        {
            StartCoroutine(TitleToHomeDelay());
        }
    }

    /// <summary>
    /// �^�C�g���̃��[�h�ɏ����f�B���C�������鏈��
    /// </summary>
    /// <returns></returns>
    IEnumerator TitleToHomeDelay()
    {
        yield return new WaitForSeconds(popPressAnyKeyDelay);

        if (Keyboard.current.anyKey.wasPressedThisFrame == true)
        {
            SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
            sFXManager.SetGameStartSound();
            GetBeforeSceneName();
            sceneName = "HomeScene";

            Color color = Color.white;
            Initiate.Fade(sceneName, color, 1.0f);
            //OnLoadScene();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //�v���C���[��Shop�O��F���������ۂ̏���
        if(other.gameObject.tag == "Player" && gameObject.tag == "Shop")
        {
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                player_enter_shop_position = other.transform.position;
                //mouseCursorController = FindAnyObjectByType<MouseCursorController>();
                mouseCursorController = GameObject.FindWithTag("MainCamera").GetComponent<MouseCursorController>();
                mouseCursorController.CursorLock = false;

                GetBeforeSceneName();
                sceneName = "ShopScene";
                OnLoadScene();
            }
        }
        //�v���C���[��SpellShop�O��F���������ۂ̏���
        else if(other.gameObject.tag == "Player" && gameObject.tag == "SpellShop")
        {
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                player_enter_shop_position = other.transform.position;
                //mouseCursorController = FindAnyObjectByType<MouseCursorController>();
                mouseCursorController = GameObject.FindWithTag("MainCamera").GetComponent<MouseCursorController>();
                mouseCursorController.CursorLock = false;

                GetBeforeSceneName();
                sceneName = "SpellShopScene";
                OnLoadScene();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //�v���C���[���S�[�����ۂ̏���
        if(other.gameObject.tag == "Player" && gameObject.tag == "Goal")
        {
            GetBeforeSceneName() ;
            sceneName = "ResultScene";
            OnLoadScene();
        }
        //�v���C���[���X�e�[�W�I����ʂɍs�����Ƃ����ۂ̏���
        else if(other.gameObject.tag == "Player" && gameObject.tag == "StageSelect")
        {
            mouseCursorController = GameObject.FindWithTag("MainCamera").GetComponent<MouseCursorController>();
            mouseCursorController.CursorLock = false;

            GetBeforeSceneName();
            sceneName = "StageSelectScene";
            OnLoadScene();

        }
        //�v���C���[���X�e�[�W�P��I�������ۂ̏���
        else if (other.gameObject.tag == "Player" && gameObject.tag == "Stage")
        {
            GetBeforeSceneName();
            OnLoadScene();
        }
    }
    /// <summary>
    /// �߂�{�^�����������ۂ̏���
    /// </summary>
    public void OnClickBackButton()
    {
        GetBeforeSceneName();
        sceneName = "HomeScene";
        OnLoadScene();
    }

    /// <summary>
    /// �X�e�[�W�I���ŁA�X�e�[�W�P�������ꂽ�ۂ̏���
    /// </summary>
    public void OnClickStageButton()
    {
        SelectUIController selectUIController = GetComponent<SelectUIController>();
        selectUIController.ShowFrame(0);
        GetBeforeSceneName();
        sceneName = "Stage";
    }
    /// <summary>
    /// �X�e�[�W�I���ŁA�X�e�[�W�Q�������ꂽ�ۂ̏���
    /// </summary>
    public void OnClickStage2Button()
    {
        SelectUIController selectUIController = GetComponent<SelectUIController>();
        selectUIController.ShowFrame(1); //����1���ĉ��H�R�����g�c����
        GetBeforeSceneName();
        sceneName = "Stage2";
    }
    /// <summary>
    /// �X�e�[�W�I���ŁA�X�e�[�W�R�������ꂽ�ۂ̏���
    /// </summary>
    public void OnClickStage3Button()
    {
        SelectUIController selectUIController = GetComponent<SelectUIController>();
        selectUIController.ShowFrame(2); //���̂Q���ĉ��H�R�����g�c����
        GetBeforeSceneName();
        sceneName = "Stage3";
    }

    //�t�@�e�B���R�����g�F���͂��̂���̂ŁA

    /// <summary>
    /// �X�e�[�W�ɓ��鉉�o�V�[�����Đ����鏈��
    /// </summary>
    public void LoadWalkInToStageScene()
    {
        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        Invoke("LoadDelay",sFXManager.SfxLength);
    }

    /// <summary>
    /// ���[�h�܂Ńf�B���C�����鏈��
    /// </summary>
    void LoadDelay()
    {
        SceneManager.LoadScene("WalkToStageScene");
    }

    /// <summary>
    /// ��O�̃V�[�������擾���鏈��
    /// </summary>
    void GetBeforeSceneName()
    {
        beforeScene = currentScene;
    }
    /// <summary>
    /// �V�[�������[�h����R���[�`�����J�n����
    /// </summary>
    public void OnLoadScene()
    {
        StartCoroutine(Loading());
    }

    /// <summary>
    /// ���ʉ��̍Đ����I�������V�[�������[�h����
    /// </summary>
    /// <returns></returns>
    IEnumerator Loading()
    {
        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        yield return new WaitForSeconds(sFXManager.SfxLength);
        SceneManager.LoadScene(sceneName);
    }
}
