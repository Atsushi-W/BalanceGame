using UnityEngine;
using TMPro;

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
            gameObject.SetActive(false);
            AudioManager.Instance.PlaySE(AudioManager.SEName.GameStart);
        }
        else
        {
            _countText.text = count.ToString();
            AudioManager.Instance.PlaySE(AudioManager.SEName.StartTimer);
        }
    }
}
