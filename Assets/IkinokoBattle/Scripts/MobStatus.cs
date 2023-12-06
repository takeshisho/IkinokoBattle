using UnityEngine;

// Mob(動くオブジェクト)の状態管理

public class MobStatus : MonoBehaviour{  
    protected enum StateEnum
    {
        Normal,
        Attack,
        Die
    }

    public bool IsMovable => StateEnum.Normal == _state;
    public bool IsAttackable => StateEnum.Normal == _state;
    public float LifeMax => lifeMax;
    public float Life => _life;

    [SerializeField] private float lifeMax = 10;
    protected Animator _animator;
    protected StateEnum _state = StateEnum.Normal;
    private float _life;

    // protectedは自分のクラスと派生クラスでのみ参照できる
    // virtual仮想関数　つけなくてもおそらくいいが、派生クラスにおいても仮にあるものとして考えることができる。
    protected virtual void Start()
    {
        _life = lifeMax;
        // mobstatusの子クラスであるenemystatusと同じところについてるanimatorなんでinchildren?
        _animator = GetComponentInChildren<Animator>();
    }

    protected virtual void OnDie()
    {

    }

    public void Damage(int damage)
    {
        if (_state == StateEnum.Die) return;

        _life -= damage;
        if (_life > 0) return;

        _state = StateEnum.Die;
        _animator.SetTrigger("Die");
        OnDie();
    }

    public void GoToAttackStateIfPossible()
    {
        if (!IsAttackable) return;

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");

    }

    public void GoToNormalStateIfPossible()
    {
        if (_state == StateEnum.Die) return;
        _state = StateEnum.Normal;
    }
}
