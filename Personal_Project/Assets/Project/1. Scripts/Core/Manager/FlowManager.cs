using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FlowManager : SingletonBehaviour<FlowManager>
{
    [SerializeField] private spawnPoint[] _spawnPoints;
    [SerializeField] private FloorSO[] _floorSOList;
    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private Button _nextFloorButton;
    [SerializeField] private GameObject _cantGoNextFloorText;
    
    private FloorSO _currentFloorSO;
    private int _currentFloor;
    private int _spawnMonsterCount;
    private bool _isSpawnWaiting;
    private PlayerControls _controls;
    private WaitForSeconds _waitForSpawnMonsterTime;

    public HashSet<ItemInstance> StorageItemList = new();
    public ObservableProperty<int> RemainMonsterCount;
    
    // ---------- Event Method ----------

    private void Awake()
    {
        _inventoryCanvas.gameObject.SetActive(false);
        SetSingleton();
        _currentFloor = 0;
        _currentFloorSO = _floorSOList[_currentFloor];
    }

    private void OnEnable() => BindNextFloorButton();

    private void OnDisable() => UnBindNextFloorButton();

    private void Start()
    {
        _waitForSpawnMonsterTime = new WaitForSeconds(4f);
        RemainMonsterCount.Value = _currentFloorSO.spawnMonsterAmount;
    }

    private void Update()
    {
        GameStart();
    }

    // ---------- private Method ----------

    private void BindNextFloorButton()
    {
        _nextFloorButton.onClick.AddListener(GotoNextFloor);
    }
    
    private void UnBindNextFloorButton()
    {
        _nextFloorButton.onClick.RemoveListener(GotoNextFloor);
    }

    private void UIInputSystemSet(bool active)
    {
        if (active)
        {
            _controls.UI.Enable();
            _controls.Player.Disable();
        }
        else
        {
            _controls.UI.Disable();
            _controls.Player.Enable();
        }
    }

    private void GameStart()
    {
        if(!_isSpawnWaiting && _spawnMonsterCount < _currentFloorSO.spawnMonsterAmount)
        {
            StartCoroutine(WaitForSpawnMonster());
        }
    }

    private void DecreaseRemainMonsterCount(MonsterController monster)
    {
        RemainMonsterCount.Value--;
        
        monster.GetComponent<MonsterData>().OnDead -= DecreaseRemainMonsterCount;

        if (RemainMonsterCount.Value <= 0)
        {
            UIInputSystemSet(true);
            _inventoryCanvas.gameObject.SetActive(true);
        }
    }

    private void GotoNextFloor()
    {
        // TODO 보상을 골랐는가도 검사 필요
        if (StorageItemList.Count != 0)
        {
            StartCoroutine(InActiveWarningUI());
            return;
        }
        
        _currentFloor++;
        
        GameManager.Instance.Player.transform.position = Vector3.zero;
        _currentFloorSO = _floorSOList[_currentFloor];
        RemainMonsterCount.Value = _currentFloorSO.spawnMonsterAmount;
        _spawnMonsterCount = 0;
        
        _inventoryCanvas.gameObject.SetActive(false);
        UIInputSystemSet(false);
    }
    
    private IEnumerator WaitForSpawnMonster()
    {
        _isSpawnWaiting = true;
        _spawnMonsterCount++;
        yield return _waitForSpawnMonsterTime;
        Debug.Log("몬스터 스폰....");
        SpawnMonster();
        _isSpawnWaiting = false;
    }

    private IEnumerator InActiveWarningUI()
    {
        _cantGoNextFloorText.SetActive(true);
        yield return new WaitForSeconds(3f);
        _cantGoNextFloorText.SetActive(false);
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
