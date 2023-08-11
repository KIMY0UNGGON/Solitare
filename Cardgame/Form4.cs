using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klondike
{
    public partial class Form4 : Form
    {
        public int stage = 0;
        public int day = 0;
        public Form4()
        {
            InitializeComponent();
        }
        Graphics gr;
        Bitmap bt;
        Bitmap C1, C2, C3, C4;

        private void initDC()
        {
            bt = new Bitmap(this.Width, this.Height);
            gr = Graphics.FromImage(bt);
            
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            initDC();
            imgae();
  
            label2.Text = "우리 버뮤다 연구소에 온것을 환영하네";
            if(stage == 10)
            {
                if (day <= 3)
                {
                    label2.Text = "설마 이렇게나 빨리 전달 할 줄이야... 정말 대단하군";
                }
                else
                {
                    label2.Text = "11일 만에 의뢰를 끝내다니";
                }
                
            }

        }
        private void imgae()
        {

            C1 = new Bitmap(global::Klondike.Properties.Resources.c1,new Size(60,80));
            C2 = new Bitmap(global::Klondike.Properties.Resources.c2, new Size(60,80));
            C3 = new Bitmap(global::Klondike.Properties.Resources.c3, new Size(60, 80));
            C4 = new Bitmap(global::Klondike.Properties.Resources.c4, new Size(60, 80));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(stage == 0)
            {
                
                label2.Text = "상황이 급하니 바로 본론에 들어가도록 하지. \n자네를 여기 부른 이유는 다름이 아니라 자네에게 부탁할 의뢰가 있어서라네";
                stage = 1;
            }
            else if(stage == 1)
            {
               
                label2.Text = "의뢰의 내용은 이것들을 지정된 장소로 전달하는 걸세. \n이것들이야말로 지구를 살릴 수 있는 연구들이야";
                stage = 2;
                
            }
            else if(stage == 2)
            {

                gr.DrawImage(C1, 150, 30);
                gr.DrawImage(C2, 300, 30);
                gr.DrawImage(C3, 450, 30);
                gr.DrawImage(C4, 600, 30);
                Invalidate();
                label2.Text = "─고효율 태양열 전지 설계도 \n─플라스틱 입자 분해 미생물 \n─완성된 항암 치료제 \n─오존컨트롤 시스템 프로토타입";
                stage= 3;
                

            }
            else if(stage == 3)
            {
                
                gr.Clear(Color.WhiteSmoke);
                Invalidate();
                button1.Text = "마침";
                label2.Text = "그리고 자네에게 AI 서포터를 붙여주지 \n자 어서 빨리 테러리스트들이 눈치채기 전에 움직여주게 \n그럼 행운을 빌겠네.";
                stage= 4;
                
            }
            else if(stage == 4)
            {
                
                Close();
               
            }
            else if(stage == 10)
            {
                button1.Text = "마침";
                label2.Text = "수고했네 자네 덕분에 세상이 평화로워 졌네";
                stage = 11;
            }
            else if(stage == 11)
            {
                Close();
            }

        }

        private void Form4_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(bt, 0, 10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
        
            Close();

         
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {

            //CardGame cardGame = new CardGame();
            //cardGame.ShowDialog();



        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
       
        }
    }
}
