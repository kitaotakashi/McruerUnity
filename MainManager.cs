using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    private int state;
    private int localmode;
    //test

    // Start is called before the first frame update
    void Start()
    {
        localmode = IndexManager.getSelectMode();
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0 && Input.GetKey(KeyCode.Q))
        {
            state = 1;
        }

        if (state == 1)
        {
            SceneManager.LoadScene("index");
        }
        else { }
    }
}
