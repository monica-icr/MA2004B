namespace AlgoritmoMonteCarlo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
            textBox8 = new TextBox();
            label8 = new Label();
            label9 = new Label();
            textBox9 = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(582, 64);
            button1.Name = "button1";
            button1.Size = new Size(94, 52);
            button1.TabIndex = 0;
            button1.Text = "Correr Algoritmo";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(178, 75);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(468, 75);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 2;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(178, 116);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 3;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(468, 116);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 4;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(178, 154);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(100, 23);
            textBox5.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 80);
            label1.Name = "label1";
            label1.Size = new Size(160, 15);
            label1.TabIndex = 6;
            label1.Text = "Cantidad minima por pedido";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(300, 78);
            label2.Name = "label2";
            label2.Size = new Size(162, 15);
            label2.TabIndex = 7;
            label2.Text = "Cantidad maxima por pedido";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(55, 119);
            label3.Name = "label3";
            label3.Size = new Size(106, 15);
            label3.TabIndex = 8;
            label3.Text = "Pedidos simulados";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(325, 119);
            label4.Name = "label4";
            label4.Size = new Size(116, 15);
            label4.TabIndex = 9;
            label4.Text = "Cantidad de pedidos";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(55, 157);
            label5.Name = "label5";
            label5.Size = new Size(117, 15);
            label5.TabIndex = 10;
            label5.Text = "Pedido Seleccionado";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(444, 361);
            label6.Name = "label6";
            label6.Size = new Size(50, 15);
            label6.TabIndex = 14;
            label6.Text = "Varianza";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(454, 329);
            label7.Name = "label7";
            label7.Size = new Size(40, 15);
            label7.TabIndex = 13;
            label7.Text = "Media";
            // 
            // textBox6
            // 
            textBox6.ImeMode = ImeMode.NoControl;
            textBox6.Location = new Point(546, 358);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(100, 23);
            textBox6.TabIndex = 12;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(546, 329);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(100, 23);
            textBox7.TabIndex = 11;
            textBox7.TextChanged += textBox7_TextChanged;
            // 
            // textBox8
            // 
            textBox8.Location = new Point(546, 392);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(100, 23);
            textBox8.TabIndex = 15;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(412, 400);
            label8.Name = "label8";
            label8.Size = new Size(112, 15);
            label8.TabIndex = 16;
            label8.Text = "Desviación Estandar";
            label8.Click += label8_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(352, 157);
            label9.Name = "label9";
            label9.Size = new Size(89, 15);
            label9.TabIndex = 18;
            label9.Text = "Total Productos";
            // 
            // textBox9
            // 
            textBox9.Location = new Point(468, 154);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(100, 23);
            textBox9.TabIndex = 17;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label9);
            Controls.Add(textBox9);
            Controls.Add(label8);
            Controls.Add(textBox8);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(textBox6);
            Controls.Add(textBox7);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox textBox8;
        private Label label8;
        private Label label9;
        private TextBox textBox9;
    }
}
