using System.Collections.Generic;
using UnityEngine;

public class ValidPlacementChecker : MonoBehaviour
{
    [SerializeField] private LayerMask m_invalidLayers;

    public bool isValid { get; private set; } = true;
    [SerializeField] private List<Collider> m_collidingObjects = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & m_invalidLayers) != 0)
        {
            m_collidingObjects.Add(other);
            isValid = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & m_invalidLayers) != 0)
        {
            m_collidingObjects.Remove(other);
            isValid = m_collidingObjects.Count <= 0;
        }
    }
}
