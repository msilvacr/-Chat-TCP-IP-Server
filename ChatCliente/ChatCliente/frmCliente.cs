using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System;

namespace ChatCliente
{
    public partial class frmCliente : Form
    {
        // Trata o nome do usuário
        private string usuario = "Desconhecido";
        private StreamWriter stwEnviador;
        private StreamReader strReceptor;
        private TcpClient tcpServidor;
        private delegate void AtualizaLogCallBack(string strMensagem);
        private delegate void FechaConexaoCallBack(string strMotivo);
        private Thread mensagemThread;
        private IPAddress enderecoIP;
        private bool statusConexao;

        public frmCliente()
        {
           Application.ApplicationExit += new EventHandler(OnApplicationExit);
           InitializeComponent();
        }

        private void btnConectar_Click(object sender, System.EventArgs e)
        {
            Conectar();
        }


        private void Conectar()
        {
            if (statusConexao == false)
            {
                inicializarConexao();
                this.groupBoxBatePapo.Enabled = true;
                this.txtMensagem.Focus();
            }
            else 
            {
                encerrarCon("Desconectado a pedido do usuário.");
                this.groupBoxBatePapo.Enabled = false;
            }
        }
        private void inicializarConexao()
        {
            try
            {
                enderecoIP = IPAddress.Parse(txtServidorIP.Text);
                tcpServidor = new TcpClient();
                tcpServidor.Connect(enderecoIP, 2502);
                statusConexao = true;
                usuario = txtUsuario.Text;
                txtServidorIP.Enabled = false;
                txtUsuario.Enabled = false;
                txtMensagem.Enabled = true;
                btnEnviar.Enabled = true;
                btnConectar.Text = "Desconectar";
                
                stwEnviador = new StreamWriter(tcpServidor.GetStream());
                stwEnviador.WriteLine(txtUsuario.Text);
                stwEnviador.Flush();

                mensagemThread = new Thread(new ThreadStart(receberMensagem));
                mensagemThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message, "Erro ao conectar com o servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void receberMensagem()
        {
            strReceptor = new StreamReader(tcpServidor.GetStream());
            string ConResposta = strReceptor.ReadLine();
            if (ConResposta[0] == '1')
            {
                this.Invoke(new AtualizaLogCallBack(this.atualizarChat), new object[] { "Conectado com sucesso!" });
            }
            else // Se o primeiro caractere não for 1 a conexão falhou
            {
                string causaDesconecao = "Não Conectado: ";
                causaDesconecao += ConResposta.Substring(2, ConResposta.Length - 2);
                this.Invoke(new FechaConexaoCallBack(this.encerrarCon), new object[] { causaDesconecao });
               return;
            }
            while (statusConexao)
            {
                try
                {
                    this.Invoke(new AtualizaLogCallBack(this.atualizarChat), new object[] { strReceptor.ReadLine() });
                }
                catch
                {

                }
            }
        }

        private void atualizarChat(string strMensagem)
        {
            if (strMensagem.Contains("[ADM]"))
            {
                rTxtLog.SelectionColor = System.Drawing.Color.Red;
                rTxtLog.AppendText(strMensagem + "\r\n");
                rTxtLog.SelectionColor = System.Drawing.Color.Black;
            }
            else if (strMensagem.Contains("Conectado com sucesso!"))
            {
                rTxtLog.SelectionColor = System.Drawing.Color.Green;
                rTxtLog.AppendText(strMensagem + "\r\n");
                rTxtLog.SelectionColor = System.Drawing.Color.Black;
            }
            else
            {
                rTxtLog.AppendText(strMensagem + "\r\n");
            }

            rTxtLog.ScrollToCaret();
        }

        private void btnEnviar_Click(object sender, System.EventArgs e)
        {
            enviarMensagem();
        }

        private void txtMensagem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                enviarMensagem();
            }
        }
        private void enviarMensagem()
        {
            if (txtMensagem.Lines.Length >= 1)
            {
                stwEnviador.WriteLine(txtMensagem.Text);
                stwEnviador.Flush();
                txtMensagem.Lines = null;
            }
            txtMensagem.Text = "";
            txtMensagem.Focus();
        }
        private void encerrarCon(string Motivo)
        {
            rTxtLog.SelectionColor = System.Drawing.Color.Red;
            rTxtLog.AppendText(Motivo + "\r\n");
            rTxtLog.SelectionColor = System.Drawing.Color.Black;
            rTxtLog.ScrollToCaret();

            txtServidorIP.Enabled = true;
            txtUsuario.Enabled = true;
            txtMensagem.Enabled = false;
            btnEnviar.Enabled = false;
            btnConectar.Text = "Conectar";
            this.groupBoxBatePapo.Enabled = false;

            statusConexao = false;
            stwEnviador.Close();
            strReceptor.Close();
            tcpServidor.Close();
        }

        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (statusConexao == true)
            {
                statusConexao = false;
                stwEnviador.Close();
                strReceptor.Close();
                tcpServidor.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            enviarMensagem();
        }

        private void desconectarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusConexao == true)
            {
                encerrarCon("Desconectado a pedido do usuário.");
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            this.groupBoxBatePapo.Enabled = false;  
        }

        private void txtServidorIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.txtUsuario.Focus();
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Conectar();
            }
        }
    }
}
