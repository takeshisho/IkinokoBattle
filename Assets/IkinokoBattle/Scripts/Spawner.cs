using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private GameObject enemyPrefab;

    private void Start() {
        // Coroutine: Unityで非同期に処理を実行する仕組み「⚪︎秒待ってから何かする、⚪︎秒ごとに何かするなどができる」
        StartCoroutine(SpawnLoop());
    }

    // 敵出現のCoroutine
    private IEnumerator SpawnLoop()
    {
        while(true)
        {
            var distanceVector = new Vector3(10, 0);
            // y軸に対して、上記のベクトルを0~360度ランダムに回転している
            // Quaternion.Euler x, y, z軸それぞれの回転を引数で受け取る。
            var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;
            var spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer;

            // 指定座標から一番近いNavMeshを探す
            NavMeshHit navMeshHit;
            // https://docs.unity3d.com/ja/2020.3/ScriptReference/AI.NavMesh.SamplePosition.html
            if (NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10, NavMesh.AllAreas))
            {
                // navMeshHit.positionに上記で見つけた最も近いNavMeshの位置が入っている。
                Instantiate(enemyPrefab, navMeshHit.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(10);

            if (playerStatus.Life <= 0) break;
        }
    }
}
