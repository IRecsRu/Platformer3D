using Modules.Core.Infrastructure.LevelLoader;
using Zenject;

namespace Modules.GameScene
{
	public class ProjectInstallers  : MonoInstaller
	{
		public override void InstallBindings() =>
			Container.Bind<LoadingCurtainShower>().FromNew().AsSingle();
	}
}