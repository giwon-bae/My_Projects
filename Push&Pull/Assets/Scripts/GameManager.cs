using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int curStage = 0;//->private
    public bool check_p1 = false, check_p2 = false;
    public bool hasKey = false;
    public GameObject p1, p2;
    public GameObject[] stages;
    public GameObject[] Panels;
    public GameObject startButton;
    public Image KeyImg;

    public int canEnterStage = 0;//->private
    private Player1_Controller p1Ctrl;
    private Player2_Controller p2Ctrl;

    private void Awake()
    {
        p1Ctrl = p1.GetComponent<Player1_Controller>();
        p2Ctrl = p2.GetComponent<Player2_Controller>();
    }

    private void Start()
    {
        Panels[0].SetActive(true);
    }

    public void GoToStageMenu()
    {
        Panels[0].SetActive(false);
        Panels[1].SetActive(true);
    }

    public void StartStage(int stage)
    {
        if (canEnterStage < stage)
        {
            return;
        }
        curStage = stage;
        Panels[1].SetActive(false);
        Panels[2].SetActive(true);
        stages[curStage].SetActive(true);

        InitializingObject();
    }

    public void NextStage()
    {
        if (check_p1 && check_p2 && hasKey && curStage < stages.Length - 1)
        {
            canEnterStage++;
            stages[curStage].SetActive(false);
            stages[++curStage].SetActive(true);
            InitializingObject();

            Debug.Log("Clear this stage!");
            Debug.Log(curStage);
        }
        else
        {
            //Game Clear
            //Time.timeScale = 0;
        }
    }

    public void ReturnMenu()
    {
        p1.SetActive(false);
        p2.SetActive(false);
        stages[curStage].SetActive(false);
        Panels[2].SetActive(false);
        Panels[1].SetActive(true);
    }

    public void GetKey()
    {
        hasKey = true;
        KeyImg.color = new Color(1, 1, 1, 1);
    }

    public void InitializingObject()
    {
        stages[curStage].transform.GetChild(2).gameObject.SetActive(true);
        hasKey = false;
        KeyImg.color = new Color(1, 1, 1, 0);

        p1.SetActive(true);
        p1Ctrl.isDead = false;
        p2.SetActive(true);
        p2Ctrl.isDead = false;
        PlayerReposition();
    }

    private void PlayerReposition()
    {
        p1.transform.position = stages[curStage].transform.GetChild(0).position + new Vector3(1f, 0);
        p2.transform.position = stages[curStage].transform.GetChild(0).position - new Vector3(1f, 0);
        p1Ctrl.VelocityZero();
        p2Ctrl.VelocityZero();
    }
}
