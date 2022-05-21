using UnityEngine;
using Modules.Core.Data;
using Modules.Core.Infrastructure.Services.PersistentProgress;
using Modules.Core.Services.Input;

namespace Modules.Core.PlayerLogic
{
	[RequireComponent(typeof(CharacterController), typeof(GroundCheck))]
	public class PlayerMove : MonoBehaviour, ISavedProgress
	{
		private const int JumpAcceleration = 2;
		[SerializeField] private GameObject _fx;
		
		private CharacterController _characterController;
		private MoveStats _moveStats;

		private IInputService _inputService;
		private Camera _camera;
		private GroundCheck _groundCheck;

		private Vector3 _playerVelocity;
		private float _gravityValue;

		public void Constructor(IInputService inputService)
		{
			_gravityValue = Physics.gravity.y;
			_inputService = inputService;
			_characterController = GetComponent<CharacterController>();
			_groundCheck = GetComponent<GroundCheck>();
		}

		private void Start() =>
			_camera = Camera.main;

		private void Update()
		{
			if(CheckInputService())
				return;
			
			TryMove();
			TryJump();
		}
		
		private bool CheckInputService() =>
			_inputService == null;

		private void TryMove()
		{
			if(_inputService.Axis.sqrMagnitude > Constants.Epsilon)
			{
				Vector3 movementVector = _camera.transform.TransformDirection(_inputService.Axis);

				if(!_groundCheck.IsGrounded)
					movementVector.y *= JumpAcceleration;
				
				movementVector.y = 0;
				movementVector.Normalize();

				transform.forward = movementVector;

				movementVector *= _moveStats.MoveSpeed;


				_characterController.Move(movementVector * Time.deltaTime);
			}
		}

		private void TryJump()
		{
			if(_inputService.IsJumpButtonUp() && _groundCheck.IsGrounded)
				Jump();

			GravityRestrictions();
			_characterController.Move(_playerVelocity * Time.deltaTime);
		}
		
		private void GravityRestrictions()
		{
			_playerVelocity.y += _gravityValue * Time.deltaTime;
			
			if(_playerVelocity.y < _gravityValue)
				_playerVelocity.y = _gravityValue;
		}

		private void Jump()
		{
			_playerVelocity.y = 0f;
			_playerVelocity.y += Mathf.Sqrt(_moveStats.JumpHeight * -1 * _gravityValue);
			Instantiate(_fx, transform.position, Quaternion.identity);
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			progress.WorldData.PositionOnLevel.Position = transform.position.AsVectorData();
			progress.MoveStats = _moveStats;
		}

		public void LoadProgress(PlayerProgress progress)
		{
			_moveStats = progress.MoveStats;

			if(progress.IsClean)
				return;
			
			Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
			if(savedPosition != null)
				Warp(to: savedPosition);
		}

		private void Warp(Vector3Data to)
		{
			_characterController.enabled = false;
			transform.position = to.AsUnityVector().AddY(_characterController.height * 5);
			_characterController.enabled = true;
		}
		
		public void Warp(Vector3 to) =>
			Warp(new Vector3Data(to.x, to.y, to.z));
	}
}