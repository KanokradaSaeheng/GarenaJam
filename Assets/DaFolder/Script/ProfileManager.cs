using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileManager : MonoBehaviour
{
    [Header("Overlay UI")]
    public GameObject profileOverlay;
    public TMP_InputField nameInput;
    public Image characterImage;
    public Button leftButton;
    public Button rightButton;
    public Button confirmButton;

    [Header("Top-left profile")]
    public GameObject topLeftProfilePanel;  // NEW: The whole panel to show/hide
    public Image profileImage;
    public TMP_Text profileName;

    [Header("Character options")]
    public Sprite[] characterSprites;
    public GameObject[] characterPrefabs;

    [Header("Player")]
    public GameObject playerCube;

    [Header("Dark background")]
    public Image dimBackground;  // NEW: Assign a UI Image with black semi-transparent background

    private int currentCharacterIndex = 0;

    void Start()
    {
        ShowProfileMaker(true);

        UpdateCharacterImage();
        leftButton.onClick.AddListener(PreviousCharacter);
        rightButton.onClick.AddListener(NextCharacter);
        confirmButton.onClick.AddListener(ConfirmProfile);
    }

    void ShowProfileMaker(bool show)
    {
        profileOverlay.SetActive(show);
        dimBackground.gameObject.SetActive(show);  // Show or hide dim background
        topLeftProfilePanel.SetActive(!show);      // Hide profile when overlay is up
    }

    void UpdateCharacterImage()
    {
        characterImage.sprite = characterSprites[currentCharacterIndex];
    }

    void PreviousCharacter()
    {
        currentCharacterIndex = (currentCharacterIndex - 1 + characterSprites.Length) % characterSprites.Length;
        UpdateCharacterImage();
    }

    void NextCharacter()
    {
        currentCharacterIndex = (currentCharacterIndex + 1) % characterSprites.Length;
        UpdateCharacterImage();
    }

    void ConfirmProfile()
    {
        string playerName = nameInput.text;

        // Update top-left profile UI
        profileName.text = playerName;
        profileImage.sprite = characterSprites[currentCharacterIndex];

        // Spawn the chosen player prefab at cube position
        Vector3 spawnPos = playerCube.transform.position;
        Quaternion spawnRot = playerCube.transform.rotation;
        GameObject newPlayer = Instantiate(characterPrefabs[currentCharacterIndex], spawnPos, spawnRot);

        // Parent it to where the cube was (so health bar stays on top or correct hierarchy)
        newPlayer.transform.parent = playerCube.transform.parent;

        // Option 1: Deactivate cube (keep health bar, etc.)
        playerCube.SetActive(false);

        // OR Option 2: Remove only cube visual (if cube has mesh or sprite to hide)
        // For example:
        // playerCube.GetComponent<MeshRenderer>().enabled = false;
        // or playerCube.GetComponent<SpriteRenderer>().enabled = false;

        // Hide overlay + show profile
        ShowProfileMaker(false);
    }

}
