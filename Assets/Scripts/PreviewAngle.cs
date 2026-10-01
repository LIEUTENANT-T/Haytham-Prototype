using UnityEngine;

public class PreviewAngle : MonoBehaviour
{

    public GameObject indicatorPrefab;

    // Update is called once per frame
    void Update()
    {
        GameObject preview = Instantiate(indicatorPrefab, this.transform);

    }
}
