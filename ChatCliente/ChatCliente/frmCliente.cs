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
        // Necessário para atualizar o formulário com mensagens da outra thread
        private delegate void AtualizaLogCallBack(string strMensagem);
        // Necessário para definir o formulário para o estado "disconnected" de outra thread
        private delegate void FechaConexaoCallBack(string strMotivo);
        private Thread mensagemThread;
        private IPAddress enderecoIP;
        private bool statusConexao;

        public frmCliente()
        {
           // Na saida da aplicação : desconectar
           Application.ApplicationExit += new EventHandler(OnApplicationExit);
           InitializeComponent();
        }

        private void btnConectar_Click(object sender, System.EventArgs e)
        {
            Conectar();
        }


        private void Conectar()
        {
            // se não esta conectando aguarda a conexão
            if (statusConexao == false)
            {
                // Inicializa a conexão
                inicializarConexao();
                this.groupBoxBatePapo.Enabled = true;
                this.txtMensagem.Focus();
            }
            else // Se esta conectado entao desconecta
            {
                encerrarCon("Desconectado a pedido do usuário.");
                this.groupBoxBatePapo.Enabled = false;
            }
        }
        private void inicializarConexao()
        {
            try
            {
                // Trata o endereço IP informado em um objeto IPAdress
                enderecoIP = IPAddress.Parse(txtServidorIP.Text);
                // Inicia uma nova conexão TCP com o servidor chat
                tcpServidor = new TcpClient();
                tcpServidor.Connect(enderecoIP, 2502);

                // AJuda a verificar se estamos conectados ou não
                statusConexao = true;

                // Prepara o formulário
                usuario = txtUsuario.Text;

                // Desabilita e habilita os campos apropriados
                txtServidorIP.Enabled = false;
                txtUsuario.Enabled = false;
                txtMensagem.Enabled = true;
                btnEnviar.Enabled = true;
                btnConectar.Text = "Desconectar";
                

                // Envia o nome do usuário ao servidor
                stwEnviador = new StreamWriter(tcpServidor.GetStream());
                stwEnviador.WriteLine(txtUsuario.Text);
                stwEnviador.Flush();

                //Inicia a thread para receber mensagens e nova comunicação
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
            // recebe a resposta do servidor
            strReceptor = new StreamReader(tcpServidor.GetStream());
            string ConResposta = strReceptor.ReadLine();
            // Se o primeiro caracater da resposta é 1 a conexão foi feita com sucesso
            if (ConResposta[0] == '1')
            {
                // Atualiza o formulário para informar que esta conectado
                this.Invoke(new AtualizaLogCallBack(this.atualizarChat), new object[] { "Conectado com sucesso!" });
            }
            else // Se o primeiro caractere não for 1 a conexão falhou
            {
                string Motivo = "Não Conectado: ";
                // Extrai o motivo da mensagem resposta. O motivo começa no 3o caractere
                Motivo += ConResposta.Substring(2, ConResposta.Length - 2);
                // Atualiza o formulário como o motivo da falha na conexão
                this.Invoke(new FechaConexaoCallBack(this.encerrarCon), new object[] { Motivo });
                // Sai do método
               return;
            }

            // Enquanto estiver conectado le as linhas que estão chegando do servidor
            while (statusConexao)
            {
                try
                {
                    // exibe mensagems no Textbox
                    this.Invoke(new AtualizaLogCallBack(this.atualizarChat), new object[] { strReceptor.ReadLine() });
                }
                catch
                {

                }
            }
        }

        private void atualizarChat(string strMensagem)
        {
            // Anexa texto ao final de cada linha
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
            // Se pressionou a tecla Enter
            if (e.KeyChar == (char)13)
            {
                enviarMensagem();
            }
        }

        // Envia a mensagem para o servidor
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

        // Fecha a conexão com o servidor
        private void encerrarCon(string Motivo)
        {
            // Mostra o motivo porque a conexão encerrou
            rTxtLog.SelectionColor = System.Drawing.Color.Red;
            rTxtLog.AppendText(Motivo + "\r\n");
            rTxtLog.SelectionColor = System.Drawing.Color.Black;
            rTxtLog.ScrollToCaret();

            // Habilita e desabilita controles do formulario
            txtServidorIP.Enabled = true;
            txtUsuario.Enabled = true;
            txtMensagem.Enabled = false;
            btnEnviar.Enabled = false;
            btnConectar.Text = "Conectar";
            this.groupBoxBatePapo.Enabled = false;


            // Fecha os objetos
            statusConexao = false;
            stwEnviador.Close();
            strReceptor.Close();
            tcpServidor.Close();
        }

        // O tratador de evento para a saida da aplicação
        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (statusConexao == true)
            {
                // Fecha as conexões, streams, etc...
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
            // se não esta conectando aguarda a conexão
            if (statusConexao == true)
            {
                // Se esta conectado entao desconecta
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
            // Se pressionou a tecla Enter
            if (e.KeyChar == (char)13)
            {
                this.txtUsuario.Focus();
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Se pressionou a tecla Enter
            if (e.KeyChar == (char)13)
            {
                Conectar();
            }
        }
    }
}
