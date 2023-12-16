using Application.Utils.Patterns;
using UnityEngine;

namespace Application.Contexts.ProjectContext.Fsm.GameStates
{
    public class GameplayState : IState<GameState>
    {
        public GameState StateType => GameState.Gameplay;
        public void EnterState()
        {
        }

        public void UpdateState()
        {
        }

        public void ExitState()
        {
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