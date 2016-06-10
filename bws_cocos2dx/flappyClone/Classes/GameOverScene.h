#ifndef __GAME_OVER_SCENE_H__
#define __GAME_OVER_SCENE_H__

#include "cocos2d.h"

class GameOverScene : public cocos2d::Layer
{
public:
    static cocos2d::Scene* createScene();

    virtual bool init();
    
    // implement the "static create()" method manually
    CREATE_FUNC(GameOverScene);

private:
	void GotoMainMenuScene(cocos2d::Ref *sender);
	void GotoGameScene(cocos2d::Ref *sender);
};

#endif // __GAME_OVER_SCENE_H__
