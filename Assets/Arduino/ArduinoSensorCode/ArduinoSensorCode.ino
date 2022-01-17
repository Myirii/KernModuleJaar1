const int trigPin = 4;
const int echoPin = 2;
const int distance_threshold = 60; //cm

const int trigPin2 = 8;
const int echoPin2 = 7;
const int distance_threshold2 = 30; //cm

const int trigPin3 = 13;
const int echoPin3 = 12;
const int distance_threshold3 = 30; //cm

float duration_us, duration_us2, duration_us3, distance_cm, distance_cm2, distance_cm3;

void setup() {
  Serial.begin(9600);

  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);

  pinMode(trigPin2, OUTPUT);
  pinMode(echoPin2, INPUT);

  pinMode(trigPin3, OUTPUT);
  pinMode(echoPin3, INPUT);
}

void loop() {
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);

  duration_us = pulseIn(echoPin, HIGH);
  distance_cm = 0.017 * duration_us;

  digitalWrite(trigPin2, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin2, LOW);

  duration_us2 = pulseIn(echoPin2, HIGH);
  distance_cm2 = 0.017 * duration_us2;

  digitalWrite(trigPin3, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin3, LOW);

  duration_us3 = pulseIn(echoPin3, HIGH);
  distance_cm3 = 0.017 * duration_us3;

  if (distance_cm < distance_threshold) {
    byte cmd[] = {1, (int)distance_cm, 0};
    Serial.write(cmd, sizeof(cmd));
  }

  if (distance_cm2 < distance_threshold2) {
    byte cmd[] = {2, (int)distance_cm2, 0};
    Serial.write(cmd, sizeof(cmd));
  }

  if (distance_cm3 < distance_threshold3) {
    byte cmd[] = {3, (int)distance_cm3, 0};
    Serial.write(cmd, sizeof(cmd));
  }

  delay(100);
}
