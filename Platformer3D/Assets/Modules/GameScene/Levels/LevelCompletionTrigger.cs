using Modules.Core.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Modules.GameScene.Levels
{
    public class LevelCompletionTrigger : LevelTrigger
    {
        private Level _level;
        private ISaveLoadService _saveLoadService;
        
        public void Construct(ISaveLoadService saveLoadService, Level level)
        {
            _level = level;
            _saveLoadService = saveLoadService;
        }

        protected override void TriggerEnter(Collider other)
        {
            _level.LevelCompleted();
            _saveLoadService.SaveProgress();
            gameObject.SetActive(false);
        }
    }

}

