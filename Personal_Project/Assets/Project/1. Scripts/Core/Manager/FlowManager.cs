using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class FlowManager : SingletonBehaviour<FlowManager>
{
    [SerializeField] private spawnPoint[] _spawnPoints;
    [SerializeField] private FloorSO[] _floorSOList;
    [SerializeField] private Canvas _inventoryCanvas;
    //[SerializeField] private Image
    
    private FloorSO _currentFloorSO;
    private int _currentFloor;
    private int _spawnMonsterCount;
    private bool _isSpawnWaiting;
    private PlayerControls _controls;
    private WaitForSeconds _waitForSpawnMonsterTime;
    
    public ObservableProperty<int> RemainMonsterCount;
    
    // ---------- Event Method ----------

    private void Awake()
    {
        SetSingleton();
        _currentFloor = 0;
        _currentFloorSO = _floorSOList[_currentFloor];
    }

    private void Start()
    {
        _waitForSpawnMonsterTime = new WaitForSeconds(3f);
        RemainMonsterCount.Value = _currentFloorSO.spawnMonsterAmount;
    }

    private void Update()
    {
        if(!_isSpawnWaiting && _spawnMonsterCount < _currentFloorSO.spawnMonsterAmount)
        {
            StartCoroutine(WaitForSpawnMonster());
        }
    }

    // ---------- private Method ----------

    private IEnumerator WaitForSpawnMonster()
    {
        _isSpawnWaiting = true;
        _spawnMonsterCount++;
        yield return _waitForSpawnMonsterTime;
        Debug.Log("몬스터 스폰....");
        SpawnMonster();
        _isSpawnWaiting = false;
    }

    private void UIInputSystemSet(bool active)
    {
        if (active)
        {
            Debug.Log("UI 켰음");
            _controls.UI.Enable();
            _controls.Player.Disable();
        }
        else
        {
            Debug.Log("UI 껐음");
            _controls.UI.Disable();
            _controls.Player.Enable();
        }
    }

    private void DecreaseRemainMonsterCount(MonsterController monster)
    {
        RemainMonsterCount.Value--;
        
        monster.GetComponent<MonsterData>().OnDead -= DecreaseRemainMonsterCount;
        
        Debug.Log($"잔여 몬스터 수 : {RemainMonsterCount}");

        if (RemainMonsterCount.Value <= 0)
        {
            UIInputSystemSet(true);
            _inventoryCanvas.gameObject.SetActive(true);
        }
    }
    
    // ---------- public Method ----------

    public void SpawnMonster()
    {
        IPoolable pool;
        
        int rand = Random.Range(0, _currentFloorSO.spawnMonsterType.Length);
        
        pool = PoolManager.Instance.Take(_currentFloorSO.spawnMonsterType[rand]);
        
        int temp = Random.Range(0, _spawnPoints.Length);
        pool.GameObject.transform.position = _spawnPoints[temp].transform.position;
        pool.GameObject.transform.LookAt(Vector3.zero);

        MonsterData monsterData;
        if (pool.GameObject.TryGetComponent(out monsterData))
        {
            monsterData.OnDead += DecreaseRemainMonsterCount;
        }
    }
    
    public void SetPlayerControls(PlayerControls controls)
    {
        _controls = controls;
    }
}
