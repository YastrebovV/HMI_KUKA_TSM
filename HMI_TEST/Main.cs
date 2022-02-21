using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HMI_KUKA_TSM;
using KUKARoboter.PlugIn;
using KUKARoboter.Loader;
using KUKARoboter.KRCModel.CrossKrc;
using KUKARoboter.KRCModel;

namespace HMI_TEST
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        Label[] labelSteps;
        Panel[] panelSteps;
        string[] labelVarStep;
        int numElementStep = 0;

        bool pos_table_0 = true;
        bool pos_table_180 = false;

        private void drawStepsState(int step, int numTable)
        {
            try
            {
                IniClass ini = new IniClass();
                ini.IniFile(Application.StartupPath + "\\HMI_KUKA_TSM.ini");

                if (numTable == 2)
                {
                    numElementStep = Convert.ToInt16(ini.IniReadValue("Step" + Convert.ToString(step) + "_E2", "NumElementStep"));
                }
                else if (numTable == 3)
                {
                    numElementStep = Convert.ToInt16(ini.IniReadValue("Step" + Convert.ToString(step) + "_E3", "NumElementStep"));
                }

                int labelStartPosX = Convert.ToInt16(ini.IniReadValue("InfoSteps", "labelStartPosX"));
                int labelStartPosY = Convert.ToInt16(ini.IniReadValue("InfoSteps", "labelStartPosY"));
                labelSteps = new Label[numElementStep];
                panelSteps = new Panel[numElementStep];
                labelVarStep = new string[numElementStep];
                int stepPos = Convert.ToInt16(ini.IniReadValue("InfoSteps", "stepPos"));

                Label lbStep = new Label();
                lbStep.AutoSize = true;
                lbStep.Font = new Font("Century Gothic", 16, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                lbStep.Location = new Point(labelStartPosX, labelStartPosY);
                lbStep.Size = new Size(90, 20);
                lbStep.Text = "Step:";
                if (numTable == 2)
                {
                  //  tabPage1.Controls.Add(lbStep);
                }
                else if (numTable == 3)
                {
                   // tabPage2.Controls.Add(lbStep);
                }

                Label lbNameStep = new Label();
                lbNameStep.AutoSize = true;
                lbNameStep.Font = new Font("Century Gothic", 16, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                lbNameStep.Location = new Point(labelStartPosX + lbStep.Width + 10, labelStartPosY);
                lbNameStep.Size = new Size(90, 20);

                if (numTable == 2)
                {
                    lbNameStep.Text = ini.IniReadValue("Step" + Convert.ToString(step) + "_E2", "NameStep");
                   // tabPage1.Controls.Add(lbNameStep);
                }
                else if (numTable == 3)
                {
                    lbNameStep.Text = ini.IniReadValue("Step" + Convert.ToString(step) + "_E3", "NameStep");
                   // tabPage2.Controls.Add(lbNameStep);
                }
                labelStartPosY += 50;

                for (int i = 0; i < numElementStep; i++)
                {
                    string labelText = "";
                    if (numTable == 2)
                    {
                        labelText = ini.IniReadValue("Step" + Convert.ToString(step) + "_E2", "text" + Convert.ToString(i + 1));
                        labelVarStep[i] = ini.IniReadValue("Step" + Convert.ToString(step) + "_E2", "variable" + Convert.ToString(i + 1));
                    }
                    if (numTable == 3)
                    {
                        labelText = ini.IniReadValue("Step" + Convert.ToString(step) + "_E3", "text" + Convert.ToString(i + 1));
                        labelVarStep[i] = ini.IniReadValue("Step" + Convert.ToString(step) + "_E3", "variable" + Convert.ToString(i + 1));
                    }

                    labelSteps[i] = new Label();
                    labelSteps[i].AutoSize = true;
                    labelSteps[i].Font = new Font("Century Gothic", 10, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                    labelSteps[i].Location = new Point(labelStartPosX, labelStartPosY);
                    labelSteps[i].Size = new Size(90, 20);
                    labelSteps[i].Text = labelText;
                    if (numTable == 2)
                    {
                     //   tabPage1.Controls.Add(labelSteps[i]);
                    }
                    else if (numTable == 3)
                    {
                    //    tabPage2.Controls.Add(labelSteps[i]);
                    }

                    panelSteps[i] = new Panel();
                    panelSteps[i].BackColor = Color.Red;
                    panelSteps[i].BorderStyle = BorderStyle.FixedSingle;
                    panelSteps[i].Location = new Point(labelStartPosX + 100, labelStartPosY);                  
                    panelSteps[i].Size = new Size(15, 15);
                    panelSteps[i].TabIndex = 54;
                    if (numTable == 2)
                    {
                     //   tabPage1.Controls.Add(panelSteps[i]);
                    }
                    else if (numTable == 3)
                    {
                    //    tabPage2.Controls.Add(panelSteps[i]);
                    }

                    labelStartPosY += stepPos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            IniClass ini = new IniClass();
            ini.IniFile(Application.StartupPath + "\\HMI_KUKA_TSM.ini");

            string str = ini.IniReadValue("[Step" + Convert.ToString(1) + "_E2]", "NameStep");


            MessageBox.Show(str);
            // drawStepsState(1, 2);
            try {

                if (pos_table_0)
                {
                    pos_table_180 = true;
                    pos_table_0 = false;
                }
                else if (pos_table_180)
                {
                    pos_table_180 = false;
                    pos_table_0 = true;
                }


            //E1_0_180.Width = 318;
            //E1_0_180.Height = 239;
            //E1_0_180.Left = 93;
            //E1_0_180.Top = 176;

            //E1_0_180.BackgroundImageLayout = ImageLayout.Stretch;

            
            if (pos_table_0)
            {
                    E1_0_180.Image = Properties.Resources.E10;
                    E1_0_180.Refresh();

            }
            if (pos_table_180)
            {
                    E1_0_180.Image = Properties.Resources.E1180;
                    E1_0_180.Refresh();
            }          
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void Main_Load(object sender, EventArgs e)
        {
            
        }
    }
}
