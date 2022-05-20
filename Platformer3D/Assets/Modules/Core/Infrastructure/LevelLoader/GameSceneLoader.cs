using System.Threading.Tasks;
using Cinemachine;
using Modules.Core.Data;
using Modules.Core.Infrastructure.AddressablesServices;
using Modules.Core.Infrastructure.Services.PersistentProgress;
using Modules.Core.Infrastructure.Services.SaveLoad;
using Modules.Core.PlayerLogic;
using Modules.GameScene;
using Modules.GameScene.Levels;
using UnityEngine;
using Zenject;

namespace Modules.Core.Infrastructure.LevelLoader
{
	public class GameSceneLoader : MonoBehaviour, ILevelLoader
	{
		private const string HudKey = "Hud";
		
		[SerializeField] private CinemachineFreeLook  _freeLook;
		
		private readonly AddressablesGameObjectLoader _loader = new();
		private PlayerFactory _playerFactory;
		private ProgressLoader _progressLoader;
		private IStorageProgress _storageProgress;
		private LevelManager _levelManager;
		
		[Inject]
		private void Constructor( PlayerFactory playerFactory, ProgressLoader progressLoader, IStorageProgress storageProgress, LevelManager levelManager)
		{
			_playerFactory = playerFactory;
			_progressLoader = progressLoader;
			_storageProgress = storageProgress;
			_levelManager = levelManager;
		}

		public async Task Initialization()
		{
			PlayerProgress progress = await _progressLoader.LoadProgressOrInitNew();
			
			await InitHud();
			GameObject player = await InitPlayer();
			await InitLevelManager(player, progress);

			_storageProgress.UpdateProgress();
		}
		
		private async Task<GameObject> InitPlayer() =>
			await _playerFactory.CreatePlayer(_freeLook, _storageProgress);

		private async Task InitHud() =>
			await _loader.InstantiateGameObject(null, HudKey);

		private async Task InitLevelManager(GameObject player, PlayerProgress progress)
		{
			PlayerMove playerMove = player.GetComponent<PlayerMove>();
			_levelManager.SetPlayer(playerMove);

			await _levelManager.Initialization(progress.LastLevels, progress.LastStorage);
			_storageProgress.RegisterProgressWatchers(_levelManager);
		}
	}

}