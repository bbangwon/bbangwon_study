#ifndef __GAME_SCENE_H__
#define __GAME_SCENE_H__

#include "cocos2d.h"
#include "pipe.h"
#include "Bird.h"

class GameScene : public cocos2d::Layer
{
public:
    static cocos2d::Scene* createScene();

    virtual bool init();
    
    // implement the "static create()" method manually
    CREATE_FUNC(GameScene);

private:
	void SetPhysicsWorld(cocos2d::PhysicsWorld *world) { sceneWorld = world; }
	
	void SpawnPipe(float dt);

	bool onContactBegin(cocos2d::PhysicsContact &contact);
	cocos2d::PhysicsWorld *sceneWorld;
	Pipe pipe;
	Bird *bird;
};

#endif // __GAME_SCENE_H__
