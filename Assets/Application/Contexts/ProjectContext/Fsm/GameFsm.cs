using System.Collections.Generic;
using Application.Contexts.ProjectContext.Fsm.GameStates;
using Application.Utils.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Application.Contexts.ProjectContext.Fsm
{
    public enum GameState
    {
        Init = 0,
        MainMenu = 1,
        GameplayHub = 2,
        Gameplay = 3
    }
    
    public class GameFsm : Fsm<GameState>, ITickable
    {
        [Inject]
        private void Construct()
        {
            Debug.Log($"Construct GameFsm");
            States.Add(GameState.MainMenu, new MainMenuState());
            CurrentState = States[GameState.MainMenu];
            CurrentState.EnterState();
        }

        public void Tick()
        {
            ExecuteCurrentState();
        }
    }
}