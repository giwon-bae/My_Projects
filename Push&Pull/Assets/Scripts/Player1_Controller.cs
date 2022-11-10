using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Controller : MonoBehaviour
{
    [SerializeField] GameObject TargetObj;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Push"))
        {
            TargetObj.transform.Translate(new Vector2(0,2f));
        }
    }
}
