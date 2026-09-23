using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float m_moveSpeed = 8f;

    [Header("References")]
    [SerializeField] private Transform m_mainCamera;
    [SerializeField] private InputActionReference m_moveAction;

    private CharacterController m_characterController;
    private Vector2 m_moveInput;


    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        m_moveAction.action.performed += StoreMovementInput;
        m_moveAction.action.canceled += StoreMovementInput;
    }

    private void OnDisable()
    {
        m_moveAction.action.performed -= StoreMovementInput;
        m_moveAction.action.canceled -= StoreMovementInput;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
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

        m_characterController.Move(movement * Time.deltaTime);
    }

}
