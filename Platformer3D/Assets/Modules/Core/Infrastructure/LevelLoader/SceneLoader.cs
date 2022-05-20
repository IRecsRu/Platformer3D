using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Modules.Core.Infrastructure.LevelLoader
{
	public class SceneLoader : IUserCurtainShower
	{
		private readonly LoadingCurtainShower _loadingCurtainShower;

		public SceneLoader(LoadingCurtainShower loadingCurtainShower)
		{
			_loadingCurtainShower = loadingCurtainShower;
		}
		
		public async Task StartLoad(Func<Task<SceneInstance>> task)
		{
			await _loadingCurtainShower.StartShow(this);

			SceneInstance sceneInstance = await task.Invoke();

			await Initialization(sceneInstance);
		}

		private async Task Initialization(SceneInstance sceneInstance)
		{
			Scene scene = sceneInstance.Scene;

			if(!TypeFinderOnScene.TryGetType(out ILevelLoader maineMenuLoader, scene))
				Debug.LogError($"There is no {nameof(ILevelLoader)} on the Scene {scene.name}");
			
			await maineMenuLoader.Initialization();
			
			Addressables.Release(sceneInstance);
			EndLoad();
		}

		private void EndLoad() =>
			_loadingCurtainShower.Hide(this);
	}

}