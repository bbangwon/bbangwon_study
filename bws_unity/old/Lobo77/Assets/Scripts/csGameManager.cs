using System.Text;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct sPCPlayer
{	
	public GameObject Obj;
	public Vector3 Position;
	public int LifeCount;
	public int[] cards;
}

public struct sMyCard
{
	public GameObject Obj;
	public Vector3 Position;
	public int Card;
}

public struct sAllCards
{
	//0은 사용안하는 중, 1 : 사용중, 2 : 버려진 카드 
	public int usedType;   
	public string card;
}

public class csGameManager : MonoBehaviour {
	
	public sAllCards[] AllCards;
	
	public GameObject card_prefab;
	public GameObject pcPlayer;
	//컴퓨터 개수 
	public int PCPlayerCount = 7;	
		
	private List<sMyCard> MyCard = new List<sMyCard>();
	private List<sPCPlayer> PcPlayer = new List<sPCPlayer>();
	//내 생명의 칩 개수
	private int MyLifeCnt = 3;	
	
	private GameObject CloseCard;
	private GameObject PCCard;
	private GameObject OpenCard;
	private bool bDropGetAniPlaying;
	// 0 이면 MyTurn

	enum GameState {INIT, PLAY, RESET, OUTRESET, END};
	private GameState gameState;	
	private int Turn = 0;
	private int Total = 0;
	private string lastDropCard = "";
	private int addNum = 1;
	private int CardDropCnt = 1;
	private bool bWin = false;
	private Dictionary<string, Texture2D> cardTexture = new Dictionary<string, Texture2D>();
	private int DropedCardCount = 0;
	private Texture2D transImg;

	// Use this for initialization
	void Start () {
		gameState = GameState.INIT;
		Turn = Random.Range(0, PCPlayerCount);
		Turn = 0;
		Init();
		gameState = GameState.RESET;
	}
	
	void OnGUI()
	{
		if(Turn == 0)
			GUI.Label(new Rect(30,200,100,50), "Player Turn");
		else
			GUI.Label(new Rect(30,200,100,50), "Computer" + Turn.ToString() + " Turn");
		
		GUI.Label(new Rect(30,250,100,50), "Total Point : " + Total);
	}
		
	void PCAI()
	{
		if(Turn > 0 && bDropGetAniPlaying == false)
		{
			/*
			int [] possibleCards = new int[5];
			int possibleCardsCount = 5;
		
			for(int i=0;i<5;i++)
			{
				possibleCards[i] = PcPlayer[Turn-1].cards[i];
			}
			
			for(int i=0;i<5;i++)
			{	
				if(possibleCards[i] == -1) continue;
				string scard = AllCards[possibleCards[i]].card;
	
				if(scard == "X2" || scard == "U")
				{
					if(lastDropCard == "X2")
					{	
						possibleCards[i] = -1;
						possibleCardsCount--;
					}
				}			
			}
			
			if(possibleCardsCount == 0)
			{
				gameState = GameState.OUTRESET;
				return;
			}
			
			if(possibleCardsCount > 1)
			{			
				for(int i=0;i<5;i++)
				{
					if(possibleCards[i] == -1) continue;
					string scard = AllCards[PcPlayer[Turn-1].cards[i]].card;
					
					if((int.Parse(scard) + Total) >= 77)
					{
						if(possibleCardsCount > 1)
						{
							possibleCards[i] = -1;						
							possibleCardsCount--;
						}
					}
				}
			}
			
*/		
			
			
			string sSelCard = "";
			string sSelCard2 = "";
			int selIdx = -1;
			int selIdx2 = -1;			
			if(lastDropCard == "X2")
			{
				
				//IsNumCard?				
				for(int i=0;i<5;i++)
				{					
					string scard = AllCards[PcPlayer[Turn-1].cards[i]].card;
					if(scard == "X2" || scard == "U") continue;
					
					sSelCard2 = scard;
					selIdx2 = i;
					if((int.Parse(scard) + Total) >= 77) continue;
					if((int.Parse(scard) + Total) % 11 == 0) continue;
				
					sSelCard = scard;
					selIdx = i;	
					break;
				}				
				
				//No Card
				if(selIdx == -1)					
				{
					if(selIdx2 == -1)
					{
						gameState = GameState.OUTRESET;
						return;
					}
					else
					{
						DropCard("PCCARD" + selIdx2.ToString());				
					}
				}
				else
				{
					DropCard("PCCARD" + selIdx.ToString());				
				}			
			}
			else
			{				
				for(int j=0;j<3;j++)
				{					
					//IsNumCard?				
					for(int i=0;i<5;i++)
					{
						string scard = AllCards[PcPlayer[Turn-1].cards[i]].card;
						if(j<3)
							if(scard == "X2" || scard == "U") continue;
						if(j<2)
							if((int.Parse(scard) + Total) % 11 == 0) continue;
						if(j<1)
							if((int.Parse(scard) + Total) >= 77) continue;
					
						sSelCard = scard;
						selIdx = i;	
						break;
					}
				}
				
				//No Card
				if(selIdx == -1)					
				{
					gameState = GameState.OUTRESET;
					return;
				}
				else
				{
					DropCard("PCCARD" + selIdx.ToString());				
				}							
			}
		}
	}
	
	bool TurnChange()
	{

		Turn += addNum;		
		Turn = Mathf.Clamp(Turn,-1,PCPlayerCount+1);
		if(Turn == -1)
			Turn = PCPlayerCount;
		else if(Turn == PCPlayerCount+1)
			Turn = 0;
				
		if(Turn > 0)
		{
			
			int nTurns = Turn-1;
			print ("Computer["+nTurns+"] Cards ["+AllCards[PcPlayer[nTurns].cards[0]].card+"]["+AllCards[PcPlayer[nTurns].cards[1]].card+"]["+AllCards[PcPlayer[nTurns].cards[2]].card+"]["+AllCards[PcPlayer[nTurns].cards[3]].card+"]["+AllCards[PcPlayer[nTurns].cards[4]].card+"]");			
			
			sPCPlayer player = PcPlayer[Turn-1];			
			if(player.LifeCount == -1)
				return false;
		}
		return true;
	}
	
	// Update is called once per frame
	void Update () {
		//GameOver?
		bool sw = false;
		for(int i=0;i<PCPlayerCount;i++)
		{
			if(PcPlayer[i].LifeCount > -1)
				sw = true;
		}
		if(sw == false)
		{
			bWin = true;
			gameState = GameState.END;
		}
		
		
		if(gameState == GameState.PLAY)
		{
			//Turn Change
			if(CardDropCnt <= 0)
			{
				if(lastDropCard == "X2")
				{
					CardDropCnt = 2;
				}
				else if(lastDropCard == "U")
				{
					addNum *= -1;
					CardDropCnt = 1;
				}
				else
				{
					CardDropCnt = 1;			
				}
				while(!TurnChange());
			}
			
			PCAI();
		}
		else if(gameState == GameState.RESET)
		{
			print (gameState);
			DropedCardCount = 0;
			Total = 0;
			InitStage();
//			bDropGetAniPlaying = false;					
			gameState = GameState.PLAY;
		}
		else if(gameState == GameState.OUTRESET)
		{

			LifeOut();
			gameState = GameState.RESET;
		}
		
		if(gameState != GameState.INIT)
		{
			if(DropedCardCount == 0)
				OpenCard.renderer.material.mainTexture = transImg;
			else
				OpenCard.renderer.material.mainTexture = cardTexture[lastDropCard];
			
			for(int i=0;i<5;i++)
			{
				MyCard[i].Obj.renderer.material.mainTexture = cardTexture[AllCards[MyCard[i].Card].card];
			}		
		}
		
		
	}
	
	void TotalCheck()
	{
		if(lastDropCard != "X2" && lastDropCard != "U")
		{
			Total += int.Parse(lastDropCard);
		}
		
		if(Total >= 77)
		{
			Total = 0;			
			gameState = GameState.OUTRESET;
		}
		
		if(Total > 0 && Total % 11 == 0)
		{
			LifeOut();
			gameState = GameState.PLAY;
		}		
	}
	
	void LifeOut()
	{
		if(Turn == 0)
		{
			MyLifeCnt--;
			switch(MyLifeCnt)
			{
			case -1:				
				bWin = false;
				gameState = GameState.END;
				break;
			case 0:				
				Destroy(GameObject.Find("p_life1"));
				break;
			case 1:
				Destroy(GameObject.Find("p_life2"));				
				break;
			case 2:
				Destroy(GameObject.Find("p_life3"));				
				break;
			}
		}
		else
		{
			
			sPCPlayer pcplayer = PcPlayer[Turn-1];			
			pcplayer.LifeCount--;			
			switch(pcplayer.LifeCount)
			{

			case 0:				
				Destroy(pcplayer.Obj.transform.FindChild("p_life1").gameObject);
				break;
			case 1:
				Destroy(pcplayer.Obj.transform.FindChild("p_life2").gameObject);
				break;
			case 2:
				Destroy(pcplayer.Obj.transform.FindChild("p_life3").gameObject);
				break;
			}
			
			PcPlayer[Turn-1] = pcplayer;
			
		}	
	}
	void InitStage() {	
		
		CardReset();		
		//카드 배분		
		for(int i=0;i<5;i++)
		{
			sMyCard mycard = MyCard[i];
			mycard.Card = GetCardOne();
			MyCard[i] = mycard;
		}
		
		for(int c=0;c<PCPlayerCount;c++)
		{
			sPCPlayer pcplayer = PcPlayer[c];
			for(int i=0;i<5;i++)
				pcplayer.cards[i] = GetCardOne();				
			PcPlayer[c] = pcplayer;
		}
	}

	void Init(){
		
		int roopval = 0;			
		float xpos = -0.65f;
		
		//AllCard Setting
		// 11~66, 76
		//2~9  *3
		//0, -10, x2 *4
		//방행바꾸기 *5
		// 10 *8
		
		AllCards = new sAllCards[56];
		
		
		for(int n=0;n<56;n++)
			AllCards[n].usedType = 0;
		
		int nidx = 0;
		for(int n=11;n<=66;n+=11)
			AllCards[nidx++].card = n.ToString();

		AllCards[nidx++].card = "76";
		
		for(int n2=0;n2<3;n2++)
			for(int n=2;n<=9;n++)
				AllCards[nidx++].card = n.ToString();
		
		for(int n2=0;n2<4;n2++)
		{
			AllCards[nidx++].card = "0";
			AllCards[nidx++].card = "-10";
			AllCards[nidx++].card = "X2";
		}
		
		for(int n2=0;n2<5;n2++)
			AllCards[nidx++].card = "U";
		
		for(int n2=0;n2<8;n2++)
			AllCards[nidx++].card = "10";
		
		//animations MyCard
		AnimationClip Card1Drop = (AnimationClip)Resources.Load("Animations/Card1Drop");
		AnimationClip Card2Drop = (AnimationClip)Resources.Load("Animations/Card2Drop");
		AnimationClip Card3Drop = (AnimationClip)Resources.Load("Animations/Card3Drop");
		AnimationClip Card4Drop = (AnimationClip)Resources.Load("Animations/Card4Drop");
		AnimationClip Card5Drop = (AnimationClip)Resources.Load("Animations/Card5Drop");		
		
		for(roopval=0;roopval<5;roopval++)
		{
			sMyCard card = new sMyCard();
			card.Position = new Vector3(xpos, -1.35f, -0.5f);			
			card.Obj = (GameObject)Instantiate(card_prefab, card.Position, Quaternion.identity);
			card.Obj.transform.Rotate(0f, 180f, 0f);
			card.Obj.AddComponent("Animation");
			card.Obj.animation.playAutomatically = false;
			card.Obj.tag = "MYCARD" + (roopval+1).ToString();
			card.Card = -1;
			
			MyCard.Add(card);
			xpos += 0.65f;
		}
		
		//animations 추가
		MyCard[0].Obj.animation.AddClip(Card1Drop, "CardDrop");
		MyCard[1].Obj.animation.AddClip(Card2Drop, "CardDrop");
		MyCard[2].Obj.animation.AddClip(Card3Drop, "CardDrop");
		MyCard[3].Obj.animation.AddClip(Card4Drop, "CardDrop");
		MyCard[4].Obj.animation.AddClip(Card5Drop, "CardDrop");			
		
		
		//PCPlayer Setting
		xpos = -2.0f;
		for(roopval=0;roopval<PCPlayerCount;roopval++)
		{
			sPCPlayer pcplayer = new sPCPlayer();
			pcplayer.Position = new Vector3(xpos, 1.6f, -0.1f); 
			pcplayer.Obj = (GameObject)Instantiate(pcPlayer, pcplayer.Position, Quaternion.identity);
			((TextMesh)pcplayer.Obj.transform.FindChild("Name").gameObject.GetComponent("TextMesh")).text = "Computer" + (roopval+1).ToString();
			pcplayer.LifeCount = 3;
			pcplayer.cards = new int[5];	//카드 5장 넣을 공간 만들기
			
			
			PcPlayer.Add(pcplayer);			
			xpos += 0.7f;
		}
		
		CloseCard = GameObject.FindGameObjectWithTag("CLOSECARD");	
		OpenCard = GameObject.FindGameObjectWithTag("OPENCARD");	
		PCCard = GameObject.Find("PCCard");
		
		transImg = (Texture2D)Resources.Load("images/transimg", typeof(Texture2D));

		cardTexture["-10"] = (Texture2D)Resources.Load("images/-10", typeof(Texture2D));
		cardTexture["0"] = (Texture2D)Resources.Load("images/0", typeof(Texture2D));
		cardTexture["2"] = (Texture2D)Resources.Load("images/2", typeof(Texture2D));
		cardTexture["3"] = (Texture2D)Resources.Load("images/3", typeof(Texture2D));
		cardTexture["4"] = (Texture2D)Resources.Load("images/4", typeof(Texture2D));
		cardTexture["5"] = (Texture2D)Resources.Load("images/5", typeof(Texture2D));
		cardTexture["6"] = (Texture2D)Resources.Load("images/6", typeof(Texture2D));
		cardTexture["7"] = (Texture2D)Resources.Load("images/7", typeof(Texture2D));
		cardTexture["8"] = (Texture2D)Resources.Load("images/8", typeof(Texture2D));
		cardTexture["9"] = (Texture2D)Resources.Load("images/9", typeof(Texture2D));
		cardTexture["10"] = (Texture2D)Resources.Load("images/10", typeof(Texture2D));
		cardTexture["11"] = (Texture2D)Resources.Load("images/11", typeof(Texture2D));
		cardTexture["22"] = (Texture2D)Resources.Load("images/22", typeof(Texture2D));
		cardTexture["33"] = (Texture2D)Resources.Load("images/33", typeof(Texture2D));
		cardTexture["44"] = (Texture2D)Resources.Load("images/44", typeof(Texture2D));
		cardTexture["55"] = (Texture2D)Resources.Load("images/55", typeof(Texture2D));
		cardTexture["66"] = (Texture2D)Resources.Load("images/66", typeof(Texture2D));
		cardTexture["76"] = (Texture2D)Resources.Load("images/76", typeof(Texture2D));
		cardTexture["U"] = (Texture2D)Resources.Load("images/U", typeof(Texture2D));
		cardTexture["X2"] = (Texture2D)Resources.Load("images/X2", typeof(Texture2D));

	}
	

	void DropCard(string cardtag)
	{		
		if(gameState == GameState.PLAY)
		{
			if(bDropGetAniPlaying) return;
			SendMessage("DropCardAnimation",cardtag,SendMessageOptions.DontRequireReceiver);					
		}		
	}
	
	IEnumerator DropCardAnimation(string cardtag)
	{
	
		if(cardtag.Substring(0,6) == "MYCARD")
		{
			bDropGetAniPlaying = true;
			string cardidx = cardtag.Substring(6,1);
			sMyCard myCard = MyCard[int.Parse(cardidx) - 1];
			myCard.Obj.animation.Play("CardDrop");
			yield return new WaitForSeconds(1f);
			lastDropCard = AllCards[myCard.Card].card;
			print ("My Turn : DropCard [" + lastDropCard + "]");						
			DropedCardCount++;
			CloseCard.animation.Play("Card" + cardidx + "Get");
			yield return new WaitForSeconds(1f);
			int newCard = DropCardProc(myCard.Card);
			myCard.Card = newCard;			
			myCard.Obj.transform.position = myCard.Position;
			MyCard[int.Parse(cardidx) - 1] = myCard;
			
			CloseCard.transform.position = new Vector3(0f, -0.04f, -0.6f);
			CardDropCnt--;			
			TotalCheck();
			yield return new WaitForSeconds(2f);		
			bDropGetAniPlaying = false;
		}		
		
		if(cardtag.Substring(0,6) == "PCCARD")
		{
			bDropGetAniPlaying = true;
			string cardidx = cardtag.Substring(6,1);
			sPCPlayer pcplayer = PcPlayer[Turn - 1];
			PCCard.renderer.material.mainTexture = cardTexture[AllCards[pcplayer.cards[int.Parse(cardidx)]].card];
			PCCard.animation.Play("PCDrop");
			yield return new WaitForSeconds(1f);
			lastDropCard = AllCards[pcplayer.cards[int.Parse(cardidx)]].card;
			print ("Com Turn[" + (Turn-1) +"] : DropCard [" + lastDropCard + "]");						
			DropedCardCount++;
			CloseCard.animation.Play("PCGet");
			yield return new WaitForSeconds(1f);			
			int newCard = DropCardProc(pcplayer.cards[int.Parse(cardidx)]);
			pcplayer.cards[int.Parse(cardidx)] = newCard;				
			PCCard.transform.position = new Vector3(0.75f, 2.4f, -0.45f);
			PcPlayer[Turn - 1] = pcplayer;
			CloseCard.transform.position = new Vector3(0f, -0.04f, -0.6f);
			CardDropCnt--;
			TotalCheck();			
			yield return new WaitForSeconds(2f);			
			bDropGetAniPlaying = false;

		}					
	}
	
	int DropCardProc(int dropCard)
	{
		AllCards[dropCard].usedType = 2;
		return GetCardOne();
	}
	
	void CardReset()
	{
		for(int i=0;i<56;i++)
		{
			AllCards[i].usedType = 0;			
		}
		for(int x=0;x<10;x++)
			MixCard();
	}
	
	void MixCard()
	{
		
		for(int i=0;i<55;i++)
		{
			for(int j=i+1;j<56;j++)
			{				
				// 50%확률로 카드를 뒤바꾼다.
				if(Random.Range(0, 100) < 50)		
				{	
					if(AllCards[i].usedType == 0 && AllCards[j].usedType == 0)
					{
						string strTemp = AllCards[i].card;
						AllCards[i].card = AllCards[j].card;
						AllCards[j].card = strTemp;
					}
				}
			}
		}
		
	}
	
	bool IsDropCard()
	{
		for(int i=0;i<56;i++)
		{
			if(AllCards[i].usedType == 2)	
				return true;
		}
		return false;
	}
	
	//모든 카드가 다 버려졌을 경우 버려진카드를 모두 수거한 후 다시 섞는다.
	void CheckAllCardDrop()
	{		
		for(int i=0;i<56;i++)
		{
			//남은 카드가 1장이라도 있을경우 리턴
			if(AllCards[i].usedType == 0)	
				return;
		}
		
		for(int i=0;i<56;i++)
		{			
			//아니라면 모두 수거한후 다시 섞는다. 
			if(AllCards[i].usedType == 2)
				AllCards[i].usedType = 0;
		}
		DropedCardCount = 0;
		MixCard();		
	}
	
	int GetCardOne()	{	
		
		for(int i=0;i<56;i++)
		{
			if(AllCards[i].usedType == 0)
			{
				AllCards[i].usedType = 1;
				return i;
			}			
		}
		
		CheckAllCardDrop();	
		return GetCardOne(); 
	}
	

	
		
}


