using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    private PlayerControls _controls;
    
    [HideInInspector] public bool IsDashed;
    [HideInInspector] public bool IsShoot; 
    public Vector3 MoveAxisRaw { get; private set; }
    public Vector2 MousePosition => _controls.Player.MouseAim.ReadValue<Vector2>();

    // ---------- Event Method ----------

    private void Awake() => _controls = new PlayerControls();
    
    private void OnEnable()
    {
        _controls.Player.Enable();
        BindInputEvents();
    }

    private void OnDisable()
    {
        UnBindInputEvents();
        _controls.Player.Disable();
    }

    // ----------------------------------

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        MoveAxisRaw = new Vector3(move.x, 0, 0);
    }

    private void OnMoveStop(InputAction.CallbackContext context)
    {   
        MoveAxisRaw = Vector3.zero;
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        IsDashed = true;
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        IsShoot = true;
    }

    private void OnShootStop(InputAction.CallbackContext context)
    {
        IsShoot = false;
    }

    // ---------- Event Binding ----------
    
    private void BindInputEvents()
    {
        _controls.Player.Move.performed += OnMove;
        _controls.Player.Move.canceled += OnMoveStop;

        _controls.Player.Dash.started += OnDash;

        _controls.Player.Shoot.performed += OnShoot;
        _controls.Player.Shoot.canceled += OnShootStop;
    }

    private void UnBindInputEvents()
    {
        _controls.Player.Move.performed -= OnMove;
        _controls.Player.Move.canceled -= OnMoveStop;
        
        _controls.Player.Dash.started -= OnDash;
        
        _controls.Player.Shoot.performed -= OnShoot;
        _controls.Player.Shoot.canceled -= OnShootStop;
    }
}
