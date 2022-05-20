using System.Collections.Generic;
using UnityEngine;

namespace Modules.GameScene.Levels
{
	[CreateAssetMenu(fileName = "LevelStorage", menuName = "LevelStorage")]
	public class LevelStorage : ScriptableObject
	{
		public string StorageName;
		public List<string> LevelsName;
	}
}