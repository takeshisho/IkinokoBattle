using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MobStatus))]
public class MobAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Collider attackCollider;

    private MobStatus _status;

    private void Start() {
        _status = GetComponent<MobStatus>();
    }

    public void AttackIfPossible()
    {
        if (!_status.IsAttackable) return;
        _status.GoToAttackStateIfPossible();
    }

    public void OnAttackRangeEnter(Collider collider)
    {
        AttackIfPossible();
    }
    // 攻撃の最初に呼ばれる
    public void OnAttackStart()
    {
        attackCollider.enabled = true;
    }

    // 攻撃対象にヒットした時に呼ばれる。
    public void OnHitAttack(Collider collider)
    {
        // いまいちピンとこない
        var targetMob = collider.GetComponent<MobStatus>();
        if (null == targetMob) return;

        targetMob.Damage(1);
    }

    public void OnAttackFinished()
    {
        attackCollider.enabled = false;
        StartCoroutine(CooldownCoroutime());
    }

    private IEnumerator CooldownCoroutime()
    {
        yield return new WaitForSeconds(attackCooldown);
        _status.GoToNormalStateIfPossible(); 
    }
}
