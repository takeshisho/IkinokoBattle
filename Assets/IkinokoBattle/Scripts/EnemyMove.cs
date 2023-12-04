using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]
public class EnemyMove : MonoBehaviour
{
    // [SerializeField] private PlayerController _playerController;
    private NavMeshAgent _agent;
    // 見えないRayを放ち、Rayが衝突したObjectを取得する処理
    private RaycastHit[] _raycastHits = new RaycastHit[10];
    private EnemyStatus _status;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _status = GetComponent<EnemyStatus>();
    }

    // void Update()
    // {
    //     // player目指して進む
    //     _agent.destination = _playerController.transform.position;        
    // }

    public void OnDetectObject(Collider collider)
    {
        if(!_status.IsMovable)
        {
            _agent.isStopped = true;
            return;
        }
        // 検知したobjectにPlayerタグがついていれば
        if (collider.CompareTag("Player")) {
            _agent.destination = collider.transform.position;

        // collider.transform.position(player) - transform.position(enemy)
        var positionDiff = collider.transform.position - transform.position;
        var distance = positionDiff.magnitude;
        var direction = positionDiff.normalized; // playerの方向

        // raycastHitsにヒットしたColliderや座標情報などが格納される。
        // RaycastAllと同じ機能を持つNonAllocだがメモリにゴミを残さない。
        var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance);

        Debug.Log("hitCount: " + hitCount);

        // このゲームのPlayerは、CharacterControllerを使用しており、Colliderではないので、countに含まれない
        // よって０の時は、playerとenemyとの間に障害物がないことを意味する。よって追尾する
        if (hitCount == 0)
        {
            _agent.isStopped = false;
            _agent.destination = collider.transform.position;
        }
        else _agent.isStopped = true;
        }
    }
}
