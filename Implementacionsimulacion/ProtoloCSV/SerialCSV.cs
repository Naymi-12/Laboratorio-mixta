using System.IO.Ports;
using UnityEngine;

public class SerialCSV : MonoBehaviour
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

                string[] values = data.Split(',');

                if (values.Length == 5)
                {
                    int b1 = int.Parse(values[0]);
                    int b2 = int.Parse(values[1]);
                    int b3 = int.Parse(values[2]);
                    int b4 = int.Parse(values[3]);
                    int pot = int.Parse(values[4]);

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