using System;
using System.Collections.Generic;
using Modules.Core.StaticData;

namespace Modules.Core.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public bool IsClean;
    
    public string LastStorage;
    public string LastLevels;
    public HealthState HealthState;
    public MoveStats MoveStats;
    public WorldData WorldData;

    public PlayerProgress(string initialStorage, string initialLevel, PlayerStaticData playerStaticData)
    {
      IsClean = true;
      
      LastStorage = initialStorage;
      LastLevels = initialLevel;
      
      HealthState = playerStaticData.HealthState;
      HealthState.ResetHP();
      
      MoveStats = playerStaticData.MoveStats;
      WorldData = new WorldData();
    }
  }
}