using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int stageIndex;
    public bool check_p1 = false, check_p2 = false;
    public bool hasKey = false;
    public GameObject p1, p2;
    public GameObject[] stages;

    public void NextStage()
    {
        if (check_p1 && check_p2 && hasKey && stageIndex < stages.Length - 1)
        {
            stages[stageIndex].SetActive(false);
            stages[++stageIndex].SetActive(true);
            PlayerReposition();

            Debug.Log("Clear this stage!");
            Debug.Log(stageIndex);
        }
        else
        {
            //Game Clear
            //Time.timeScale = 0;
        }
    }

    private void PlayerReposition()
    {
        p1.transform.position = stages[stageIndex].transform.GetChild(0).position + new Vector3(1f, 0);
        p2.transform.position = stages[stageIndex].transform.GetChild(0).position - new Vector3(1f, 0);
        p1.GetComponent<Player1_Controller>().VelocityZero();
        p2.GetComponent<Player2_Controller>().VelocityZero();
    }
}
