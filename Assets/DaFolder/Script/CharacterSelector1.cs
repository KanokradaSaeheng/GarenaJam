using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelector : MonoBehaviour
{
    public Sprite[] characterSprites;
    public Image characterImage;       // Only one of this!
    public TMP_InputField nameInputField;

    private int currentIndex = 0;

    void Start()
    {
        ShowCharacter(currentIndex);
        FocusNameInput();
    }

    void ShowCharacter(int index)
    {
        characterImage.sprite = characterSprites[index];
    }

    public void OnNext()
    {
        currentIndex = (currentIndex + 1) % characterSprites.Length;
        ShowCharacter(currentIndex);
    }

    public void OnPrevious()
    {
        currentIndex = (currentIndex - 1 + characterSprites.Length) % characterSprites.Length;
        ShowCharacter(currentIndex);
    }

    public void OnConfirm()
    {
        string playerName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Player name is empty!");
            return;
        }

        PlayerPrefs.SetInt("SelectedCharacter", currentIndex);
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();

        Debug.Log("Character confirmed: " + currentIndex);
        Debug.Log("Player name: " + playerName);
    }

    public void FocusNameInput()
    {
        nameInputField.ActivateInputField();
    }
}
