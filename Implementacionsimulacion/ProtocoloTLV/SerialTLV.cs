using System.IO.Ports;
using UnityEngine;

public class SerialTLV : MonoBehaviour
{
    SerialPort port = new SerialPort("COM3", 9600);

    public SequenceGame sequenceGame;

    private int previousB1 = 1;
    private int previousB2 = 1;
    private int previousB3 = 1;
    private int previousB4 = 1;

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

                
                string[] fields = data.Split('|');

                int b1 = 1;
                int b2 = 1;
                int b3 = 1;
                int b4 = 1;
                int pot = 0;

                foreach (string field in fields)
                {
                    string[] parts = field.Split(':');

                    if (parts.Length != 2)
                        continue;

                    string type = parts[0];
                    int value = int.Parse(parts[1]);

                    switch (type)
                    {
                        case "B1":
                            b1 = value;
                            break;

                        case "B2":
                            b2 = value;
                            break;

                        case "B3":
                            b3 = value;
                            break;

                        case "B4":
                            b4 = value;
                            break;

                        case "POT":
                            pot = value;
                            break;
                    }
                }

                sequenceGame.SetPotentiometer(pot);

                if (previousB1 == 1 && b1 == 0)
                {
                    sequenceGame.PressButton(0);
                }

                if (previousB2 == 1 && b2 == 0)
                {
                    sequenceGame.PressButton(1);
                }

                if (previousB3 == 1 && b3 == 0)
                {
                    sequenceGame.PressButton(2);
                }

                if (previousB4 == 1 && b4 == 0)
                {
                    sequenceGame.PressButton(3);
                }

             
                previousB1 = b1;
                previousB2 = b2;
                previousB3 = b3;
                previousB4 = b4;
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
