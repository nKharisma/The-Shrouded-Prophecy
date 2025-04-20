using UnityEngine;

public class FollowCompanion : MonoBehaviour
{
    private Transform companionTransform; // Reference to the companion's Transform

	private GameObject companion;
	private void Awake()
	{
		companion = GameObject.FindWithTag("Healer");
		if (companion == null)
		{
			Debug.LogError("Companion not found in the scene.");
			return;
		}
		companionTransform = companion.transform; // Get the Transform of the companion
	}

	private void Start()
	{
		if (companionTransform == null)
		{
			Debug.LogError("Companion Transform is not set. Make sure the companion is assigned correctly.");
		}
	}

    private void Update()
    {
        if (companionTransform != null)
        {
            // Update the position of this GameObject to match the companion's position
            transform.position = companionTransform.position;
        }
    }
}