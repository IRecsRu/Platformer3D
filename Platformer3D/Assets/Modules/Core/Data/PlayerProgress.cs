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
    public HealthState HealthState;
    public MoveStats MoveStats;
    public WorldData WorldData;

    public PlayerProgress(string initialStorage, PlayerStaticData playerStaticData)
    {
      IsClean = true;
      
      LastStorage = initialStorage;
      
      HealthState = playerStaticData.HealthState;
      HealthState.ResetHP();
      
      MoveStats = playerStaticData.MoveStats;
      WorldData = new WorldData();
    }
  }
}