using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    
    private PlayerMovement _playerMovement;
    private PlayerData _playerData;
    
    // ---------- Event Method ----------
    
    private void Awake() => CacheComponents();
    
    // ----------------------------------
    
    private void CacheComponents()
    {
        _playerData = GetComponent<PlayerData>();
        _playerMovement = GetComponent<PlayerMovement>();
    }
}
