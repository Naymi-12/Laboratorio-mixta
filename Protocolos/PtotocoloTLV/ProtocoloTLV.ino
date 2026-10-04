const int button1 = 4;
const int button2 = 5;
const int button3 = 6;
const int button4 = 7;

const int POT = A0;

void sendTLV(int type, int value)
{
  String valueString = String(value);

  Serial.print(type);
  Serial.print(":");
  Serial.print(valueString.length());
  Serial.print(":");
  Serial.print(valueString);
  Serial.print("|");
}

void setup()
{
  Serial.begin(9600);

  pinMode(button1, INPUT_PULLUP);
  pinMode(button2, INPUT_PULLUP);
  pinMode(button3, INPUT_PULLUP);
  pinMode(button4, INPUT_PULLUP);
}

void loop()
{
  int stateB1 = digitalRead(button1);
  int stateB2 = digitalRead(button2);
  int stateB3 = digitalRead(button3);
  int stateB4 = digitalRead(button4);

  int pot = analogRead(POT);

  sendTLV(1, stateB1);
  sendTLV(2, stateB2);
  sendTLV(3, stateB3);
  sendTLV(4, stateB4);
  sendTLV(5, pot);

  Serial.println();

  delay(500);
}