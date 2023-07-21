using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robo
{
    public partial class frmCauA : Form
    {
        public int m, n, k = 0;
        public int[,] a,a1;
        public int[,] check;
        public int[] luu;
        public int[,] check1;
        public int[] luu2;
        public int[] luu3;
        public int vt = 0,vt2,max1=0;
        public frmCauA()
        {
            InitializeComponent();
        }
        List<ViTri> ds;
        List<ViTri> ds1;
        List<DaTa> dta;
        List<DaTa> dta1;       
        public int tong()
        {
            int s = 0;
            for (int i = 0; i < vt; i++)
            {
                s += luu2[i];
            }
            return s;
        }        
        public void try_1(int hang, int cot)
        {
            
            check1[hang,cot] = 1;
            var vitriao = new ViTri();
            luu2[vt++] = a1[hang,cot];
            if (ds.Count > vt - 1)
            {
                var tamao=new ViTri();
                tamao.i = hang;
                tamao.j = cot;
                ds[vt - 1] = tamao;
            }
            else
            {
                var tamao = new ViTri();
                tamao.i = hang;
                tamao.j = cot;
                ds.Add(tamao);
            }
            //dieu kien dung la het duong di
            //het duong di xay ra khi vuot cot+1 va hang+1 va cot-1 va hang-1  hoacn check[, ] =1

            int t1 = 0, t2 = 0, t3 = 0, t4 = 0;
            if (cot - 1 < 1 || (cot - 1 >= 1 && check1[hang,cot - 1] == 1) || a1[hang,cot - 1] < 0)
            {
                t1 = 1;
            }
            if (cot + 1 > m || (cot + 1 <= m && check1[hang,cot + 1] == 1) || a1[hang,cot + 1] < 0)
            {
                t2 = 1;
            }
            if (hang + 1 > n || (hang + 1 <= n && check1[hang + 1,cot] == 1) || a1[hang + 1,cot] < 0)
            {
                t3 = 1;
            }
            if (hang - 1 < 1 || (hang - 1 >= 1 && check1[hang-1,cot] == 1) || a1[hang - 1,cot] < 0)
            {
                t4 = 1;
            }
            if (t1 == 1 && t2 == 1 && t3 == 1 && t4 == 1)
            {
                //so sanh max
                //neu max bes hon thi gan max vaf mang luu gia tri 
                if (max1 < tong())
                {
                    max1 = tong();

                    vt2 = vt;
                    for (int k = 0; k < vt; k++)
                    {
                        luu3[k] = luu2[k];
                        if (ds1.Count > k) ds1[k] = ds[k];
                        else
                        {
                            ds1.Add(ds[k]);
                        }
                    }
                }
            }
            else
            {
                if (cot + 1 <= m && check1[hang,cot + 1] == 0 && a1[hang,cot + 1] >= 0)
                {
                    try_1(hang, cot + 1);
                    check1[hang,cot + 1] = 0;
                    vt--;

                }
                if (cot - 1 >= 1 && check1[hang,cot - 1] == 0 && a1[hang,cot - 1] >= 0)
                {
                    try_1(hang, cot - 1);
                    check1[hang,cot - 1] = 0;
                    vt--;

                }
                if (hang + 1 <= n && check1[hang + 1,cot] == 0 && a1[hang + 1,cot] >= 0)
                {
                    try_1(hang + 1, cot);
                    check1[hang + 1,cot] = 0;
                    vt--;
                }
                if (hang - 1 >= 1 && check1[hang - 1,cot] == 0 && a1[hang - 1,cot] >= 0)
                {
                    try_1(hang - 1, cot);
                    check1[hang - 1,cot] = 0;
                    vt--;

                }

                
            }




        }
        public void main2()
        {
            max1 = 0;
            vt = 0;
            KhoiTao1();
            
            ds1 = new List<ViTri>();
            luu2 = new int[100];

            luu3 = new int[100];
            check1 = new int[n + 1, m + 1];
            for (int j = 1; j <= n; j++)
            {
                for (int k = 1; k <= m; k++)
                {
                    check1[j, k] = 0;
                }
            }
            reset();
            if (txtCot.Text.Trim().Length > 0 || txtHang.Text.Trim().Length > 0)
            {
                int x = int.Parse(txtHang.Text.ToString());
                int y = int.Parse(txtCot.Text.ToString());
                if (x>0 && x<=n  && y > 0 && y  <= m)
                {


                        if (a1[x, y] < 0)
                        {

                            MessageBox.Show("Bạn đang ở ô không đi được.Nhập giá trị bắt đầu khác  ");
                            return;
                        }
                        else
                        {
                            Stopwatch st = new Stopwatch();
                            st.Start();
                            //var watch = System.Diagnostics.Stopwatch.StartNew();
                            // the code that you want to measure comes here
                            try_1(int.Parse(txtHang.Text.ToString()), int.Parse(txtCot.Text.ToString()));
                            // watch.Stop();
                            //var elapsedMs = watch.ElapsedTicks;
                            st.Stop();
                            dta1.Add(new DaTa(n + "x" + m, st.ElapsedMilliseconds));
                            ToListView();
                            MessageBox.Show("Thời gian thực hiện là "+ st.ElapsedMilliseconds);


                    }
                    
                    
                }
                else
                {
                    MessageBox.Show("Hàng và cột vượt quá phạm vi mời nhập lại ");

                }

            }
            else
            {
                MessageBox.Show("Chưa nhập" +
                    " giá trị bắt đầu ");
            }
            

        }
        public void reset()
        {
            this.btn1_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn1_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn1_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn1_4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn1_5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn1_6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn1_7.BackColor = System.Drawing.SystemColors.ActiveBorder;

            this.btn2_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn2_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn2_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn2_4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn2_5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn2_6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn2_7.BackColor = System.Drawing.SystemColors.ActiveBorder;

            this.btn3_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn3_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn3_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn3_4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn3_5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn3_6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn3_7.BackColor = System.Drawing.SystemColors.ActiveBorder;

            this.btn4_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn4_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn4_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn4_4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn4_5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn4_6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn4_7.BackColor = System.Drawing.SystemColors.ActiveBorder;

            this.btn5_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn5_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn5_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn5_4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn5_5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn5_6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn5_7.BackColor = System.Drawing.SystemColors.ActiveBorder;

            this.btn6_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn6_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn6_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn6_4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn6_5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn6_6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn6_7.BackColor = System.Drawing.SystemColors.ActiveBorder;

            this.btn7_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn7_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn7_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn7_4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn7_5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn7_6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn7_7.BackColor = System.Drawing.SystemColors.ActiveBorder;

        }
        public void KhoiTao()
        {
            StreamReader sr = new StreamReader(path);
            //   int n = int.Parse(sr.ReadLine());
            string s = sr.ReadLine();
            // int[,] a = new int[n, n];
            string[] b = s.Split(' ');

            n = int.Parse(b[0]);
            m = int.Parse(b[1]);
            a = new int[n + 1, m + 1];
            string c = sr.ReadLine();
            int i = 1;
            while (c != null && i<=n)
            {
                string[] d = c.Split(' ');
                for (int j = 1; j <= m; j++)
                    a[i, j] = int.Parse(d[j - 1].ToString());
                i++;
                c = sr.ReadLine();
            }
            sr.Close();


        }
        public void KhoiTao1()
        {
            StreamReader sr = new StreamReader(path);
            //   int n = int.Parse(sr.ReadLine());
            string s = sr.ReadLine();
            // int[,] a = new int[n, n];
            string[] b = s.Split(' ');

            n = int.Parse(b[0]);
            m = int.Parse(b[1]);
            a1 = new int[n + 1, m + 1];
            string c = sr.ReadLine();
            int i = 1;
            while (c != null && i <= n)
            {
                string[] d = c.Split(' ');
                for (int j = 1; j <= m; j++)
                    a1[i, j] = int.Parse(d[j - 1].ToString());
                i++;
                c = sr.ReadLine();
            }
            sr.Close();


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            btnNext1.Visible = false;
            btnNext.Visible = false;
            dta = new List<DaTa>();
            dta1 = new List<DaTa>();
            ToListView();
        }        
        private void ToListView()
        {

            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add("1", "STT");
            dt.Columns[0].Width = 50;
            dt.Columns.Add("2", "Kích thước ma trận ");
            dt.Columns[1].Width = 80;
            dt.Columns.Add("3", "Thời gian chạy "); 
            dt.Columns[2].Width = 100;
           
            
            if(dta.Count > 0)
            {
                for (int j = 0; j < dta.Count; j++)
                {
                    dt.Rows.Add(j+1,dta[j].KichThuoc,dta[j].ThoiGian);

                }
            }

            dt1.Columns.Clear();
            dt1.Rows.Clear();

            dt1.Columns.Add("1", "STT");
            dt1.Columns[0].Width = 50;
            dt1.Columns.Add("2", "Kích thước ma trận ");
            dt1.Columns[1].Width = 80;
            dt1.Columns.Add("3", "Thời gian chạy ");
            dt1.Columns[2].Width = 100;
            if (dta1.Count > 0)
            {
                for (int j = 0; j < dta1.Count; j++)
                {
                    dt1.Rows.Add(j + 1, dta1[j].KichThuoc, dta1[j].ThoiGian);

                }
            }

        }
        private void DiChuyen()
        {
            int j = 0;
            foreach (ViTri q in ds)
            {
                
                if(j++==0) txtKetQua.Text = ds.Count + " " + Environment.NewLine + a[q.i,q.j];
                else
                {
                    txtKetQua.Text+=" "+a[q.i,q.j];
                }
                if (q.i == 1 && q.j == 1)
                {
                    this.btn1_1.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250);
                    this.Refresh();
                }
                if (q.i == 1 && q.j == 2)
                {
                    this.btn1_2.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 1 && q.j == 3)
                {
                    this.btn1_3.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 1 && q.j == 4)
                {
                    this.btn1_4.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 1 && q.j == 5)
                {
                    this.btn1_5.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 1 && q.j == 6)
                {
                    this.btn1_6.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 1 && q.j == 7)
                {
                    this.btn1_7.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                //hang 2

                if (q.i == 2 && q.j == 1)
                {
                    this.btn2_1.BackColor = System.Drawing.Color.Gold;

                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 2 && q.j == 2)
                {
                    this.btn2_2.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 2 && q.j == 3)
                {
                    this.btn2_3.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 2 && q.j == 4)
                {
                    this.btn2_4.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 2 && q.j == 5)
                {
                    this.btn2_5.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 2 && q.j == 6)
                {
                    this.btn2_6.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 2 && q.j == 7)
                {
                    this.btn2_7.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }

                //hang3 
                if (q.i == 3 && q.j == 1)
                {
                    this.btn3_1.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 3 && q.j == 2)
                {
                    this.btn3_2.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 3 && q.j == 3)
                {
                    this.btn3_3.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 3 && q.j == 4)
                {
                    this.btn3_4.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 3 && q.j == 5)
                {
                    this.btn3_5.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 3 && q.j == 6)
                {
                    this.btn3_6.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 3 && q.j == 7)
                {
                    this.btn3_7.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                //hang4
                if (q.i == 4 && q.j == 1)
                {
                    this.btn4_1.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 4 && q.j == 2)
                {
                    this.btn4_2.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 4 && q.j == 3)
                {
                    this.btn4_3.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 4 && q.j == 4)
                {
                    this.btn4_4.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 4 && q.j == 5)
                {
                    this.btn4_5.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 4 && q.j == 6)
                {
                    this.btn4_6.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 4 && q.j == 7)
                {
                    this.btn4_7.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                //hang5
                if (q.i == 5 && q.j == 1)
                {
                    this.btn5_1.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 5 && q.j == 2)
                {
                    this.btn5_2.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 5 && q.j == 3)
                {
                    this.btn5_3.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 5 && q.j == 4)
                {
                    this.btn5_4.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 5 && q.j == 5)
                {
                    this.btn5_5.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 5 && q.j == 6)
                {
                    this.btn5_6.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 5 && q.j == 7)
                {
                    this.btn5_7.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                //hang6
                if (q.i == 6 && q.j == 1)
                {
                    this.btn6_1.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 6 && q.j == 2)
                {
                    this.btn6_2.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 6 && q.j == 3)
                {
                    this.btn6_3.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 6 && q.j == 4)
                {
                    this.btn6_4.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 6 && q.j == 5)
                {
                    this.btn6_5.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 6 && q.j == 6)
                {
                    this.btn6_6.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 6 && q.j == 7)
                {
                    this.btn6_7.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                //hang7
                if (q.i == 7 && q.j == 1)
                {
                    this.btn7_1.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 7 && q.j == 2)
                {
                    this.btn7_2.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 7 && q.j == 3)
                {
                    this.btn7_3.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 7 && q.j == 4)
                {
                    this.btn7_4.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 7 && q.j == 5)
                {
                    this.btn7_5.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 7 && q.j == 6)
                {
                    this.btn7_6.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }
                if (q.i == 7 && q.j == 7)
                {
                    this.btn7_7.BackColor = System.Drawing.Color.Gold;
                    Thread.Sleep(250); this.Refresh();
                }


            }
        }
        public void XuLi(int hang, int cot)
        {
            if (a[hang, cot] < 0)
            {
                MessageBox.Show("Bạn đang ở ô không di chuyển được");
                return;
            }
            check[hang, cot] = 1;
            luu[k++] = a[hang, cot];
            var vitri = new ViTri();
            vitri.i = hang;
            vitri.j = cot;
            ds.Add(vitri);
            if ((hang + 1 <= n && check[hang + 1, cot] == 1) || hang + 1 > n || a[hang+1,cot]<0 )
            {
                if ((hang - 1 >= 1 && check[hang - 1, cot] == 1) || hang - 1 < 1 || a[hang-1,cot]<0)
                    if ((cot + 1 <= m && check[hang, cot + 1] == 1) || cot + 1 > m || a[hang,cot+1]<0)
                        if ((cot - 1 >= 1 && check[hang, cot - 1] == 1) || cot - 1 < 1 || a[hang,cot-1]<0)
                        {
                            MessageBox.Show("Đã chạy xong.Nhấn vào OK để coi kết quả ^_^");
                            return;
                        }

            }

            bool check1 = false, check2 = false, check3 = false, check4 = false;
            if (hang + 1 <= n)
            {
                if (check[hang + 1, cot] == 0 && a[hang+1,cot]>=0)
                {

                }
                else
                {

                    check1 = true;
                }

            }
            else
            {
                check1 = true;
            }

            if (hang - 1 >= 1)
            {
                if (check[hang - 1, cot] == 0 && a[hang - 1, cot] >= 0)
                {

                }
                else
                {

                    check2 = true;
                }

            }
            else
            {
                check2 = true;
            }

            if (cot - 1 >= 1)
            {
                if (check[hang, cot - 1] == 0 && a[hang, cot-1] >= 0)
                {

                }
                else
                {

                    check3 = true;
                }

            }
            else
            {
                check3 = true;
            }
            if (cot + 1 <= m)
            {
                if (check[hang, cot + 1] == 0 && a[hang , cot + 1] >= 0)
                {

                }
                else
                {

                    check4 = true;
                }

            }
            else
            {
                check4 = true;
            }
            if (check1 && check2 && check3 && check4)
            {
                MessageBox.Show("Het duong");
                return;
            }
            int cotnext = 0;
            int hangnext = 0;
            int max = -1;
            if (hang + 1 <= n && check[hang + 1, cot] == 0  && a[hang + 1, cot] >=0)
            {
                if (a[hang + 1, cot] > max)
                {
                    max = a[hang + 1, cot];
                    hangnext = hang + 1;
                    cotnext = cot;
                }
            }
            if (hang - 1 >= 1 && check[hang - 1, cot] == 0 && a[hang - 1, cot] >= 0)
            {
                if (a[hang - 1, cot] > max)
                {
                    max = a[hang - 1, cot];
                    hangnext = hang - 1;
                    cotnext = cot;
                }
            }
            if (cot + 1 <= m && check[hang, cot + 1] == 0 && a[hang, cot+1] >= 0)
            {
                if (a[hang, cot + 1] > max)
                {
                    max = a[hang, cot + 1];
                    hangnext = hang;
                    cotnext = cot + 1;
                }
            }

            if (cot - 1 >= 1 && check[hang, cot - 1] == 0 && a[hang , cot-1] >= 0)
            {
                if (a[hang, cot - 1] > max)
                {
                    max = a[hang, cot - 1];
                    hangnext = hang;
                    cotnext = cot - 1;
                }
            }
            XuLi(hangnext, cotnext);




        }
        public void main()
        {
            k = 0;

            KhoiTao();
            luu = new int[100];
            check = new int[n + 1, m + 1];
            for (int j = 1; j <= n; j++)
            {
                for (int k = 1; k <= m; k++)
                {
                    check[j, k] = 0;
                }
            }
            reset();
            if(txtCot.Text.Trim().Length > 0 || txtHang.Text.Trim().Length > 0)
            {
                //DateTime startdate = DateTime.Now;

                // Thời gian kết thúc
                //DateTime finishDate = DateTime.Now;
                //var watch = System.Diagnostics.Stopwatch.StartNew();
                Stopwatch st = new Stopwatch();
                st.Start();
                //var watch = System.Diagnostics.Stopwatch.StartNew();
                // the code that you want to measure comes here
                XuLi(int.Parse(txtHang.Text.ToString()), int.Parse(txtCot.Text.ToString()));
                // watch.Stop();
                //var elapsedMs = watch.ElapsedTicks;
                st.Stop();
                dta.Add(new DaTa(n + "x" + m, st.ElapsedMilliseconds));
                // the code that you want to measure comes here
                
               // watch.Stop();
                //var elapsedMs = watch.ElapsedTicks;

                
                //Tổng thời gian thực hiện 
                //TimeSpan time = finishDate - startdate;
              
                ToListView();
                this.Refresh();
            }
            else
            {
                MessageBox.Show("Chưa nhập" +
                    " giá trị bắt đầu ");
                return;
            }
            

        }
        //btnLienKet
        private void btnClick_Click(object sender, EventArgs e)

        {
                
                int tam = 0;
                if (path != null)
                {
                    txtKetQua.Clear();
                    ds = new List<ViTri>();
                    main();
                    
                    
                   
                    if (rbtnTD.Checked == true)
                    {
                        DiChuyen();
                        
                    }
                    else
                    {

                        MessageBox.Show("Đã chạy xong luồng dữ liệu, mời bạn click nút Next để xem từng dữ liệu ");
                        tam = 1;

                    }
                    
                }
                else
                {
                    MessageBox.Show("Không có đường dẫn lấy dữ liệu ");
                }
                if (tam==1)
                {
                    btnNext.Enabled = true;
                    btnNext.Visible = true;
                }
                else
                {
                    btnNext.Enabled = false;
                }
        }
        public string path;
        int p=0;
        private void btnNext_Click(object sender, EventArgs e)
        {
            
            int q= ds.Count;
            
            if (p < q)
            {
                
                if (ds[p].i == 1 && ds[p].j == 1)
                {
                    this.btn1_1.BackColor = System.Drawing.Color.Gold;
                    
                    this.Refresh();
                }
                if (ds[p].i == 1 && ds[p].j == 2)
                {
                    this.btn1_2.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds[p].i == 1 && ds[p].j == 3)
                {
                    this.btn1_3.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 1 && ds[p].j == 4)
                {
                    this.btn1_4.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds[p].i == 1 && ds[p].j == 5)
                {
                    this.btn1_5.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds[p].i == 1 && ds[p].j == 6)
                {
                    this.btn1_6.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 1 && ds[p].j == 7)
                {
                    this.btn1_7.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                //hang 2

                if (ds[p].i == 2 && ds[p].j == 1)
                {
                    this.btn2_1.BackColor = System.Drawing.Color.Gold;

                     this.Refresh();
                }
                if (ds[p].i == 2 && ds[p].j == 2)
                {
                    this.btn2_2.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 2 && ds[p].j == 3)
                {
                    this.btn2_3.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds[p].i == 2 && ds[p].j == 4)
                {
                    this.btn2_4.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 2 && ds[p].j == 5)
                {
                    this.btn2_5.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 2 && ds[p].j == 6)
                {
                    this.btn2_6.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 2 && ds[p].j == 7)
                {
                    this.btn2_7.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }

                //hang3 
                if (ds[p].i == 3 && ds[p].j == 1)
                {
                    this.btn3_1.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 3 && ds[p].j == 2)
                {
                    this.btn3_2.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds[p].i == 3 && ds[p].j == 3)
                {
                    this.btn3_3.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds[p].i == 3 && ds[p].j == 4)
                {
                    this.btn3_4.BackColor = System.Drawing.Color.Gold;
                  this.Refresh();
                }
                if (ds[p].i == 3 && ds[p].j == 5)
                {
                    this.btn3_5.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 3 && ds[p].j == 6)
                {
                    this.btn3_6.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds[p].i == 3 && ds[p].j == 7)
                {
                    this.btn3_7.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                //hang4
                if (ds[p].i == 4 && ds[p].j == 1)
                {
                    this.btn4_1.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds[p].i == 4 && ds[p].j == 2)
                {
                    this.btn4_2.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 4 && ds[p].j == 3)
                {
                    this.btn4_3.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 4 && ds[p].j == 4)
                {
                    this.btn4_4.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 4 && ds[p].j == 5)
                {
                    this.btn4_5.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds[p].i == 4 && ds[p].j == 6)
                {
                    this.btn4_6.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds[p].i == 4 && ds[p].j == 7)
                {
                    this.btn4_7.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                //hang5
                if (ds[p].i == 5 && ds[p].j == 1)
                {
                    this.btn5_1.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 5 && ds[p].j == 2)
                {
                    this.btn5_2.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 5 && ds[p].j == 3)
                {
                    this.btn5_3.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds[p].i == 5 && ds[p].j == 4)
                {
                    this.btn5_4.BackColor = System.Drawing.Color.Gold;
                   this.Refresh();
                }
                if (ds[p].i == 5 && ds[p].j == 5)
                {
                    this.btn5_5.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 5 && ds[p].j == 6)
                {
                    this.btn5_6.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 5 && ds[p].j == 7)
                {
                    this.btn5_7.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                //hang6
                if (ds[p].i == 6 && ds[p].j == 1)
                {
                    this.btn6_1.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 6 && ds[p].j == 2)
                {
                    this.btn6_2.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 6 && ds[p].j == 3)
                {
                    this.btn6_3.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 6 && ds[p].j == 4)
                {
                    this.btn6_4.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 6 && ds[p].j == 5)
                {
                    this.btn6_5.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds[p].i == 6 && ds[p].j == 6)
                {
                    this.btn6_6.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 6 && ds[p].j == 7)
                {
                    this.btn6_7.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                //hang7
                if (ds[p].i == 7 && ds[p].j == 1)
                {
                    this.btn7_1.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 7 && ds[p].j == 2)
                {
                    this.btn7_2.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 7 && ds[p].j == 3)
                {
                    this.btn7_3.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 7 && ds[p].j == 4)
                {
                    this.btn7_4.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 7 && ds[p].j == 5)
                {
                    this.btn7_5.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 7 && ds[p].j == 6)
                {
                    this.btn7_6.BackColor = System.Drawing.Color.Gold;
                     this.Refresh();
                }
                if (ds[p].i == 7 && ds[p].j == 7)
                {
                    this.btn7_7.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (p == 0) txtKetQua.Text = ds.Count + " " + Environment.NewLine;
                txtKetQua.Text += " " + a[ds[p].i, ds[p].j];
                this.Refresh();
                p++;
                
                //hien thi ket qua 
                
            }
            else
            {
                MessageBox.Show("Bạn đã đi hết đường ");
                p = 0;
                btnNext.Enabled = false;
                btnNext1.Visible = false;
            }
        }
        private void btnNext1_Click(object sender, EventArgs e)
        {
            

            if (p < vt2)
            {

                if (ds1[p].i == 1 && ds1[p].j == 1)
                {
                    this.btn1_1.BackColor = System.Drawing.Color.Gold;

                    this.Refresh();
                }
                if (ds1[p].i == 1 && ds1[p].j == 2)
                {
                    this.btn1_2.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 1 && ds1[p].j == 3)
                {
                    this.btn1_3.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 1 && ds1[p].j == 4)
                {
                    this.btn1_4.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 1 && ds1[p].j == 5)
                {
                    this.btn1_5.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 1 && ds1[p].j == 6)
                {
                    this.btn1_6.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 1 && ds1[p].j == 7)
                {
                    this.btn1_7.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                //hang 2

                if (ds1[p].i == 2 && ds1[p].j == 1)
                {
                    this.btn2_1.BackColor = System.Drawing.Color.Gold;

                    this.Refresh();
                }
                if (ds1[p].i == 2 && ds1[p].j == 2)
                {
                    this.btn2_2.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 2 && ds1[p].j == 3)
                {
                    this.btn2_3.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 2 && ds1[p].j == 4)
                {
                    this.btn2_4.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 2 && ds1[p].j == 5)
                {
                    this.btn2_5.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 2 && ds1[p].j == 6)
                {
                    this.btn2_6.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 2 && ds1[p].j == 7)
                {
                    this.btn2_7.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }

                //hang3 
                if (ds1[p].i == 3 && ds1[p].j == 1)
                {
                    this.btn3_1.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 3 && ds1[p].j == 2)
                {
                    this.btn3_2.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 3 && ds1[p].j == 3)
                {
                    this.btn3_3.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 3 && ds1[p].j == 4)
                {
                    this.btn3_4.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 3 && ds1[p].j == 5)
                {
                    this.btn3_5.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 3 && ds1[p].j == 6)
                {
                    this.btn3_6.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 3 && ds1[p].j == 7)
                {
                    this.btn3_7.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                //hang4
                if (ds1[p].i == 4 && ds1[p].j == 1)
                {
                    this.btn4_1.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 4 && ds1[p].j == 2)
                {
                    this.btn4_2.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 4 && ds1[p].j == 3)
                {
                    this.btn4_3.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 4 && ds1[p].j == 4)
                {
                    this.btn4_4.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 4 && ds1[p].j == 5)
                {
                    this.btn4_5.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 4 && ds1[p].j == 6)
                {
                    this.btn4_6.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 4 && ds1[p].j == 7)
                {
                    this.btn4_7.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                //hang5
                if (ds1[p].i == 5 && ds1[p].j == 1)
                {
                    this.btn5_1.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 5 && ds1[p].j == 2)
                {
                    this.btn5_2.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 5 && ds1[p].j == 3)
                {
                    this.btn5_3.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 5 && ds1[p].j == 4)
                {
                    this.btn5_4.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 5 && ds1[p].j == 5)
                {
                    this.btn5_5.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 5 && ds1[p].j == 6)
                {
                    this.btn5_6.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 5 && ds1[p].j == 7)
                {
                    this.btn5_7.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                //hang6
                if (ds1[p].i == 6 && ds1[p].j == 1)
                {
                    this.btn6_1.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 6 && ds1[p].j == 2)
                {
                    this.btn6_2.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 6 && ds1[p].j == 3)
                {
                    this.btn6_3.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 6 && ds1[p].j == 4)
                {
                    this.btn6_4.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 6 && ds1[p].j == 5)
                {
                    this.btn6_5.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 6 && ds1[p].j == 6)
                {
                    this.btn6_6.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 6 && ds1[p].j == 7)
                {
                    this.btn6_7.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                //hang7
                if (ds1[p].i == 7 && ds1[p].j == 1)
                {
                    this.btn7_1.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 7 && ds1[p].j == 2)
                {
                    this.btn7_2.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 7 && ds1[p].j == 3)
                {
                    this.btn7_3.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 7 && ds1[p].j == 4)
                {
                    this.btn7_4.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 7 && ds1[p].j == 5)
                {
                    this.btn7_5.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 7 && ds1[p].j == 6)
                {
                    this.btn7_6.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                if (ds1[p].i == 7 && ds1[p].j == 7)
                {
                    this.btn7_7.BackColor = System.Drawing.Color.Gold;
                    this.Refresh();
                }
                int s = 0;
                if (p == 0)
                {
                    for(int j = 0; j < vt2; j++)
                    {
                        s += luu3[j];
                    }
                    txtKetQua.Text = "Tổng là: " + s + " " + Environment.NewLine + luu3[p];

                }
                else
                {
                    txtKetQua.Text += " "+ luu3[p];
                }
                    
                    
                
                p++;

                //hien thi ket qua 

            }
            else
            {
                MessageBox.Show("Bạn đã đi hết đường ");
                p = 0;
                btnNext1.Enabled = false;
                btnNext1.Visible= false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtKetQua.Text != null)
            {
                string MyString = txtKetQua.Text;
                char[] MyChar = { '-', '>'};
                string NewString = MyString.TrimStart(MyChar);
                Common.GhiThongTin("D:/HOC TAP/PTTKGT/Robo/Robo/output.txt",NewString);
                MessageBox.Show("Lưu thành công ");
            }
            else
            {
                MessageBox.Show("Kết quả rỗng. Không nên lưu");
            }
        }
        //btn reset
        private void button1_Click(object sender, EventArgs e)
        {
            txtKetQua.Clear();
            txtCot.Clear();
            txtHang.Clear();
            btnNext.Visible = false;
            btnNext1.Visible= false;

            this.btn1_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn1_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn1_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn1_4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn1_5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn1_6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn1_7.BackColor = System.Drawing.SystemColors.ActiveBorder;

            this.btn2_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn2_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn2_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn2_4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn2_5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn2_6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn2_7.BackColor = System.Drawing.SystemColors.ActiveBorder;

            this.btn3_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn3_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn3_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn3_4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn3_5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn3_6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn3_7.BackColor = System.Drawing.SystemColors.ActiveBorder;

            this.btn4_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn4_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn4_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn4_4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn4_5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn4_6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn4_7.BackColor = System.Drawing.SystemColors.ActiveBorder;

            this.btn5_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn5_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn5_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn5_4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn5_5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn5_6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn5_7.BackColor = System.Drawing.SystemColors.ActiveBorder;

            this.btn6_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn6_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn6_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn6_4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn6_5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn6_6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn6_7.BackColor = System.Drawing.SystemColors.ActiveBorder;

            this.btn7_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn7_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn7_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn7_4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn7_5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn7_6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn7_7.BackColor = System.Drawing.SystemColors.ActiveBorder;

            


        }
        private void btncaub_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
                ds = new List<ViTri>();
                txtKetQua.Clear();
                main2();
                string s = "";
                int tong = 0;
                if (rbtnTD.Checked == true)
                {
                    for (int j = 0; j < vt2; j++)
                    {
                        tong += luu3[j];

                    }
                    txtKetQua.Text = "Tổng là: " + tong + Environment.NewLine;
                    for (int j = 0; j < vt2; j++)
                    {


                        s += luu3[j];
                        if (j < vt2 - 1) s += "-> ";
                        if (ds1[j].i == 1 && ds1[j].j == 1)
                        {
                            this.btn1_1.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250);
                            this.Refresh();
                        }
                        if (ds1[j].i == 1 && ds1[j].j == 2)
                        {
                            this.btn1_2.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 1 && ds1[j].j == 3)
                        {
                            this.btn1_3.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 1 && ds1[j].j == 4)
                        {
                            this.btn1_4.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 1 && ds1[j].j == 5)
                        {
                            this.btn1_5.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 1 && ds1[j].j == 6)
                        {
                            this.btn1_6.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 1 && ds1[j].j == 7)
                        {
                            this.btn1_7.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        //hang 2

                        if (ds1[j].i == 2 && ds1[j].j == 1)
                        {
                            this.btn2_1.BackColor = System.Drawing.Color.Gold;

                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 2 && ds1[j].j == 2)
                        {
                            this.btn2_2.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 2 && ds1[j].j == 3)
                        {
                            this.btn2_3.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 2 && ds1[j].j == 4)
                        {
                            this.btn2_4.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 2 && ds1[j].j == 5)
                        {
                            this.btn2_5.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 2 && ds1[j].j == 6)
                        {
                            this.btn2_6.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 2 && ds1[j].j == 7)
                        {
                            this.btn2_7.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }

                        //hang3 
                        if (ds1[j].i == 3 && ds1[j].j == 1)
                        {
                            this.btn3_1.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 3 && ds1[j].j == 2)
                        {
                            this.btn3_2.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 3 && ds1[j].j == 3)
                        {
                            this.btn3_3.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 3 && ds1[j].j == 4)
                        {
                            this.btn3_4.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 3 && ds1[j].j == 5)
                        {
                            this.btn3_5.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 3 && ds1[j].j == 6)
                        {
                            this.btn3_6.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 3 && ds1[j].j == 7)
                        {
                            this.btn3_7.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        //hang4
                        if (ds1[j].i == 4 && ds1[j].j == 1)
                        {
                            this.btn4_1.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 4 && ds1[j].j == 2)
                        {
                            this.btn4_2.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 4 && ds1[j].j == 3)
                        {
                            this.btn4_3.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 4 && ds1[j].j == 4)
                        {
                            this.btn4_4.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 4 && ds1[j].j == 5)
                        {
                            this.btn4_5.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 4 && ds1[j].j == 6)
                        {
                            this.btn4_6.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 4 && ds1[j].j == 7)
                        {
                            this.btn4_7.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        //hang5
                        if (ds1[j].i == 5 && ds1[j].j == 1)
                        {
                            this.btn5_1.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 5 && ds1[j].j == 2)
                        {
                            this.btn5_2.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 5 && ds1[j].j == 3)
                        {
                            this.btn5_3.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 5 && ds1[j].j == 4)
                        {
                            this.btn5_4.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 5 && ds1[j].j == 5)
                        {
                            this.btn5_5.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 5 && ds1[j].j == 6)
                        {
                            this.btn5_6.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 5 && ds1[j].j == 7)
                        {
                            this.btn5_7.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        //hang6
                        if (ds1[j].i == 6 && ds1[j].j == 1)
                        {
                            this.btn6_1.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 6 && ds1[j].j == 2)
                        {
                            this.btn6_2.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 6 && ds1[j].j == 3)
                        {
                            this.btn6_3.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 6 && ds1[j].j == 4)
                        {
                            this.btn6_4.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 6 && ds1[j].j == 5)
                        {
                            this.btn6_5.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 6 && ds1[j].j == 6)
                        {
                            this.btn6_6.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 6 && ds1[j].j == 7)
                        {
                            this.btn6_7.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        //hang7
                        if (ds1[j].i == 7 && ds1[j].j == 1)
                        {
                            this.btn7_1.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 7 && ds1[j].j == 2)
                        {
                            this.btn7_2.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 7 && ds1[j].j == 3)
                        {
                            this.btn7_3.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 7 && ds1[j].j == 4)
                        {
                            this.btn7_4.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 7 && ds1[j].j == 5)
                        {
                            this.btn7_5.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 7 && ds1[j].j == 6)
                        {
                            this.btn7_6.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        if (ds1[j].i == 7 && ds1[j].j == 7)
                        {
                            this.btn7_7.BackColor = System.Drawing.Color.Gold;
                            Thread.Sleep(250); this.Refresh();
                        }
                        txtKetQua.Text += " " + luu3[j];
                    }
                }
                else
                {
                    MessageBox.Show("Mời bạn nhấn Next để coi từng bước ");
                    btnNext1.Enabled = true;
                    btnNext1.Visible = true;
                }
                
                //int thu = 1;
            }
            else
            {
                MessageBox.Show("Không có nguồn lấy dữ liệu ");
            }

        }
        private void btnLienKet_Click(object sender, EventArgs e)
        {
            reset();
            this.btn1_1.Text = "";
            this.btn1_2.Text = "";
            this.btn1_3.Text = "";
            this.btn1_4.Text = "";
            this.btn1_5.Text = "";
            this.btn1_6.Text = "";
            this.btn1_7.Text = "";

            this.btn2_1.Text = "";
            this.btn2_2.Text = "";
            this.btn2_3.Text = "";
            this.btn2_4.Text = "";
            this.btn2_5.Text = "";
            this.btn2_6.Text = "";
            this.btn2_7.Text = "";

            this.btn3_1.Text = "";
            this.btn3_2.Text = "";
            this.btn3_3.Text = "";
            this.btn3_4.Text = "";
            this.btn3_5.Text = "";
            this.btn3_6.Text = "";
            this.btn3_7.Text = "";

            this.btn4_1.Text = "";
            this.btn4_2.Text = "";
            this.btn4_3.Text = "";
            this.btn4_4.Text = "";
            this.btn4_5.Text = "";
            this.btn4_6.Text = "";
            this.btn4_7.Text = "";

            this.btn5_1.Text = "";
            this.btn5_2.Text = "";
            this.btn5_3.Text = "";
            this.btn5_4.Text = "";
            this.btn5_5.Text = "";
            this.btn5_6.Text = "";
            this.btn5_7.Text = "";

            this.btn6_1.Text = "";
            this.btn6_2.Text = "";
            this.btn6_3.Text = "";
            this.btn6_4.Text = "";
            this.btn6_5.Text = "";
            this.btn6_6.Text = "";
            this.btn6_7.Text = "";

            this.btn7_1.Text = "";
            this.btn7_2.Text = "";
            this.btn7_3.Text = "";
            this.btn7_4.Text = "";
            this.btn7_5.Text = "";
            this.btn7_6.Text = "";
            this.btn7_7.Text = "";

            

            if (path != null)
            {
                StreamReader sr = new StreamReader(path);
                //   int n = int.Parse(sr.ReadLine());
                string s = sr.ReadLine();
                // int[,] a = new int[n, n];
                string[] b = s.Split(' ');
                int u;
                int o;//n
                
                o = int.Parse(b[0]);
                u = int.Parse(b[1]);
                txtHang.Text = b[2];
                txtCot.Text = b[3];
                this.Refresh();
                
                int[,] v = new int[o+1, u+1];
                string c = sr.ReadLine();
                int i = 1;
                while (c != null && i<=o)
                {
                    string[] d = c.Split(' ');
                    for (int j = 1; j <= u; j++)
                        v[i, j] = int.Parse(d[j-1].ToString());
                    i++;
                    c = sr.ReadLine();
                }
                sr.Close();

                int thu = 1;
                if (thu <= o)
                {
                    int thu1 = 1;
                    if (thu1 <= u)
                    {
                        this.btn1_1.Text = Convert.ToString(v[1, 1]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn1_2.Text = Convert.ToString(v[1, 2]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn1_3.Text = Convert.ToString(v[1, 3]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn1_4.Text = Convert.ToString(v[1, 4]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn1_5.Text = Convert.ToString(v[1, 5]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn1_6.Text = Convert.ToString(v[1, 6]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn1_7.Text = Convert.ToString(v[1, 7]);
                        thu1++;
                    }
                    
                    thu++;
                }
                if (thu <= o)
                {
                    int thu1 = 1;
                    if (thu1 <= u)
                    {
                        this.btn2_1.Text = Convert.ToString(v[2, 1]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn2_2.Text = Convert.ToString(v[2, 2]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn2_3.Text = Convert.ToString(v[2, 3]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn2_4.Text = Convert.ToString(v[2, 4]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn2_5.Text = Convert.ToString(v[2, 5]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn2_6.Text = Convert.ToString(v[2, 6]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn2_7.Text = Convert.ToString(v[2, 7]);
                        thu1++;
                    }
                    
                    thu++;
                }
                if (thu <= o)
                {
                    int thu1 = 1;
                    if (thu1 <= u)
                    {
                        this.btn3_1.Text = Convert.ToString(v[3, 1]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn3_2.Text = Convert.ToString(v[3, 2]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn3_3.Text = Convert.ToString(v[3, 3]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn3_4.Text = Convert.ToString(v[3, 4]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn3_5.Text = Convert.ToString(v[3, 5]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn3_6.Text = Convert.ToString(v[3, 6]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn3_7.Text = Convert.ToString(v[3, 7]);
                        thu1++;
                    }
                    
                    thu++;
                }
                if(thu <= o)
                {
                    int thu1 = 1;
                    if (thu1 <= u)
                    {
                        this.btn4_1.Text = Convert.ToString(v[4, 1]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn4_2.Text = Convert.ToString(v[4, 2]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn4_3.Text = Convert.ToString(v[4, 3]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn4_4.Text = Convert.ToString(v[4, 4]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn4_5.Text = Convert.ToString(v[4, 5]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                       
                        this.btn4_6.Text = Convert.ToString(v[4, 6]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                       
                        this.btn4_7.Text = Convert.ToString(v[4, 7]);
                        thu1++;
                    }
                    
                    thu++;

                }
                if (thu <= o)
                {
                    int thu1 = 1;
                    if (thu1 <= u)
                    {
                        this.btn5_1.Text = Convert.ToString(v[5, 1]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn5_2.Text = Convert.ToString(v[5, 2]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn5_3.Text = Convert.ToString(v[5, 3]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn5_4.Text = Convert.ToString(v[5, 4]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn5_5.Text = Convert.ToString(v[5, 5]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn5_6.Text = Convert.ToString(v[5, 6]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                       
                        this.btn5_7.Text = Convert.ToString(v[5, 7]);
                        thu1++;
                    }
                    
                    thu++;

                }
                if (thu <= o)
                {
                    int thu1 = 1;
                    if (thu1 <= u)
                    {
                        this.btn6_1.Text = Convert.ToString(v[6, 1]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn6_2.Text = Convert.ToString(v[6, 2]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn6_3.Text = Convert.ToString(v[6, 3]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn6_4.Text = Convert.ToString(v[6, 4]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        
                        this.btn6_5.Text = Convert.ToString(v[6, 5]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        this.btn6_6.Text = Convert.ToString(v[6, 6]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        this.btn6_7.Text = Convert.ToString(v[6, 7]);
                        thu1++;
                    }
                    
                    
                    
                    thu++;

                }
                if (thu <= o)
                {
                    int thu1 = 1;
                    if (thu1 <= u)
                    {
                        this.btn7_1.Text = Convert.ToString(v[7, 1]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        this.btn7_2.Text = Convert.ToString(v[7, 2]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        this.btn7_3.Text = Convert.ToString(v[7, 3]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        this.btn7_4.Text = Convert.ToString(v[7, 4]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        this.btn7_5.Text = Convert.ToString(v[7, 5]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        this.btn7_6.Text = Convert.ToString(v[7, 6]);
                        thu1++;
                    }
                    if (thu1 <= u)
                    {
                        this.btn7_7.Text = Convert.ToString(v[7, 7]);
                        thu1++;
                    }
                    thu++;

                }

            }
            else
            {
                MessageBox.Show("Không có đường dẫn" +
                    "");
            }

        }
        private void liênKếtDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDocFile f = new frmDocFile();
            f.ShowDialog();
            path = f.LayTen;


        }
    }
}