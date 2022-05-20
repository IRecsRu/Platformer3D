using UnityEngine;

namespace Modules.Core.Infrastructure.Services.PersistentProgress
{
	public interface IStorageProgress
	{
		void UpdateProgress();
		void RegisterProgressWatchers(GameObject gameObject);
		void RegisterProgressWatchers(ISavedProgress savedProgress);
	}
}