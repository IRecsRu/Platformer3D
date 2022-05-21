using Modules.GameScene.Levels;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomEditor(typeof(SpawnPoint))]
	public class SpawnPointEditor : UnityEditor.Editor
	{
		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
		public static void RenderCustomGizmo(SpawnPoint spawner, GizmoType gizmo)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(spawner.transform.position, 0.5f);
		}
	}
}