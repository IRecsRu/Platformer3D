using System.Collections.Generic;
using Modules.Core.Infrastructure.Services.SaveLoad;

namespace Modules.Core.Infrastructure.Services.PersistentProgress
{
	public class StorageProgressWriters : IStorageProgressWriters
	{
		public IReadOnlyList<ISavedProgress> ProgressWriters => _progressWriters;

		private readonly List<ISavedProgress> _progressWriters = new();

		public void RegisterWriters(ISavedProgress savedProgress) =>
			_progressWriters.Add(savedProgress);
	}

}
