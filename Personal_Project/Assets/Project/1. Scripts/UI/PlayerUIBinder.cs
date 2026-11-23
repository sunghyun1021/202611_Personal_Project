using UnityEngine;

public class PlayerUIBinder : MonoBehaviour
{
    [SerializeField] private PlayerHealthUI _playerHealthUI;
    [SerializeField] private PlayerShieldUI _playerShieldUI;

    private PlayerData _playerData;
    
    // ---------- Event Method ----------

    private void Awake() => CacheComponents();

    private void OnEnable() => BindPlayerStatChangeEvents();

    private void OnDisable() => UnBindPlayerStatChangeEvents();

    // ---------- private Method ----------

    private void CacheComponents()
    {
        _playerData = GetComponent<PlayerData>();
    }

    private void BindPlayerStatChangeEvents()
    {
        _playerData.CurrentHealth.AddListener(_playerHealthUI.RefreshCurrentHp);
        _playerData.MaxHealth.AddListener(_playerHealthUI.InitMaxHp);
        _playerData.CurrentShield.AddListener(_playerShieldUI.RefreshCurrentShield);
        _playerData.MaxShield.AddListener(_playerShieldUI.InitMaxShield);
    }
    
    private void UnBindPlayerStatChangeEvents()
    {
        _playerData.CurrentHealth.RemoveListener(_playerHealthUI.RefreshCurrentHp);
        _playerData.MaxHealth.RemoveListener(_playerHealthUI.InitMaxHp);
        _playerData.CurrentShield.RemoveListener(_playerShieldUI.RefreshCurrentShield);
        _playerData.MaxShield.RemoveListener(_playerShieldUI.InitMaxShield);
    }
    
    // ---------- public Method ----------
    
}
