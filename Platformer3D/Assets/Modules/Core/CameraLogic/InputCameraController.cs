using Cinemachine;
using UnityEngine;

namespace Modules.Core.CameraLogic
{
	public class InputCameraController : MonoBehaviour
	{
		private const string CameraHorizontal = "CameraHorizontal";
 
		private void Start () =>
			CinemachineCore.GetInputAxis = HandleAxisInputDelegate;

		float HandleAxisInputDelegate(string axisName)
		{
			if(axisName == CameraHorizontal)
				return SimpleInput.GetAxis(CameraHorizontal);
			
			return 0f;
		}
	}
}