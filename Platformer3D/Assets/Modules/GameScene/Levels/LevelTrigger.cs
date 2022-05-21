using UnityEngine;

namespace Modules.GameScene.Levels
{
	public abstract class LevelTrigger : MonoBehaviour
	{
		[SerializeField] private BoxCollider _collider;
		public BoxCollider Collider => _collider;
        
		private void OnTriggerEnter(Collider other) =>
			TriggerEnter(other);

		protected abstract void TriggerEnter(Collider other);
	}
}