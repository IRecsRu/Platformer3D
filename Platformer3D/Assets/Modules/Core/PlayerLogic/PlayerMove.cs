/*
using Modules.Core.Infrastructure.Services.PersistentProgress;
*/

using UnityEngine;
using Modules.Core.Data;
using Modules.Core.Services.Input;
using UnityEngine.SceneManagement;

//using Zenject;

namespace Modules.Core.PlayerLogic
{
	[RequireComponent(typeof(CharacterController))]
	public class PlayerMove : MonoBehaviour //, ISavedProgress
	{
		private CharacterController _characterController;

		private MoveStats _moveStats = new();

		private IInputService _inputService;
		private Camera _camera;

		private Vector3 _playerVelocity;
		[SerializeField] private float _jumpHeight = 1.0f;
		private float _gravityValue = -9.81f;

		public void Constructor(IInputService inputService)
		{
			_inputService = inputService;
			_characterController = GetComponent<CharacterController>();
		}

		private void Start()
		{
			_gravityValue = Physics.gravity.y;
			_inputService = new StandaloneInputService();
			_characterController = GetComponent<CharacterController>();
			_camera = Camera.main;
		}

		private void Update()
		{
			TryMove();
			TryJump();
		}

		private void TryMove()
		{
			if(_inputService.Axis.sqrMagnitude > Constants.Epsilon)
			{
				Vector3 movementVector = _camera.transform.TransformDirection(_inputService.Axis);

				movementVector.y = 0;
				movementVector.Normalize();

				transform.forward = movementVector;

				movementVector *= _moveStats.MoveSpeed;

				_characterController.Move(movementVector * Time.deltaTime);
			}
		}

		private void TryJump()
		{
			if(_inputService.IsJumpButtonUp())
			{
				_playerVelocity.y = 0f;
				_playerVelocity.y += Mathf.Sqrt(_jumpHeight * -1 * _gravityValue);
				Debug.Log("Jump" + _playerVelocity.y);
			}
			
			_playerVelocity.y += _gravityValue * Time.deltaTime;
			_characterController.Move(_playerVelocity * Time.deltaTime);
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
			progress.MoveStats = _moveStats;
		}

		public void LoadProgress(PlayerProgress progress)
		{
			if(CurrentLevel() != progress.WorldData.PositionOnLevel.Level) return;

			_moveStats = progress.MoveStats;
			Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
			if(savedPosition != null)
				Warp(to: savedPosition);
		}

		private static string CurrentLevel() =>
			SceneManager.GetActiveScene().name;

		private void Warp(Vector3Data to)
		{
			_characterController.enabled = false;
			transform.position = to.AsUnityVector().AddY(_characterController.height);
			_characterController.enabled = true;
		}
	}
}