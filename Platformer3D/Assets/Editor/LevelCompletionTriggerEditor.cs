using Modules.GameScene.Levels;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomEditor(typeof(LevelCompletionTrigger))]
	public class LevelCompletionTriggerEditor : UnityEditor.Editor
	{
		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
		public static void RenderCustomGizmo(LevelCompletionTrigger trigger, GizmoType gizmo)
		{
			if(!trigger.Collider) return;

			Gizmos.color = new Color32(30, 200, 30, 130);
			Gizmos.DrawCube(trigger.transform.position + trigger.Collider.center, trigger.Collider.size);
		}
	}

}