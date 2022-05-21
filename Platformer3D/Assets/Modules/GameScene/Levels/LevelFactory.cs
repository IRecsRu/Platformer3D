using System.Threading.Tasks;
using Modules.Core.Infrastructure.AddressablesServices;
using Modules.Core.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Modules.GameScene.Levels
{
	public class LevelFactory
	{
		private readonly AddressablesGameObjectLoader _loaderGameObject = new();
		readonly ISaveLoadService _saveLoadService;

		public LevelFactory(ISaveLoadService saveLoadService) =>
			_saveLoadService = saveLoadService;

		public async Task<Level> CreateLevel(string levelName)
		{
			GameObject levelObject = await _loaderGameObject.InstantiateGameObject(null, levelName);
			Level level = levelObject.GetComponent<Level>();

			foreach(SaveTrigger saveTrigger in levelObject.GetComponentsInChildren<SaveTrigger>())
				saveTrigger.Construct(_saveLoadService);

			foreach(LevelCompletionTrigger saveTrigger in levelObject.GetComponentsInChildren<LevelCompletionTrigger>())
				saveTrigger.Construct(_saveLoadService, level);

			SpawnPoint spawnPoint = levelObject.GetComponentInChildren<SpawnPoint>();
			spawnPoint.Construct(_saveLoadService);
			level.SetSpawnPoint(spawnPoint.transform);

			levelObject.transform.position = Vector3.zero;

			return levelObject.GetComponent<Level>();
		}
	}

}