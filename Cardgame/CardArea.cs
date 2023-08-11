using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klondike
{
    public class CardArea
    {
        public static int MARGIN = 50;
        Rectangle[][] rectAllImg;
        Bitmap bmpBack, bmpCards;
        Graphics memDC;
        Control.ControlCollection Controls;

        public Rectangle[] rectAreas;//
        public List<Card>[] cardofArea;
        public Rectangle rectAll;//이 영역을 빨간색 사각형으로 그려보기

        public CardArea(Rectangle[][] rectAllImg, int stX, int stY, int count,
            Bitmap bmpBack, Bitmap bmpCards, Graphics memDC,
            Control.ControlCollection Controls, Rectangle[] rect, bool flag = true)
        {
            int a = 0; 
            rectAreas = new Rectangle[count];
            cardofArea = new List<Card>[count];
            rectAll = new Rectangle();
            rectAll.X = stX; 
            rectAll.Y = stY;
           
            for (int i = 0; i < count; i++)
            {
                cardofArea[i] = new List<Card>();
                rectAreas[i] = Card.makeCard(rect[i].X,rect[i].Y);//Card.makeCard(stX,stY);
                //stX += ma;//(MARGIN+Card.CARD_WIDTH);

                
    
            }
            //rectAll.Height = rectAreas[0].Height;
            //rectAll.Width = a + (MARGIN * (count - 1));

            //rectAll.Height = 670;
            //rectAll.Width =1000;

            if(count == 2)
            {
                rectAll.Height = Card.CARD_HEIGHT;
                rectAll.Width = 759;
            }
            if(count == 7)
            {
                rectAll.Height = 702;
                rectAll.Width = 1095;
            }
            if(count == 4)
            {
                rectAll.Height = 350;
                rectAll.Width = 458;
            }


           // memDC.DrawRectangle(Pens.Red, rectAll);
            this.memDC = memDC;
            this.rectAllImg = rectAllImg;
            this.bmpCards = bmpCards;
            this.bmpBack = bmpBack;
            this.Controls = Controls;
        }
        public void update()
        {
            for (int a = 0; a < cardofArea.Length; a++)
            {
                setEmptyImg(rectAreas[a]);
                if (cardofArea[a].Count == 0)
                {
                    setBorderImg(rectAreas[a]);
                }
                else
                {
                    for (int i = 0; i < cardofArea[a].Count; i++)

                        setCardImg(cardofArea[a][i]);

                }
            }
        }

        public void init()
        {  
            for(int a = 0; a < cardofArea.Length; a++)
            {
                if(cardofArea[a].Count == 0)
                {
                     setBorderImg(rectAreas[a]);
                }
                else
                {
                    for(int i =0;i < cardofArea[a].Count; i++)
                        setCardImg(cardofArea[a][i]);
                }                   
            }
        }
        
        public void setCardImg(Card c)
        {
            if (c.fliped)
            {
                memDC.DrawImage(bmpCards, c.nowPos ,c.imgPos ,GraphicsUnit.Pixel);
            }
            else
            {
                memDC.DrawImage(bmpCards, c.nowPos,
                    rectAllImg[(int)CardGame.CardShape.Back][0], GraphicsUnit.Pixel);
            }
        }
        public void setBorderImg(Rectangle rect)
        {
            memDC.DrawImage(bmpCards, rect,
                rectAllImg[(int)CardGame.CardShape.Border][0], GraphicsUnit.Pixel);
        }
        public void setEmptyImg(Rectangle rect)
        {
            memDC.DrawImage(bmpBack, rect, rect, GraphicsUnit.Pixel);
        }
        public bool contains(Point clicked)
        {
            if (rectAll.Contains(clicked))
                return true;
            else
                return false;
        }
        public int cardcontain(Point clicked)//, out)
        {
            int idx = -1;

            for (int i = 0; i < rectAreas.Length; i++)
            {
               
               
               if (rectAreas[i].Contains(clicked))
               {
                   idx = i;
                   break;
               }
             }

            if (idx == -1)
                return -1;
            else
                return idx;
        }
        public int cacontain(Point pt)
        {
            int idx = -1;

            for (int i = 0; i < rectAreas.Length; i++)
            {
                for (int a = 0; a < cardofArea[i].Count; a++)
                {
                    if (cardofArea[i][a].nowPos.Contains(pt))
                    {
                        idx = i;
                        break;
                    }
                }
                if (idx != -1)
                    break;
            }
            if (idx == -1)
                return -1;
            else
                return idx;
            
        }
        public int licontain(Point pt,int pos)
        {
           
            int ax = -1;

                for (int a = cardofArea[pos].Count-1; a >= 0 ; a--)
                {
                    if (cardofArea[pos][a].nowPos.Contains(pt))
                    {
                   
                        ax = a;
                  
                        break;
                    }
                }

            if (ax == -1)
                return -1;
            else
                return ax;

        }
    }
}
