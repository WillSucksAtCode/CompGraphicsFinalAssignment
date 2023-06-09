using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    PlayerInput _playerControls;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        _playerControls = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return _playerControls.Movement.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseMovement()
    {
        return _playerControls.Movement.Look.ReadValue<Vector2>();
    }

    public bool GetJumped()
    {
        return _playerControls.Movement.Jump.triggered;
    }

    public bool GetSprint()
    {
        if (_playerControls.MovementEffects.SprintEnabled.IsPressed())
        {
            return true;
        }
        if (_playerControls.MovementEffects.SprintDisabled.WasReleasedThisFrame())
        {
            return false;
        }
        return false;
    }

    public bool GetCrouch()
    {
        return _playerControls.MovementEffects.Crouch.triggered;
    }

    public bool GetADSEnable()
    {
        if (_playerControls.Weapons.ADS.IsPressed())
            return true;
        if (_playerControls.Weapons.ADS.WasReleasedThisFrame())
            return false;

        return false;
    }

    public bool GetShooting()
    {
        if (_playerControls.Weapons.ShootEnable.IsPressed())
        {
            return true;
        }
        if (_playerControls.Weapons.ShootDisable.WasReleasedThisFrame())
        {
            return false;
        }
        return false;
    }

    public bool GetSingleShooting()
    {
        return _playerControls.Weapons.ShootEnable.triggered;
    }

    public bool GetSwitchGamepad()
    {
        return _playerControls.Weapons.SwitchWeaponGamepad.triggered;
    }

    public bool GetSwitchWeaponOne()
    {
        return _playerControls.Weapons.SwitchWeaponOne.triggered;
    }

    public bool GetSwitchWeaponTwo()
    {
        return _playerControls.Weapons.SwitchWeaponTwo.triggered;
    }

    public bool GetSwitchWeaponThree()
    {
        return _playerControls.Weapons.SwitchWeaponThree.triggered;
    }

    public bool GetReload()
    {
        return _playerControls.Weapons.Reload.triggered;
    }

    public bool GetInteract()
    {
        return _playerControls.OtherControls.Interact.triggered;
    }

    public bool GetEscape()
    {
        return _playerControls.OtherControls.Escape.triggered;
    }

    public bool GetHeal()
    {
        return _playerControls.OtherControls.Heal.triggered;
    }
}