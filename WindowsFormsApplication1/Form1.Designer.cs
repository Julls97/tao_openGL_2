namespace OpenGL_cube
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.simpleOpenGlControl1 = new Tao.Platform.Windows.SimpleOpenGlControl();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// simpleOpenGlControl1
			// 
			this.simpleOpenGlControl1.AccumBits = ((byte)(0));
			this.simpleOpenGlControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleOpenGlControl1.AutoCheckErrors = false;
			this.simpleOpenGlControl1.AutoFinish = false;
			this.simpleOpenGlControl1.AutoMakeCurrent = true;
			this.simpleOpenGlControl1.AutoSwapBuffers = true;
			this.simpleOpenGlControl1.BackColor = System.Drawing.Color.Black;
			this.simpleOpenGlControl1.ColorBits = ((byte)(32));
			this.simpleOpenGlControl1.DepthBits = ((byte)(16));
			this.simpleOpenGlControl1.Location = new System.Drawing.Point(2, 2);
			this.simpleOpenGlControl1.Name = "simpleOpenGlControl1";
			this.simpleOpenGlControl1.Size = new System.Drawing.Size(650, 399);
			this.simpleOpenGlControl1.StencilBits = ((byte)(0));
			this.simpleOpenGlControl1.TabIndex = 0;
			this.simpleOpenGlControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.simpleOpenGlControl1_Paint);
			this.simpleOpenGlControl1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.simpleOpenGlControl1_KeyPress);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 1;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(653, 402);
			this.Controls.Add(this.simpleOpenGlControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl simpleOpenGlControl1;
        private System.Windows.Forms.Timer timer1;

    }
}

