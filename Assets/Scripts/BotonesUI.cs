using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesUI : MonoBehaviour
{
    public void Viajar()
    {
        Debug.Log("Botón Viajar presionado");
    }

    public void Galeria()
    {
        Debug.Log("Botón Galería presionado");
    }

    public void Salir()
    {
        Debug.Log("Botón Salir presionado");
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
