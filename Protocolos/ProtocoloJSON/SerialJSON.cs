using System.IO.Ports;
using UnityEngine;

public class SerialJSON : MonoBehaviour
{
    SerialPort port = new SerialPort("COM3", 9600);

    [System.Serializable]
    public class ArduinoData
    {
        public int b1;
        public int b2;
        public int b3;
        public int b4;
        public int pot;
    }

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

                ArduinoData values =
                    JsonUtility.FromJson<ArduinoData>(data);

                Debug.Log(
                    "B1: " + values.b1 +
                    " | B2: " + values.b2 +
                    " | B3: " + values.b3 +
                    " | B4: " + values.b4 +
                    " | Potentiometer: " + values.pot
                );
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