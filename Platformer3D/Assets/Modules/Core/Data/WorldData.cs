using System;
using UnityEngine;

namespace Modules.Core.Data
{
  [Serializable]
  public class WorldData
  {
    public PositionOnLevel PositionOnLevel;
    public string LevelName;
    
    public WorldData()
    {
      PositionOnLevel = new PositionOnLevel();
    }
  }
}