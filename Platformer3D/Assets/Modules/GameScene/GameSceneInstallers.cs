using Modules.Core.Infrastructure.Services.PersistentProgress;
using Modules.Core.Infrastructure.Services.SaveLoad;
using Modules.Core.PlayerLogic;
using Modules.Core.Services.Input;
using UnityEngine;
using Zenject;

namespace Modules.GameScene
{
	public class GameSceneInstallers  : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindInputService();
			BindISaveLoadService();
			Container.Bind<PlayerFactory>().FromNew().AsSingle();
			Container.Bind<ProgressLoader>().FromNew().AsSingle();
			Container.Bind<LevelFactory>().FromNew().AsSingle();
			Container.Bind<LevelManager>().FromNew().AsSingle();
		}
	
		private void BindISaveLoadService()
		{
			Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
			Container.Bind<IStorageProgressWriters>().To<StorageProgressWriters>().AsSingle();
			Container.Bind<IStorageProgress>().To<StorageProgress>().AsSingle();
			Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
		}

		private void BindInputService()
		{
			if (Application.isMobilePlatform)
				Container.Bind<IInputService>().To<MobileInputService>().FromNew().AsSingle();
			else
				Container.Bind<IInputService>().To<StandaloneInputService>().FromNew().AsSingle();
		}

	}

}
