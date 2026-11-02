using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPlacer : MonoBehaviour
{
    public bool placementMode = false;
    [SerializeField] InputActionReference m_placementModeInput;
    [SerializeField] InputActionReference m_placeObjectInput;

    [Header("Placement Parameters")]
    [SerializeField] private GameObject m_placeableObjectPrefab;
    [SerializeField] private GameObject m_previewObjectPrefab;
    [SerializeField] private Camera m_playerCamera;
    [SerializeField] private LayerMask m_placementSurfaceLayerMask;

    [Header("Preview Materials")]
    [SerializeField] private Material m_previewMaterial;
    [SerializeField] private Color validColour;
    [SerializeField] private Color invalidColour;

    [Header("Raycast Parameters")]
    [SerializeField] private float m_placementDistance;
    [SerializeField] private float m_raycastStartVerticalOffset;
    [SerializeField] private float m_raycastDistance;

    private GameObject m_previewObject = null;
    private Vector3 m_currentPlacementPosition;
    private bool m_validPreviewState = false;

    public static ObjectPlacer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        m_placementModeInput.action.started += TogglePlacementMode;
        m_placeObjectInput.action.started += PlaceObject;
    }

    private void OnDisable()
    {
        m_placementModeInput.action.started -= TogglePlacementMode;
        m_placeObjectInput.action.started -= PlaceObject;
    }

    void Update()
    {
        if (placementMode)
        {
            UpdateCurrentPlacementPosition();

            if (CanPlaceObject())
            {
                SetValidPreviewState();
            }
            else
            {
                SetInvalidPreviewState();
            }
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

    private bool CanPlaceObject()
    {
        if (m_previewObject == null)
            return false;

        return m_previewObject.GetComponentInChildren<ValidPlacementChecker>().isValid;
    }

    private void SetValidPreviewState()
    {
        m_previewMaterial.color = validColour;
        m_validPreviewState = true;
    }

    private void SetInvalidPreviewState()
    {
        m_previewMaterial.color = invalidColour;
        m_validPreviewState = false;
    }

    private void TogglePlacementMode(InputAction.CallbackContext context)
    {
        if (placementMode)
        {
            Debug.Log("ExitPlacementMode");

            Destroy(m_previewObject);
            m_previewObject = null;
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

    private void PlaceObject(InputAction.CallbackContext context)
    {
        if (!placementMode || !m_validPreviewState)
            return;

        Quaternion rotation = Quaternion.Euler(-90f, m_playerCamera.transform.eulerAngles.y, 0f);
        Instantiate(m_placeableObjectPrefab, m_currentPlacementPosition, rotation, transform);
    }
}
