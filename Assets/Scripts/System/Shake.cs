using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// �I�u�W�F�N�g�̃V�F�C�N�p�X�N���v�g
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
    // �V�F�C�N�ł����
    [SerializeField]
    private int _shakeCount;

    private int _initialShakeCount;
    private float _initialPosition;
    private float _movePosition;
    private float _minPosition;
    private float _maxPosition;
    // �؂�ւ��t���O(false:��,true:��)
    private bool _directionflag;
    // �V�F�C�N�J�E���g�\���p
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
    /// �V�F�C�N�J�E���g�̃��Z�b�g
    /// </summary>
    public void RestShakeCount()
    {
        _shakeCount = _initialShakeCount;
        _shaketext.text = _shakeCount.ToString();
    }

    /// <summary>
    /// �|�W�V�������V�F�C�N����(������)
    /// </summary>
    IEnumerator ShakePosition()
    {
        // �V�F�C�N�t���O��false�ɐݒ�
        _shakeflag = false;

        // �V�F�C�N�����𗬂�
        AudioManager.Instance.PlaySE(AudioManager.SEName.Shake);

        // �ݒ肵���񐔕��J��Ԃ�
        for (int i = 0;i < _shakeNum; i++)
        {
            // �͈͂𒴂����ꍇ�͕�����؂�ւ���
            if (_movePosition <= _minPosition || _maxPosition <= _movePosition)
            {
                _directionflag = !_directionflag;
            }

            // �ړ����ݒ�
            _movePosition = _directionflag ? _movePosition + (_speed * Time.deltaTime) : _movePosition - (_speed * Time.deltaTime);
            _movePosition = Mathf.Clamp(_movePosition, _minPosition, _maxPosition);

            // �|�W�V�������Z�b�g
            _shakeObject.transform.localPosition = new Vector3(_movePosition, _shakeObject.transform.localPosition.y, _shakeObject.transform.localPosition.z);

            // 1�t���[���i�߂�
            yield return null;
        }

        // �V�F�C�N�t���O��true�ɐݒ�
        _shakeflag = true;

        // �V�F�C�N�ł���񐔂����炷;
        _shakeCount--;

        // �V�F�C�N�J�E���g�\���X�V
        _shaketext.text = _shakeCount.ToString();

        // �|�W�V���������ɖ߂�
        ResetPosition();
    }

    /// <summary>
    /// �|�W�V�����̃��Z�b�g
    /// </summary>
    private void ResetPosition()
    {
        _shakeObject.transform.localPosition = new Vector3(_initialPosition, _shakeObject.transform.localPosition.y, _shakeObject.transform.localPosition.z);
    }
}
