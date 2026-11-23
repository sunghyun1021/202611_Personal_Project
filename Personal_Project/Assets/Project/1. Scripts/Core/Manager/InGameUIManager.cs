using TMPro;
using UnityEngine;

public class InGameUIManager : SingletonBehaviour<InGameUIManager>
{
    [SerializeField] private TextMeshProUGUI _remainMonsterCount;
    
    // ---------- Event Method ----------

    private void Awake() => SetSingleton();

    private void OnEnable() => BindRemainMonsterEvent();

    private void OnDisable() => UnBindRemainMonsterEvent();
    
    // ---------- private Method ----------

    private void BindRemainMonsterEvent()
    {
        FlowManager.Instance.RemainMonsterCount.AddListener(ChangeRemainMonsterText); 
    }

    private void UnBindRemainMonsterEvent()
    {
        
    }

    private void ChangeRemainMonsterText(int count)
    {
        _remainMonsterCount.text = $"x {count}";
    }
    
    // ---------- public Method ----------
}
