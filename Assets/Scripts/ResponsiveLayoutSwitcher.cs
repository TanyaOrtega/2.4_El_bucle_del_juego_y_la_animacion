using UnityEngine;

public class ResponsiveLayoutSwitcher : MonoBehaviour
{
    public GameObject contentH;
    public GameObject contentV;

    private bool isPortraitLast;

    void Start()
    {
        ApplyLayout();
    }

    void Update()
    {
        bool isPortrait = Screen.height > Screen.width;
        if (isPortrait != isPortraitLast)
        {
            isPortraitLast = isPortrait;
            ApplyLayout();
        }
    }

    void ApplyLayout()
    {
        bool isPortrait = Screen.height > Screen.width;

        contentH.SetActive(!isPortrait); // Landscape
        contentV.SetActive(isPortrait);  // Portrait
    }
}
