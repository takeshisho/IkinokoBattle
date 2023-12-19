using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// このGameObjectにCharactorControllerがなければ必ずつける。
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStatus))]
[RequireComponent(typeof(MobAttack))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float jumpPower = 3;
    [SerializeField] private Animator animator;
    
    // CharacterControllerのキャッシュらしい
    private CharacterController _characterController;
    // 以下も全てキャッシュ（変数に元々の値を代入しておいてる）
    private Transform _transform;
    private Vector3 _moveVelocity;
    private PlayerStatus _status;
    private MobAttack _attack;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = transform;
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
    }

    void Update()
    {
 
        // デフォルトだとマウスの左クリック
        if (Input.GetButtonDown("Fire1"))
        {
            // MobAttack.csにあるMobAttackクラスのメソッドAttackIfPossibleをコール
            _attack.AttackIfPossible();
        }

        if(_status.IsMovable)
        {
            // wasdで移動ができるようにする。
            _moveVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
            _moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed;
            // 移動方向に向く
            _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));
        }
        else
        {
            _moveVelocity.x = 0;
            _moveVelocity.z = 0;
        }

        if(_characterController.isGrounded) {
            if(Input.GetButtonDown("Jump")) {
                Debug.Log("Jump");
                _moveVelocity.y = jumpPower;
            }
        }
        else {
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }
        
        // 速度＊時間で動かす距離になる
        _characterController.Move(_moveVelocity * Time.deltaTime);
        // .magnitude Vectorの大きさを返す。
        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);
    }
}
