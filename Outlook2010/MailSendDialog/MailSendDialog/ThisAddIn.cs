using Outlook = Microsoft.Office.Interop.Outlook;

namespace MailSendDialog
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            // イベント追加
            Application.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(Application_ItemSend);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        void Application_ItemSend(object Item, ref bool Cancel)
        {
            // メールオブジェクトキャスト
            Outlook.MailItem MailItem = (Outlook.MailItem)Item;

            SendMessageBox MailMessage = new SendMessageBox();
            MailMessage.SubjectText = MailItem.Subject;
            MailMessage.gAddress = MailItem.Recipients;
            MailMessage.gTempFile = MailItem.Attachments;

            if (MailMessage.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                // 送信キャンセル
                Cancel = true;
            }
            else
            {
                // 宛先名の削除
                if (MailMessage.DeleteMailName)
                {
                    for (int i = 0; i < MailItem.Recipients.Count; i++)
                    {
                        Outlook.Recipient objRecip = MailItem.Recipients[1];
                        MailItem.Recipients.Remove(1);
                        Outlook.Recipient objNewRecip = MailItem.Recipients.Add(objRecip.Address);
                        objNewRecip.Type = objRecip.Type;
                        objNewRecip.Resolve();
                    }
                }
            }
            return;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
