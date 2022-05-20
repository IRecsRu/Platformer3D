using System.Threading.Tasks;
using Cinemachine;
using Modules.Core.Infrastructure.AddressablesServices;
using Modules.Core.Infrastructure.Services.PersistentProgress;
using Modules.Core.Services.Input;
using UnityEngine;

namespace Modules.Core.PlayerLogic
{
	public class PlayerFactory
	{
		private const string PlayerKey = "Player";
		
		private readonly IInputService _inputService;
		private readonly AddressablesGameObjectLoader _loaderGameObject = new();
		
		public PlayerFactory(IInputService inputService) =>
			_inputService = inputService;

		public async Task<GameObject> CreatePlayer(CinemachineFreeLook  freeLook, IStorageProgress storageProgress)
		{
			GameObject player = await _loaderGameObject.InstantiateGameObject(null, PlayerKey);
			player.GetComponent<PlayerMove>().Constructor(_inputService);
			player.GetComponent<PlayerAnimator>().Constructor(_inputService);
			SetCameraFollow(freeLook, player);
			storageProgress.RegisterProgressWatchers(player);
			return player;
		}
		
		private static void SetCameraFollow(CinemachineFreeLook freeLook, GameObject player)
		{
			freeLook.transform.position = player.transform.position;
			freeLook.LookAt = player.transform;
			freeLook.Follow = player.transform;
		}
	}
}