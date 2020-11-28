# dotnet-iot-pi-and-arduino-temperature-sensor

Raspberry Pi and Arduino Edge System which communicates with a web/cloud end point and transmits sensor data.

For the purpose of prototyping Adafruit IO has been used as the web endpoint.

The .NET/C# code runs on the raspberry pi which streams in sensor data calculated by the Arduino through serial and posts it to the Adafruit API.
