using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Robo
{
    public partial class frmDocFile : Form
    {
        public frmDocFile()
        {
            InitializeComponent();
        }

        private void frmDocFile_Load(object sender, EventArgs e)
        {

        }

        private void btnDuongDan_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Hiển thị đường dẫn file lên giao diện
                txtChonFile.Text = openFileDialog1.FileName;
                LayTen=openFileDialog1.FileName;
            }
                
        }

        public string LayTen;
        private void btnDocFile_Click(object sender, EventArgs e)
        {
            //
            string noidung = Common.DocThongTin(txtChonFile.Text);
            //Hiển thị kết quả
            txtNoiDung.Text = noidung;
            MessageBox.Show("Đọc thành công");

        }

        private void btnGhiFile_Click(object sender, EventArgs e)
        {
            //
            Common.GhiThongTin(txtChonFile.Text,txtNoiDung.Text);
            MessageBox.Show("Ghi thanh công");

        }

       
    }
}
