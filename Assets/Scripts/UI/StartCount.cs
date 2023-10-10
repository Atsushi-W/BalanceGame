using UnityEngine;
using TMPro;

/// <summary>
/// インゲーム開始時のスリーカウント表示のコンポーネント
/// </summary>
public class StartCount : MonoBehaviour
{
    private TextMeshProUGUI _countText;

    private void Awake()
    {
        _countText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStartCountUpdate += UpdateCount;
    }

    private void UpdateCount(int count)
    {
        if (count == 0)
        {
            _countText.text = "";
            AudioManager.Instance.PlaySE(AudioManager.SEName.GameStart);
        }
        else
        {
            _countText.text = count.ToString();
            AudioManager.Instance.PlaySE(AudioManager.SEName.StartTimer);
        }
    }
}
