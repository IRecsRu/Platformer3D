using System;
using Modules.Core.Data;
using Modules.Core.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Modules.Core.Infrastructure.Services.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string ProgressKey = "ProgressKey";
    
    private readonly IPersistentProgressService _progressService;
    private readonly IStorageProgressWriters _storageProgressWriters;
    
    public SaveLoadService(IPersistentProgressService progressService, IStorageProgressWriters storageProgressWriters)
    {
      _progressService = progressService;
      _storageProgressWriters = storageProgressWriters;
    }

    public void SaveProgress()
    {
      foreach (ISavedProgress progressWriter in _storageProgressWriters.ProgressWriters)
        progressWriter.UpdateProgress(_progressService.Progress);

      _progressService.Progress.IsClean = false;
      
      PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
    }

    public PlayerProgress LoadProgress()
    {
      return PlayerPrefs.GetString(ProgressKey)?
        .ToDeserialized<PlayerProgress>();
    }
  }

}