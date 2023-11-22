using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    public float timeScale;
    void Update()
    {
        Time.timeScale = timeScale;
    }
}
