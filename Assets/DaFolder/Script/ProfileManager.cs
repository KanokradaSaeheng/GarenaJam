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
    public Image profileImage;
    public TMP_Text profileName;

    [Header("Character options")]
    public Sprite[] characterSprites;
    public GameObject[] characterPrefabs;

    [Header("Player")]
    public GameObject playerCube;

    private int currentCharacterIndex = 0;

    void Start()
    {
        profileOverlay.SetActive(true);
        UpdateCharacterImage();
        leftButton.onClick.AddListener(PreviousCharacter);
        rightButton.onClick.AddListener(NextCharacter);
        confirmButton.onClick.AddListener(ConfirmProfile);
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

        // Replace player cube with chosen character prefab
        Vector3 spawnPos = playerCube.transform.position;
        Quaternion spawnRot = playerCube.transform.rotation;
        Destroy(playerCube);
        Instantiate(characterPrefabs[currentCharacterIndex], spawnPos, spawnRot);

        // Hide overlay
        profileOverlay.SetActive(false);

        // Optional: resume game if paused, or reset timescale
        // Time.timeScale = 1;
    }
}
