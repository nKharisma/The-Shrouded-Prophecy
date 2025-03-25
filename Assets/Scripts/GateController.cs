using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private GameObject gateObject; // Reference to the gate (e.g., model or sprite)
    private bool isGateOpen = false;
    private float animationDuration = 1.5f; // Duration of the animation in seconds

    private void Start()
    {
        // Ensure the gate starts closed
        gateObject.SetActive(true); // True means the gate is "closed"
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
    }

    private void StartQuest(string id) //fix this later
    {
        Quest quest = QuestManager.instance.GetQuestById("FirstImpressionsSO");
        if (quest.questState == QuestState.In_Progress)
        {
            StartCoroutine(AnimateGateOpen());
            isGateOpen = true;
        }
    }

    private IEnumerator AnimateGateOpen()
    {
        Vector3 startPosition = gateObject.transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + 2.6f, startPosition.z);
        float elapsedTime = 0.0f;

        while (elapsedTime < animationDuration)
        {
            gateObject.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gateObject.transform.position = endPosition; // Ensure the gate reaches the final position
    }
}
