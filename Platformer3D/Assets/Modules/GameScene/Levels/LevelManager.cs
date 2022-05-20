using System.Threading.Tasks;
using Modules.Core.Data;
using Modules.Core.Infrastructure.AddressablesServices;
using Modules.Core.Infrastructure.LevelLoader;
using Modules.Core.Infrastructure.Services.PersistentProgress;
using Modules.Core.PlayerLogic;
using UnityEngine;

namespace Modules.GameScene
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

		public async Task Initialization(string sceneName,string storageName)
		{
			_levelStorage = await AddressablesAssetLoader.LoadAssetAsync<LevelStorage>(storageName);
			
			if(_currentLevel != null)
				await LevelChange();
			else
				await Load(GetLevelName(sceneName));
		}

		private async Task LevelChange()
		{
			await _loadingCurtainShower.StartShow(this);

			_currentLevel.Completed -= OnLevelCompleted;
			Object.Destroy(_currentLevel.gameObject);
			
			string levelName = GetLevelName();
			
			await Load(levelName);
			
			_loadingCurtainShower.Hide(this);
		}

		private async Task Load(string levelName)
		{
			_currentLevel = await _levelFactory.CreateLevel(levelName);
			_currentLevel.Completed += OnLevelCompleted;
			_player.Warp(_currentLevel.SpawnPoint.position);
		}

		private string GetLevelName(string name)
		{
			int currentIndex = _levelStorage.LevelsName.IndexOf(name);
			return GetLevelName(currentIndex);
		}
		
		private string GetLevelName()
		{
			int currentIndex = _levelStorage.LevelsName.IndexOf(_currentLevel.Name);
			Debug.Log(currentIndex);
			currentIndex = currentIndex+ 1;
			Debug.Log(currentIndex);
			return GetLevelName(currentIndex);
		}

		private string GetLevelName(int currentIndex)
		{
			if(currentIndex >= _levelStorage.LevelsName.Count)
				currentIndex = 0;

			string sceneName = _levelStorage.StorageName + _levelStorage.LevelsName[currentIndex];
			Debug.Log(sceneName);
			
			return sceneName;
		}
		
		private async void OnLevelCompleted() =>
			await LevelChange();
		
		public void LoadProgress(PlayerProgress progress)
		{
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			if(progress.LastLevels != _currentLevel.Name)
			{
				progress.WorldData.PositionOnLevel = new PositionOnLevel();
				progress.LastStorage = _levelStorage.StorageName;
				progress.WorldData.PositionOnLevel = new();
				progress.LastLevels = _currentLevel.Name;
			}
		}
	}
}