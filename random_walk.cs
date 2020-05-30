using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class random_walk : MonoBehaviour
{

    private GameObject randomwalk;
    private const double a0 = 90.0d;////tekitou
    private System.DateTime startMsec0;
    private readonly double f0 = 0.028722d;//サンプリング周期は0.034s
    //private readonly double f0 = 0.029297d;
    private readonly double p = 1.25d;
    private readonly double[] phai_k_x = {
        2.629537374d,
        0.291427544d,
        5.421483965d,
        1.863955251d,
        2.679611213d,
        5.311100531d,
        1.940009382d,
        4.714983294d,
        3.05893645d,
        1.794691312d,
        3.926670709d,
        1.055437224d,
        3.867778111d,
        1.061666252d,
        4.664218925d,
        1.148128894d,
        2.13771452d
    };
    private readonly double[] phai_k_y = {
        1.893737885d,
        1.605035751d,
        0.265786709d,
        1.055484924d,
        1.282344727d,
        0.247031639d,
        3.250749355d,
        0.331731329d,
        0.086289062d,
        0.923433284d,
        1.478363426d,
        4.857359259d,
        2.564555467d,
        3.394336338d,
        0.04257195d,
        0.513488425d,
        3.753182111d,
    };

    private double[] pk = new double[18];
    private double[] f0pk = new double[18];
    private double t = 0;
    private int k;
    private int starttime;
    private int now;
    private int milisec;
    private float xf;
    private float yf;
    private float zf;
    private float tf;
    private bool flag;
    private double x;
    private double y;
    private double z;

    private int trialnum = 0;
    private const int trialmaxnum = 50000;


    private double[] logx = new double[trialmaxnum];
    private double[] logy = new double[trialmaxnum];
    private double[] logz = new double[trialmaxnum];
    private double[] logt = new double[trialmaxnum];


    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("debug comment");
        if ((randomwalk = GameObject.Find("target")) == null)
        {
            UnityEngine.Debug.Log(" GameObject.Find(random_walk) --> FAILED");
            return;

        }
        randomwalk.transform.position = new Vector3(0, 0, 0);
        //Vector3 pos = randomwalk.transform.position;
        for (int k = 0; k < 18; k++)
        {
            pk[k] = Math.Pow(p, k + 1);
            f0pk[k] = 2.0d * Math.PI * f0 * pk[k];
        }
        System.DateTime now = System.DateTime.Now;
        startMsec0 = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Return))
        {
            starttime = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
            flag = true;
            UnityEngine.Debug.Log("Start!!");
            //return;
        }
        if (!flag) return;
        now = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
        milisec = now - starttime;
        x = 0.0d;
        y = 0.0d;
        for (k = 0; k < 17; k++)
        {
            //x += (a0 * Math.Sin((f0pk[k] * (double)milisec / 1000.0d) + phai_k_x[k])) / pk[k];
            x += (a0 * Math.Sin((f0pk[k] * (double)milisec / 1000.0d) + phai_k_x[k])) / pk[k] / 1000;
            //y += (a0 * Math.Sin((f0pk[k] * (double)milisec / 1000.0d) + phai_k_y[k])) / pk[k];
            y += (a0 * Math.Sin((f0pk[k] * (double)milisec / 1000.0d) + phai_k_y[k])) / pk[k] / 1000;
        }
        xf = (float)x;
        yf = (float)y;
        Vector3 pos = randomwalk.transform.position;
        randomwalk.transform.position = new Vector3(xf, yf, pos.z);

        if (trialnum < trialmaxnum)
        {

            logx[trialnum] = x;
            logy[trialnum] = y;
            logz[trialnum] = z;
            logt[trialnum] = milisec;
            trialnum++;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            csvtSave();
        }

    }
    public void csvtSave()
    {
        DateTime dt = DateTime.Now;
        string filenameday = dt.ToString("yyyy_MM_dd_HH_mm_ss");

        UnityEngine.Debug.Log(filenameday);
        StreamWriter sw = new StreamWriter("SaveLogFiles/random_walk_log" + filenameday + ".csv", false, System.Text.Encoding.GetEncoding("shift_jis")); //true=追記 false=上書き
        //エラーが出る場合はシーンフォルダ直下にSaveLogFilesフォルダを作成
        sw.WriteLine("t[ms],x[mm],y[mm],z[mm]");

        for (int i = 0; i < trialmaxnum; i++)
        {
            sw.WriteLine(logt[i] + "," + logx[i] + "," + logy[i] + "," + logz[i]);
        }


        sw.Flush();
        sw.Close();


    }
}
