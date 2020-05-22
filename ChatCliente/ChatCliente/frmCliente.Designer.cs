namespace ChatCliente
{
    partial class frmCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCliente));
            this.txtServidorIP = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConectar = new System.Windows.Forms.Button();
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxBatePapo = new System.Windows.Forms.GroupBox();
            this.rTxtLog = new System.Windows.Forms.RichTextBox();
            this.btnEnviar = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBoxBatePapo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEnviar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtServidorIP
            // 
            this.txtServidorIP.Location = new System.Drawing.Point(13, 42);
            this.txtServidorIP.Name = "txtServidorIP";
            this.txtServidorIP.Size = new System.Drawing.Size(98, 20);
            this.txtServidorIP.TabIndex = 0;
            this.txtServidorIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtServidorIP_KeyPress);
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(118, 42);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(123, 20);
            this.txtUsuario.TabIndex = 1;
            this.txtUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUsuario_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Servidor IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Usuário";
            // 
            // btnConectar
            // 
            this.btnConectar.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnConectar.BackColor = System.Drawing.SystemColors.Control;
            this.btnConectar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnConectar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConectar.Location = new System.Drawing.Point(247, 39);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(119, 25);
            this.btnConectar.TabIndex = 5;
            this.btnConectar.Text = "&Conectar";
            this.btnConectar.UseVisualStyleBackColor = false;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // txtMensagem
            // 
            this.txtMensagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMensagem.Location = new System.Drawing.Point(13, 277);
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(313, 26);
            this.txtMensagem.TabIndex = 6;
            this.txtMensagem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMensagem_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConectar);
            this.groupBox1.Controls.Add(this.txtServidorIP);
            this.groupBox1.Controls.Add(this.txtUsuario);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 82);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conectar";
            // 
            // groupBoxBatePapo
            // 
            this.groupBoxBatePapo.Controls.Add(this.rTxtLog);
            this.groupBoxBatePapo.Controls.Add(this.btnEnviar);
            this.groupBoxBatePapo.Controls.Add(this.txtMensagem);
            this.groupBoxBatePapo.Location = new System.Drawing.Point(8, 93);
            this.groupBoxBatePapo.Name = "groupBoxBatePapo";
            this.groupBoxBatePapo.Size = new System.Drawing.Size(379, 316);
            this.groupBoxBatePapo.TabIndex = 9;
            this.groupBoxBatePapo.TabStop = false;
            this.groupBoxBatePapo.Text = "Bate Papo";
            // 
            // rTxtLog
            // 
            this.rTxtLog.Location = new System.Drawing.Point(13, 19);
            this.rTxtLog.Name = "rTxtLog";
            this.rTxtLog.ReadOnly = true;
            this.rTxtLog.Size = new System.Drawing.Size(353, 252);
            this.rTxtLog.TabIndex = 10;
            this.rTxtLog.Text = "";
            // 
            // btnEnviar
            // 
            this.btnEnviar.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnEnviar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnEnviar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviar.Image = global::ChatCliente.Properties.Resources.send_message_icon_png_3;
            this.btnEnviar.Location = new System.Drawing.Point(332, 277);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(34, 26);
            this.btnEnviar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnEnviar.TabIndex = 7;
            this.btnEnviar.TabStop = false;
            this.btnEnviar.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // frmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(397, 419);
            this.Controls.Add(this.groupBoxBatePapo);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat - Cliente";
            this.Load += new System.EventHandler(this.frmCliente_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxBatePapo.ResumeLayout(false);
            this.groupBoxBatePapo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEnviar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtServidorIP;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxBatePapo;
        private System.Windows.Forms.PictureBox btnEnviar;
        private System.Windows.Forms.RichTextBox rTxtLog;
    }
}

