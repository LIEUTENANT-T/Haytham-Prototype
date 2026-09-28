using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float m_projectileSpeed;
    [SerializeField] float m_projectileLifetime = 200f;
    private float m_currentLifespan;

    private Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_currentLifespan = m_projectileLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        m_currentLifespan -= m_currentLifespan * Time.deltaTime;

        Debug.Log(m_currentLifespan);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Mirror")
        {
            var firstContact = collision.contacts[0];
            Vector3 newVelocity = Vector3.Reflect(direction.normalized, firstContact.normal);
            Shoot(newVelocity.normalized);
            m_currentLifespan = m_projectileLifetime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Shoot(Vector3 direction)
    {
        this.direction = direction;
        rb.linearVelocity = this.direction * m_projectileSpeed;
    }
}
