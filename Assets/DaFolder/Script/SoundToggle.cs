using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public AudioSource[] audioSources;

    // Track if sound is muted
    private bool isMuted = false;

    public void ToggleSound()
    {
        isMuted = !isMuted;

        foreach (AudioSource source in audioSources)
        {
            if (source != null)
            {
                source.mute = isMuted;
            }
        }
    }
}
