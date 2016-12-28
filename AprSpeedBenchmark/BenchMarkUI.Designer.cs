using System;

namespace GBA_effect
{
    partial class BenchMarkUI
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BenchMarkUI));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label_1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label_2 = new System.Windows.Forms.Label();
            this.label_3 = new System.Windows.Forms.Label();
            this.label_4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(12, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 512);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Run (綜合)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.benchmark_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 589);
            this.progressBar1.MarqueeAnimationSpeed = 10;
            this.progressBar1.Maximum = 68904000;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(511, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // label_1
            // 
            this.label_1.AutoSize = true;
            this.label_1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_1.Location = new System.Drawing.Point(12, 615);
            this.label_1.Name = "label_1";
            this.label_1.Size = new System.Drawing.Size(137, 16);
            this.label_1.TabIndex = 5;
            this.label_1.Text = "平均時速(綜合) : ";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Run(整數)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(149, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Run (綜合,無輸出)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(149, 41);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(131, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Run(整數,無輸出)";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label_2
            // 
            this.label_2.AutoSize = true;
            this.label_2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_2.Location = new System.Drawing.Point(12, 640);
            this.label_2.Name = "label_2";
            this.label_2.Size = new System.Drawing.Size(137, 16);
            this.label_2.TabIndex = 9;
            this.label_2.Text = "平均時速(整數) : ";
            // 
            // label_3
            // 
            this.label_3.AutoSize = true;
            this.label_3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_3.Location = new System.Drawing.Point(230, 615);
            this.label_3.Name = "label_3";
            this.label_3.Size = new System.Drawing.Size(193, 16);
            this.label_3.TabIndex = 10;
            this.label_3.Text = "平均時速(綜合,無輸出) : ";
            // 
            // label_4
            // 
            this.label_4.AutoSize = true;
            this.label_4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_4.Location = new System.Drawing.Point(230, 640);
            this.label_4.Name = "label_4";
            this.label_4.Size = new System.Drawing.Size(193, 16);
            this.label_4.TabIndex = 11;
            this.label_4.Text = "平均時速(整數,無輸出) : ";
            // 
            // BenchMarkUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 672);
            this.Controls.Add(this.label_4);
            this.Controls.Add(this.label_3);
            this.Controls.Add(this.label_2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label_1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BenchMarkUI";
            this.Text = "SpeedBenchmark";
            this.Shown += new System.EventHandler(this.BenchMarkUI_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label_1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label_2;
        private System.Windows.Forms.Label label_3;
        private System.Windows.Forms.Label label_4;
    }
}

