﻿
namespace NxWifiScanner
{
    partial class WifiScanner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WifiScanner));
            this.panel_AvailableWifi = new System.Windows.Forms.Panel();
            this.pictureBox_SavedWifi = new System.Windows.Forms.PictureBox();
            this.pictureBox_AvailableWifi = new System.Windows.Forms.PictureBox();
            this.label_SavedWifi = new System.Windows.Forms.Label();
            this.label_AvailableWifi = new System.Windows.Forms.Label();
            this.button_Back = new System.Windows.Forms.Button();
            this.button_WifiDetails = new System.Windows.Forms.Button();
            this.listBox_AvailableWifi = new System.Windows.Forms.ListBox();
            this.listView_wifiPasswords = new System.Windows.Forms.ListView();
            this.label_WifiToolDesc = new System.Windows.Forms.Label();
            this.lstLocal = new System.Windows.Forms.ListView();
            this.button_AdminConsole = new System.Windows.Forms.Button();
            this.button_wifiState = new System.Windows.Forms.Button();
            this.label_wifiState = new System.Windows.Forms.Label();
            this.label_wifiAdmin = new System.Windows.Forms.Label();
            this.panel_AvailableWifi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SavedWifi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AvailableWifi)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_AvailableWifi
            // 
            this.panel_AvailableWifi.Controls.Add(this.label_wifiAdmin);
            this.panel_AvailableWifi.Controls.Add(this.label_wifiState);
            this.panel_AvailableWifi.Controls.Add(this.button_wifiState);
            this.panel_AvailableWifi.Controls.Add(this.button_AdminConsole);
            this.panel_AvailableWifi.Controls.Add(this.lstLocal);
            this.panel_AvailableWifi.Controls.Add(this.pictureBox_SavedWifi);
            this.panel_AvailableWifi.Controls.Add(this.pictureBox_AvailableWifi);
            this.panel_AvailableWifi.Controls.Add(this.label_SavedWifi);
            this.panel_AvailableWifi.Controls.Add(this.label_AvailableWifi);
            this.panel_AvailableWifi.Controls.Add(this.button_Back);
            this.panel_AvailableWifi.Controls.Add(this.button_WifiDetails);
            this.panel_AvailableWifi.Controls.Add(this.listBox_AvailableWifi);
            this.panel_AvailableWifi.Controls.Add(this.listView_wifiPasswords);
            this.panel_AvailableWifi.Location = new System.Drawing.Point(12, 25);
            this.panel_AvailableWifi.Name = "panel_AvailableWifi";
            this.panel_AvailableWifi.Size = new System.Drawing.Size(760, 398);
            this.panel_AvailableWifi.TabIndex = 0;
            // 
            // pictureBox_SavedWifi
            // 
            this.pictureBox_SavedWifi.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_SavedWifi.Image")));
            this.pictureBox_SavedWifi.Location = new System.Drawing.Point(294, 0);
            this.pictureBox_SavedWifi.Name = "pictureBox_SavedWifi";
            this.pictureBox_SavedWifi.Size = new System.Drawing.Size(62, 49);
            this.pictureBox_SavedWifi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_SavedWifi.TabIndex = 7;
            this.pictureBox_SavedWifi.TabStop = false;
            // 
            // pictureBox_AvailableWifi
            // 
            this.pictureBox_AvailableWifi.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_AvailableWifi.Image")));
            this.pictureBox_AvailableWifi.Location = new System.Drawing.Point(304, 12);
            this.pictureBox_AvailableWifi.Name = "pictureBox_AvailableWifi";
            this.pictureBox_AvailableWifi.Size = new System.Drawing.Size(36, 36);
            this.pictureBox_AvailableWifi.TabIndex = 6;
            this.pictureBox_AvailableWifi.TabStop = false;
            // 
            // label_SavedWifi
            // 
            this.label_SavedWifi.AutoSize = true;
            this.label_SavedWifi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_SavedWifi.Location = new System.Drawing.Point(18, 12);
            this.label_SavedWifi.Name = "label_SavedWifi";
            this.label_SavedWifi.Size = new System.Drawing.Size(252, 24);
            this.label_SavedWifi.TabIndex = 5;
            this.label_SavedWifi.Text = "Saved Wifi Connections";
            // 
            // label_AvailableWifi
            // 
            this.label_AvailableWifi.AutoSize = true;
            this.label_AvailableWifi.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_AvailableWifi.Location = new System.Drawing.Point(19, 12);
            this.label_AvailableWifi.Name = "label_AvailableWifi";
            this.label_AvailableWifi.Size = new System.Drawing.Size(279, 24);
            this.label_AvailableWifi.TabIndex = 4;
            this.label_AvailableWifi.Text = "Available Wifi Connections";
            // 
            // button_Back
            // 
            this.button_Back.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Back.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Back.Location = new System.Drawing.Point(654, 331);
            this.button_Back.Name = "button_Back";
            this.button_Back.Size = new System.Drawing.Size(85, 48);
            this.button_Back.TabIndex = 3;
            this.button_Back.Text = "Back";
            this.button_Back.UseVisualStyleBackColor = false;
            this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
            // 
            // button_WifiDetails
            // 
            this.button_WifiDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button_WifiDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_WifiDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_WifiDetails.Location = new System.Drawing.Point(22, 331);
            this.button_WifiDetails.Name = "button_WifiDetails";
            this.button_WifiDetails.Size = new System.Drawing.Size(91, 48);
            this.button_WifiDetails.TabIndex = 1;
            this.button_WifiDetails.Text = "More Details";
            this.button_WifiDetails.UseVisualStyleBackColor = false;
            this.button_WifiDetails.Click += new System.EventHandler(this.button_WifiDetails_Click);
            // 
            // listBox_AvailableWifi
            // 
            this.listBox_AvailableWifi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_AvailableWifi.FormattingEnabled = true;
            this.listBox_AvailableWifi.ItemHeight = 18;
            this.listBox_AvailableWifi.Location = new System.Drawing.Point(23, 55);
            this.listBox_AvailableWifi.Name = "listBox_AvailableWifi";
            this.listBox_AvailableWifi.Size = new System.Drawing.Size(716, 256);
            this.listBox_AvailableWifi.TabIndex = 0;
            this.listBox_AvailableWifi.SelectedIndexChanged += new System.EventHandler(this.listBox_AvailableWifi_SelectedIndexChanged);
            // 
            // listView_wifiPasswords
            // 
            this.listView_wifiPasswords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView_wifiPasswords.HideSelection = false;
            this.listView_wifiPasswords.Location = new System.Drawing.Point(22, 55);
            this.listView_wifiPasswords.Name = "listView_wifiPasswords";
            this.listView_wifiPasswords.Size = new System.Drawing.Size(717, 252);
            this.listView_wifiPasswords.TabIndex = 2;
            this.listView_wifiPasswords.UseCompatibleStateImageBehavior = false;
            this.listView_wifiPasswords.SelectedIndexChanged += new System.EventHandler(this.listView_wifiPasswords_SelectedIndexChanged);
            // 
            // label_WifiToolDesc
            // 
            this.label_WifiToolDesc.AutoSize = true;
            this.label_WifiToolDesc.Location = new System.Drawing.Point(31, 439);
            this.label_WifiToolDesc.Name = "label_WifiToolDesc";
            this.label_WifiToolDesc.Size = new System.Drawing.Size(713, 26);
            this.label_WifiToolDesc.TabIndex = 1;
            this.label_WifiToolDesc.Text = resources.GetString("label_WifiToolDesc.Text");
            this.label_WifiToolDesc.Click += new System.EventHandler(this.label1_Click);
            // 
            // lstLocal
            // 
            this.lstLocal.HideSelection = false;
            this.lstLocal.Location = new System.Drawing.Point(99, 54);
            this.lstLocal.Name = "lstLocal";
            this.lstLocal.Size = new System.Drawing.Size(640, 253);
            this.lstLocal.TabIndex = 8;
            this.lstLocal.UseCompatibleStateImageBehavior = false;
            // 
            // button_AdminConsole
            // 
            this.button_AdminConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button_AdminConsole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_AdminConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_AdminConsole.Location = new System.Drawing.Point(345, 331);
            this.button_AdminConsole.Name = "button_AdminConsole";
            this.button_AdminConsole.Size = new System.Drawing.Size(91, 48);
            this.button_AdminConsole.TabIndex = 9;
            this.button_AdminConsole.Text = "Admin Console";
            this.button_AdminConsole.UseVisualStyleBackColor = false;
            this.button_AdminConsole.Click += new System.EventHandler(this.button_AdminConsole_Click);
            // 
            // button_wifiState
            // 
            this.button_wifiState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_wifiState.Location = new System.Drawing.Point(22, 155);
            this.button_wifiState.Name = "button_wifiState";
            this.button_wifiState.Size = new System.Drawing.Size(59, 45);
            this.button_wifiState.TabIndex = 10;
            this.button_wifiState.Text = "ON";
            this.button_wifiState.UseVisualStyleBackColor = true;
            this.button_wifiState.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_wifiState
            // 
            this.label_wifiState.AutoSize = true;
            this.label_wifiState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_wifiState.Location = new System.Drawing.Point(8, 111);
            this.label_wifiState.Name = "label_wifiState";
            this.label_wifiState.Size = new System.Drawing.Size(85, 32);
            this.label_wifiState.TabIndex = 11;
            this.label_wifiState.Text = "Wifi \r\nConnection";
            this.label_wifiState.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label_wifiState.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label_wifiAdmin
            // 
            this.label_wifiAdmin.AutoSize = true;
            this.label_wifiAdmin.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_wifiAdmin.Location = new System.Drawing.Point(276, 12);
            this.label_wifiAdmin.Name = "label_wifiAdmin";
            this.label_wifiAdmin.Size = new System.Drawing.Size(180, 24);
            this.label_wifiAdmin.TabIndex = 12;
            this.label_wifiAdmin.Text = "Wifi Admin Panel";
            // 
            // WifiScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(784, 498);
            this.Controls.Add(this.label_WifiToolDesc);
            this.Controls.Add(this.panel_AvailableWifi);
            this.MaximizeBox = false;
            this.Name = "WifiScanner";
            this.Text = "Wifi Scanner";
            this.Load += new System.EventHandler(this.WifiScanner_Load);
            this.panel_AvailableWifi.ResumeLayout(false);
            this.panel_AvailableWifi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SavedWifi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AvailableWifi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_AvailableWifi;
        private System.Windows.Forms.ListBox listBox_AvailableWifi;
        private System.Windows.Forms.Button button_WifiDetails;
        private System.Windows.Forms.ListView listView_wifiPasswords;
        private System.Windows.Forms.Button button_Back;
        private System.Windows.Forms.Label label_AvailableWifi;
        private System.Windows.Forms.Label label_SavedWifi;
        private System.Windows.Forms.Label label_WifiToolDesc;
        private System.Windows.Forms.PictureBox pictureBox_SavedWifi;
        private System.Windows.Forms.PictureBox pictureBox_AvailableWifi;
        private System.Windows.Forms.ListView lstLocal;
        private System.Windows.Forms.Button button_AdminConsole;
        private System.Windows.Forms.Button button_wifiState;
        private System.Windows.Forms.Label label_wifiState;
        private System.Windows.Forms.Label label_wifiAdmin;
    }
}

