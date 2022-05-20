using UnityEngine;

namespace Modules.Core.PlayerLogic
{
	public class GroundCheck : MonoBehaviour
	{
		[SerializeField] private float _distToGround;

		public bool IsGrounded => CheckGrounded();
		
		private bool CheckGrounded() =>
			Physics.Raycast(transform.position + (-transform.forward * 2), transform.TransformDirection(-Vector3.up), out RaycastHit hit,_distToGround + 0.1f);
	}
}