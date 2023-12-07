using Application.Utils.Patterns;
using UnityEngine;

namespace Application.Contexts.ProjectContext.Fsm.GameStates
{
    public class MainMenuState : IState<GameState>
    {
        public GameState StateType => GameState.MainMenu;
        public void EnterState()
        {
            Debug.Log($"Enter {StateType}");
        }

        public void UpdateState()
        {
            Debug.Log($"Update {StateType}");
        }

        public void ExitState()
        {
            Debug.Log($"Exit {StateType}");
        }

        public GameState GetNextState()
        {
            return StateType;
        }

        public void OnTriggerEnter(Collider2D collider2D)
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerStay(Collider2D collider2D)
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerExit(Collider2D collider2D)
        {
            throw new System.NotImplementedException();
        }
    }
}