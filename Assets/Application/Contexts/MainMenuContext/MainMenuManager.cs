using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Application.Contexts.MainMenuContext
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private List<UIPanel> _panels;
        public void OpenGameplay()
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
