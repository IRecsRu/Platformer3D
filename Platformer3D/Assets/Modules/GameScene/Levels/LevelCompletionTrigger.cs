using UnityEngine;

namespace Modules.GameScene
{
    public class LevelCompletionTrigger : MonoBehaviour
    {
        [SerializeField] private Level _level;
        [SerializeField] private BoxCollider _collider;

        private void OnTriggerEnter(Collider other)
        {
            _level.LevelCompleted();
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

