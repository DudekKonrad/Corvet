using UnityEngine;
using UnityEngine.SceneManagement;

namespace Application.Contexts.GameplayContext
{
    public class GameplayManager : MonoBehaviour
    {
        public void OpenMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
