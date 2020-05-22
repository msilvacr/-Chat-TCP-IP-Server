using System.Windows.Forms;
using System.Net;
using System;
using System.Drawing;

namespace ChatServidor
{
    public partial class FormServidor : System.Windows.Forms.Form
    {
        private delegate void AtualizaStatusCallback(string strMensagem);

        public FormServidor()
        {
            InitializeComponent();
        }

        public void mudancaStatus(object sender, StatusChangedEventArgs e)
        {
            this.Invoke(new AtualizaStatusCallback(this.AtualizaStatus), new object[] { e.EventMessage });
        }

        private void AtualizaStatus(string strMensagem)
        {
            if (strMensagem.Contains("[ADM]"))
            {
                rTxtLog.SelectionColor = Color.Red;
                rTxtLog.AppendText(strMensagem + "\r\n");
                rTxtLog.SelectionColor = Color.Black;
            }
            else
            {
                rTxtLog.AppendText(strMensagem + "\r\n");
            }
            rTxtLog.ScrollToCaret();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            conectarServidor();
        }

        private void conectarServidor()
        {

            if (txtIP.Text == string.Empty)
            {
                MessageBox.Show("Informe o endereço IP.");
                txtIP.Focus();
                return;
            }
            try
            {
                IPAddress enderecoIP = IPAddress.Parse(txtIP.Text);
                ChatServidor mainServidor = new ChatServidor(enderecoIP);
                ChatServidor.StatusChanged += new StatusChangedEventHandler(mudancaStatus);
                mainServidor.IniciaAtendimento();
                rTxtLog.SelectionColor = Color.Blue;
                rTxtLog.AppendText("Servidor conetado.\r\n");
                rTxtLog.SelectionColor = Color.Black;
                rTxtLog.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro de conexão : " + ex.Message);
            }
        }
        private void txtIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                conectarServidor();
            }
        }
    }
}
