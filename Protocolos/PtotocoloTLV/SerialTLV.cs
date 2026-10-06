using System.IO.Ports;
using UnityEngine;

public class SerialTLV : MonoBehaviour
{
    SerialPort port = new SerialPort("COM3", 9600);

    void Start()
    {
        try
        {
            port.Open();
            port.ReadTimeout = 100;
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

           
                Debug.Log("TLV received: " + data);

                string[] fields = data.Split('|');

                int b1 = 0;
                int b2 = 0;
                int b3 = 0;
                int b4 = 0;
                int pot = 0;
                int checksumReceived = 0;

                foreach (string field in fields)
                {
                    if (string.IsNullOrEmpty(field))
                        continue;

                    string[] parts = field.Split(':');

                    if (parts.Length != 3)
                        continue;

                    int type = int.Parse(parts[0]);
                    int length = int.Parse(parts[1]);
                    int value = int.Parse(parts[2]);

                    if (parts[2].Length != length)
                    {
                        Debug.LogWarning(
                            "Invalid TLV length for type: " + type
                        );

                        return;
                    }

                    switch (type)
                    {
                        case 1:
                            b1 = value;
                            break;

                        case 2:
                            b2 = value;
                            break;

                        case 3:
                            b3 = value;
                            break;

                        case 4:
                            b4 = value;
                            break;

                        case 5:
                            pot = value;
                            break;

                        case 6:
                            checksumReceived = value;
                            break;
                    }
                }

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
                        " | Calculated checksum: " +
                        checksumCalculated
                    );
                }
            }
            catch (System.TimeoutException)
            {
            }
            catch (System.Exception e)
            {
                Debug.LogError("TLV error: " + e.Message);
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