using System.IO.Ports;
using UnityEngine;

public class SerialTLV : MonoBehaviour
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

                Debug.Log("TLV received: " + data);
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