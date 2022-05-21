using Modules.Core.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Modules.GameScene.Levels
{
	public class SaveTrigger : LevelTrigger
	{
		private ISaveLoadService _saveLoadService;

		[SerializeField] private GameObject _fx;

		public void Construct(ISaveLoadService saveLoadService) =>
			_saveLoadService = saveLoadService;

		protected override void TriggerEnter(Collider other)
		{
			_saveLoadService.SaveProgress();
			Instantiate(_fx, other.transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}
	}
}