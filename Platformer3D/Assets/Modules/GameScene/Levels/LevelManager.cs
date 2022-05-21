using System.Threading.Tasks;
using Modules.Core.Data;
using Modules.Core.Infrastructure.AddressablesServices;
using Modules.Core.Infrastructure.LevelLoader;
using Modules.Core.Infrastructure.Services.PersistentProgress;
using Modules.Core.PlayerLogic;
using UnityEngine;

namespace Modules.GameScene.Levels
{
	public class LevelManager : ISavedProgress, IUserCurtainShower
	{
		private readonly LevelFactory _levelFactory;
		private readonly LoadingCurtainShower _loadingCurtainShower;
		
		private Level _currentLevel;
		private PlayerMove _player;
		
		private LevelStorage _levelStorage;
		
		public LevelManager(LevelFactory levelFactory, LoadingCurtainShower loadingCurtainShower)
		{
			_levelFactory = levelFactory;
			_loadingCurtainShower = loadingCurtainShower;
		}

		public void SetPlayer(PlayerMove player) =>
			_player = player;

		public async Task Initialization(string storageName)
		{
			_levelStorage = await AddressablesAssetLoader.LoadAssetAsync<LevelStorage>(storageName);
			
			if(_currentLevel != null)
				await LevelChange();
			else
				await Load(_levelStorage.GetLevel());
		}

		private async Task LevelChange()
		{
			await _loadingCurtainShower.StartShow(this);

			_currentLevel.Completed -= OnLevelCompleted;
			Object.Destroy(_currentLevel.gameObject);
			
			string levelName = _levelStorage.GetNextLevel();
			
			await Load(levelName);
			
			_loadingCurtainShower.Hide(this);
		}

		private async Task Load(string levelName)
		{
			_currentLevel = await _levelFactory.CreateLevel(levelName);
			_currentLevel.Completed += OnLevelCompleted;
			_player.Warp(_currentLevel.SpawnPoint.position);
		}

		private async void OnLevelCompleted() =>
			await LevelChange();
		
		public void LoadProgress(PlayerProgress progress){}

		public void UpdateProgress(PlayerProgress progress)
		{
			if(progress.WorldData.LevelName != _levelStorage.GetLevel())
			{
				if(!string.IsNullOrEmpty(progress.WorldData.LevelName))
					progress.WorldData.PositionOnLevel = new();
				
				progress.WorldData.LevelName = _levelStorage.GetLevel();
				progress.LastStorage = _levelStorage.Name;
			}
		}
	}
}