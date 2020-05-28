using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class IndexManager : MonoBehaviour
{
    public Text modeText;
    private int state;
    public static int mode;
    private int MODEMAX = 5;

    //git test

    public static int getSelectMode()
    {
        return mode;
    }

    // Start is called before the first frame update
    void Start()
    { 
        state = 0;
        mode = 0;
        modeText.GetComponent<Text>().text = "mode: " + Convert.ToString(mode);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0 && Input.GetKey(KeyCode.M))
        {
            mode++;
            if (mode == MODEMAX) {
                mode = 0;
            }
            modeText.GetComponent<Text>().text = "mode: " + Convert.ToString(mode+1);
        }

        if (state ==0 && Input.GetKey(KeyCode.S)) {
            state = 1;
        }

        if (state == 1)
        {
            SceneManager.LoadScene("Main");
        }
        else { }

    }
}
