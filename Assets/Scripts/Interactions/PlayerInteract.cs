using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform m_playerCamera;
    [SerializeField] private float m_interactionDistance;

    [SerializeField] private InputActionReference m_interactionReference;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void PlayerInteracted(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Ray ray = new Ray(m_playerCamera.position, m_playerCamera.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, m_interactionDistance))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out InteractableInterface interactable))
                {
                    interactable.Interact();
                }
            }
        }
    }
}
