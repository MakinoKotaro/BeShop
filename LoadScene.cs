using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
/// <summary>
/// シーンロードの処理をまとめたスクリプト
/// </summary>
public class LoadScene : MonoBehaviour
{
    private static string sceneName; //遷移先のシーン名 なぜstaticをつける必要がある？
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
        //現在のシーンがShopかSpellShopの場合、入った時の場所に戻ってくるようにする
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
    /// タイトルのロードに少しディレイを加える処理
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
        //プレイヤーがShop前でFを押した際の処理
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
        //プレイヤーがSpellShop前でFを押した際の処理
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
        //プレイヤーがゴール下際の処理
        if(other.gameObject.tag == "Player" && gameObject.tag == "Goal")
        {
            GetBeforeSceneName() ;
            sceneName = "ResultScene";
            OnLoadScene();
        }
        //プレイヤーがステージ選択画面に行こうとした際の処理
        else if(other.gameObject.tag == "Player" && gameObject.tag == "StageSelect")
        {
            mouseCursorController = GameObject.FindWithTag("MainCamera").GetComponent<MouseCursorController>();
            mouseCursorController.CursorLock = false;

            GetBeforeSceneName();
            sceneName = "StageSelectScene";
            OnLoadScene();

        }
        //プレイヤーがステージ１を選択した際の処理
        else if (other.gameObject.tag == "Player" && gameObject.tag == "Stage")
        {
            GetBeforeSceneName();
            OnLoadScene();
        }
    }
    /// <summary>
    /// 戻るボタンを押した際の処理
    /// </summary>
    public void OnClickBackButton()
    {
        GetBeforeSceneName();
        sceneName = "HomeScene";
        OnLoadScene();
    }

    /// <summary>
    /// ステージ選択で、ステージ１が押された際の処理
    /// </summary>
    public void OnClickStageButton()
    {
        SelectUIController selectUIController = GetComponent<SelectUIController>();
        selectUIController.ShowFrame(0);
        GetBeforeSceneName();
        sceneName = "Stage";
    }
    /// <summary>
    /// ステージ選択で、ステージ２が押された際の処理
    /// </summary>
    public void OnClickStage2Button()
    {
        SelectUIController selectUIController = GetComponent<SelectUIController>();
        selectUIController.ShowFrame(1); //この1って何？コメント残して
        GetBeforeSceneName();
        sceneName = "Stage2";
    }
    /// <summary>
    /// ステージ選択で、ステージ３が押された際の処理
    /// </summary>
    public void OnClickStage3Button()
    {
        SelectUIController selectUIController = GetComponent<SelectUIController>();
        selectUIController.ShowFrame(2); //この２って何？コメント残して
        GetBeforeSceneName();
        sceneName = "Stage3";
    }

    //ファティンコメント：今はこのあるので、

    /// <summary>
    /// ステージに入る演出シーンを再生する処理
    /// </summary>
    public void LoadWalkInToStageScene()
    {
        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        Invoke("LoadDelay",sFXManager.SfxLength);
    }

    /// <summary>
    /// ロードまでディレイをつける処理
    /// </summary>
    void LoadDelay()
    {
        SceneManager.LoadScene("WalkToStageScene");
    }

    /// <summary>
    /// 一つ前のシーン名を取得する処理
    /// </summary>
    void GetBeforeSceneName()
    {
        beforeScene = currentScene;
    }
    /// <summary>
    /// シーンをロードするコルーチンを開始する
    /// </summary>
    public void OnLoadScene()
    {
        StartCoroutine(Loading());
    }

    /// <summary>
    /// 効果音の再生が終わったらシーンをロードする
    /// </summary>
    /// <returns></returns>
    IEnumerator Loading()
    {
        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        yield return new WaitForSeconds(sFXManager.SfxLength);
        SceneManager.LoadScene(sceneName);
    }
}
