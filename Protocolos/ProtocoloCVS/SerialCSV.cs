using System.IO.Ports;
using UnityEngine;

public class SerialCSV : MonoBehaviour
{
    SerialPort port = new SerialPort("COM3", 9600);

    void Start()
    {
        port.Open();
        port.ReadTimeout = 100;
    }

    void Update()
    {
        if (port.IsOpen)
        {
            try
            {
                string data = port.ReadLine();

                string[] values = data.Split(',');

                if (values.Length == 5)
                {
                    int b1 = int.Parse(values[0]);
                    int b2 = int.Parse(values[1]);
                    int b3 = int.Parse(values[2]);
                    int b4 = int.Parse(values[3]);
                    int pot = int.Parse(values[4]);

                    Debug.Log(
                        "B1: " + b1 +
                        " | B2: " + b2 +
                        " | B3: " + b3 +
                        " | B4: " + b4 +
                        " | Potentiometer: " + pot
                    );
                }
            }
            catch (System.Exception)
            {
                
            }
        }
    }

    void OnApplicationQuit()
    {
        if (port.IsOpen)
        {
            port.Close();
        }
    }
}