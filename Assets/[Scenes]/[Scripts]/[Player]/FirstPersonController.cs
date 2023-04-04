using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour, IDataPersistence
{
    public bool CanMove { get; private set; } = true;
    private bool IsSprinting => canSprint && input.GetSprint();
    private bool ShouldJump => input.GetJumped() && characterController.isGrounded;
    private bool ShouldCrouch => input.GetCrouch() && !duringCrouchAnim && characterController.isGrounded;

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canUseHeadbob = true;
    [SerializeField] private bool willSlideOnSlopes = true;
    [SerializeField] private bool canInteract = true;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float slopeSpeed = 8.0f;

    [Header("Footstep Parameters")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultiplier = 1.5f;
    [SerializeField] private float sprintStepMutliplier = 0.6f;
    [SerializeField] PlayerAudio playerSounds;
    private float footstepTimer = 0;
    private float GetCurrentOffset => isCrouching ? baseStepSpeed * crouchStepMultiplier : IsSprinting ? baseStepSpeed * sprintStepMutliplier : baseStepSpeed;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [Header("Health Parameters")]
    [SerializeField] public float maxHealth = 100;
    public static Action<float> OnTakeDamage;
    public static Action<float> OnDamage;
    public static Action<float> OnHeal;
    private UI ui;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject weaponHolder;

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 2.0f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenter = Vector3.zero;
    private bool isCrouching;
    private bool duringCrouchAnim;

    [Header("Headbob Parameters")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.1f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYPos = 0;
    private float timer;

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
        //this.currentHealth = data.playerCurrentHealth;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
        //data.playerCurrentHealth = this.currentHealth;
    }
    // SLIDING PARAMETERS
    private Vector3 hitPointNormal;

    private bool IsSliding
    {
        get
        {
            if (characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2f))
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }

    [Header("Interaction")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    private Interactable currentInteractable;

    private Camera playerCamera;
    private CharacterController characterController;
    private InputManager input;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;

    private void OnEnable()
    {
        OnTakeDamage += ApplyDamage;
    }

    private void OnDisable()
    {
        OnTakeDamage -= ApplyDamage;
    }

    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        ui = transform.Find("/-- UI --/Canvas").GetComponent<UI>();
        defaultYPos = playerCamera.transform.localPosition.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    void Start()
    {
        input = InputManager.Instance;
        ui.UpdateMeds();
    }

    void Update()
    {
        ApplyAll();
    }

    private void ApplyAll()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();
            HandleHeal();

            if (canJump)
                HandleJump();

            if (canCrouch)
                HandleCrouch();

            if (canUseHeadbob)
                HandleHeadbob();
            
            HandleFootsteps();

            if (canInteract)
            {
                HandleInteractionCheck();
                HandleInteractionInput();
            }

            ApplyFinalMovements();
        }
    }

    private void HandleMovementInput()
    {
        // Get movement input & speed
        currentInput = new Vector2((isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * input.GetPlayerMovement().y, (isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * input.GetPlayerMovement().x);

        // Store movement values
        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        //Look up and down
        rotationX -= input.GetMouseMovement().y * lookSpeedY * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Look left and right
        transform.rotation *= Quaternion.Euler(0, input.GetMouseMovement().x * lookSpeedX * Time.deltaTime, 0);
    }

    private void HandleJump()
    {
        if (ShouldJump)
            moveDirection.y = jumpForce;
    }

    private void HandleCrouch()
    {
        if (ShouldCrouch)
            StartCoroutine(CrouchStand());
    }

    private void HandleHeadbob()
    {
        // Check if character is grounded
        if (!characterController.isGrounded)
            return;

        // Check if player is moving
        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            // Determine headbob speed & amount : Apply
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
                playerCamera.transform.localPosition.z);
        }
    }

    private void HandleInteractionCheck()
    {
        // Check if raycast is hitting interactable
        if (Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance))
        {
            // If on interactable layer and (no interactable object selected or object your looking at isnt the current object stored
            if (hit.collider.gameObject.layer == 9 && (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID()))
            {
                // Get new current interactable
                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                    currentInteractable.OnFocus();
            }
        }
        // If raycast doesnt find anything
        else if (currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }

    private void HandleInteractionInput()
    {
        // Check if looking at interactable & input
        if (input.GetInteract() && currentInteractable != null &&
            Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            currentInteractable.OnInteract();
        }
    }
    
    private void HandleFootsteps()
    {
        if (!characterController.isGrounded)
            return;

        if (currentInput == Vector2.zero)
            return;

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0)
        {
            playerSounds.PlayFootsteps();

            footstepTimer = GetCurrentOffset;
        }
    }

    private void HandleHeal()
    {
        if (input.GetHeal() && Variables.health < 100 && Variables.meds > 0)
        {
            Variables.meds--;
            Variables.health += 25;
            ui.UpdateMeds();

            if (Variables.health >= 100)
                Variables.health = 100;

            OnHeal?.Invoke(Variables.health);
        }
    }

    private void ApplyDamage(float damage)
    {
        Variables.health -= damage;
        OnDamage?.Invoke(Variables.health);

        if (Variables.health <= 0)
            KillPlayer();
    }

    private void KillPlayer()
    {
        Variables.health = 0;

        CanMove = false;
        Time.timeScale = 0;
        weaponHolder.SetActive(false);
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ApplyFinalMovements()
    {
        // Apply gravity if in air
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        if (characterController.velocity.y < -1 && characterController.isGrounded)
            moveDirection.y = 0;

        // Apply slide to movement if on slope
        if (willSlideOnSlopes && IsSliding)
            moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed;

        // Move character
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private IEnumerator CrouchStand()
    {
        // Check if object is above character
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
            yield break;

        // Start crouchign anim(time)
        duringCrouchAnim = true;

        // Set height and center variables
        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        // Lerp height and center
        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Confirm height & center are correct
        characterController.height = targetHeight;
        characterController.center = targetCenter;

        // Change crouching bool
        isCrouching = !isCrouching;

        // End crouching anim(time)
        duringCrouchAnim = false;
    }
}