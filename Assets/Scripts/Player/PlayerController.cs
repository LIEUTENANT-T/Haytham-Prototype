using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float m_moveSpeed = 8f;

    [Header("Jump")]
    [SerializeField] private float m_jumpForce = 8f;
    [SerializeField] private float m_gravity = -10f;
    [SerializeField] private float m_initialFallVelocity = -2f;
    private InputAction m_jumpInput;
    private bool m_hasJumped;

    [Header("References")]
    [SerializeField] private Transform m_mainCamera;
    private InputAction m_moveAction;

    [HideInInspector] public CharacterController m_characterController;
    private Vector2 m_moveInput;
    private float m_verticalVelocity;

    public static PlayerController instance;

    private void Awake()
    { 
        if (instance == null)
        {
            instance = this;
        }

        m_characterController = GetComponent<CharacterController>();
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_jumpInput = InputSystem.actions.FindAction("Jump");
    }

    private void OnEnable()
    {
        m_moveAction.performed += StoreMovementInput;
        m_moveAction.canceled += StoreMovementInput;

        m_jumpInput.started += Jump;
    }

    private void OnDisable()
    {
        m_moveAction.performed -= StoreMovementInput;
        m_moveAction.canceled -= StoreMovementInput;

        m_jumpInput.performed -= Jump;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleGravity();
        if (!m_characterController.isGrounded)
        {
            m_hasJumped = false;
        }
    }

    private void StoreMovementInput(InputAction.CallbackContext context)
    {
        m_moveInput = context.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        var move = m_mainCamera.TransformDirection(new Vector3(m_moveInput.x, 0, m_moveInput.y));
        var currentSpeed = m_moveSpeed;
        var movement = move * currentSpeed;
        movement.y = m_verticalVelocity;

        m_characterController.Move(movement * Time.deltaTime);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (m_characterController.isGrounded)
        {
            m_verticalVelocity = m_jumpForce;
        }
    }

    private void HandleGravity()
    {
        if (m_characterController.isGrounded && m_verticalVelocity < 0)
        {
            m_verticalVelocity = m_initialFallVelocity;
        }

        m_verticalVelocity += m_gravity * Time.deltaTime;
    }
}
