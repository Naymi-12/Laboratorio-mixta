const int button1 = 4;
const int button2 = 5;
const int button3 = 6;
const int button4 = 7;

const int POT = A0;

void setup() {
  Serial.begin(9600);

  pinMode(button1, INPUT_PULLUP);
  pinMode(button2, INPUT_PULLUP);
  pinMode(button3, INPUT_PULLUP);
  pinMode(button4, INPUT_PULLUP);
}

void loop() {

  int stateB1 = digitalRead(button1);
  int stateB2 = digitalRead(button2);
  int stateB3 = digitalRead(button3);
  int stateB4 = digitalRead(button4);

  int pot = analogRead(POT);

  Serial.print("{");
  Serial.print("\"b1\":");
  Serial.print(stateB1);

  Serial.print(",\"b2\":");
  Serial.print(stateB2);

  Serial.print(",\"b3\":");
  Serial.print(stateB3);

  Serial.print(",\"b4\":");
  Serial.print(stateB4);

  Serial.print(",\"pot\":");
  Serial.print(pot);

  Serial.println("}");

  delay(500);
}