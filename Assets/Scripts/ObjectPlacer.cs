using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPlacer : MonoBehaviour
{
    public bool placementMode = false;
    [SerializeField] InputActionReference m_placementModeInput;

    [SerializeField] private GameObject m_placeableObjectPrefab;
    [SerializeField] private GameObject m_previewObjectPrefab;
    [SerializeField] private Camera m_playerCamera;
    [SerializeField] private LayerMask m_placementSurfaceLayerMask;

    [SerializeField] private float m_placementDistance;
    [SerializeField] private float m_raycastStartVerticalOffset;
    [SerializeField] private float m_raycastDistance;

    private GameObject m_previewObject = null;
    private Vector3 m_currentPlacementPosition;

    private void OnEnable()
    {
        m_placementModeInput.action.started += togglePlacementMode;
    }

    private void OnDisable()
    {
        m_placementModeInput.action.canceled -= togglePlacementMode;
    }

    void Update()
    {
        if (placementMode)
        {
            UpdateCurrentPlacementPosition();
        }
    }

    private void UpdateCurrentPlacementPosition()
    {
        Vector3 cameraForward = new Vector3(m_playerCamera.transform.forward.x, 0f, m_playerCamera.transform.forward.z);
        cameraForward.Normalize();

        Vector3 startPos = m_playerCamera.transform.position + (cameraForward * m_placementDistance);
        startPos.y += m_raycastStartVerticalOffset;

        RaycastHit hitInfo;
        if (Physics.Raycast(startPos, Vector3.down, out hitInfo, m_raycastDistance, m_placementSurfaceLayerMask))
        {
            m_currentPlacementPosition = hitInfo.point;
        }

        Quaternion rotation = Quaternion.Euler(-90f, m_playerCamera.transform.eulerAngles.y, 0f);
        m_previewObject.transform.position = m_currentPlacementPosition;
        m_previewObject.transform.rotation = rotation;
    }

    private void togglePlacementMode(InputAction.CallbackContext context)
    {
        if (placementMode)
        {
            Debug.Log("ExitPlacementMode");
            placementMode = false;
        }
        else if (!placementMode)
        {
            Debug.Log("EnterPlacementMode");

            Quaternion rotation = Quaternion.Euler(0f, m_playerCamera.transform.eulerAngles.y, 0f);
            m_previewObject = Instantiate(m_previewObjectPrefab, m_currentPlacementPosition, rotation, transform);
            placementMode = true;
        }
    }
}
