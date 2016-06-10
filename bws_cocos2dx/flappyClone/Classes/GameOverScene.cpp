#include "GameOverScene.h"
#include "GameScene.h"
#include "MainMenuScene.h"
#include "Definitions.h"

USING_NS_CC;

Scene* GameOverScene::createScene()
{
    // 'scene' is an autorelease object
    auto scene = Scene::create();
    
    // 'layer' is an autorelease object
    auto layer = GameOverScene::create();

    // add layer as a child to scene
    scene->addChild(layer);

    // return the scene
    return scene;
}

// on "init" you need to initialize your instance
bool GameOverScene::init()
{
    //////////////////////////////
    // 1. super init first
    if ( !Layer::init() )
    {
        return false;
    }
    
    Size visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();

	auto backgroundSprite = Sprite::create("background.png");
	backgroundSprite->setPosition(Vec2(visibleSize.width / 2 + origin.x, visibleSize.height / 2 + origin.y));

	this->addChild(backgroundSprite);

	auto retryItem = MenuItemImage::create("Retry Button.png", "Retry Button Clicked.png", CC_CALLBACK_1(GameOverScene::GotoGameScene, this));
	retryItem->setPosition(Vec2(visibleSize.width / 2 + origin.x, visibleSize.height/4 * 3));

	auto mainMenuItem = MenuItemImage::create("Menu Button.png", "Menu Button Clicked.png", CC_CALLBACK_1(GameOverScene::GotoMainMenuScene, this));
	mainMenuItem->setPosition(Vec2(visibleSize.width / 2 + origin.x, visibleSize.height / 4));

	auto menu = Menu::create(retryItem, mainMenuItem, nullptr);
	menu->setPosition(Vec2::ZERO);

	this->addChild(menu);

    return true;
}

void GameOverScene::GotoMainMenuScene(cocos2d::Ref * sender)
{
	auto scene = MainMenuScene::createScene();

	Director::getInstance()->replaceScene(TransitionFade::create(TRANSITION_TIME, scene));
}

void GameOverScene::GotoGameScene(cocos2d::Ref * sender)
{
	auto scene = GameScene::createScene();

	Director::getInstance()->replaceScene(TransitionFade::create(TRANSITION_TIME, scene));
}




