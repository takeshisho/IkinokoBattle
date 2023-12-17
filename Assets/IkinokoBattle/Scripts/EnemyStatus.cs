using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStatus : MobStatus
{
    private NavMeshAgent _agent;
    // スーパークラスのメソッドを上書きすることで、同じ名前のメソッドでも異なる動きをさせることが出来る
    protected override void Start()
    {
        base.Start(); // 子クラスから親クラスのメソッドを呼ぶために、baseをつける
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // NavMeshAgentの速度でanimatorセット
        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
    }

    protected override void OnDie()
    {
        base.OnDie();
        // ScoreクラスのInstanceプロパティを使って、Scoreクラスの唯一の(シングルトンだから)インスタンスを取得する
        Score.Instance.AddScore();
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        // このスクリプトがついているgameobject自身
        Destroy(gameObject);
    }
}
