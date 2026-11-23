using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image _playerHealthBar;

    private int _currentHp;
    private int _maxHp;

    // ---------- private Method ----------
    
    private void RefreshCurrentHpBar()
    {
        _playerHealthBar.fillAmount = (float)_currentHp / _maxHp;
    }
    
    // ---------- public Method ----------
    
    public void RefreshCurrentHp(int currentHp)
    {
        _currentHp = currentHp;
        RefreshCurrentHpBar();
    }

    public void InitMaxHp(int maxHp)
    {
        _maxHp = maxHp;
    }
}
