using UnityEngine;
using UnityEngine.UI;

public class PlayerShieldUI : MonoBehaviour
{
    [SerializeField] private Image playerShieldBar;

    private float _currentShield;
    private float _maxShield;

    // ---------- private Method ----------
    
    private void RefreshCurrentShieldBar()
    {
        playerShieldBar.fillAmount = _currentShield / _maxShield;
    }
    
    // ---------- public Method ----------
    
    public void RefreshCurrentShield(float currentShield)
    {
        _currentShield = currentShield;
        RefreshCurrentShieldBar();
    }

    public void InitMaxShield(float maxShield)
    {
        _maxShield = maxShield;
    }

}
