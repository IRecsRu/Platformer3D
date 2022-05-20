using System.Threading.Tasks;
using Modules.Core.Infrastructure.AddressablesServices;
using Modules.Core.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Modules.GameScene
{
	public class LevelFactory
	{
		private readonly AddressablesGameObjectLoader _loaderGameObject = new();
		ISaveLoadService _saveLoadService;

		public LevelFactory(ISaveLoadService saveLoadService) =>
			_saveLoadService = saveLoadService;

		public async Task<Level> CreateLevel(string levelName)
		{
			GameObject level = await _loaderGameObject.InstantiateGameObject(null, levelName);

			foreach(SaveTrigger saveTrigger in level.GetComponentsInChildren<SaveTrigger>())
				saveTrigger.Construct(_saveLoadService);

			level.transform.position = Vector3.zero;
			
			return level.GetComponent<Level>();
		}
	}

}