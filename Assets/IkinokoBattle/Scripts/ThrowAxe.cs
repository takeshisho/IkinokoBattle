using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ThrowAxe : MonoBehaviour
{
    // 理想の仕様　未完成部分
    // アイテムを持った段階でplayerの動きを止める
    // 一定距離進んだら消える

    private const int THROW_AXE_DAMAGE = 3;
    private Rigidbody rb;
    bool isThrown = false;
    private EnemyStatus HittedEnemyStatus;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!isThrown) Throw();
    }

    private void Throw()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isThrown = true;

            // キャラの向いている方向に投げる
            var playerOrientation = GameObject.Find("Query-Chan-SD").transform.rotation;
            rb.AddForce(playerOrientation * Vector3.forward * 500);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 途中で敵に当たったらダメージを与えて消える
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            HittedEnemyStatus = collision.gameObject.GetComponent<EnemyStatus>();
            HittedEnemyStatus.Damage(THROW_AXE_DAMAGE);

            Destroy(gameObject);
        }

        // 途中で壁に当たったら消える
        if (collision.gameObject.name == "Cube")
        {
            Destroy(gameObject);
        }
    }
}
