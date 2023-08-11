using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klondike
{
    public class MoveCards
    {
        public CardArea ca;
        public int idxY, idxX;
        public List<PictureBox> picBoxs = new List<PictureBox>();
        public List<Card> cards = new List<Card>();
        Bitmap bmpCards;
        Control.ControlCollection Controls;
        Point clicked;//클릭한 카드의 왼쪽위 좌표
        public int clickY, clickX,pos;

        public MoveCards(CardArea ca, int x, int y, int pos, 
            Bitmap bmpCards, Control.ControlCollection Controls)
        {
            this.bmpCards = bmpCards;
            this.Controls = Controls;
            this.clickX = x;
            this.clickY = y;
            this.ca = ca;
            this.pos = pos;
            //this.idxY = idxY;

        }
//List<Card>
        int a,b;
        public void addCards(Card add, Point clicked)
        {
            this.cards.Add(add);


        }
        public void addCards(List<Card> add, Point clicked)
        {
            this.cards.AddRange(add);
            this.clicked = clicked;

            //Point newPt = new Point();

            for (int i = add.Count; i > 0; i--)    //(int i=0; i<add.Count; i++)
            {
                Card c = add[i-1];
                makePictureBox(c.nowPos.Location, c.imgPos);
            }

        }
        

        public void makePictureBox(Point loc, Rectangle imgRect)
        {
            Bitmap ar = bmpCards.Clone(imgRect, System.Drawing.Imaging.PixelFormat.DontCare);
            PictureBox pb = new PictureBox();
            // a = loc.X+ clickX ;
            //  b = loc.Y+clickY ;
            //  pb.Location = new Point(a,b);
            pb.Location = loc;
            pb.Name = "pb";


            pb.Size = new Size(imgRect.Size.Width, imgRect.Size.Height);
            pb.Image = ar;
            Controls.Add(pb);
            picBoxs.Add(pb);
        }
        
        public void moveBoxes(Point newClicked)
        {

          newClicked.X =  newClicked.X + clickX;
            newClicked.Y = newClicked.Y + clickY + picBoxs.Count * Card.OVER_MARGIN;
          for (int i =0;i < picBoxs.Count; i++)
            {

                newClicked.Y -= Card.OVER_MARGIN;//clickY;
                picBoxs[i].Location = newClicked;

            }
        }
        public void moveCard(Point startpt, int margin)
        {

        }
        public void removeBox()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                cards.Remove(cards.Last());
            }

            for (int i = picBoxs.Count-1; i >= 0; i--)
            {
                Controls.Remove(picBoxs.Last());
                picBoxs.Remove(picBoxs.Last());  
            }

        }
        
    }
}
