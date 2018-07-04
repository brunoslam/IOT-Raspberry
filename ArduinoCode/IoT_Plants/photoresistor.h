// FILE: photoresistor.h
// VERSION: 0.1
// PURPOSE: Light sensor library for Arduino
// LICENSE: GPL v3 (http://www.gnu.org/licenses/gpl.html)
// AUTHOR: BRUNO
#ifndef photoresistor_h
#define photoresistor_h

#if defined(ARDUINO) && (ARDUINO >= 100)
#include <Arduino.h>
#else
#include <WProgram.h>
#endif

class photoresistor
{
public:
	void read(int pin);
	int value;
	int percentage;
};
#endif
//
// END OF FILE
//

