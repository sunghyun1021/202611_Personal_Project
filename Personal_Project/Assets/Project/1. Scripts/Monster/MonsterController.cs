using System.Collections;
using Unity.Behavior;
using UnityEngine;

public class MonsterController : MonoBehaviour, IPoolable, IDamageable
{
    [field: Header("몬스터 타입")]
    [field: SerializeField] public MonsterType Type { get; set; }

    [Header("몬스터 공격 타입")] 
    [SerializeField] private AttackBase _attackType;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _playerTransform;

    private MonsterData _monsterData;
    private BehaviorGraphAgent _behaviorAgent;
    private Rigidbody _rigidbody;
    
    public GameObject GameObject { get => gameObject; }
    public Transform Transform { get => transform; }
    public ObjectPool Source { get; set; }
    
    
    // ---------- Event Method ----------

    private void Awake() => CacheComponents();

    private void OnEnable() => DataReset();

    private void Start() => Init();
    
    // ----------------------------------

    private void CacheComponents()
    {
        _monsterData = GetComponent<MonsterData>();
        _behaviorAgent = GetComponent<BehaviorGraphAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>(); 
    }

    private void Init()
    {
        _playerTransform = GameManager.Instance.Player.transform;
        _behaviorAgent.enabled = true;
        if (_attackType == null) _attackType = GetComponent<AttackBase>();
        _behaviorAgent.SetVariableValue("player", _playerTransform);
        _behaviorAgent.SetVariableValue("attackType", _attackType);
        _behaviorAgent.SetVariableValue("animator", _animator);
        _attackType.PlayerTransform = _playerTransform;
    }

    private void DataReset()
    {
        _monsterData.InitData();
    }

    private IEnumerator WaitDeadAnimation()
    {
        float animationTime = _animator.GetCurrentAnimatorStateInfo(0).length;
        
        yield return new WaitForSeconds(animationTime + 1);
        ReturnToPool();
    }
    
    public void Dead()
    {
        _monsterData.OnDead?.Invoke(this);
        _animator.SetTrigger("Dead");
        _behaviorAgent.enabled = false;
        // 잔여 몬스터 카운트 감소

        StartCoroutine(WaitDeadAnimation());
    }
    
    public void TakeDamage(int damage)
    {
        Debug.Log($"데미지 : {damage}");
        _monsterData.CurrentHealth.Value -= damage;

        Debug.Log($"남은 체력 : {_monsterData.CurrentHealth.Value}");
        if (_monsterData.CurrentHealth.Value <= 0)
        {
            Debug.Log("죽음");
            Dead();
        }
    }
    
    public void ReturnToPool()
    {
        Source.Push(this);
    }
}
