/*  
Temperature Reading Script
*/

// JSON Library
#include "ArduinoJson.h"
// import seven segment LED module library
#include "SevSeg.h";
SevSeg sevseg;

// temperature sensor and pin inits
const int sensor = A1;
float tempc;
float tempf;
float vout;


// JSON object delcared with mem
StaticJsonDocument<48> doc;

void setup() {
    // set up seven segment LED parameters
    byte numDigits = 4;
    byte digitPins[] = {10, 11, 12, 13};
    byte segmentPins[] = {9, 2, 3, 5, 6, 8, 7, 4};

    bool resistorsOnSegments = true;
    bool updateWithDelaysIn = true;
    byte hardwareConfig = COMMON_CATHODE;
    sevseg.begin(hardwareConfig, numDigits, digitPins, segmentPins, resistorsOnSegments);
    sevseg.setBrightness(90);

    // set up temperature sensor parameters
    pinMode(sensor, INPUT); // Configure pin A1 as input
    Serial.begin(9600);
    while (!Serial) continue;
}

void loop() {
    vout = analogRead(sensor);
    vout = ( vout * 500 ) / 1023;
    tempc = vout;
    tempf = ( vout * 1.8 ) + 32;
    static unsigned long timer = millis();

    if (millis() >= timer) {
        timer += 5000;
	sevseg.setNumber(tempf, 2);
        doc["c"] = tempc;
	doc["f"] = tempf;
	serializeJson(doc, Serial);
        Serial.println();
    }
    
    sevseg.refreshDisplay();
}
