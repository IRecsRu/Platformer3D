using System.Collections.Generic;
using UnityEngine;

namespace Modules.GameScene.Levels
{
	[CreateAssetMenu(fileName = "LevelStorage", menuName = "LevelStorage")]
	public class LevelStorage : ScriptableObject
	{
		[SerializeField] private string _name;
		[SerializeField] private List<string> _levelsName;

		private string _lastLevelName;

		public string Name => _name;
		
		private void UpdateSave() =>
			PlayerPrefs.SetString(_name, _lastLevelName);

		private void LadeSave()
		{
			if(!PlayerPrefs.HasKey(_name))
				PlayerPrefs.SetString(_name, _levelsName[0]);

			_lastLevelName = PlayerPrefs.GetString(_name);
		}

		public string GetNextLevel()
		{
			if(string.IsNullOrEmpty(_lastLevelName))
				LadeSave();
			else
				ChangeLevel();

			return _name + _lastLevelName;
		}

		public string GetLevel()
		{
			if(string.IsNullOrEmpty(_lastLevelName))
				LadeSave();
			
			return _name + _lastLevelName;
		}
		
		private void ChangeLevel()
		{
			int index = _levelsName.IndexOf(_lastLevelName);
			index = index + 1;

			if(index >= _levelsName.Count)
				index = 0;

			_lastLevelName = _levelsName[index];
			UpdateSave();
		}
	}
}