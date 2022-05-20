using System.Collections;
using System.Collections.Generic;
using Modules.Core.Infrastructure.States;
using UnityEngine;
using Zenject;

public class MenuButton : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;

    [Inject]
    public void Constructor(GameStateMachine stateMachine) =>
        _gameStateMachine = stateMachine;

    public async void OnClickable() =>
        await _gameStateMachine.Enter<MaineMenuState>();
}
