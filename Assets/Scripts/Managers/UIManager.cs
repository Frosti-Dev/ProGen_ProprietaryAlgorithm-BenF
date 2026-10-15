using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text coinText;
    public PlayerController player;

    private void Start()
    {
        player = ServiceHub.Instance.playerController;
    }

    private void Update()
    {
        coinText.text = $"Coins: {player.coins}";
    }

}
