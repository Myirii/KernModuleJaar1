using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoConnect : MonoBehaviour
{
    private SerialPort sp;
    private PlayerMovement pm;

    private void Start()
    {
        StartCoroutine(Search());
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        if (sp != null)
        {
            if (sp.IsOpen)
            {
                if (sp.BytesToRead != 0)
                {
                    int serialCommand = Convert.ToInt32(sp.ReadByte());
                    int serialParameter = 0;
                    int serialEnding;
                    bool serialRead = false;
                    if (serialCommand > 0 && serialCommand <= 5)
                    {
                        serialParameter = Convert.ToInt32(sp.ReadByte()); //read the second byte coming in
                        if (serialParameter > 0 && serialParameter <= 255)
                        {
                            serialEnding = Convert.ToInt32(sp.ReadByte()); //read the ending byte to assemble the complete command
                            if (serialEnding == 0)
                            {
                                serialRead = true;
                            }
                        }
                    }

                    if (serialRead)
                    {
                        switch (serialCommand)//run the command sent from Arduino
                        {
                            case 1://dash sensor
                                pm.SetDashBonus(serialParameter);
                                break;
                            case 2://jump sensor
                                pm.SetJumpStrength(serialParameter);
                                break;
                            case 3://shield sensor
                                pm.SetShieldStrength(serialParameter);
                                break;
                        }
                    }
                }
            }
        }
    }

    private IEnumerator Search()
    {
        while (sp == null)
        {
            foreach (string str in SerialPort.GetPortNames())
            {
                Debug.Log(string.Format("Existing COM port: {0}", str));
                if (str == "COM4")
                {
                    sp = new SerialPort("COM4", 9600);
                    sp.Open();
                    sp.ReadTimeout = 5;
                    Debug.Log("ARDUINO FOUND");
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
}
