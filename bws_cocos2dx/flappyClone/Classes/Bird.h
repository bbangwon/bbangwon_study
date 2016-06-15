#pragma once

#include "cocos2d.h"
#include "AudioEngine.h"

class Bird
{
public:
	Bird(cocos2d::Layer *layer);

	void Fall();
	void Fly() { cocos2d::experimental::AudioEngine::play2d("Sounds/Wing.mp3"); isFalling = false; }
	void StopFlying() { isFalling = true; }

private:
	cocos2d::Size visibleSize;
	cocos2d::Vec2 origin;

	cocos2d::Sprite *flappyBird;

	bool isFalling;
};

