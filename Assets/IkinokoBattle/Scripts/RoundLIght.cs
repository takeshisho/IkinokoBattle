using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundLIght : MonoBehaviour
{

    void Update()
    {   
        // deltataime: 前フレームからの経過時間（s）を取得
        // よって１秒間に12度移動
        transform.Rotate(new Vector3(0, -2) * Time.deltaTime);
    }
}
