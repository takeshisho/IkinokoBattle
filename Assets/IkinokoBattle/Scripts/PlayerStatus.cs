using System.Collections;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MobStatus
{

    private void Update() {
        StartCoroutine(test());
    }

    private IEnumerator test()
    {
        yield return new WaitForSeconds(60);
        this.Damage(10);
    }

    protected override void OnDie()
    {
        base.OnDie();
        StartCoroutine(GoToGameOverCoroutine());
    }

    private IEnumerator GoToGameOverCoroutine()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOverScene");
    }
}
