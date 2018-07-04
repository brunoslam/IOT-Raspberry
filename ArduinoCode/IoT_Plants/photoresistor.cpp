// FILE: photoresistor.cpp
// VERSION: 0.1
// PURPOSE: Light sensor library for Arduino
// LICENSE: GPL v3 (http://www.gnu.org/licenses/gpl.html)
// AUTHOR: BRUNO

#include "photoresistor.h"

void photoresistor::read(int pin)
{
	float val = 1023 - (int)analogRead(pin);
	value = (int)analogRead(pin);
	percentage =  100 -((100 * val) / 1023) ;
}

