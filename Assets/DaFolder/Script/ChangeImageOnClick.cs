using UnityEngine;
using UnityEngine.UI;

public class ChangeImageOnClick : MonoBehaviour
{
    [Header("This Button")]
    public Image thisButtonImage;
    public Sprite spriteOn;
    public Sprite spriteOff;

    [Header("Target UI Image")]
    public Image targetImage;
    public Sprite targetSpriteOn;
    public Sprite targetSpriteOff;

    private bool isOn = true;

    public void ToggleUI()
    {
        isOn = !isOn;

        // Change this button's image
        if (thisButtonImage != null)
            thisButtonImage.sprite = isOn ? spriteOn : spriteOff;

        // Change the target image
        if (targetImage != null)
            targetImage.sprite = isOn ? targetSpriteOn : targetSpriteOff;
    }
}
