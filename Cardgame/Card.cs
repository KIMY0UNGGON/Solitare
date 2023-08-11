using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klondike
{
    public class Card
    {
        public const int CARD_WIDTH = 109;
        public const int CARD_HEIGHT = 153;
        public const int CARD_MARGIN_X = 19;
        public const int CARD_MARGIN_Y = 13;

        public const int OVER_MARGIN = 20;
        public const int OVER_MARGIN_BACK = 5;
        public Rectangle imgPos, nowPos;
        public int cardY;
        public int cardX;



        public bool fliped;
        public Card(Rectangle[][] deckOriRect, int cardNum)
        {
            this.cardY = cardNum / 13;
            this.cardX = cardNum % 13;
            imgPos = deckOriRect[cardY][cardX];
            fliped = false;
        }
        public Card(Rectangle[][] deckOriRect, int y, int x)
        {
            this.cardY = y;
            this.cardX = x;
            imgPos = deckOriRect[cardY][cardX];
            fliped = false;
        }
        public void setPos(Rectangle nowPos)
        {
            this.nowPos = nowPos;
        }
        public void flip(Rectangle nowPos)
        {
            this.nowPos = nowPos;
            fliped = !fliped;
        }
        public bool contains(Point clicked)
        {
            return nowPos.Contains(clicked);
        }
        public bool contains(Rectangle r)
        {
            return nowPos.IntersectsWith(r);
        }
        public static Rectangle makeCard(int cx, int cy)
        {
            return new Rectangle(cx, cy, Card.CARD_WIDTH, Card.CARD_HEIGHT);
        }
       
    }
}
