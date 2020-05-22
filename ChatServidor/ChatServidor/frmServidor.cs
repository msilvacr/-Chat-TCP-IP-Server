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

        public void mainServidor_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Chama o método que atualiza o formulário
            this.Invoke(new AtualizaStatusCallback(this.AtualizaStatus), new object[] { e.EventMessage });
        }

        private void AtualizaStatus(string strMensagem)
        {
            // Atualiza o logo com mensagens
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
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            Conectar();
        }

        private void Conectar()
        {

            if (txtIP.Text == string.Empty)
            {
                MessageBox.Show("Informe o endereço IP.");
                txtIP.Focus();
                return;
            }
            try
            {
                // Analisa o endereço IP do servidor informado no textbox
                IPAddress enderecoIP = IPAddress.Parse(txtIP.Text);

                // Cria uma nova instância do objeto ChatServidor
                ChatServidor mainServidor = new ChatServidor(enderecoIP);

                // Vincula o tratamento de evento StatusChanged a mainServer_StatusChanged
                ChatServidor.StatusChanged += new StatusChangedEventHandler(mainServidor_StatusChanged);

                // Inicia o atendimento das conexões
                mainServidor.IniciaAtendimento();

                // Mostra que nos iniciamos o atendimento para conexões
                rTxtLog.SelectionColor = Color.Blue;
                rTxtLog.AppendText("Monitorando as conexões...\r\n");
                rTxtLog.SelectionColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro de conexão : " + ex.Message);
            }
        }
        private void txtIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Se pressionou a tecla Enter
            if (e.KeyChar == (char)13)
            {
                Conectar();
            }
        }
    }
}
