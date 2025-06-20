using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonImage : MonoBehaviour
{
    // Assign these in the Inspector
    public Sprite defaultSprite;
    public Sprite clickedSprite;

    private Image buttonImage;

    void Start()
    {
        // Get the Image component attached to the button
        buttonImage = GetComponent<Image>();

        // Set the default image at start
        if (buttonImage != null && defaultSprite != null)
        {
            buttonImage.sprite = defaultSprite;
        }
    }

    // Call this method from the Button's OnClick() event
    public void OnClickChangeImage()
    {
        if (buttonImage != null && clickedSprite != null)
        {
            buttonImage.sprite = clickedSprite;

            // Optional: reset after 1 second
            Invoke("ResetImage", 1f);
        }
    }

    // Reset back to the default image
    private void ResetImage()
    {
        if (buttonImage != null && defaultSprite != null)
        {
            buttonImage.sprite = defaultSprite;
        }
    }
}
