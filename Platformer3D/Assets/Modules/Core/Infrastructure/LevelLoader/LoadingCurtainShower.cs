using System.Collections.Generic;
using System.Threading.Tasks;
using Modules.Core.Infrastructure.AddressablesServices;
using UnityEngine;

namespace Modules.Core.Infrastructure.LevelLoader
{
	public class LoadingCurtainShower
	{
		private const string LoadingCurtainKey = "LoadingCurtain";
		private LoadingCurtain _loadingCurtain;

		private readonly List<IUserCurtainShower> _users = new List<IUserCurtainShower>();
		private bool _isShow;
		
		public LoadingCurtainShower() =>
			BindLoadingCurtain();

		private async void BindLoadingCurtain()
		{
			AddressablesGameObjectLoader loader = new();
			GameObject loadingCurtainObject = await loader.InstantiateGameObject(null, LoadingCurtainKey);
			_loadingCurtain = loadingCurtainObject.GetComponent<LoadingCurtain>();
			_loadingCurtain.Hide();
		}

		public async Task StartShow(IUserCurtainShower user)
		{
			_users.Add(user);
			
			if(_isShow)
				return;

			_isShow = true;
			
			while(_loadingCurtain == null)
				await Task.Delay(100);

			_loadingCurtain.Show();
		}
		
		public void Hide(IUserCurtainShower user)
		{
			_users.Remove(user);

			if(_users.Count == 0)
			{
				_isShow = false;
				_loadingCurtain.Hide();
			}
		}
	}

	public interface IUserCurtainShower
	{
		
	}
}