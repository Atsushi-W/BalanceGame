using UnityEngine;
using TMPro;

/// <summary>
/// �C���Q�[���J�n���̃X���[�J�E���g�\���̃R���|�[�l���g
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
