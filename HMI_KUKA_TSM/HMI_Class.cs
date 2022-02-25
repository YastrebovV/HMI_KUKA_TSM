using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using KUKARoboter.PlugIn;
using KUKARoboter.Loader;
using KUKARoboter.KRCModel.CrossKrc;
using KUKARoboter.KRCModel;

//using KUKARoboter.KRCModel.Commands;
//using KUKARoboter.KRCModel.Robot;
//using KUKARoboter.KRCModel.Robot.Interpreter;
//using KUKARoboter.KRCModel.Robot.Variables;

namespace HMI_KUKA_TSM
{
    [PlugInPlacement(DispPlacement.Right, CommandBarMode = CmdBarMode.AssertAll, DisplayMode = DispMode.FullSize,
        DisplayStyle = DisplayStyleFlags.Normal), PlugInQuality(PlugInRole.Display, CanUnLoad = true,
        LoadingType = PlugInLoadingType.OnDemand)]
    public class HMI_Class: PagedPlugInBase
    {
        int Step_E2_Old = 0;
        int Step_E3_Old = 0;

        private Timer Update_Var;
        private Timer Step_Timer;
        private Panel Main_panel;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Label label6;
        private Panel State_In_Home;
        private Label label1;
        private Panel State_Light;
        private Panel State_Emg_But_Door;
        private Panel State_Emg_But_PO;
        private Panel State_Part3_E2;
        private Panel State_Part2_E2;
        private Panel State_Part1_E2;
        private Panel State_Part1_E3;
        private Label label13;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label12;
        private TabPage stepsE2;
        private TabPage stepsE3;
        private Label label16;
        private Label label25;
        private Label label24;
        private Panel State_Part2_E3;
        private Label label26;
        private PictureBox pictureBox1;
        private PictureBox E1_0_180;
        private Panel App_run;
        private Label label2;
        private IContainer components;
        Label[] labelSteps;
        Panel[] panelSteps;
        string[] labelVarStep;
        private Button StartBut;
        private Button But_Reset_Step_E3;
        private Button But_Reset_Step_E2;
        int numElementStep = 0;

        public HMI_Class(DispMode displayMode, CmdBarMode commandBarMode) : base(displayMode, commandBarMode)
		{
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Update_Var = new System.Windows.Forms.Timer(this.components);
            this.Step_Timer = new System.Windows.Forms.Timer(this.components);
            this.Main_panel = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.But_Reset_Step_E3 = new System.Windows.Forms.Button();
            this.But_Reset_Step_E2 = new System.Windows.Forms.Button();
            this.StartBut = new System.Windows.Forms.Button();
            this.App_run = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.State_Light = new System.Windows.Forms.Panel();
            this.State_Emg_But_Door = new System.Windows.Forms.Panel();
            this.State_Emg_But_PO = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.State_In_Home = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.State_Part2_E3 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.State_Part1_E3 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.State_Part3_E2 = new System.Windows.Forms.Panel();
            this.State_Part2_E2 = new System.Windows.Forms.Panel();
            this.State_Part1_E2 = new System.Windows.Forms.Panel();
            this.E1_0_180 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.stepsE2 = new System.Windows.Forms.TabPage();
            this.stepsE3 = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.childPanel.SuspendLayout();
            this.Main_panel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.E1_0_180)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.stepsE3.SuspendLayout();
            this.SuspendLayout();
            // 
            // childPanel
            // 
            this.childPanel.BackColor = System.Drawing.Color.LightGray;
            this.childPanel.Controls.Add(this.Main_panel);
            this.childPanel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.childPanel.ForeColor = System.Drawing.Color.Black;
            this.childPanel.Location = new System.Drawing.Point(0, 0);
            this.childPanel.Size = new System.Drawing.Size(520, 684);
            // 
            // Update_Var
            // 
            this.Update_Var.Interval = 1000;
            this.Update_Var.Tick += new System.EventHandler(this.Update_Var_Tick);
            // 
            // Step_Timer
            // 
            this.Step_Timer.Interval = 1000;
            this.Step_Timer.Tick += new System.EventHandler(this.Step_Timer_Tick);
            // 
            // Main_panel
            // 
            this.Main_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Main_panel.Controls.Add(this.tabControl1);
            this.Main_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Main_panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Main_panel.Location = new System.Drawing.Point(0, 0);
            this.Main_panel.Name = "Main_panel";
            this.Main_panel.Size = new System.Drawing.Size(520, 684);
            this.Main_panel.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.stepsE2);
            this.tabControl1.Controls.Add(this.stepsE3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(6, 10);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(518, 682);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.But_Reset_Step_E3);
            this.tabPage1.Controls.Add(this.But_Reset_Step_E2);
            this.tabPage1.Controls.Add(this.StartBut);
            this.tabPage1.Controls.Add(this.App_run);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.State_Light);
            this.tabPage1.Controls.Add(this.State_Emg_But_Door);
            this.tabPage1.Controls.Add(this.State_Emg_But_PO);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.State_In_Home);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label25);
            this.tabPage1.Controls.Add(this.label24);
            this.tabPage1.Controls.Add(this.label26);
            this.tabPage1.Controls.Add(this.State_Part2_E3);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.State_Part1_E3);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.State_Part3_E2);
            this.tabPage1.Controls.Add(this.State_Part2_E2);
            this.tabPage1.Controls.Add(this.State_Part1_E2);
            this.tabPage1.Controls.Add(this.E1_0_180);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(510, 635);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Основные";
            // 
            // But_Reset_Step_E3
            // 
            this.But_Reset_Step_E3.Location = new System.Drawing.Point(381, 574);
            this.But_Reset_Step_E3.Name = "But_Reset_Step_E3";
            this.But_Reset_Step_E3.Size = new System.Drawing.Size(123, 55);
            this.But_Reset_Step_E3.TabIndex = 70;
            this.But_Reset_Step_E3.Text = "Сброс шагов Е3";
            this.But_Reset_Step_E3.UseVisualStyleBackColor = true;
            this.But_Reset_Step_E3.Click += new System.EventHandler(this.But_Reset_Step_E3_Click);
            // 
            // But_Reset_Step_E2
            // 
            this.But_Reset_Step_E2.Location = new System.Drawing.Point(252, 574);
            this.But_Reset_Step_E2.Name = "But_Reset_Step_E2";
            this.But_Reset_Step_E2.Size = new System.Drawing.Size(123, 55);
            this.But_Reset_Step_E2.TabIndex = 69;
            this.But_Reset_Step_E2.Text = "Сброс шагов Е2";
            this.But_Reset_Step_E2.UseVisualStyleBackColor = true;
            this.But_Reset_Step_E2.Click += new System.EventHandler(this.But_Reset_Step_E2_Click);
            // 
            // StartBut
            // 
            this.StartBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.StartBut.Location = new System.Drawing.Point(6, 485);
            this.StartBut.Name = "StartBut";
            this.StartBut.Size = new System.Drawing.Size(98, 51);
            this.StartBut.TabIndex = 68;
            this.StartBut.Text = "Старт";
            this.StartBut.UseVisualStyleBackColor = false;
            this.StartBut.Click += new System.EventHandler(this.StartBut_Click);
            // 
            // App_run
            // 
            this.App_run.BackColor = System.Drawing.Color.Red;
            this.App_run.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.App_run.Location = new System.Drawing.Point(201, 74);
            this.App_run.Name = "App_run";
            this.App_run.Size = new System.Drawing.Size(15, 15);
            this.App_run.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(54, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 16);
            this.label2.TabIndex = 67;
            this.label2.Text = "Программа запущена:";
            // 
            // State_Light
            // 
            this.State_Light.BackColor = System.Drawing.Color.Red;
            this.State_Light.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.State_Light.Location = new System.Drawing.Point(90, 441);
            this.State_Light.Name = "State_Light";
            this.State_Light.Size = new System.Drawing.Size(326, 6);
            this.State_Light.TabIndex = 50;
            // 
            // State_Emg_But_Door
            // 
            this.State_Emg_But_Door.BackColor = System.Drawing.Color.Red;
            this.State_Emg_But_Door.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.State_Emg_But_Door.Location = new System.Drawing.Point(317, 15);
            this.State_Emg_But_Door.Name = "State_Emg_But_Door";
            this.State_Emg_But_Door.Size = new System.Drawing.Size(15, 15);
            this.State_Emg_But_Door.TabIndex = 49;
            // 
            // State_Emg_But_PO
            // 
            this.State_Emg_But_PO.BackColor = System.Drawing.Color.Red;
            this.State_Emg_But_PO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.State_Emg_But_PO.Location = new System.Drawing.Point(43, 455);
            this.State_Emg_But_PO.Name = "State_Emg_But_PO";
            this.State_Emg_But_PO.Size = new System.Drawing.Size(15, 15);
            this.State_Emg_But_PO.TabIndex = 48;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(186, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(121, 16);
            this.label13.TabIndex = 36;
            this.label13.Text = "Аварийная кнопка:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(66, 454);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(117, 16);
            this.label12.TabIndex = 35;
            this.label12.Text = "Аварийная кнопка";
            // 
            // State_In_Home
            // 
            this.State_In_Home.BackColor = System.Drawing.Color.Red;
            this.State_In_Home.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.State_In_Home.Location = new System.Drawing.Point(160, 55);
            this.State_In_Home.Name = "State_In_Home";
            this.State_In_Home.Size = new System.Drawing.Size(15, 15);
            this.State_In_Home.TabIndex = 52;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(54, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 51;
            this.label1.Text = "Робот в доме:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.DarkOrange;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(264, 286);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 21);
            this.label6.TabIndex = 54;
            this.label6.Text = ":180°";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label25.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label25.Location = new System.Drawing.Point(407, 362);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(40, 19);
            this.label25.TabIndex = 61;
            this.label25.Text = "180°";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label24.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label24.Location = new System.Drawing.Point(59, 210);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(40, 19);
            this.label24.TabIndex = 60;
            this.label24.Text = "180°";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.White;
            this.label26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label26.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label26.Location = new System.Drawing.Point(156, 240);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(170, 14);
            this.label26.TabIndex = 63;
            this.label26.Text = "Наличие детали 2 стол Е3:";
            // 
            // State_Part2_E3
            // 
            this.State_Part2_E3.BackColor = System.Drawing.Color.Red;
            this.State_Part2_E3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.State_Part2_E3.Location = new System.Drawing.Point(332, 240);
            this.State_Part2_E3.Name = "State_Part2_E3";
            this.State_Part2_E3.Size = new System.Drawing.Size(15, 15);
            this.State_Part2_E3.TabIndex = 64;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(156, 186);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(170, 14);
            this.label8.TabIndex = 31;
            this.label8.Text = "Наличие детали 1 стол Е3:";
            // 
            // State_Part1_E3
            // 
            this.State_Part1_E3.BackColor = System.Drawing.Color.Red;
            this.State_Part1_E3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.State_Part1_E3.Location = new System.Drawing.Point(332, 187);
            this.State_Part1_E3.Name = "State_Part1_E3";
            this.State_Part1_E3.Size = new System.Drawing.Size(15, 15);
            this.State_Part1_E3.TabIndex = 44;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(156, 336);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(170, 14);
            this.label11.TabIndex = 34;
            this.label11.Text = "Наличие детали 3 стол Е2:";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(156, 364);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(170, 14);
            this.label10.TabIndex = 33;
            this.label10.Text = "Наличие детали 2 стол Е2:";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(157, 391);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(159, 14);
            this.label9.TabIndex = 32;
            this.label9.Text = "Наличие детали 1 стол Е2:";
            // 
            // State_Part3_E2
            // 
            this.State_Part3_E2.BackColor = System.Drawing.Color.Red;
            this.State_Part3_E2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.State_Part3_E2.Location = new System.Drawing.Point(332, 337);
            this.State_Part3_E2.Name = "State_Part3_E2";
            this.State_Part3_E2.Size = new System.Drawing.Size(15, 15);
            this.State_Part3_E2.TabIndex = 47;
            // 
            // State_Part2_E2
            // 
            this.State_Part2_E2.BackColor = System.Drawing.Color.Red;
            this.State_Part2_E2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.State_Part2_E2.Location = new System.Drawing.Point(332, 365);
            this.State_Part2_E2.Name = "State_Part2_E2";
            this.State_Part2_E2.Size = new System.Drawing.Size(15, 15);
            this.State_Part2_E2.TabIndex = 46;
            // 
            // State_Part1_E2
            // 
            this.State_Part1_E2.BackColor = System.Drawing.Color.Red;
            this.State_Part1_E2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.State_Part1_E2.Location = new System.Drawing.Point(332, 391);
            this.State_Part1_E2.Name = "State_Part1_E2";
            this.State_Part1_E2.Size = new System.Drawing.Size(15, 15);
            this.State_Part1_E2.TabIndex = 45;
            // 
            // E1_0_180
            // 
            this.E1_0_180.BackgroundImage = global::HMI_KUKA_TSM.Properties.Resources.E1180;
            this.E1_0_180.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.E1_0_180.ErrorImage = null;
            this.E1_0_180.Location = new System.Drawing.Point(93, 176);
            this.E1_0_180.Name = "E1_0_180";
            this.E1_0_180.Size = new System.Drawing.Size(318, 239);
            this.E1_0_180.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.E1_0_180.TabIndex = 66;
            this.E1_0_180.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::HMI_KUKA_TSM.Properties.Resources.Positioner1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(498, 473);
            this.pictureBox1.TabIndex = 65;
            this.pictureBox1.TabStop = false;
            // 
            // stepsE2
            // 
            this.stepsE2.BackColor = System.Drawing.Color.LightGray;
            this.stepsE2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepsE2.Location = new System.Drawing.Point(4, 4);
            this.stepsE2.Name = "stepsE2";
            this.stepsE2.Padding = new System.Windows.Forms.Padding(3);
            this.stepsE2.Size = new System.Drawing.Size(510, 635);
            this.stepsE2.TabIndex = 1;
            this.stepsE2.Text = "Шаги Е2";
            // 
            // stepsE3
            // 
            this.stepsE3.BackColor = System.Drawing.Color.LightGray;
            this.stepsE3.Controls.Add(this.label16);
            this.stepsE3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepsE3.Location = new System.Drawing.Point(4, 4);
            this.stepsE3.Name = "stepsE3";
            this.stepsE3.Size = new System.Drawing.Size(510, 635);
            this.stepsE3.TabIndex = 2;
            this.stepsE3.Text = "Шаги Е3";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 42);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(0, 16);
            this.label16.TabIndex = 46;
            // 
            // HMI_Class
            // 
            this.Name = "HMI_Class";
            this.Size = new System.Drawing.Size(520, 684);
            this.Load += new System.EventHandler(this.HMI_Class_Load);
            this.childPanel.ResumeLayout(false);
            this.Main_panel.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.E1_0_180)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.stepsE3.ResumeLayout(false);
            this.stepsE3.PerformLayout();
            this.ResumeLayout(false);

        }

        private void drawStepsState(int step, int numTable)
        {
            try
            {
                IniClass ini = new IniClass();
                ini.IniFile(Application.StartupPath + "\\HMI_KUKA_TSM.ini");

                if (numTable == 2)
                {
                    numElementStep = Convert.ToInt16(ini.IniReadValue("[Step" + Convert.ToString(step) + "_E2]", "NumElementStep"));
                }
                else if (numTable == 3)
                {
                    numElementStep = Convert.ToInt16(ini.IniReadValue("[Step" + Convert.ToString(step) + "_E3]", "NumElementStep"));
                }

                Panel panel = new Panel();
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.Location = new Point(3, 60);
                panel.Size = new Size(504, 540);
                panel.TabIndex = 49;
                if (numTable == 2)
                {
                    stepsE2.Controls.Add(panel);
                }
                else if (numTable == 3)
                {
                    stepsE3.Controls.Add(panel);
                }

                int labelStartPosX = Convert.ToInt16(ini.IniReadValue("[InfoSteps]", "labelStartPosX"));
                int labelStartPosY = Convert.ToInt16(ini.IniReadValue("[InfoSteps]", "labelStartPosY"));
                labelSteps = new Label[numElementStep];
                panelSteps = new Panel[numElementStep];
                labelVarStep = new string[numElementStep];
                int stepPos = Convert.ToInt16(ini.IniReadValue("[InfoSteps]", "stepPos"));

                Label lbTab = new Label();
                lbTab.AutoSize = true;
                lbTab.Font = new Font("Arial", 20, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                lbTab.Location = new Point(10, 17);
                lbTab.Size = new Size(90, 20);
                lbTab.Text = "Состояние шагов:";
                if (numTable == 2)
                {
                    stepsE2.Controls.Add(lbTab);
                }
                else if (numTable == 3)
                {
                    stepsE3.Controls.Add(lbTab);
                }

                Label lbStep = new Label();
                lbStep.AutoSize = true;
                lbStep.Font = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                lbStep.Location = new Point(labelStartPosX, labelStartPosY);
                lbStep.Size = new Size(90, 20);
                lbStep.Text = "Шаг:";
                if (numTable == 2)
                {
                    panel.Controls.Add(lbStep);
                }
                else if (numTable == 3)
                {
                    panel.Controls.Add(lbStep);
                }

                Label lbNameStep = new Label();
                lbNameStep.AutoSize = true;
                lbNameStep.Font = new Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                lbNameStep.Location = new Point(labelStartPosX + lbStep.Width + 10, labelStartPosY);
                lbNameStep.Size = new Size(90, 20);

                if (numTable == 2)
                {
                    lbNameStep.Text = ini.IniReadValue("[Step" + Convert.ToString(step) + "_E2]", "NameStep");
                    panel.Controls.Add(lbNameStep);
                }
                else if (numTable == 3)
                {
                    lbNameStep.Text = ini.IniReadValue("[Step" + Convert.ToString(step) + "_E3]", "NameStep");
                    panel.Controls.Add(lbNameStep);
                }
                labelStartPosY += 50;

                for (int i = 0; i < numElementStep; i++)
                {
                    string labelText = "";
                    if (numTable == 2)
                    {
                        labelText = ini.IniReadValue("[Step" + Convert.ToString(step) + "_E2]", "text" + Convert.ToString(i + 1));
                        labelVarStep[i] = ini.IniReadValue("[Step" + Convert.ToString(step) + "_E2]", "variable" + Convert.ToString(i + 1));
                    }
                    if (numTable == 3)
                    {
                        labelText = ini.IniReadValue("[Step" + Convert.ToString(step) + "_E3]", "text" + Convert.ToString(i + 1));
                        labelVarStep[i] = ini.IniReadValue("[Step" + Convert.ToString(step) + "_E3]", "variable" + Convert.ToString(i + 1));
                    }

                    labelSteps[i] = new Label();
                    labelSteps[i].AutoSize = true;
                    labelSteps[i].Font = new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                    labelSteps[i].Location = new Point(labelStartPosX, labelStartPosY);
                    labelSteps[i].Size = new Size(90, 20);
                    labelSteps[i].Text = labelText;
                    if (numTable == 2)
                    {
                        panel.Controls.Add(labelSteps[i]);
                    }
                    else if (numTable == 3)
                    {
                        panel.Controls.Add(labelSteps[i]);
                    }

                    panelSteps[i] = new Panel();
                    panelSteps[i].BackColor = Color.Red;
                    panelSteps[i].BorderStyle = BorderStyle.FixedSingle;
                    panelSteps[i].Location = new Point(labelStartPosX + 250, labelStartPosY);
                    panelSteps[i].Size = new Size(15, 15);
                    panelSteps[i].TabIndex = 54;
                    if (numTable == 2)
                    {
                        panel.Controls.Add(panelSteps[i]);
                    }
                    else if (numTable == 3)
                    {
                        panel.Controls.Add(panelSteps[i]);
                    }

                    labelStartPosY += stepPos;
                }
                
            }
            catch (Exception ex)
            {
                //richTextBox1.Text += ex.Message + "\n";
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.OnDisconnection();
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        //timers
        private void Update_Var_Tick(object sender, EventArgs e)
        {
            Update_Var.Enabled = false;
            try
            {
                if (Convert.ToBoolean(KrcVariableCommands.ShowVar("RESET_STEPS_E2"))) { KrcVariableCommands.SetVar("RESET_STEPS_E2", "false"); }
                if (Convert.ToBoolean(KrcVariableCommands.ShowVar("RESET_STEPS_E3"))) { KrcVariableCommands.SetVar("RESET_STEPS_E3", "false"); }
                if (Convert.ToBoolean(KrcVariableCommands.ShowVar("START_BUT"))) { KrcVariableCommands.SetVar("START_BUT", "false"); }

                bool inhome = Convert.ToBoolean(KrcVariableCommands.ShowVar("$IN_HOME"));
                if (inhome){ State_In_Home.BackColor = Color.Green;}else State_In_Home.BackColor = Color.Red;

                bool pos_table_0 = Convert.ToBoolean(KrcVariableCommands.ShowVar("$AXWORKSTATE1"));
                bool pos_table_180 = Convert.ToBoolean(KrcVariableCommands.ShowVar("$AXWORKSTATE2"));

                bool app_run = Convert.ToBoolean(KrcVariableCommands.ShowVar("$APPL_RUN"));
                if (app_run) { App_run.BackColor = Color.Green; } else App_run.BackColor = Color.Red;

                label24.Text = Convert.ToString(Math.Truncate(Convert.ToDouble(KrcVariableCommands.ShowVar("$AXIS_ACT.E2")))) + "°";
                label25.Text = Convert.ToString(Math.Truncate(Convert.ToDouble(KrcVariableCommands.ShowVar("$AXIS_ACT.E3")))) + "°";

                if (pos_table_0) { label6.Text = ":0°"; }
                if (pos_table_180) { label6.Text = ":180°"; }

                if (pos_table_0)
                {
                    E1_0_180.Image = Properties.Resources.E10;
                    E1_0_180.Refresh();

                    if (Convert.ToBoolean(KrcVariableCommands.ShowVar("$OUT[503]")) == true)
                    {
                        label9.Left = 156; label9.Top = 186; State_Part1_E2.Left = 332; State_Part1_E2.Top = 187;
                        label10.Left = 156; label10.Top = 213; State_Part2_E2.Left = 332; State_Part2_E2.Top = 214;
                        label11.Left = 156; label11.Top = 240; State_Part3_E2.Left = 332; State_Part3_E2.Top = 241;
                    }
                    else if (Convert.ToBoolean(KrcVariableCommands.ShowVar("$OUT[503]")) == false)
                    {
                        label11.Left = 156; label11.Top = 186; State_Part3_E2.Left = 332; State_Part3_E2.Top = 187;
                        label10.Left = 156; label10.Top = 213; State_Part2_E2.Left = 332; State_Part2_E2.Top = 214;
                        label9.Left = 156; label9.Top = 240; State_Part1_E2.Left = 332; State_Part1_E2.Top = 241;
                    }

                    if (Convert.ToBoolean(KrcVariableCommands.ShowVar("$OUT[504]")) == true)
                    {
                        label26.Left = 156; label26.Top = 337; State_Part2_E3.Left = 332; State_Part2_E3.Top = 338;                       
                        label8.Left = 156; label8.Top = 390; State_Part1_E3.Left = 332; State_Part1_E3.Top = 391;
                    }
                    else if (Convert.ToBoolean(KrcVariableCommands.ShowVar("$OUT[504]")) == false)
                    {
                        label8.Left = 156; label8.Top = 337; State_Part1_E3.Left = 332; State_Part1_E3.Top = 338;
                        label26.Left = 156; label26.Top = 390; State_Part2_E3.Left = 332; State_Part2_E3.Top = 391;
                    }

                }
                if (pos_table_180)
                {
                    E1_0_180.Image = Properties.Resources.E1180;
                    E1_0_180.Refresh();

                    if (Convert.ToBoolean(KrcVariableCommands.ShowVar("$OUT[504]")) == true)
                    {
                        label11.Left = 156; label11.Top = 337; State_Part3_E2.Left = 332; State_Part3_E2.Top = 338;
                        label10.Left = 156; label10.Top = 364; State_Part2_E2.Left = 332; State_Part2_E2.Top = 365;
                        label9.Left = 156; label9.Top = 390; State_Part1_E2.Left = 332; State_Part1_E2.Top = 391;
                    }
                    else if (Convert.ToBoolean(KrcVariableCommands.ShowVar("$OUT[504]")) == false)
                    {
                        label9.Left = 156; label9.Top = 337; State_Part1_E2.Left = 332; State_Part1_E2.Top = 338;
                        label10.Left = 156; label10.Top = 364; State_Part2_E2.Left = 332; State_Part2_E2.Top = 365;
                        label11.Left = 156; label11.Top = 390; State_Part3_E2.Left = 332; State_Part3_E2.Top = 391;
                    }

                    if (Convert.ToBoolean(KrcVariableCommands.ShowVar("$OUT[503]")) == true)
                    {
                        label26.Left = 156; label26.Top = 186; State_Part1_E3.Left = 332; State_Part1_E3.Top = 187;
                        label8.Left = 156; label8.Top = 240; State_Part2_E3.Left = 332; State_Part2_E3.Top = 240;
                    }
                    else if (Convert.ToBoolean(KrcVariableCommands.ShowVar("$OUT[503]")) == false)
                    {
                        label8.Left = 156; label8.Top = 186; State_Part2_E3.Left = 332; State_Part2_E3.Top = 187;
                        label26.Left = 156; label26.Top = 240; State_Part1_E3.Left = 332; State_Part1_E3.Top = 240;
                    }

                }

                bool state_part1_E3 = Convert.ToBoolean(KrcVariableCommands.ShowVar("STATE_PART1_E3"));
                if (state_part1_E3) { State_Part1_E3.BackColor = Color.Green; }else State_Part1_E3.BackColor = Color.Red;

                bool state_part2_E3 = Convert.ToBoolean(KrcVariableCommands.ShowVar("STATE_PART2_E3"));
                if (state_part2_E3) { State_Part2_E3.BackColor = Color.Green; } else State_Part2_E3.BackColor = Color.Red;

                bool state_part1_E2 = Convert.ToBoolean(KrcVariableCommands.ShowVar("STATE_PART1_E2"));
                if (state_part1_E2) { State_Part1_E2.BackColor = Color.Green; } else State_Part1_E2.BackColor = Color.Red;

                bool state_part2_E2 = Convert.ToBoolean(KrcVariableCommands.ShowVar("STATE_PART2_E2"));
                if (state_part2_E2) { State_Part2_E2.BackColor = Color.Green; } else State_Part2_E2.BackColor = Color.Red;

                bool state_part3_E2 = Convert.ToBoolean(KrcVariableCommands.ShowVar("STATE_PART3_E2"));
                if (state_part3_E2) { State_Part3_E2.BackColor = Color.Green; } else State_Part3_E2.BackColor = Color.Red;

                bool state_emg_but_PO = Convert.ToBoolean(KrcVariableCommands.ShowVar("STATE_EMG_BUT_PO"));
                if (state_emg_but_PO) { State_Emg_But_PO.BackColor = Color.Green; } else State_Emg_But_PO.BackColor = Color.Red;

                bool state_emg_but_Door = Convert.ToBoolean(KrcVariableCommands.ShowVar("STATE_EMG_BUT_DOOR"));
                if (state_emg_but_Door) {State_Emg_But_Door.BackColor = Color.Green; } else State_Emg_But_Door.BackColor = Color.Red;

                bool stateLight = Convert.ToBoolean(KrcVariableCommands.ShowVar("STATE_LIGHT"));
                if (stateLight) { State_Light.BackColor = Color.Green; } else State_Light.BackColor = Color.Red;             
            }
            catch (Exception ex)
            {
                //richTextBox1.Text += ex.Message + "\n";
            }
            finally
            {
                Update_Var.Enabled = true;
            }          
        }
        private void Step_Timer_Tick(object sender, EventArgs e)
        {
            Step_Timer.Enabled = false;
            try
            {
                int steps_E2 = Convert.ToInt32(KrcVariableCommands.ShowVar("STEPS_E2"));
                int steps_E3 = Convert.ToInt32(KrcVariableCommands.ShowVar("STEPS_E3"));

                if (tabControl1.SelectedIndex == 1 && steps_E2 != Step_E2_Old)
                {
                    Step_E2_Old = steps_E2;
                    stepsE2.Controls.Clear();
                    drawStepsState(Convert.ToInt32(KrcVariableCommands.ShowVar("STEPS_E2")), 2);
                }
                if (tabControl1.SelectedIndex == 2 && steps_E3 != Step_E3_Old)
                {
                    Step_E3_Old = steps_E3;
                    stepsE3.Controls.Clear();
                    drawStepsState(Convert.ToInt32(KrcVariableCommands.ShowVar("STEPS_E3")), 3);
                }

                for (int i = 0; i < numElementStep; i++)
                {
                    if (Convert.ToBoolean(KrcVariableCommands.ShowVar(labelVarStep[i])))
                    {
                        panelSteps[i].BackColor = Color.Green;
                    }else
                    {
                        panelSteps[i].BackColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Step_Timer.Enabled = true;
            }      
        }
        //timers_end

        private void HMI_Class_Load(object sender, EventArgs e)
        {           
            Update_Var.Enabled = true;
            drawStepsState(Convert.ToInt32(KrcVariableCommands.ShowVar("STEPS_E2")), 2);
            drawStepsState(Convert.ToInt32(KrcVariableCommands.ShowVar("STEPS_E3")), 3);
            Step_Timer.Enabled = true;
        }//Загрузка формы
        private void But_Reset_Step_E2_Click(object sender, EventArgs e)//Сброс шагов стола Е2
        {
            if (Convert.ToBoolean(KrcVariableCommands.ShowVar("$IN_HOME")) &&
                Convert.ToBoolean(KrcVariableCommands.ShowVar("$AXWORKSTATE3")) &&
                Convert.ToBoolean(KrcVariableCommands.ShowVar("$AXWORKSTATE4")) &&
                !Convert.ToBoolean(KrcVariableCommands.ShowVar("$APPL_RUN")))
            {
                KrcVariableCommands.SetVar("RESET_STEPS_E2", "true");
            }
        }
        private void But_Reset_Step_E3_Click(object sender, EventArgs e)//Сброс шагов стола Е3
        {
            KrcVariableCommands.SetVar("RESET_STEPS_E3", "true");
        }

        private void StartBut_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(KrcVariableCommands.ShowVar("$IN_HOME")) &&
                Convert.ToBoolean(KrcVariableCommands.ShowVar("$AXWORKSTATE3")) &&
                Convert.ToBoolean(KrcVariableCommands.ShowVar("$AXWORKSTATE4")) &&
                !Convert.ToBoolean(KrcVariableCommands.ShowVar("$APPL_RUN")))
            {
                KrcVariableCommands.SetVar("START_BUT", "true");
            }
        }
    }
}
