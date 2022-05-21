using Modules.GameScene.Levels;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomEditor(typeof(SaveTrigger))]
	public class SaveTriggerEditor : UnityEditor.Editor
	{
		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
		public static void RenderCustomGizmo(SaveTrigger trigger, GizmoType gizmo)
		{
			if(!trigger.Collider) return;

			Gizmos.color = new Color32(200, 30, 30, 130);
			Gizmos.DrawCube(trigger.transform.position + trigger.Collider.center, trigger.Collider.size);
		}
	}
}