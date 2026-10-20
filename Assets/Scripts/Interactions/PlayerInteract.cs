using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform m_playerCamera;
    [SerializeField] private float m_interactionDistance;
    [SerializeField] private GameObject interactPrompt;
    private BaseInteract m_interactable;

    [SerializeField] private InputActionReference m_interactionReference;

    private void OnEnable()
    {
        m_interactionReference.action.Enable();
        m_interactionReference.action.started += PlayerInteracted;
        
    }

    private void OnDisable()
    {
        m_interactionReference.action.Disable();
    }

    private void Update()
    {
        HandleInteractPrompt();
    }

    private void PlayerInteracted(InputAction.CallbackContext context)
    {
        if (context.started && m_interactable != null)
        {
            m_interactable.Interact();
            m_interactable = null;
        }
    }

    private void HandleInteractPrompt()
    {
        Ray ray = new Ray(m_playerCamera.position, m_playerCamera.forward);
        if (!Physics.Raycast(ray, out RaycastHit hitinfo, m_interactionDistance))
        {
            interactPrompt.SetActive(false);
            return;
        }
        
        if (hitinfo.collider.gameObject.TryGetComponent(out BaseInteract interactable))
        {
            interactPrompt.SetActive(true);
            m_interactable = interactable;
        }

    }
}
