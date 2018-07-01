

#ifndef _DHT_h
#define _DHT_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

class DhtClass
{
 protected:


 public:
	void init();
};

extern DhtClass Dht;

#endif

