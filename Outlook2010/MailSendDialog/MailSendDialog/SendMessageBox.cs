using System;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace MailSendDialog
{
    public partial class SendMessageBox : Form
    {
        public SendMessageBox()
        {
            InitializeComponent();
        }

        public Boolean DeleteMailName
        {
            get { return checkBox1.Checked; }
            private set { checkBox1.Checked = value; }
        }
        public String SubjectText { set; private get; }
        public Outlook.Recipients gAddress { private get; set; }
        public Outlook.Attachments gTempFile { private get; set; }

        private void SendMessageBox_Load(object sender, EventArgs e)
        {
            // 宛名削除標準ON
            DeleteMailName = true;

            // 件名セット
            if (String.IsNullOrEmpty(SubjectText))
                lbl_Title.Text = "**** 件名なし ****";
            else
                lbl_Title.Text = SubjectText;

            // 宛先セット
            label3.Text += "　件数：" + gAddress.Count;
            int lcnt = 0;
            listBox1.Items.Clear();
            foreach (Outlook.Recipient oAddress in gAddress)
            {
                lcnt++;
                listBox1.Items.Add(String.Format("{0:D2}", lcnt) + ". " + oAddress.Name + " <" + oAddress.Address + "> ");
            }

            // 添付ファイルセット
            label4.Text += "　件数：" + gTempFile.Count;
            lcnt = 0;
            listBox2.Items.Clear();
            if (gTempFile.Count > 0)
            {
                foreach (Outlook.Attachment oTempFile in gTempFile)
                {
                    lcnt++;
                    listBox2.Items.Add(String.Format("{0:D2}", lcnt) + ". " + oTempFile.FileName);
                }
            }
            else { listBox2.Items.Add("**** 添付ファイルなし ****"); }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            listBox1.SelectedIndex = -1;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex == -1) return;
            listBox2.SelectedIndex = -1;
        }

    }
}
