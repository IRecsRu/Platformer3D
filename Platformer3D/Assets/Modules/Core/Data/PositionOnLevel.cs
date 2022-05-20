using System;

namespace Modules.Core.Data
{
  [Serializable]
  public class PositionOnLevel
  {
    public Vector3Data Position;

    public PositionOnLevel(Vector3Data position)
    {
      Position = position;
    }
    
    public PositionOnLevel()
    {
      Position = new Vector3Data(0,0,0);
    }
  }
}