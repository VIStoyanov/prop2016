namespace StatusOfEvent
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.listBoxVisitors = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxArticles = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxShops = new System.Windows.Forms.ListBox();
            this.listBoxStatus = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonInfoAboutAllEventAccounts = new System.Windows.Forms.Button();
            this.labelNrOfPresentLeftArriving = new System.Windows.Forms.Label();
            this.buttonStatusOfCampingSpot = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxVisitors = new System.Windows.Forms.TextBox();
            this.buttonSearchVisitors = new System.Windows.Forms.Button();
            this.buttonSearchArticles = new System.Windows.Forms.Button();
            this.textBoxArticles = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // listBoxVisitors
            // 
            this.listBoxVisitors.BackColor = System.Drawing.SystemColors.Menu;
            this.listBoxVisitors.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxVisitors.FormattingEnabled = true;
            this.listBoxVisitors.ItemHeight = 24;
            this.listBoxVisitors.Location = new System.Drawing.Point(34, 36);
            this.listBoxVisitors.Name = "listBoxVisitors";
            this.listBoxVisitors.Size = new System.Drawing.Size(214, 100);
            this.listBoxVisitors.TabIndex = 0;
            this.listBoxVisitors.SelectedIndexChanged += new System.EventHandler(this.listBoxVisitors_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGreen;
            this.label1.Location = new System.Drawing.Point(108, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Visitors";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkGreen;
            this.label2.Location = new System.Drawing.Point(408, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Articles";
            // 
            // listBoxArticles
            // 
            this.listBoxArticles.BackColor = System.Drawing.SystemColors.Menu;
            this.listBoxArticles.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxArticles.FormattingEnabled = true;
            this.listBoxArticles.ItemHeight = 24;
            this.listBoxArticles.Location = new System.Drawing.Point(339, 36);
            this.listBoxArticles.Name = "listBoxArticles";
            this.listBoxArticles.Size = new System.Drawing.Size(216, 100);
            this.listBoxArticles.TabIndex = 4;
            this.listBoxArticles.SelectedIndexChanged += new System.EventHandler(this.listBoxArticles_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkGreen;
            this.label3.Location = new System.Drawing.Point(711, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "Shops";
            // 
            // listBoxShops
            // 
            this.listBoxShops.BackColor = System.Drawing.SystemColors.Menu;
            this.listBoxShops.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxShops.FormattingEnabled = true;
            this.listBoxShops.ItemHeight = 24;
            this.listBoxShops.Location = new System.Drawing.Point(636, 36);
            this.listBoxShops.Name = "listBoxShops";
            this.listBoxShops.Size = new System.Drawing.Size(218, 100);
            this.listBoxShops.TabIndex = 6;
            this.listBoxShops.SelectedIndexChanged += new System.EventHandler(this.listBoxShops_SelectedIndexChanged);
            // 
            // listBoxStatus
            // 
            this.listBoxStatus.BackColor = System.Drawing.SystemColors.Menu;
            this.listBoxStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxStatus.FormattingEnabled = true;
            this.listBoxStatus.ItemHeight = 24;
            this.listBoxStatus.Location = new System.Drawing.Point(34, 199);
            this.listBoxStatus.Name = "listBoxStatus";
            this.listBoxStatus.Size = new System.Drawing.Size(820, 172);
            this.listBoxStatus.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkGreen;
            this.label4.Location = new System.Drawing.Point(30, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "Status";
            // 
            // buttonInfoAboutAllEventAccounts
            // 
            this.buttonInfoAboutAllEventAccounts.BackColor = System.Drawing.Color.YellowGreen;
            this.buttonInfoAboutAllEventAccounts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonInfoAboutAllEventAccounts.Location = new System.Drawing.Point(34, 394);
            this.buttonInfoAboutAllEventAccounts.Name = "buttonInfoAboutAllEventAccounts";
            this.buttonInfoAboutAllEventAccounts.Size = new System.Drawing.Size(240, 90);
            this.buttonInfoAboutAllEventAccounts.TabIndex = 10;
            this.buttonInfoAboutAllEventAccounts.Text = "Display info about all event-accounts";
            this.buttonInfoAboutAllEventAccounts.UseVisualStyleBackColor = false;
            this.buttonInfoAboutAllEventAccounts.Click += new System.EventHandler(this.buttonInfoAboutAllEventAccounts_Click);
            // 
            // labelNrOfPresentLeftArriving
            // 
            this.labelNrOfPresentLeftArriving.BackColor = System.Drawing.Color.Yellow;
            this.labelNrOfPresentLeftArriving.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNrOfPresentLeftArriving.Location = new System.Drawing.Point(672, 394);
            this.labelNrOfPresentLeftArriving.Name = "labelNrOfPresentLeftArriving";
            this.labelNrOfPresentLeftArriving.Size = new System.Drawing.Size(164, 90);
            this.labelNrOfPresentLeftArriving.TabIndex = 12;
            this.labelNrOfPresentLeftArriving.Text = "labelPresent/Left/Arriving";
            // 
            // buttonStatusOfCampingSpot
            // 
            this.buttonStatusOfCampingSpot.BackColor = System.Drawing.Color.YellowGreen;
            this.buttonStatusOfCampingSpot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStatusOfCampingSpot.Location = new System.Drawing.Point(306, 394);
            this.buttonStatusOfCampingSpot.Name = "buttonStatusOfCampingSpot";
            this.buttonStatusOfCampingSpot.Size = new System.Drawing.Size(240, 90);
            this.buttonStatusOfCampingSpot.TabIndex = 13;
            this.buttonStatusOfCampingSpot.Text = "Display status of camping spots";
            this.buttonStatusOfCampingSpot.UseVisualStyleBackColor = false;
            this.buttonStatusOfCampingSpot.Click += new System.EventHandler(this.buttonStatusOfCampingSpot_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(644, 372);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 22);
            this.label5.TabIndex = 14;
            this.label5.Text = "Status of all people";
            // 
            // textBoxVisitors
            // 
            this.textBoxVisitors.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxVisitors.Location = new System.Drawing.Point(34, 142);
            this.textBoxVisitors.Name = "textBoxVisitors";
            this.textBoxVisitors.Size = new System.Drawing.Size(112, 26);
            this.textBoxVisitors.TabIndex = 15;
            // 
            // buttonSearchVisitors
            // 
            this.buttonSearchVisitors.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSearchVisitors.Location = new System.Drawing.Point(152, 142);
            this.buttonSearchVisitors.Name = "buttonSearchVisitors";
            this.buttonSearchVisitors.Size = new System.Drawing.Size(96, 41);
            this.buttonSearchVisitors.TabIndex = 16;
            this.buttonSearchVisitors.Text = "Search";
            this.buttonSearchVisitors.UseVisualStyleBackColor = true;
            this.buttonSearchVisitors.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonSearchArticles
            // 
            this.buttonSearchArticles.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSearchArticles.Location = new System.Drawing.Point(459, 142);
            this.buttonSearchArticles.Name = "buttonSearchArticles";
            this.buttonSearchArticles.Size = new System.Drawing.Size(96, 41);
            this.buttonSearchArticles.TabIndex = 18;
            this.buttonSearchArticles.Text = "Search";
            this.buttonSearchArticles.UseVisualStyleBackColor = true;
            this.buttonSearchArticles.Click += new System.EventHandler(this.buttonSearchArticles_Click);
            // 
            // textBoxArticles
            // 
            this.textBoxArticles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxArticles.Location = new System.Drawing.Point(339, 142);
            this.textBoxArticles.Name = "textBoxArticles";
            this.textBoxArticles.Size = new System.Drawing.Size(114, 26);
            this.textBoxArticles.TabIndex = 17;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.BackgroundImage = global::StatusOfEvent.Properties.Resources.wallhaven_242837;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(894, 496);
            this.Controls.Add(this.buttonSearchArticles);
            this.Controls.Add(this.textBoxArticles);
            this.Controls.Add(this.buttonSearchVisitors);
            this.Controls.Add(this.textBoxVisitors);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonStatusOfCampingSpot);
            this.Controls.Add(this.labelNrOfPresentLeftArriving);
            this.Controls.Add(this.buttonInfoAboutAllEventAccounts);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listBoxStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBoxShops);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxArticles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxVisitors);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxVisitors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxArticles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxShops;
        private System.Windows.Forms.ListBox listBoxStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonInfoAboutAllEventAccounts;
        private System.Windows.Forms.Label labelNrOfPresentLeftArriving;
        private System.Windows.Forms.Button buttonStatusOfCampingSpot;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxVisitors;
        private System.Windows.Forms.Button buttonSearchVisitors;
        private System.Windows.Forms.Button buttonSearchArticles;
        private System.Windows.Forms.TextBox textBoxArticles;
        private System.Windows.Forms.Timer timer1;
    }
}

