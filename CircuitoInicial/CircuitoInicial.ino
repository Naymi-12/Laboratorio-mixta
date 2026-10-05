const int button1 = 4;
const int button2 = 5;
const int button3 = 6;
const int button4 = 7;

const int potentiometer = A0;

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

  int valuePot = analogRead(potentiometer);

  Serial.print("B1:");
  Serial.print(stateB1);

  Serial.print(" B2:");
  Serial.print(stateB2);

  Serial.print(" B3:");
  Serial.print(stateB3);

  Serial.print(" B4:");
  Serial.print(stateB4);

  Serial.print(" POT:");
  Serial.println(valuePot);

  delay(500);
}