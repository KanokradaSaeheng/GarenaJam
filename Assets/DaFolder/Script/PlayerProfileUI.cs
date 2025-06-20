using UnityEngine;
using TMPro;

public class PlayerProfileUI : MonoBehaviour
{
    public TMP_Text nameText;  // Drag your TMP Text here in inspector

    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        nameText.text = playerName;
    }
}
