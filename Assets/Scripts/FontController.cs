using UnityEngine;
using TMPro;

public class FontController : MonoBehaviour
{
    public TMP_Text targetText;
    public float step = 4f;

    public void IncreaseFont() { if (targetText) targetText.fontSize += step; }
    public void DecreaseFont() { if (targetText) targetText.fontSize = Mathf.Max(8, targetText.fontSize - step); }
    public void SetSize(float size) { if (targetText) targetText.fontSize = size; }
}
