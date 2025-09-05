using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreEntity : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text playerScoreText;
    [SerializeField] private Button minus100Button;
    [SerializeField] private Button minus10Button;
    [SerializeField] private Button minus1Button;
    [SerializeField] private Button plus1Button;
    [SerializeField] private Button plus10Button;
    [SerializeField] private Button plus100Button;

    private Player player;
    
    public void Setup(Player playerToListen)
    {
        this.player = playerToListen;
        setupTexts();
        setupButtons();
        player.ScoreChanged += onScoreChanged;
        player.IsSpectatingChanged += onIsSpectatingChanged;
        gameObject.SetActive(!player.IsSpectating);
    }

    private void setupTexts()
    {
        string playerNameDisplayText = !player.Name.Equals(string.Empty) ? player.Name : "(no name)";
        playerNameText.text = playerNameDisplayText;
        playerScoreText.text = player.Score.ToString();
    }

    private void setupButtons()
    {
        bool isLocalPlayer = player.isLocalPlayer;
        if (isLocalPlayer)
        {
            minus100Button.onClick.AddListener(() => player.Score -= 100);
            minus10Button.onClick.AddListener(() => player.Score -= 10);
            minus1Button.onClick.AddListener(() => player.Score -= 1);
            plus1Button.onClick.AddListener(() => player.Score += 1);
            plus10Button.onClick.AddListener(() => player.Score += 10);
            plus100Button.onClick.AddListener(() => player.Score += 100);
        }
        minus100Button.gameObject.SetActive(isLocalPlayer);
        minus10Button.gameObject.SetActive(isLocalPlayer);
        minus1Button.gameObject.SetActive(isLocalPlayer);
        plus1Button.gameObject.SetActive(isLocalPlayer);
        plus10Button.gameObject.SetActive(isLocalPlayer);
        plus100Button.gameObject.SetActive(isLocalPlayer);
    }

    private void OnDestroy()
    {
        player.ScoreChanged -= onScoreChanged;
        player.IsSpectatingChanged -= onIsSpectatingChanged;
        unsetButtons();
        player = null;
    }

    private void unsetButtons()
    {
        minus100Button.onClick.RemoveAllListeners();
        minus10Button.onClick.RemoveAllListeners();
        minus1Button.onClick.RemoveAllListeners();
        plus1Button.onClick.RemoveAllListeners();
        plus10Button.onClick.RemoveAllListeners();
        plus100Button.onClick.RemoveAllListeners();
    }

    private void onScoreChanged(int _, int newValue)
    {
        playerScoreText.text = newValue.ToString();
    }

    private void onIsSpectatingChanged(bool newValue)
    {
        gameObject.SetActive(newValue);
    }
}
