using System.Threading.Tasks;
using Modules.Core.Data;
using Modules.Core.Infrastructure.AddressablesServices;
using Modules.Core.Infrastructure.Services.PersistentProgress;
using Modules.Core.StaticData;

namespace Modules.Core.Infrastructure.Services.SaveLoad
{
	public class ProgressLoader
	{
		private const string PlayerStaticDataKey = "PlayerData";
		private const string InitialLevel = "1";
		private const string InitialStorage = "Test";
		
		private readonly ISaveLoadService _saveLoadService;
		private readonly IPersistentProgressService _progressService;
		
		public ProgressLoader(ISaveLoadService saveLoadService, IPersistentProgressService progressService)
		{
			_saveLoadService = saveLoadService;
			_progressService = progressService;
		}
		
		public async Task<PlayerProgress> LoadProgressOrInitNew()
		{
			_progressService.Progress = 
				_saveLoadService.LoadProgress() 
				?? await NewProgress();

			return _progressService.Progress;
		}

		private async Task<PlayerProgress> NewProgress()
		{
			PlayerStaticData playerStaticData = await AddressablesAssetLoader.LoadAssetAsync<PlayerStaticData>(PlayerStaticDataKey);
			var progress =  new PlayerProgress(InitialStorage,InitialLevel, playerStaticData);

			return progress;
		}
	}
}