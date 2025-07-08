using UnityEngine;
using UnityEngine.UI;

public class PauseBtn : MonoBehaviour
{
    private Button _button;
    private Image _image;

    [SerializeField] private Sprite _pauseImage;
    [SerializeField] private Sprite _playImage;

    private bool _isPause;
    private float originTimeScale;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _isPause = false;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ReleaseBtn);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ReleaseBtn);
    }

    private void ReleaseBtn()
    {
        if (_isPause)
        {
            _image.sprite = _playImage;
            Time.timeScale = originTimeScale;
        }
        else
        {
            _image.sprite = _pauseImage;
            originTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        _isPause = !_isPause;
    }

}
