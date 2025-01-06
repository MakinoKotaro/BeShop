using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using Cinemachine;
using System.Runtime.InteropServices;

/// <summary>
/// �v���C���[�̑�����Ǘ�����X�N���v�g
/// </summary>
public class PlayerController : MonoBehaviour  
{
    // ���̃X�N���v�g�Ŏg���ϐ��ꗗ

    private PlayerInput playerInput; // �v���C���[�̓��͂��Ǘ����邽�߂̕ϐ�
    private CharacterController charaCon;       // �L�����N�^�[�R���|�[�l���g�p�̕ϐ�
    private Animator animCon;  //  �A�j���[�V�������邽�߂̕ϐ�

    // �ړ����x�iSerializeField���C���X�y�N�^�Œ����\�j
    [SerializeField]
    float moveSpeed = 5.0f;

    // �v���C���[�̉�]���x
    [SerializeField]
    float rotateSpeed = 1200.0f;   
    
    bool ClearFlag = false; //�N���A�t���O

    //�U������
    bool canAttack = true; //�U���\
    bool isAttacking = false; //�U����

    bool use_cant_move_spell = false; //�ړ��s�̖��@���g�������ǂ���

    //�U���Ԋu, �o�ߎ���
    float attackIntervalTime = 1f, elapsedTime;

    //�n�ʂɂ��邩�ǂ�������p
    private bool groundedPlayer = false;


    // �L�����N�^�[����ŉ������
    [SerializeField]
    private float dodgeForce = 12;

    //����̃N�[���^�C��
    private bool canDodge = true;

    //�v���C���[�̐��ʕ������擾
    [SerializeField] Vector3 playerForward;

    //�d��
    private float gravityValue = -9.81f / 2;

    bool isFalling = false; //���������ǂ���

    float fallingTime = 0; //��������

    float fallTriggerTime = 0.5f; //�����A�j���[�V�������Đ�����܂ł̎���

    private Vector3 playerVelocity; //�v���C���[�̑��x

    int score = 0; //�X�R�A
    int nowScore = 0; //���݂̃X�R�A

    //�n�ʔ���
    [SerializeField]
    Transform groundCheck; //�n�ʔ���p�̃I�u�W�F�N�g

    [SerializeField]
    float groundCheckRadius; //�n�ʔ���̔��a

    [SerializeField]
    LayerMask[] groundLayers; //�n�ʂ̃��C���[


    //���L������΃N���A��
    [SerializeField]
    int clearScore = 0;

    //�L����
    int kills = 0;

    public UnityEvent onClearCallback = new UnityEvent(); //�N���A���̃R�[���o�b�N

    [SerializeField]
    private GameObject OnScreenControls; //�X�}�z�p�̃R���g���[��

    private PlayerAnimationScript playerAnimationScript; //�v���C���[�̃A�j���[�V�������Ǘ�����X�N���v�g���擾

    //MagicManager���擾
    [SerializeField] private GameObject magicManager;

    //====���@�𔭎˂���ꏊ====     //������މr���A�j���[�V������ǉ��\��̂��߁A�����邩��
    [SerializeField] private Transform castPoint01;
    //==========================


    //====���@�𒅒n������ꏊ====     //������މr���A�j���[�V������ǉ��\��̂��߁A���̕ϐ��������邩��
    [SerializeField] private Transform targetPoint01;

    public Transform TargetPoint01 { get => targetPoint01; set => targetPoint01 = value; }
    public bool CanDodge { get => canDodge; set => canDodge = value; }

    //==========================

    private float dodgeInterval = 0.5f; //����̃N�[���^�C��
    private float normalAttackInterval = 0.3f; //�ʏ�U���̃N�[���^�C��

 
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>(); //PlayerInput���擾
        charaCon = GetComponent<CharacterController>(); // �L�����N�^�[�R���g���[���[�̃R���|�[�l���g���Q�Ƃ���
        animCon = GetComponent<Animator>(); // �A�j���[�^�[�̃R���|�[�l���g���Q�Ƃ���

        playerAnimationScript = GetComponent<PlayerAnimationScript>();
        //status = GetComponent<CharacterStatusScript>();

    }

    /// <summary>
    /// �X�R�A�X�V
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private int ScoreUpdate(int s) 
    {

        score += s;

        return score;
    }

    void Start()
    {

        //���̎��_�ŃX�R�A�̏������I��点��K�v������

        //���̌�̏���������ɂ͍ŐV�̃X�R�A���K�v
        //�X�R�A�\���̃R�[�h

        //�g����������~����
        nowScore = ScoreUpdate(100);
      
       

#if UNITY_ANDROID

        if(OnScreenControls != null)
        {
            OnScreenControls.SetActive(true);
        }
#endif

#if UNITY_IOS

        if(OnScreenControls != null)
        {
            OnScreenControls.SetActive(true);
        }
#endif

#if UNITY_EDITOR

        if (OnScreenControls != null)
        {
            OnScreenControls.SetActive(true);
        }

#endif

#if UNITY_STANDALONE && !UNITY_EDITOR


        if (OnScreenControls != null)
        {
            OnScreenControls.SetActive(false);
        }

        // �}�E�X�J�[�\�����\���ɂ��A�ʒu���Œ�
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    
#endif
    }
    // �����t���[����Ɏ��s���鏈��
    void Update()
    {

        if (ClearFlag == true)
        {
            animCon.SetBool("Run", false);
            animCon.SetBool("Clear", true);

            charaCon.enabled = false;

            return;
        }

        //***�ړ�����***//
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        //Debug.Log("moveInput" + moveInput);

        //0 ���P�� -1�ɂ���
        moveInput = new Vector2(Mathf.Round(moveInput.x), Mathf.Round(moveInput.y));

        //Debug.Log("move x: " + moveInput.x);
        //Debug.Log("move x: " + moveInput.y);

        //�ړ����Ă��Ȃ��A����\�ȏ�ԂŁA�������łȂ��Ȃ�
        if (moveInput == Vector2.zero && canDodge == true && isFalling == false)
        {
            //Debug.Log("no input");
            //animCon.SetBool("Run", false);  //  Run���[�V�������Ȃ�

            playerAnimationScript.PlayIdleAnim();
        }
        //�ړ����Ă邩�A��𒆂��A�������Ȃ�
        else
        {
            var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;  //  �J�������Ǐ]���邽�߂̓���

            //Vector3 direction = cameraForward * Input.GetAxis("Vertical") + Camera.main.transform.right * Input.GetAxis("Horizontal");  //  �e���L�[��3D�X�e�B�b�N�̓��́iGetAxis�j�������direction�ɒl��Ԃ�
            Vector3 direction = cameraForward * moveInput.y + Camera.main.transform.right * moveInput.x;

            //animCon.SetBool("Run", true);  //  Run���[�V��������

            //�ړ��݂̂��Ă���ꍇ
            if (canDodge == true && isFalling == false)
            {
                playerAnimationScript.PlayWalkAnim(); //�����A�j���[�V�������Đ�
            }

            
            if (canDodge == true && use_cant_move_spell == false)
            {
                //������ς��铮��̏��������s����
                ChangeDirection(direction);

                //�ړ����铮��̏��������s����
                charaCon.Move(direction * Time.deltaTime * moveSpeed);   // �v���C���[�̈ړ������͎��ԁ~�ړ��X�s�[�h�̒l
            }
        }

        //�����ɒn�ʂ����邩�`�F�b�N����
        foreach (LayerMask groundLayer in groundLayers)
        {
            groundedPlayer = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        }

        //Debug.Log("Grounded " + groundedPlayer);
        
        //������A�N�V�����i�X�y�[�X�L�[�j��������Ă��āA����\�ȏ�Ԃ̂Ƃ��A
        if (playerInput.actions["Dodge"].triggered && canDodge == true)
        {
            StartCoroutine(Dodge()); //������R���[�`�����J�n
        }

        if (playerInput.actions["Cancel"].triggered)
        {

            Debug.Log("�L�����Z��");
        }

        //�v���C���[���n�ʂɂ��Ȃ��āA�܂��������肪�o�Ă��Ȃ��ꍇ�A��������ɂ���
        if (groundedPlayer == false && isFalling == false)
        {
            isFalling = true;
        }

        //�v���C���[���n�ʂɂ��āA�������肪�o�Ă���ꍇ�A���������
        if (groundedPlayer == true && isFalling == true)
        {
            isFalling = false;
        }

        //�������Ԃ��Đ��̂ɕK�v�Ȏ��Ԃ𒴂����ꍇ
        if (fallingTime > fallTriggerTime)
        {
            Fall(); //�����A�j���[�V�������Đ�
        }

        //Debug.Log(isFalling);
        //Debug.Log(groundedPlayer);

        //�d��
        playerVelocity.y += gravityValue * Time.deltaTime;

        //  �ړ����铮��̏��������s����
        charaCon.Move(playerVelocity * Time.deltaTime);   // �v���C���[�̈ړ������͎��ԁ~�ړ��X�s�[�h�̒l


        // �������A�N�V��������������
        //  �{�^������������A�N�V��������
        if (playerInput.actions["Attack"].triggered && canAttack && !isAttacking)
        {

            StartCoroutine(Attack()); //�U���̏����J�n

            canAttack = false;

        }

        //�E�N���b�N�����ꍇ
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            StartCoroutine(Attack02()); //�E�N���b�N�p�̍U�����J�n
        }

        if (!canAttack)
        {

            elapsedTime += Time.deltaTime;

            if (elapsedTime > attackIntervalTime)
            {

                elapsedTime = 0;
                canAttack = true;

            }
        }


        playerForward = Camera.main.transform.forward;
        playerForward.y = 0;
        //Debug.Log(playerForward.ToString());
        Quaternion playerRotation = Quaternion.LookRotation(playerForward);
        gameObject.transform.rotation = playerRotation;
    }

    //�I�����ꂽ��n�ʃ`�F�b�N�p�̃I�u�W�F�N�g��`��
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

/// <summary>
/// �L�������J�E���g����
/// </summary>
    public void CountKills()
    {
        kills++;

        if (kills >= clearScore)
        {
            onClearCallback.Invoke();

        }
    }

    /// <summary>
    /// ���N���b�N�̍U���R���[�`��
    /// </summary>
    /// <returns></returns>
    public IEnumerator Attack()
    {

        playerAnimationScript.PlayNormalAttackAnim(); //���N���b�N�̍U���A�j���[�V�������Đ�
        isAttacking = true; //�U�����ɂ���

        yield return new WaitForSeconds(0.15f); //0.15�b�҂���

        NormalAttack normalAttack = magicManager.GetComponent<NormalAttack>(); //MagicManager����NormalAttack���擾���A

        //castPoint01�̈ʒu����targetPoint01�̈ʒu�ɖ��@����
        normalAttack.Cast(castPoint01); 
        normalAttack.Behaviour(targetPoint01.position); 

        yield return new WaitForSeconds(normalAttackInterval); //0.30�b�҂���

        isAttacking = false; //�U���\�ɂ���

    }

    /// <summary>
    /// �E�N���b�N�̍U���R���[�`��
    /// </summary>
    /// <returns></returns>
    public IEnumerator Attack02()
    {
        if (use_cant_move_spell == false)
        {
            playerAnimationScript.PlayAreaMagicAnim(); //�E�N���b�N�̍U���A�j���[�V�������Đ�
            use_cant_move_spell = true; //�ړ��s�̍U��������
            isAttacking = true; //�U���s�ɂ���
            yield return new WaitForSeconds(0.8f); //0.8�b�҂��� >> �������ǁA�ϐ�������������������
            HealMagic healMagic = magicManager.GetComponent<HealMagic>();

            healMagic.Cast(gameObject.transform);
            yield return new WaitForSeconds(1.0f); //1.0�b�҂��ā@>> �������ǁA�ϐ�������������������

            healMagic.DestroySpell();
        }
        use_cant_move_spell = false; //�ړ��s�̍U�������Ă��Ȃ�����ɂ���
        isAttacking = false; //�U���\�ɂ���
    }

    //IsAttacking��T��
    public bool GetIsAttacking()
    {
        return isAttacking;
    }
    //ISAttacking���O���瑀��ł���悤�ɂ���
    public void SetIsAttacking(bool b)
    {

        isAttacking = b;
    }

    /// <summary>
    /// ����R���[�`���J�n
    /// </summary>
    /// <returns></returns>
    IEnumerator Dodge()
    {
        canDodge = false; //���s�ɂ���

        //�v���C���[�������Ă�������ɉ�����鏈��
        playerForward.y = 0;
        playerForward.Normalize();
        playerVelocity = playerForward * dodgeForce;
        playerAnimationScript.PlayDodgeAnim();

        yield return new WaitForSeconds(dodgeInterval); //0.5�b�҂���

        playerVelocity = playerForward / dodgeForce; //����̈ړ�����߂�
        canDodge = true; //����\�ɂ���
    }

    /// <summary>
    /// �����A�j���[�V�����̏���
    /// </summary>
    public void Fall()
    {
        playerAnimationScript.PlayFallAnim(); //�����A�j���[�V�����Đ�
        //isFalling = true;
    }

    public void ClearFlags(bool c)
    {
        ClearFlag = c;
    }

    // ��������ς��铮��̏���
    void ChangeDirection(Vector3 direction)
    {
        Quaternion q = Quaternion.LookRotation(direction);          // �����������p��Quaternion�^�ɒ���
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime);   // ������ q �Ɍ����Ă���`���ƕω�������.
    }

    //�b���I������Ƃ�
    public void OnFinishedTalking()
    {
        playerInput.SwitchCurrentActionMap("Player");
    }

    private void FixedUpdate()
    {
        //�������ɗ������Ԃ��v�Z����
        if (isFalling == true)
        {
            fallingTime += Time.deltaTime;
        }
        else
        {
            fallingTime = 0;
        }
    }

    //�t�@�e�B���R�����g�F�K�v�Ȃ������͏����܂��傤�B�Q�l�Ɏc�������R�[�h������ꍇ�͈Ⴄ�t�@�C���Ɉړ����Ă��������B
    //��A���̂܂܂��ƃ����R���ɑΉ����Ă��Ȃ��Q�[���ɂȂ�̂ŁANewInputSystem�̃����R���ɑΉ����邽�߂̃R�[�h�ɕύX���Ă��������B
}
