using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    [SerializeField] Projectile m_firedProjectile;
    [SerializeField] Transform m_projectileSpawnPosition;

    [SerializeField] private Transform m_cameraTransform;
    [SerializeField] private InputActionReference m_shootAction;


    private void Awake()
    {
 
    }

    private void OnEnable()
    {
        m_shootAction.action.performed += Shoot;
    }

    private void OnDisable()
    {
        m_shootAction.action.performed -= Shoot;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Shoot(InputAction.CallbackContext context)
    {
        Vector3 direction = m_cameraTransform.forward;
        Projectile projectile = Instantiate(m_firedProjectile, m_projectileSpawnPosition.position, Quaternion.identity);
        projectile.Shoot(direction.normalized);
    }
}
