using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OpenGameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
