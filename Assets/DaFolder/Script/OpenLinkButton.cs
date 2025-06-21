using UnityEngine;

public class OpenLinkButton : MonoBehaviour
{
    // This is the link you want to open
    public string url = "https://youtu.be/dQw4w9WgXcQ?si=8JWuLV-gx2_NxpU4";

    public void OpenLink()
    {
        Application.OpenURL(url);
    }
}
