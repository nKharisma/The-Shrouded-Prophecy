using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCTimer : MonoBehaviour
{
    private int waitSecondsInt;
    [SerializeField] public float waitSeconds;
    [SerializeField] TMP_Text text;

    private void FixedUpdate()
    {
        if (waitSeconds > 0)
        {
            waitSeconds -= Time.fixedDeltaTime;
            waitSecondsInt = (int)waitSeconds;
            text.text = waitSecondsInt.ToString();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
