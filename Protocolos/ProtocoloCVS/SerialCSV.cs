using System.IO.Ports;
using UnityEngine;

public class SerialCSV : MonoBehaviour
{
    SerialPort port = new SerialPort("COM3", 9600);

    void Start()
    {
        try
        {
            port.Open();
            port.ReadTimeout = 100;

            Debug.Log("Serial port opened successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Could not open serial port: " + e.Message);
        }
    }

    void Update()
    {
        if (port.IsOpen)
        {
            try
            {
                string data = port.ReadLine();

                Debug.Log("Received: " + data);

                string[] values = data.Split(',');

                if (values.Length == 6)
                {
                    int b1 = int.Parse(values[0]);
                    int b2 = int.Parse(values[1]);
                    int b3 = int.Parse(values[2]);
                    int b4 = int.Parse(values[3]);
                    int pot = int.Parse(values[4]);
                    int checksumReceived = int.Parse(values[5]);

                    int checksumCalculated =
                        b1 ^ b2 ^ b3 ^ b4 ^ pot;

                    if (checksumCalculated == checksumReceived)
                    {
                        Debug.Log(
                            "Correct data | " +
                            "B1: " + b1 +
                            " | B2: " + b2 +
                            " | B3: " + b3 +
                            " | B4: " + b4 +
                            " | Potentiometer: " + pot +
                            " | Checksum OK"
                        );
                    }
                    else
                    {
                        Debug.LogWarning(
                            "Integrity error | " +
                            "Received checksum: " + checksumReceived +
                            " | Calculated checksum: " + checksumCalculated
                        );
                    }
                }
                else
                {
                    Debug.LogWarning(
                        "Invalid number of values: " + values.Length
                    );
                }
            }
            catch (System.TimeoutException)
            {
                
            }
            catch (System.Exception e)
            {
                Debug.LogError("Serial error: " + e.Message);
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
