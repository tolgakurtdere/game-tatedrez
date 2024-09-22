using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TK
{
    public class GameEndUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI endText;
        [SerializeField] private Button restartButton;

        private void Awake()
        {
            gameObject.SetActive(false);
            restartButton.onClick.AddListener(OnRestartButtonClick);
            GameManager.OnGameEnd += OnOnGameEnd;
        }

        private void OnRestartButtonClick()
        {
            gameObject.SetActive(false);
            GameManager.Instance.RestartGame();
        }

        private void OnOnGameEnd(Player winner)
        {
            endText.text = $"{winner.Name} Wins!";
            gameObject.SetActive(true);
        }
    }
}