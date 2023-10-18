using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// オブジェクトのシェイク用スクリプト
/// </summary>
public class Shake : MonoBehaviour
{
    [SerializeField]
    private GameObject _shakeObject;
    [SerializeField]
    [Range(0, 1)]
    private float _range;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int _shakeNum;
    [SerializeField]
    private bool _shakeflag;
    // シェイクできる回数
    [SerializeField]
    private int _shakeCount;

    private int _initialShakeCount;
    private float _initialPosition;
    private float _movePosition;
    private float _minPosition;
    private float _maxPosition;
    // 切り替えフラグ(false:負,true:正)
    private bool _directionflag;
    // シェイクカウント表示用
    private TextMeshProUGUI _shaketext;

    private void Awake()
    {
        _shaketext = GetComponent<TextMeshProUGUI>();
        _initialShakeCount = _shakeCount;
    }

    private void Start()
    {
        _initialPosition = _shakeObject.transform.localPosition.x;
        _minPosition = _initialPosition - _range;
        _maxPosition = _initialPosition + _range;
        _directionflag = false;
        _shaketext.text = _shakeCount.ToString();
    }

    private void Update()
    {
        if (GameManager.Instance.Startflag && Input.GetMouseButtonDown(1) && _shakeflag && _shakeCount > 0)
        {
            StartCoroutine("ShakePosition");
        }
    }

    /// <summary>
    /// シェイクカウントのリセット
    /// </summary>
    public void RestShakeCount()
    {
        _shakeCount = _initialShakeCount;
        _shaketext.text = _shakeCount.ToString();
    }

    /// <summary>
    /// ポジションをシェイクする(横方向)
    /// </summary>
    IEnumerator ShakePosition()
    {
        // シェイクフラグをfalseに設定
        _shakeflag = false;

        // シェイク音声を流す
        AudioManager.Instance.PlaySE(AudioManager.SEName.Shake);

        // 設定した回数分繰り返す
        for (int i = 0;i < _shakeNum; i++)
        {
            // 範囲を超えた場合は方向を切り替える
            if (_movePosition <= _minPosition || _maxPosition <= _movePosition)
            {
                _directionflag = !_directionflag;
            }

            // 移動先を設定
            _movePosition = _directionflag ? _movePosition + (_speed * Time.deltaTime) : _movePosition - (_speed * Time.deltaTime);
            _movePosition = Mathf.Clamp(_movePosition, _minPosition, _maxPosition);

            // ポジションをセット
            _shakeObject.transform.localPosition = new Vector3(_movePosition, _shakeObject.transform.localPosition.y, _shakeObject.transform.localPosition.z);

            // 1フレーム進める
            yield return null;
        }

        // シェイクフラグをtrueに設定
        _shakeflag = true;

        // シェイクできる回数を減らす;
        _shakeCount--;

        // シェイクカウント表示更新
        _shaketext.text = _shakeCount.ToString();

        // ポジションを元に戻す
        ResetPosition();
    }

    /// <summary>
    /// ポジションのリセット
    /// </summary>
    private void ResetPosition()
    {
        _shakeObject.transform.localPosition = new Vector3(_initialPosition, _shakeObject.transform.localPosition.y, _shakeObject.transform.localPosition.z);
    }
}
