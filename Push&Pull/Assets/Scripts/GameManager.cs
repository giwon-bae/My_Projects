using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int stageIndex;
    public bool check_p1 = false, check_p2 = false;
    public bool hasKey = false;

    public void NextStage()
    {
        if (check_p1 && check_p2 && hasKey)
        {
            stageIndex++;
            Debug.Log("Clear this stage!");
        }
    }
}
