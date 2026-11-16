using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI m_popUpText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


}
