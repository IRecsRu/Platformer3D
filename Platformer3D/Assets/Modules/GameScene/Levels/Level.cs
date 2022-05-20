using System;
using UnityEngine;

namespace Modules.GameScene.Levels
{
	public class Level : MonoBehaviour
	{
		[SerializeField] private string _name;
		[SerializeField] private Transform _spawnPoint;
		
		public event Action Completed;
		
		public Transform SpawnPoint => _spawnPoint;
		public string Name => _name;

		public void LevelCompleted() =>
			Completed?.Invoke();
	}
}