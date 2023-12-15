using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrowAxe : MonoBehaviour
{
    // 理想のしよう
    // アイテムを持った段階でplayerの動きを止める
    // 一定距離進んだら消える
    // 途中で敵に当たったら消える、かつダメージを与える
    // 途中で壁に当たったら消える
    private Rigidbody rb;
    bool isThrown = false;

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
}
