using UnityEngine;

public class ResetButton : MonoBehaviour
{
	public static void ClearPrefs()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
	}
}
