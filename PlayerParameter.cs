using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// �v���C���[�̃p�����[�^���Ǘ�����X�N���v�g
/// </summary>
public class PlayerParameter : MonoBehaviour
{
    //======�X�e�[�^�X�ϐ�======
    private static float playerHitPoint = 10; // �v���C���[��HP
    private static float playerPower = 5; // �v���C���[�̍U����
    private static float playerSpeed = 3; // �v���C���[�̈ړ����x
    private float maxHealth; // �v���C���[�̍ő�HP
    private bool takePoison = false; // �ł��󂯂����ǂ���
    //==========================

    private float warningHitPoint; // HP�����̒l�������ƌx��UI��\������


    //======�X�e�[�^�X �v���p�e�B======
    public float PlayerHitPoint { get => playerHitPoint; set => playerHitPoint = value; }
    public float PlayerPower { get => playerPower; set => playerPower = value; }
    public float PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public bool TakePoison { get => takePoison; set => takePoison = value; }
    public bool NowTakingPoison { get => nowTakingPoison; set => nowTakingPoison = value; }

    //=======================================

    private bool nowTakingPoison = false; //���ł��󂯂Ă��邩�ǂ���

    [SerializeField] private GameObject hpBarManagerObj;  // HP�o�[�̃I�u�W�F�N�g
    GameObject warningUIProcessingObj; // �x��UI�̃I�u�W�F�N�g
    WarningUIProcessing warningUIProcessing; // �x��UI�̃X�N���v�g
    private bool isShowingWarningUI = false; // �x��UI��\�����Ă��邩�ǂ���

    PlayerController playerController; // �v���C���[�̃R���g���[���[
    private void Start()
    {
        maxHealth = playerHitPoint;
        warningHitPoint = maxHealth / 3; //�}�W�b�N�i���o�[�����I�ϐ������Ă��������B
    }

    private void Update()
    {
        //����̓f�o�b�O�p���ȁH�Ȃ��Unity_EDITOR���g���ׂ��ł��B

        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            StopCoroutine(TakingPoison());
        }

        
        if (playerHitPoint <= warningHitPoint && isShowingWarningUI == false)
        {
            warningUIProcessing = GetComponent<WarningUIProcessing>();
            warningUIProcessing.canShowWarningUI = true;

            isShowingWarningUI = true;
        }
        else
        {
            isShowingWarningUI = false;

            warningUIProcessing = GetComponent<WarningUIProcessing>();
            warningUIProcessing.canShowWarningUI = false;
        }
    }

    /// <summary>
    /// �v���C���[���_���[�W���󂯂鏈��
    /// </summary>
    /// <param name="takeDamage"></param>
    public void PlayerTakeDamage(float takeDamage)
    {
        playerHitPoint -= takeDamage;
        Debug.Log($"���݂�HP�F{playerHitPoint / maxHealth}");
        float hp = playerHitPoint / maxHealth;

        HpBarManager hpBarManager = hpBarManagerObj.GetComponent<HpBarManager>();

        if (playerHitPoint <= 0)
        {
            PlayerAnimationScript playerAnimationScript = GetComponent<PlayerAnimationScript>();
            playerAnimationScript.PlayDeadAnim();
        }


        if (hpBarManager != null)
        {
            hpBarManager.ShowCurrentHp(hp);
        }
        else
        {
            Debug.LogError("HpBarManager��������܂���BhpBarManagerObj�ɐ������I�u�W�F�N�g��ݒ肵�Ă��������B");
        }
        //hpBarManager.ShowCurrentHp( hp );
    }

    /// <summary>
    /// �v���C���[���񕜂��鏈��
    /// </summary>
    /// <param name="healAmount"></param>
    public void PlayerHeal(float healAmount)
    {
        playerHitPoint += healAmount;  // �񕜗ʂ�ǉ�

        // �ő�HP�𒴂��Ȃ��悤�ɂ���
        if (playerHitPoint > maxHealth)
        {
            playerHitPoint = maxHealth;
        }

        Debug.Log($"���݂�HP�F{playerHitPoint / maxHealth}");

        // HP�o�[�̍X�V
        float hp = playerHitPoint / maxHealth;
        HpBarManager hpBarManager = hpBarManagerObj.GetComponent<HpBarManager>();

        if (hpBarManager != null)
        {
            hpBarManager.ShowCurrentHp(hp);
        }
        else
        {
            Debug.LogError("HpBarManager��������܂���BhpBarManagerObj�ɐ������I�u�W�F�N�g��ݒ肵�Ă��������B");
        }
    }

    /// <summary>
    /// �v���C���[���ł��󂯂����̏���
    /// </summary>
    public void PlayerTakePoison()
    {
        if (nowTakingPoison == false)
        {
            StartCoroutine(TakingPoison());
            nowTakingPoison = true;
        }
    }

    /// <summary>
    /// �Ń_���[�W�̃R���[�`��
    /// </summary>
    /// <returns></returns>
    IEnumerator TakingPoison()
    {
        
        for (int i = 0; i < 10; i++) //�}�W�b�N�i���o�[�����I�ϐ������Ă��������B
        {
            PlayerTakeDamage(0.2f);�@//�}�W�b�N�i���o�[�����I�ϐ������Ă��������B

            yield return new WaitForSeconds(1);  // 1�b���Ƃɑҋ@���ă��[�v
        }

        takePoison = false;
        nowTakingPoison = false;
    }

    

    /// <summary>
    /// �w�������A�C�e����+�v�Z�̂��̂������Ƃ��̏���
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <param name="itemCount"></param>
    public void AddParameter(string type, float amount, int itemCount)
    {
        switch (type)
        {
            case "damage":
                playerPower += amount * itemCount;
                Debug.Log("now playerPower�F" + playerPower);
                break;
            case "hp":
                playerHitPoint += amount * itemCount;
                Debug.Log("now HitPoint�F" + playerHitPoint);
                break;
            case "speed":
                playerSpeed += amount * itemCount;
                Debug.Log("now playerSpeed�F" + playerSpeed);
                break;
        }
    }

    /// <summary>
    /// �w�������A�C�e����-�v�Z�̂��̂������Ƃ��̏���
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <param name="itemCount"></param>
    public void MulParameter(string type, float amount, int itemCount)
    {
        switch (type)
        {
            case "damage":
                playerPower *= amount * itemCount;
                Debug.Log("now playerPower�F" + playerPower);
                break;
            case "hp":
                playerHitPoint *= amount * itemCount;
                Debug.Log("now HitPoint�F" + playerHitPoint);
                break;
            case "speed":
                playerSpeed *= amount * itemCount;
                Debug.Log("now playerSpeed�F" + playerSpeed);
                break;
        }
    }

    /// <summary>
    /// �w�������A�C�e���ɓ���Ȍ��ʂ�����ꍇ�̏���
    /// </summary>
    /// <param name="itemName"></param>
    public void UnlockSpecialMove(string itemName)
    { 
        //�����ǉ��\��@�i����Ȍ��ʂ𔭓�������j
    }
}
