using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileManager : MonoBehaviour
{
    [Header("Overlay UI")]
    public GameObject profileMakerPanel;
    public TMP_InputField nameInput;
    public Image characterPreviewImage;
    public Button leftButton, rightButton, confirmButton;

    [Header("Profile Display (Top-Left)")]
    public GameObject profileDisplayPanel;
    public Image profileImage;
    public TextMeshProUGUI profileNameText;

    [Header("Player")]
    public GameObject playerObject;

    private Sprite[] characterSprites;
    private int currentSpriteIndex = 0;

    void Start()
    {
        // Automatically load all character sprites from Resources/Characters
        characterSprites = Resources.LoadAll<Sprite>("Characters");
        if (characterSprites.Length == 0)
        {
            Debug.LogError("No character sprites found in Resources/Characters!");
            return;
        }

        profileMakerPanel.SetActive(true);
        profileDisplayPanel.SetActive(false);
        Time.timeScale = 0;

        UpdateCharacterPreview();

        leftButton.onClick.AddListener(ShowPreviousCharacter);
        rightButton.onClick.AddListener(ShowNextCharacter);
        confirmButton.onClick.AddListener(ConfirmProfile);
    }

    void UpdateCharacterPreview()
    {
        characterPreviewImage.sprite = characterSprites[currentSpriteIndex];
    }

    void ShowPreviousCharacter()
    {
        currentSpriteIndex = (currentSpriteIndex - 1 + characterSprites.Length) % characterSprites.Length;
        UpdateCharacterPreview();
    }

    void ShowNextCharacter()
    {
        currentSpriteIndex = (currentSpriteIndex + 1) % characterSprites.Length;
        UpdateCharacterPreview();
    }

    void ConfirmProfile()
    {
        string playerName = nameInput.text.Trim();
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Name is empty!");
            return;
        }

        profileImage.sprite = characterSprites[currentSpriteIndex];
        profileNameText.text = playerName;
        playerObject.GetComponent<SpriteRenderer>().sprite = characterSprites[currentSpriteIndex];

        profileDisplayPanel.SetActive(true);
        profileMakerPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
