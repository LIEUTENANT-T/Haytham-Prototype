using NUnit.Framework;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEngine;

public class shotAnglePreview : MonoBehaviour
{
    [Header("Indicator Settings")]
    public LayerMask layerMask;
    public float range;
    public int reflectionNumber;

    private LineRenderer m_lineRenderer;
    private RaycastHit hit;

    private Ray m_ray;
    private Vector3 m_direction;

    private void Start()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    private void LateUpdate()
    {
        m_ray = new Ray (transform.position, transform.forward);

        m_lineRenderer.positionCount = 1;
        m_lineRenderer.SetPosition(0, transform.position);

        float remainingRange = range;

        for (int i = 0; i < reflectionNumber; i++)
        {
            if (Physics.Raycast(m_ray.origin, m_ray.direction, out hit, remainingRange, layerMask))
            {
                m_lineRenderer.positionCount += 1;
                m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, hit.point);

                remainingRange -= Vector3.Distance(m_ray.origin, hit.point);

                m_ray = new Ray (hit.point, Vector3.Reflect(m_ray.direction, hit.normal));

            }
            else
            {
                m_lineRenderer.positionCount += 1;
                m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, m_ray.origin + (m_ray.direction * remainingRange));
            }
        }
    }
}
