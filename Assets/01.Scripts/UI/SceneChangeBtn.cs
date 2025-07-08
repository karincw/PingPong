using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeBtn : MonoBehaviour
{
    [SerializeField] private int idx;

    public void SceneLoad()
    {
        SceneManager.LoadScene(idx);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
