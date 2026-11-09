using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    private PlayerControls _controls;
    
    public Vector3 MoveAxisRaw { get; private set; }

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
        MoveAxisRaw = new Vector3(move.x, 0, move.y);
    }

    private void OnMoveStop(InputAction.CallbackContext context)
    {
        MoveAxisRaw = Vector3.zero;
    }

    private void BindInputEvents()
    {
        _controls.Player.Move.performed += OnMove;
        _controls.Player.Move.canceled += OnMoveStop;
    }

    private void UnBindInputEvents()
    {
        _controls.Player.Move.performed -= OnMove;
        _controls.Player.Move.canceled -= OnMoveStop;
    }
}
