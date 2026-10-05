using System.IO.Ports;
using UnityEngine;

public class SerialJSON : MonoBehaviour
{
    SerialPort port = new SerialPort("COM3", 9600);

    public SequenceGame sequenceGame;

    [System.Serializable]
    public class ArduinoData
    {
        public int b1;
        public int b2;
        public int b3;
        public int b4;
        public int pot;
    }

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

                ArduinoData values =
                    JsonUtility.FromJson<ArduinoData>(data);

            
                sequenceGame.SetPotentiometer(values.pot);

              
                if (previousB1 == 1 && values.b1 == 0)
                {
                    sequenceGame.PressButton(0);
                }

              
                if (previousB2 == 1 && values.b2 == 0)
                {
                    sequenceGame.PressButton(1);
                }

           
                if (previousB3 == 1 && values.b3 == 0)
                {
                    sequenceGame.PressButton(2);
                }

          
                if (previousB4 == 1 && values.b4 == 0)
                {
                    sequenceGame.PressButton(3);
                }

         
                previousB1 = values.b1;
                previousB2 = values.b2;
                previousB3 = values.b3;
                previousB4 = values.b4;
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