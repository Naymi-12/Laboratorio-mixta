using System.IO.Ports;
using UnityEngine;

public class SerialCSV : MonoBehaviour
{
    SerialPort puerto = new SerialPort("COM3", 9600);

    void Start()
    {
        puerto.Open();
        puerto.ReadTimeout = 100;
    }

    void Update()
    {
        if (puerto.IsOpen)
        {
            try
            {
                string datos = puerto.ReadLine();

                string[] valores = datos.Split(',');

                if (valores.Length == 5)
                {
                    int b1 = int.Parse(valores[0]);
                    int b2 = int.Parse(valores[1]);
                    int b3 = int.Parse(valores[2]);
                    int b4 = int.Parse(valores[3]);
                    int pot = int.Parse(valores[4]);

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
        if (puerto.IsOpen)
        {
            puerto.Close();
        }
    }
}