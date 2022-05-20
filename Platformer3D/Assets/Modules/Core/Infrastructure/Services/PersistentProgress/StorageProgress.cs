using System.Collections.Generic;
using Modules.Core.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Modules.Core.Infrastructure.Services.PersistentProgress
{
	public class StorageProgress : IStorageProgress
	{
		private readonly IStorageProgressWriters _storageProgressWriters;
		private readonly IPersistentProgressService _progressService;

		private List<ISavedProgressReader> _progressReaders = new();

		public StorageProgress(IStorageProgressWriters storageProgressWriters, IPersistentProgressService progressService)
		{
			_storageProgressWriters = storageProgressWriters;
			_progressService = progressService;
		}

		public void UpdateProgress()
		{
			foreach(ISavedProgressReader reader in _progressReaders)
				reader.LoadProgress(_progressService.Progress);
		}

		public void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach(ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
				Register(progressReader);
		}
		
		public void RegisterProgressWatchers(ISavedProgress savedProgress)
		{
			_storageProgressWriters.RegisterWriters(savedProgress);
			_progressReaders.Add(savedProgress);
		}

		private void Register(ISavedProgressReader progressReader)
		{
			if(progressReader is ISavedProgress progressWriter)
				_storageProgressWriters.RegisterWriters(progressWriter);

			_progressReaders.Add(progressReader);
		}
	}

}