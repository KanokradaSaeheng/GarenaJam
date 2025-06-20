using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public AudioSource audioToToggle;

    private bool isMuted = false;

    public void ToggleSound()
    {
        if (audioToToggle == null) return;

        isMuted = !isMuted;
        audioToToggle.mute = isMuted;
    }
}
