using UnityEngine;

namespace Modules.GameScene.Levels
{
    public class LevelCompletionTrigger : LevelTrigger
    {
        [SerializeField] private Level _level;
        
        protected override void TriggerEnter(Collider other)
        {
            _level.LevelCompleted();
            gameObject.SetActive(false);
        }
    }

}

