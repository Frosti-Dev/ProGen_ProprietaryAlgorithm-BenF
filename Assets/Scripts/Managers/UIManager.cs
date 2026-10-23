using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerController player;

    private void Start()
    {
        player = ServiceHub.Instance.playerController;
    }

    private void Update()
    {
        //coinText.text = $"Coins: {player.coins}";
    }

}
