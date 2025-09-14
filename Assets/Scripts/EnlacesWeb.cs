using UnityEngine;

public class EnlacesWeb : MonoBehaviour
{
    public void AbrirTikTok()
    {
        Application.OpenURL("https://www.tiktok.com/@tecnicolortravel");
    }

    public void AbrirInstagram()
    {
        Application.OpenURL("https://www.instagram.com/tecnicolortravel/");
    }

    public void AbrirWeb()
    {
        Application.OpenURL("https://tecnicolortravel.com/");
    }
}
