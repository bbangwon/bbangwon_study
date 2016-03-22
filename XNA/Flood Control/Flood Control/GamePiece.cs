using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Flood_Control
{
    class GamePiece
    {
        public static string[] PieceTypes = 
        {
            "Left,Right",
            "Top,Bottom",
            "Left,Top",
            "Top,Right",
            "Right,Bottom",
            "Bottom,Left",
            "Empty"
        };  //파이프 타입들. 모든 GamePiece 인스턴스가 같은 배열의 데이터를 사용할수 있도록 static로 선언

        
        public const int PieceHeight = 40;
        public const int PieceWidth = 40;
        
        public const int MaxPlayablePieceIndex = 5; //파이프 인덱스 최대값
        public const int EmptyPieceIndex = 6;       //비어있는 파이프 인덱스

        //스프라이트시트에서 원하는 타입의 파이프 스프라이트 영역을 얻는데 필요한 정보
        private const int textureOffsetX = 1;   
        private const int textureOffsetY = 1;
        private const int texturePaddingX = 1;
        private const int texturePaddingY = 1;

        private string pieceType = "";      //타입은 뭐인감?
        private string pieceSuffix = "";    //물로 채워져있는지?

        public string PieceType
        {
            get { return pieceType;  }
        }

        public string Suffix
        {
            get { return pieceSuffix;  }
        }


        public GamePiece(string type, string suffix)
        {
            pieceType = type;
            pieceSuffix = suffix;
        }

        public GamePiece(string type)
        {
            pieceType = type;
            pieceSuffix = "";
        }

        public void SetPiece(string type, string suffix)
        {
            pieceType = type;
            pieceSuffix = suffix;
        }

        public void SetPiece(string type)
        {
            pieceType = type;
            pieceSuffix = "";
        }

        public void AddSuffix(string suffix)
        {
            if (!pieceSuffix.Contains(suffix))
                pieceSuffix += suffix;
        }

        public void RemoveSuffix(string suffix)
        {
            pieceSuffix = pieceSuffix.Replace(suffix, "");
        }

        public void RotatePiece(bool Clockwise)
        {
            switch(pieceType)
            {
                case "Left,Right":
                    pieceType = "Top,Bottom";
                    break;
                case "Top,Bottom":
                    pieceType = "Left,Right";
                    break;
                case "Left,Top":
                    if (Clockwise)
                        pieceType = "Top,Right";
                    else
                        pieceType = "Bottom,Left";
                    break;
                case "Top,Right":
                    if (Clockwise)
                        pieceType = "Right,Bottom";
                    else
                        pieceType = "Left,Top";
                    break;
                case "Right,Bottom":
                    if (Clockwise)
                        pieceType = "Bottom,Left";
                    else
                        pieceType = "Top,Right";
                    break;
                case "Bottom,Left":
                    if (Clockwise)
                        pieceType = "Left,Top";
                    else
                        pieceType = "Right,Bottom";
                    break;
                case "Empty":
                    break;
            }
        }

        public string[] GetOtherEnds(string startingEnd)
        {
            List<string> opposites = new List<string>();
            foreach(string end in pieceType.Split(','))
            {
                if (end != startingEnd)
                    opposites.Add(end);
            }
            return opposites.ToArray();
        }

        public bool HasConnector(string direction)
        {
            return pieceType.Contains(direction);
        }

        public Rectangle GetSourceRect()
        {
            int x = textureOffsetX;
            int y = textureOffsetY;

            if (pieceSuffix.Contains("W"))
                x += PieceWidth + texturePaddingX;
            y += (Array.IndexOf(PieceTypes, pieceType) * (PieceHeight + texturePaddingY));

            return new Rectangle(x, y, PieceWidth, PieceHeight);
        }


    }
}
