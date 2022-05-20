using Modules.Core.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Modules.GameScene
{
	public class SaveTrigger : MonoBehaviour
	{
		private ISaveLoadService _saveLoadService;

		[SerializeField] private GameObject _fx;
		[SerializeField] private BoxCollider _collider;

		public void Construct(ISaveLoadService saveLoadService) =>
			_saveLoadService = saveLoadService;

		private void OnTriggerEnter(Collider other)
		{
			_saveLoadService.SaveProgress();
			Instantiate(_fx, other.transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}

		private void OnDrawGizmos()
		{
			if(!_collider) return;

			Gizmos.color = new Color32(30, 200, 30, 130);
			Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
		}
	}
}