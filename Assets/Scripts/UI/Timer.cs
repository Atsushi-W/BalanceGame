using UnityEngine;
using TMPro;

/// <summary>
/// インゲーム中のタイマー表示のコンポーネント
/// </summary>
public class Timer : MonoBehaviour
{
    private TextMeshProUGUI _timeText;

    private void Awake()
    {
        _timeText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnTimeUpdate += UpdateTime;
    }

    private void UpdateTime(int time)
    {
        int minutes = time / 60;
        int seconds = time % 60;

        if (seconds < 0) seconds = 0;

        _timeText.text = minutes.ToString("00") + "：" + seconds.ToString("00");
    }
}
