using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;

    [SerializeField] private string _leftWinText;
    [SerializeField] private string _rightWinText;

    [SerializeField] private string _lobbySceneName;

    private CanvasGroup _group;

    private void Awake()
    {
        _group = GetComponent<CanvasGroup>();
    }

    public void SetWin(Camp camp)
    {
        switch (camp)
        {
            case Camp.Player:
                _titleText.text = _leftWinText;
                break;
            case Camp.Enemy:
                _titleText.text = _rightWinText;
                break;
        }
    }

    public void Open()
    {
        _group.DOFade(1, 1f).OnComplete(() => Time.timeScale = 0);
        _group.blocksRaycasts = true;
        _group.interactable = true;
    }

    public void HandleLobby()
    {
        SceneManager.LoadScene(_lobbySceneName);
    }
    public void HandleReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void HandleExit()
    {
        Application.Quit();
    }
}
