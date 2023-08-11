using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klondike
{
    public partial class CardGame : Form
    {
        Bitmap bmpBack, bmpCards;
        Graphics memDC;
        Bitmap memBit;
        Graphics aiDC;
        Bitmap aiBit;
        bool gamestart = false;
        //bool hintstate = false;
       // public int statecount = 0;
        public enum CardShape { Clover, Diamond, Spade, Heart, Jocker, Back, Border };
        enum AREA { DECK, SEVEN, FOUR };
        
        
         
        Rectangle[][] rectAllImg = new Rectangle[7][];
        List<Card> allCards = new List<Card>();
        CardArea[] allArea = new CardArea[3];
        MoveCards m_mc;

        bool hintflag = false;
        bool flagGame = false, flagDrag = false;
        private void initBackImage()
        {
            Image backimg = global::Klondike.Properties.Resources.back5;
            bmpBack = new Bitmap(backimg, new Size(1350, 964));//((CardArea.MARGIN + Card.CARD_WIDTH) * 7 + CardArea.MARGIN,(CardArea.MARGIN + Card.CARD_HEIGHT) * 2 + Card.OVER_MARGIN * 13 + Card.OVER_MARGIN_BACK * 6 + CardArea.MARGIN));
            this.Width = 1366;
            this.Height = 1026;
           //this.Width += bmpBack.Width - ClientRectangle.Width;
           //this.Height += bmpBack.Height - ClientRectangle.Height + menuStrip1.Height;
        }
        private void initDC()
        {
            memBit = new Bitmap(this.Width, this.Height);
            memDC = Graphics.FromImage(memBit);
            
        }
        private void Aiinit()
        {
            aiBit = new Bitmap(this.Width, this.Height);
            aiDC = Graphics.FromImage(aiBit);
        }
        private void initImage()
        {
            Image cardimg = global::Klondike.Properties.Resources.deck3;
            bmpCards = new Bitmap(cardimg);
            
            for (int y = 0; y < (Card.CARD_HEIGHT*4+Card.CARD_MARGIN_Y*3); y+=(Card.CARD_HEIGHT+Card.CARD_MARGIN_Y))
            {
                rectAllImg[(y / (Card.CARD_HEIGHT + Card.CARD_MARGIN_Y))] = new Rectangle[13];
                for (int x = 0; x < (Card.CARD_WIDTH * 13 + Card.CARD_MARGIN_X * 12); x += (Card.CARD_WIDTH + Card.CARD_MARGIN_X))
                {

                    Rectangle ca = Card.makeCard(x, y);
                    rectAllImg[(y / (Card.CARD_HEIGHT + Card.CARD_MARGIN_Y))][(x / (Card.CARD_WIDTH + Card.CARD_MARGIN_X))] = ca;
                    Card card = new Card(rectAllImg, (y / (Card.CARD_HEIGHT + Card.CARD_MARGIN_Y)), (x / (Card.CARD_WIDTH + Card.CARD_MARGIN_X)));
                   // card.setPos(ca);
                    card.imgPos = ca;
                    //memDC.DrawImage(bmpCards, card.nowPos, ca, GraphicsUnit.Pixel);
                    allCards.Add(card);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                Rectangle ca = Card.makeCard(i*(Card.CARD_WIDTH + Card.CARD_MARGIN_X),4*( Card.CARD_HEIGHT + Card.CARD_MARGIN_Y));
                rectAllImg[i+4] = new Rectangle[1];
                rectAllImg[i+4][0] = ca;
            } 
        }
        private void drawBackImg()
        {
            memDC.DrawImage(bmpBack, new Point(0, 0));
        }
        private void CardGame_Load(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.ShowDialog();
            Form2 f2 = new Form2();
            f2.ShowDialog();
            //gReady();
            timer2.Start();
            timer1.Start();
            timer3.Start();
            this.DoubleBuffered = true;
            
            initBackImage();
            initDC();
            drawBackImg();
            initImage();
            initCardArea();
            flagGame = true;
            //Aiinit();
            //amestart = false;

        }
        private void gstart()
        {
            Controls.Clear();
            timer2.Start();
            timer1.Start();
            this.DoubleBuffered = true;
            initBackImage();
            initDC();
            drawBackImg();
            initImage();
            initCardArea();
            flagGame = true;
        }
        private void gReady()
        {
            Button b1 = new Button();
            b1.Location = new System.Drawing.Point(500,300);
            b1.Name = "button1";
            b1.Size = new System.Drawing.Size(75, 40);
            b1.TabIndex = 2;
            b1.Text = "게임 스타트";
            b1.UseVisualStyleBackColor = true;
            b1.Click += new System.EventHandler(this.b1_Click);
            Controls.Add(b1);
        }
        private void b1_Click(object sender, EventArgs e)
        {
            gamestart = true;
            gstart();
        }

        private void 설명ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.ShowDialog();
        }

        private void CardGame_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(memBit, 0, menuStrip1.Height);
            if (hintflag)
            {
                e.Graphics.DrawImageUnscaled(aiBit, 0, menuStrip1.Height);
            }
        }

        public CardGame()
        {
            InitializeComponent();
        }

        bool vic()
        {
            int a = 0;
            for (int i = 0; i < 4; i++)
            {
                if (allArea[2].cardofArea[i].Count == 13)
                    a++;
            }
            if (a == 4) return true;
            else return false;
        }

        private void CardGame_MouseUp(object sender, MouseEventArgs e)
        {
            //if (gamestart)
            //{
                if (flagGame)
                {
                    if (flagDrag == true)
                    {
                        bool flag = false;
                        int idx = -1;
                        //if (e.Button == MouseButtons.Right) return;
                        for (int i = 2; i >= 0; i--)
                        {
                            if (allArea[i].contains(eloc(e.Location)))
                            {
                                idx = i;
                                break;
                            }
                        }
                        if (e.Button == MouseButtons.Left)
                        {
                            if (idx == (int)AREA.DECK)
                            {
                                CardArea ca = allArea[idx];
                                int pos = ca.cardcontain(eloc(e.Location));
                                while (true)
                                {
                                    if (pos != -1 && m_mc == null)
                                    {
                                        flag = true;
                                        break;
                                    }
                                    else
                                    {
                                        re();
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    if (pos == 0)
                                    {
                                        if (ca.cardofArea[pos].Count == 0)
                                        {
                                            var cf = ca.cardofArea[1].Select(c => c);
                                            foreach (var cg in cf)
                                                cg.flip(ca.rectAreas[0]);
                                            ca.cardofArea[1].Reverse();
                                            ca.cardofArea[pos].AddRange(ca.cardofArea[1]);
                                            ca.cardofArea[1].Clear();
                                        }
                                        else
                                        {
                                            Card card = ca.cardofArea[pos].Last();
                                            ca.cardofArea[1].Add(card);
                                            card.flip(ca.rectAreas[1]);
                                            ca.cardofArea[pos].Remove(ca.cardofArea[pos].Last());
                                        }
                                    }
                                }
                                ca.update();
                            }
                            else if (idx == (int)AREA.FOUR)
                            {

                                CardArea ca = allArea[idx];

                                int p = ca.cardcontain(eloc(e.Location));
                                while (true)
                                {

                                    if (m_mc != null && p != -1 && m_mc.cards.Last().cardX == ca.cardofArea[p].Count)
                                    {
                                        if (ca.cardofArea[p].Count == 0 || m_mc.cards.Last().cardY == ca.cardofArea[p].Last().cardY)
                                        {
                                            flag = true;
                                            break;
                                        }
                                        else
                                        {
                                            re();
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        re();
                                        break;
                                    }

                                }
                            
                            if (flag)
                                {
                                    if (m_mc != null && m_mc.picBoxs.Count > 0)
                                    {

                                        if (ca.cardofArea[p].Count == 0 || ca.cardofArea[p].Last().cardY == m_mc.cards.Last().cardY)
                                        {

                                            ca.cardofArea[p].Add(m_mc.cards.Last());
                                            ca.cardofArea[p].Last().setPos(ca.rectAreas[p]);
                                            m_mc.removeBox();
                                            //m_mc.cards.Remove(m_mc.cards.Last());
                                            m_mc = null;
                                            ca.update();
                                        }
                                    }
                                    if (hintflag == true)
                                    {
                                        if (ca.cardofArea[p].Count != 0)
                                        {
                                            if ( ca.cardofArea[p].Last().cardX == HcardX && ca.cardofArea[p].Last().cardY == HcardY)
                                            {
                                                hintflag = false;

                                                Seven = -1;
                                                amount = -1;
                                                aiDC.Dispose();
                                                //hintstate = false;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (idx == (int)AREA.SEVEN)
                            {
                                CardArea ca = allArea[idx];
                                int pos = ca.cardcontain(eloc(e.Location));

                                while (true)
                                {
                                    if (pos != -1)
                                    {
                                        flag = true;
                                        break;
                                    }
                                    else
                                    {
                                        re();
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    if (ca.cardofArea[pos].Count != 0)
                                    {
                                        Card card = ca.cardofArea[pos].Last();
                                        if (card.fliped == false)
                                        {
                                            if (m_mc == null)
                                            {
                                                card.flip(ca.cardofArea[pos].Last().nowPos);
                                                ca.update();
                                            }
                                            else
                                            {
                                                re();
                                            }
                                        }
                                    }


                                    if (m_mc != null && m_mc.picBoxs.Count > 0)
                                    {
                                        int a = ca.cardofArea[pos].Count;
                                        // int b = 0;

                                        if (ca.cardofArea[pos].Count == 0)
                                        {

                                            for (int i = 0; i < m_mc.cards.Count; i++)
                                            {

                                                ca.cardofArea[pos].Add(m_mc.cards[i]);
                                                ca.cardofArea[pos][i].setPos(Card.makeCard(ca.rectAreas[pos].X, ca.rectAreas[pos].Y + i * Card.OVER_MARGIN));
                                                if (m_mc.cards.Count > 1) ca.rectAreas[pos].Height = Card.CARD_HEIGHT + (ca.cardofArea[pos].Count) * Card.OVER_MARGIN;
                                                else ca.rectAreas[pos].Height = Card.CARD_HEIGHT + (ca.cardofArea[pos].Count - 1) * Card.OVER_MARGIN;
                                                ca.rectAll.Height += Card.OVER_MARGIN;

                                            }

                                            m_mc.removeBox();

                                            m_mc = null;
                                            ca.update();
                                        }
                                        else
                                        {
                                            int count = ca.cardofArea[pos].Count;
                                            int pa = ca.cacontain(eloc(e.Location));
                                            if (pa == -1)
                                            {
                                                re();
                                                return;
                                            }
                                            int sy = ca.cardofArea[pa].Last().nowPos.Y;

                                            //card X 숫자 card Y 색깔
                                            int cy = ca.cardofArea[pa].Last().cardY;
                                            int cy1 = m_mc.cards[0].cardY;
                                            int cx = ca.cardofArea[pa].Last().cardX;
                                            int cx1 = m_mc.cards[0].cardX;
                                            if (ca.cardofArea[pa].Last().cardY != m_mc.cards[0].cardY)
                                            {
                                                if (cy == 0 && cy1 == 1 ||
                                                    cy == 0 && cy1 == 3 ||
                                                    cy == 1 && cy1 == 0 ||
                                                    cy == 1 && cy1 == 2 ||
                                                    cy == 2 && cy1 == 1 ||
                                                    cy == 2 && cy1 == 3 ||
                                                    cy == 3 && cy1 == 0 ||
                                                    cy == 3 && cy1 == 2)
                                                {
                                                    if (cx1 == cx - 1)
                                                    {
                                                        for (int i = 0; i < m_mc.cards.Count; i++)
                                                        {

                                                            ca.cardofArea[pa].Add(m_mc.cards[i]);

                                                            int testa = ca.cardofArea[pa].Count;
                                                            int testb = m_mc.cards.Count;

                                                            int ia = ca.cardofArea[pa].Count - 1;
                                                            ca.cardofArea[pa][ia].setPos(Card.makeCard(ca.rectAreas[pa].X, sy + (i + 1) * Card.OVER_MARGIN));

                                                        }
                                                        if (m_mc.cards.Count == 1)
                                                            ca.rectAreas[pos].Height = Card.CARD_HEIGHT + (ca.cardofArea[pos].Count - 1) * Card.OVER_MARGIN;
                                                        else
                                                            ca.rectAreas[pos].Height = Card.CARD_HEIGHT + (ca.cardofArea[pos].Count) * Card.OVER_MARGIN;

                                                        ca.rectAll.Height += Card.OVER_MARGIN;

                                                        m_mc.removeBox();
                                                        m_mc = null;

                                                        ca.update();
                                                    }
                                                    else re();

                                                }
                                                else re();

                                            }
                                            else
                                            {
                                                re();
                                            }
                                        }

                                    }
                                }
                            }
                            else
                            {
                                re();

                            }

                            Invalidate();
                            flagDrag = false;
                        }
                        if (e.Button == MouseButtons.Right)
                        {
                            CardArea ca2 = allArea[2];

                            if (m_mc == null)
                            {
                                if (idx == (int)AREA.DECK)
                                {
                                    CardArea ca = allArea[idx];
                                    int p = ca.cardcontain(eloc(e.Location));
                                if (hintflag == true && Dflag == true)
                                {
                                    
                                        hintflag = false;
                                        Dflag = false;
                                        Seven = -1;
                                        amount = -1;
                                        aiDC.Dispose();
                                        //hintstate = false;
                          
                                }
                                if (ca.cardofArea[1].Count > 0 && p == 1)
                                {
                                    int cy = ca.cardofArea[p].Last().cardY;
                                    int cx = ca.cardofArea[p].Last().cardX;

                                    for (int i = 0; i < allArea[2].cardofArea.Count(); i++)
                                    {

                                        if (ca2.cardofArea[i].Count == 0 && cx == 0)
                                        {
                                            ca2.cardofArea[i].Add(ca.cardofArea[1].Last());
                                            ca2.cardofArea[i].Last().setPos(ca2.rectAreas[i]);
                                            ca.setEmptyImg(ca.cardofArea[p].Last().nowPos);
                                            ca.cardofArea[p].Remove(ca.cardofArea[p].Last());
                                            break;
                                        }
                                        else if (cx == ca2.cardofArea[i].Count && cy == ca2.cardofArea[i].Last().cardY)
                                        {
                                            ca2.cardofArea[i].Add(ca.cardofArea[1].Last());

                                            ca2.cardofArea[i].Last().setPos(ca2.rectAreas[i]);
                                            ca.setEmptyImg(ca.cardofArea[p].Last().nowPos);
                                            ca.cardofArea[p].Remove(ca.cardofArea[p].Last());
                                            break;
                                        }

                                    }

                                }

                                ca.update();

                                }
                                else if (idx == (int)AREA.SEVEN)
                                {
                                    CardArea ca = allArea[idx];
                                    int pos = ca.cardcontain(eloc(e.Location));
                                    if (pos == -1)
                                        return;
                                    if (ca.cardofArea[pos].Count == 0)
                                        return;
                                    if (ca.cardofArea[pos].Last().fliped == false)
                                        return;


                                    int cy = ca.cardofArea[pos].Last().cardY;
                                    int cx = ca.cardofArea[pos].Last().cardX;
                                    if (hintflag == true && Dflag == false)
                                    {
                                    if (Seven != -1 && amount != -1)
                                    {
                                        if (ca.cardofArea[pos].Last() == ca.cardofArea[Seven][amount])
                                        {
                                            hintflag = false;

                                            Seven = -1;
                                            amount = -1;
                                            aiDC.Dispose();
                                            //hintstate = false;
                                        }
                                    }

                                    }
                                    for (int i = 0; i < allArea[2].cardofArea.Count(); i++)
                                    {
                                        Card c = ca.cardofArea[pos].Last();
                                        ca.setEmptyImg(c.nowPos);
                                        if (cx == 0 && allArea[2].cardofArea[i].Count == 0)
                                        {
                                            ca2.cardofArea[i].Add(c);
                                            ca2.cardofArea[i].Last().setPos(ca2.rectAreas[i]);

                                            ca.cardofArea[pos].Remove(ca.cardofArea[pos].Last());
                                            ca.rectAreas[pos].Height = Card.CARD_HEIGHT;
                                            break;

                                        }
                                        else if (cx == ca2.cardofArea[i].Count && cy == ca2.cardofArea[i].Last().cardY)
                                        {

                                            ca2.cardofArea[i].Add(c);
                                            ca2.cardofArea[i].Last().setPos(ca2.rectAreas[i]);

                                            ca.cardofArea[pos].Remove(ca.cardofArea[pos].Last());



                                            ca.rectAreas[pos].Height = Card.CARD_HEIGHT + ca.cardofArea[pos].Count * Card.OVER_MARGIN;

                                            break;
                                        }

                                    }

                                    ca.update();
                                }
                            }
                            else re();

                            ca2.update();
                            Invalidate();
                            flagDrag = false;
                        }
                    }
                }
            //}
        }

        private void initCardArea()
        {
            Rectangle[] rect = new Rectangle[2];
            Rectangle[] rect1 = new Rectangle[7];
            Rectangle[] rect2 = new Rectangle[4];

            rect[0].Location = new Point(459, 50);
            rect[1].Location = new Point(768, 50);

            rect1[0].Location = new Point(300, 223);
            rect1[1].Location = new Point(927, 223);

            rect1[2].Location = new Point(141, 396);
            rect1[3].Location = new Point(1095, 396);

            rect1[4].Location = new Point(300, 569);
            rect1[5].Location = new Point(927, 569);

            rect1[6].Location = new Point(618, 589);

            rect2[0].Location = new Point(618, 223);
            rect2[1].Location = new Point(459, 396);
            rect2[2].Location = new Point(618, 396);
            rect2[3].Location = new Point(777, 396);//569);

            allArea[(int)AREA.DECK] =
                new CardArea(rectAllImg,300,50,2,bmpBack,bmpCards,memDC,Controls,rect);
            allArea[(int)AREA.SEVEN] = 
                new CardArea(rectAllImg, 121, 223, 7, bmpBack, bmpCards, memDC, Controls,rect1);
            allArea[(int)AREA.FOUR] = new CardArea(rectAllImg, 439, 223, 4, bmpBack, bmpCards, memDC, Controls,rect2);


            //allArea[(int)AREA.SEVEN] = new CardArea(rectAllImg, allArea[(int)AREA.DECK].rectAreas[0].Left, allArea[(int)AREA.DECK].rectAreas[0].Bottom + 50, 7, bmpBack, bmpCards, memDC, Controls);
            //allArea[(int)AREA.FOUR] = new CardArea(rectAllImg, 100 + allArea[(int)AREA.DECK].rectAreas[1].Right + allArea[(int)AREA.DECK].rectAreas[0].Width, 50, 4, bmpBack, bmpCards, memDC, Controls);
            CardDistribution();

            for (int i =0; i<=2; i++)
            {
                allArea[i].init();
            }

        }

        private void CardGame_MouseDown(object sender, MouseEventArgs e)
        {
            if (flagGame)
            {
                if (flagDrag)
                    return;
                flagDrag = true;

                Point eLocation = eloc(e.Location);


                // if (e.Button == MouseButtons.Right) return;
                int idx = 0;

                for (int i = 2; i >= 0; i--)
                {
                    if (allArea[i].contains(eLocation))
                    {
                        idx = i;
                        break;
                    }
                }
                if (e.Button == MouseButtons.Left)
                {
                    if (idx == (int)AREA.DECK)
                    {
                        CardArea ca = allArea[idx];
                        int pos = ca.cardcontain(eLocation);
                        if (pos == -1) return;
                        if (ca.cardofArea[pos].Count == 0)
                            return;
                        if (ca.cardofArea[pos].Last().fliped == false)
                            return;
                        if (pos == 1)
                        {
                            if (hintflag == true && Dflag == true)
                            {
                                if (ca.cardofArea[1].Last().cardX == HcardX && ca.cardofArea[1].Last().cardY == HcardY)
                                {

                                    hintflag = false;
                                    Dflag = false;
                                    Seven = -1;
                                    amount = -1;
                                    aiDC.Dispose();
                                    //hintstate = false;
                                }

                            }
                            m_mc = new MoveCards(ca, ca.rectAreas[pos].X - eLocation.X,
                                ca.rectAreas[pos].Y - eLocation.Y, pos, bmpCards, Controls);
                            m_mc.addCards(ca.cardofArea[1].Last(), eLocation);
                            m_mc.makePictureBox(eLocation, m_mc.cards.Last().imgPos);

                            ca.cardofArea[1].Remove(ca.cardofArea[1].Last());
                          
                            ca.update();
                        }



                    }
                    else if (idx == (int)AREA.SEVEN)
                    {
                        //int count = 0;
                        CardArea ca = allArea[idx];
                        int pos = ca.cardcontain(eLocation);
                        if (pos == -1)
                            return;
                        if (ca.cardofArea[pos].Count == 0)
                            return;
                        if (ca.cardofArea[pos].Last().fliped == false)
                            return;

                        int pa = ca.licontain(eLocation, pos);

                        int cacount = ca.cardofArea[pos].Count;
                        int cflip = 0;
                        for (int i = 0; i < ca.cardofArea[pos].Count; i++)
                            if (ca.cardofArea[pos][i].fliped == true)
                                cflip++;
                        if (hintflag == true && Dflag == false)
                        {
                            if (ca.cardofArea[pos].Last() == ca.cardofArea[Seven][amount])//hintcard)
                            {
                                //aiDC.DrawImage(bmpBack, ca.cardofArea[Seven][amount].nowPos, ca.cardofArea[Seven][amount].nowPos, GraphicsUnit.Pixel);
                                hintflag = false;
                  
                                Seven = -1;
                                amount = -1;
                                aiDC.Dispose();
                                //hintstate = false;
                            }
                        }

                        if (cflip == 1 || pa == ca.cardofArea[pos].Count - 1)
                        {
                            //Time.Text = pa.ToString() + " | " + cflip.ToString();
                            m_mc = new MoveCards(ca, ca.rectAreas[pos].X - eLocation.X, ca.cardofArea[pos].Last().nowPos.Y - eLocation.Y, pos, bmpCards, Controls);
                            m_mc.addCards(ca.cardofArea[pos].Last(), eLocation);


                            m_mc.makePictureBox(eLocation, ca.cardofArea[pos].Last().imgPos);
                            //ca.rectAreas[pos].Height -= Card.OVER_MARGIN;
                            if (ca.cardofArea[pos].Count == 0)
                            {
                                ca.rectAreas[pos].Height = Card.CARD_HEIGHT;
                            }
                            else
                            {
                                ca.rectAreas[pos].Height = Card.CARD_HEIGHT + (ca.cardofArea[pos].Count - 1) * Card.OVER_MARGIN;
                            }

                            ca.setEmptyImg(ca.cardofArea[pos].Last().nowPos);
                            ca.cardofArea[pos].Remove(ca.cardofArea[pos].Last());

                        }
                        else
                        {


                            if (ca.cardofArea[pos].Last().fliped == false)
                                return;

                            // Time.Text = pa.ToString() + " | " + cflip.ToString();

                            int countcard = ca.cardofArea[pos].Count;
                            List<Card> moveCard = ca.cardofArea[pos].GetRange(pa, countcard - pa);

                            if (moveCard[0].fliped == false)
                                return;



                            for (int i = 0; i < moveCard.Count; i++)
                                ca.setEmptyImg(moveCard[i].nowPos);

                            ca.cardofArea[pos].RemoveRange(pa, countcard - pa);




                            m_mc = new MoveCards(ca, moveCard[0].nowPos.X - eLocation.X, moveCard[0].nowPos.Y - eLocation.Y, pos, bmpCards, Controls);
                            m_mc.addCards(moveCard, eLocation);

                            //ca.rectAreas[pos].Height -= (countcard - pa) * Card.OVER_MARGIN;

                            if (ca.cardofArea[pos].Count == 0)
                            {
                                ca.rectAreas[pos].Height = Card.CARD_HEIGHT;
                            }
                            else
                            {
                                ca.rectAreas[pos].Height = Card.CARD_HEIGHT + ca.cardofArea[pos].Count * Card.OVER_MARGIN;
                            }



                            /*



                            m_mc = new MoveCards(ca, ca.rectAreas[pos].X - e.Location.X , ca.cardofArea[pos][0].nowPos.Y - eloc(e.Location).Y, pos, bmpCards, Controls);
                            for (int i = 0; i < ca.cardofArea[pos].Count - pa; i++)
                            {
                                if (cacount - cflip == 0) m_mc.addCards(ca.cardofArea[pos][i], eloc(e.Location));
                                else m_mc.addCards(ca.cardofArea[pos][pa + i], eloc(e.Location));   
                            }


                            for(int y=pa; y< countcard; y++)
                            {

                            }



                            for (int i = pa; i > pa-cflip; i--)
                            {

                                m_mc.makePictureBox(eloc(e.Location), m_mc.cards[countcard-1].imgPos);
                                /*




                                if (ca.rectAreas[pos].Height >= Card.CARD_HEIGHT) ca.rectAreas[pos].Height = ca.rectAreas[pos].Height = Card.CARD_HEIGHT;
                                ////else ca.rectAreas[pos].Height = Card.CARD_HEIGHT;

                                ca.setEmptyImg(ca.cardofArea[pos].Last().nowPos);
                                ca.cardofArea[pos].Remove(ca.cardofArea[pos].Last());
                                countcard--;

                            }
                        */
                            /*

                                                for (int i = 0; i < pa; i++)
                            {
                                m_mc.makePictureBox(eloc(e.Location), m_mc.cards[i].imgPos);
                                ca.rectAreas[pos].Height -= ca.cardofArea[pos].Count * Card.OVER_MARGIN;
                                if (ca.rectAreas[pos].Height < Card.CARD_HEIGHT)
                                {
                                    ca.rectAreas[pos].Height = Card.CARD_HEIGHT;
                                }
                                ca.setEmptyImg(ca.cardofArea[pos][pa-i].nowPos);
                                ca.cardofArea[pos].Remove(ca.cardofArea[pos][pa  -i]);
                            }
                            */


                            cflip = 0;
                             //ca.cardofArea[pos].Remove(ca.cardofArea[pos].Last());
                        }

                        ca.update();
                    }
                    else if (idx == (int)AREA.FOUR)
                    {
                        CardArea ca = allArea[idx];
                        int pos = ca.cardcontain(eloc(e.Location));
                        
                        
                        if (pos == -1)
                            return;
                        if (hintflag == true)
                        {
                            if (ca.cardofArea[pos].Count != 0)
                            {
                                if (ca.cardofArea[pos].Last().cardX == HcardX && ca.cardofArea[pos].Last().cardY == HcardY)
                                {
                                    hintflag = false;

                                    Seven = -1;
                                    amount = -1;
                                    aiDC.Dispose();
                                    //hintstate = false;
                                }
                            }
                        }
                        if (ca.cardofArea[pos].Count == 0)
                            return;
                        if (ca.cardofArea[pos].Last().fliped == false)
                            return;

                        m_mc = new MoveCards(ca, ca.rectAreas[pos].X - e.Location.X, ca.rectAreas[pos].Y - eloc(e.Location).Y, pos, bmpCards, Controls);
                        m_mc.addCards(ca.cardofArea[pos].Last(), eloc(e.Location));
                        m_mc.makePictureBox(eloc(e.Location), ca.cardofArea[pos].Last().imgPos);
                        ca.cardofArea[pos].Remove(ca.cardofArea[pos].Last());
                        ca.update();

                    }
                    Invalidate();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    //re();
                }
            }
           
        }

        int ti = 0;
        int tim = 0;
        int day = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {


            if (tim == 0) Time.Text = ti.ToString();
            else if (ti < 10) Time.Text = tim.ToString() + ":" + "0" + ti.ToString();
            else Time.Text = tim.ToString() + ":" + ti.ToString();
            if (tim == 0)
            {
                Time.Text = "1일째";
                day = 1;
            }
            else Time.Text = (tim + 1).ToString() + "일째"; day = tim + 1;
            ti++;
            if (ti == 60)
            {
                ti = 0;
                tim += 1;
            }


        }
      
        private void CardGame_MouseMove(object sender, MouseEventArgs e)
        {
            if (flagDrag == false)
                return;

            if (m_mc != null )//&& e.Button == MouseButtons.Left)
            {
                m_mc.moveBoxes(e.Location);
            }
        }
        
        private void Suffle()
        {
           
            Random rnd = new Random();
            for (int i = 0; i < 52; i++)
            {
                int a = rnd.Next(0, 51);
                Card card = allCards[a];
                allCards[a] = allCards[i];
                allCards[i] = card;
            }
        }

        private void re()
        {
            if(m_mc != null)
            {
                int p = m_mc.pos;
               
                int coun = m_mc.cards.Count();

                if (m_mc.cards.Count == 1)
                {
                    m_mc.ca.cardofArea[m_mc.pos].Add(m_mc.cards.Last());
                    m_mc.ca.cardofArea[m_mc.pos].Last().setPos(m_mc.ca.cardofArea[m_mc.pos].Last().nowPos);
                }
                else
                {
                    for (int i = 0; i < m_mc.cards.Count; i++)
                    {
                        int count = m_mc.ca.cardofArea[m_mc.pos].Count;
                        Card nowCard = m_mc.cards[i];
                        int fl = 0;
                        m_mc.ca.cardofArea[m_mc.pos].Add(nowCard);
                 
                        if (m_mc.ca.cardofArea[m_mc.pos].Count-1 > 0 && m_mc.ca.cardofArea[m_mc.pos][count - 1].fliped == true) fl = 1;

                        if (i == 0 && count != 0 && fl == 0) nowCard.setPos(Card.makeCard(m_mc.ca.rectAreas[m_mc.pos].X, m_mc.ca.cardofArea[m_mc.pos][count-1].nowPos.Y + Card.OVER_MARGIN_BACK));
                        else if(i == 0 && count == 0) nowCard.setPos(Card.makeCard(m_mc.ca.rectAreas[m_mc.pos].X,
                              m_mc.ca.rectAreas[m_mc.pos].Y));
                        else if(i == 0 && fl == 1) nowCard.setPos(Card.makeCard(m_mc.ca.rectAreas[m_mc.pos].X, m_mc.ca.cardofArea[m_mc.pos][count - 1].nowPos.Y + Card.OVER_MARGIN));
                        else
                        {
                            nowCard.setPos(Card.makeCard(m_mc.ca.rectAreas[m_mc.pos].X,
                          m_mc.cards[i-1].nowPos.Y + Card.OVER_MARGIN));
                        }
                    }
                }
                m_mc.ca.update();
                m_mc.removeBox();
                m_mc = null;
                Invalidate();
            }

        }

        private void 랭킹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            if (flagGame)
            {
                timer1.Stop();
                f3.ShowDialog();
                timer1.Start();
            }
            else
            {
                f3.ShowDialog();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (vic())
            {
                timer2.Stop();
                timer1.Stop();
                timer3.Stop();
                aiDC.Dispose();
                MessageBox.Show("게임승리");
                
                Form4 f4 = new Form4();
                f4.stage = 10;
                f4.day = day;
                f4.ShowDialog();
                flagGame = false;
            
                Form1 f1 = new Form1();
                f1.ShowDialog();

                StreamWriter sw = new StreamWriter(new FileStream("rank.txt", FileMode.Append));
                sw.Write(f1.name + " " + (tim * 60 + ti));
                sw.Close();
                aiDC.Dispose();
            }
        }


        private void 재시작ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            delete();
          
            drawBackImg();
            initImage();
         
            initCardArea();
            
            flagGame = true;
            ti = 0;
            tim = 0;
            
            timer1.Start();
            timer2.Start();
            hintflag = false;
            //aiDC.Dispose();
            Seven = -1;
            amount = -1;
            //hintstate = false;
            Invalidate();
        }

        private void delete()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int o = 0; o < allArea[i].cardofArea.Length; o++)
                {
                    allArea[i].cardofArea[o].Clear();
                }
                allArea[i].rectAll.Height = allArea[i].rectAreas[0].Height;
                allArea[i].update();

            }
            allCards.Clear();
            //rectAllImg = null;
            Invalidate();
            

        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //private void testToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //}

        private void CardDistribution()
        {
            
            int p = 0;
            Suffle();
            for (int i = 1; i <7; i++)
            {
                //int a = 0;
                for (int o = 0; o < i; o++)
                {
                    Card c = allCards[p];
                    
                    allArea[(int)AREA.SEVEN].cardofArea[i].Add(c);
                    
                    if (o > 0)
                    {
                        Point pt1 = allArea[(int)AREA.SEVEN].rectAreas[i].Location;
                        
                        Point pt = allArea[(int)AREA.SEVEN].cardofArea[i].Last().nowPos.Location;
                        
                        c.setPos(Card.makeCard(pt1.X, pt1.Y+o*Card.OVER_MARGIN_BACK));
                        
                    }
                    else
                    {
                        c.setPos(allArea[(int)AREA.SEVEN].rectAreas[i]);
                    }
                    
                    p++;
                }
               
            }
            
            for (int i = p; i < 52; i++)
            {
                Card c = allCards[i];
                allArea[(int)AREA.DECK].cardofArea[0].Add(c);
                c.setPos(allArea[(int)AREA.DECK].rectAreas[0]);
            }
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
            Hint();
        }

        private Point eloc(Point pt)
        {
            Point a = new Point();
            a.X = pt.X;
            a.Y = (pt.Y - menuStrip1.Height);
            return a;
        }

        public int Seven, amount = -1;
        public int Bseven, Bamount = -1;

        int HcardX, HcardY = -1;

        private void timer3_Tick(object sender, EventArgs e)
        {
            Hint();
           
        }
        public bool Dflag = false;
        private void Hint()
        {
            
            if (hintflag == false)
            {
               
                CardArea ca = allArea[(int)AREA.SEVEN];
                CardArea four = allArea[(int)AREA.FOUR];

                //if (Cardhelp() == null)
                //    CardSearch(0, -1);
                //else CardSearch(Cardhelp().cardX+1,Cardhelp().cardY);
                CardHintTest();
                //if (statecount < 4)
                //{
                if (amount == -1)
                {
                    aiDC.DrawRectangle(new Pen(Color.Red, 5), allArea[(int)AREA.DECK].rectAll);
                    //hintstate = true;
                    //statecount++;
                    hintflag = true;
                    Dflag = true;
                }
                //MessageBox.Show("필요한 카드가 줄스택에 없습니다.");
                else
                {
                    Rectangle rect = ca.cardofArea[Seven][amount].nowPos;
                    aiDC.DrawRectangle(new Pen(Color.Red, 5), rect);


                    //hintstate = true;
                    hintflag = true;
                    //statecount++;
                    //hintcard = ca.cardofArea[Seven][amount];
                }
                //}


                Invalidate();
            }
            
        }

        //card X 숫자 card Y 색깔
        private void CardSearch(Card card)
        {

            //int testcount = statecount;
            for (int i = 0; i < 7; i++)
            {
                List<Card> ca = allArea[(int)AREA.SEVEN].cardofArea[i];
                for (int k = ca.Count-1; k >=0 ; k--)
                {
                    //if (card == null)
                    //{
                    //    if (ca[k].cardX == 0)
                    //    {
                    //        Seven = i;
                    //        amount = k;
                    //        break;
                    //    }
                    //}
                    //else
                    //{
                        if (ca[k].cardX == card.cardX+1 && ca[k].cardY == card.cardY)
                        {
                            //if (hintstate == true)
                            //{
                                // if(testcount - 1 == 0)
                                //{
                                    //hintstate = false;
                                    
                                    //continue;
                                //}
                                //else
                                    //testcount--;
                           // }
                           // else 
                           // {
                                Seven = i;
                                amount = k;

                                break;
                           // }
                     
                        }
                   // }
                }
                if (Seven != -1 && amount != -1)
                    break;
            }
        }

        //private Card Cardhelp()
        //{

        //    for (int i = 0; i < 4; i++)
        //    {
        //        List<Card> four = allArea[(int)AREA.FOUR].cardofArea[i];

        //        if (four.Count == 0) return null;
        //        else if (four.Last().cardX == 12) continue;
        //        else
        //        {
        //            CardSearch(four.Last());
        //            if (amount == -1)
        //                continue;
        //            else return four.Last();
        //        }
        //    }
        //    return null;
        //}
        private void SearchCard()
        {
            //int testcount = statecount;
            for (int i = 0; i < 7; i++)
            {
                List<Card> ca = allArea[(int)AREA.SEVEN].cardofArea[i];
                for (int k = ca.Count - 1; k >= 0; k--)
                {
                   
                    if (ca[k].cardX == 0)
                    {
                        //if (hintstate == true)
                        //{
                        //    if (testcount - 1 == 0)
                        //    {
                        //        hintstate = false;

                        //        continue;
                        //    }
                        //    else
                        //        testcount--;
                        //}
                        //else
                        //{
                            Seven = i;
                            amount = k;

                            break;
                      // }
                    }
                    
                }
                if (Seven != -1 && amount != -1)
                    break;
            }
        }
        //public Card hintcard;
        private void CardHintTest()
        {
            Aiinit();
            for (int i = 0; i < 4; i++)
            {
                List<Card> four = allArea[(int)AREA.FOUR].cardofArea[i];

                if (four.Count == 0) { SearchCard(); break; }
                else if (four.Last().cardX == 12) continue;
                else
                {
                    CardSearch(four.Last());
                    if (amount == -1)
                        continue;
                   
                    else 
                    {
                        HcardX = four.Last().cardX;
                        HcardY = four.Last().cardY;
                        //Time.Text = HcardX.ToString() + " | " + HcardY.ToString();
                        break;
                    }

                }
                
            }
        }
        //private void CardAnimation_Clover()
        //{
        //    int i = 0;
        //    Card card = allCards[i];
        //    memDC.DrawImage(bmpCards, card.nowPos, ca, GraphicsUnit.Pixel);
        //}
        //private void CardAnimation_Heart()
        //{

        //}
        //private void CardAnimation_Spade()
        //{

        //}
        //private void CardAnimation_Dia()
        //{

        //}
    }
}