using Modules.Core.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Modules.MainMenuScene
{
	public class LoadGameButton : MonoBehaviour
	{
		private GameStateMachine _gameStateMachine;

		[Inject]
		public void Constructor(GameStateMachine stateMachine) =>
			_gameStateMachine = stateMachine;

		public async void OnClickable()
		{
			await _gameStateMachine.Enter<GameState>();
		}
	}
}