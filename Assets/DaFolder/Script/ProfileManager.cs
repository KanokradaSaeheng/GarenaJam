using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileManager : MonoBehaviour
{
    [Header("Overlay UI")]
    public GameObject profileMakerPanel;          // The overlay panel with name input + character selection
    public TMP_InputField nameInput;              // TMP Input Field for player name
    public Image characterPreviewImage;           // Preview image to show current sprite selection
    public Button leftButton;                     // Button to go to previous sprite
    public Button rightButton;                    // Button to go to next sprite
    public Button confirmButton;                  // Button to confirm selection

    [Header("Profile Display (Top-Left UI)")]
    public GameObject profileDisplayPanel;        // The small profile panel always visible during game
    public Image profileImage;                    // Top-left profile image
    public TextMeshProUGUI profileNameText;       // Top-left profile name text

    [Header("Player")]
    public GameObject playerObject;               // The player object (with SpriteRenderer)

    [Header("Character Sprites")]
    public Sprite[] characterSprites;             // Drag your desired normal pose sprites here manually

    private int currentSpriteIndex = 0;

    void Start()
    {
        // Make sure overlay is open at start
        profileMakerPanel.SetActive(true);
        profileDisplayPanel.SetActive(false); // Hide profile display until confirmed
        Time.timeScale = 0;                   // Pause game while choosing profile

        // Show first sprite
        UpdateCharacterPreview();

        // Hook up buttons
        leftButton.onClick.AddListener(ShowPreviousCharacter);
        rightButton.onClick.AddListener(ShowNextCharacter);
        confirmButton.onClick.AddListener(ConfirmProfile);
    }

    void UpdateCharacterPreview()
    {
        if (characterSprites.Length > 0)
        {
            characterPreviewImage.sprite = characterSprites[currentSpriteIndex];
        }
    }

    void ShowPreviousCharacter()
    {
        if (characterSprites.Length == 0) return;
        currentSpriteIndex = (currentSpriteIndex - 1 + characterSprites.Length) % characterSprites.Length;
        UpdateCharacterPreview();
    }

    void ShowNextCharacter()
    {
        if (characterSprites.Length == 0) return;
        currentSpriteIndex = (currentSpriteIndex + 1) % characterSprites.Length;
        UpdateCharacterPreview();
    }

    void ConfirmProfile()
    {
        string playerName = nameInput.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Player name is empty!");
            return;
        }

        if (characterSprites.Length == 0)
        {
            Debug.LogWarning("No character sprites assigned!");
            return;
        }

        // Update profile display UI
        profileImage.sprite = characterSprites[currentSpriteIndex];
        profileNameText.text = playerName;

        // Update player sprite
        SpriteRenderer playerRenderer = playerObject.GetComponent<SpriteRenderer>();
        if (playerRenderer != null)
        {
            playerRenderer.sprite = characterSprites[currentSpriteIndex];
        }

        // Show profile display
        profileDisplayPanel.SetActive(true);

        // Close overlay and resume game
        profileMakerPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
