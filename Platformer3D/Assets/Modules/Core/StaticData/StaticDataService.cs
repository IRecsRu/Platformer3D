using System.Threading.Tasks;
//using Modules.Core.Infrastructure.AddressablesServices;

namespace Modules.Core.StaticData
{
	public class StaticDataService : IStaticDataService
	{
		private const string PlayerStaticData = "PlayerStaticData";

		public async Task<PlayerStaticData> LoadPlayer()
		{
			//return await AddressablesAssetLoader.LoadAssetAsync<PlayerStaticData>(PlayerStaticData);
			return null;
		}
	}
}