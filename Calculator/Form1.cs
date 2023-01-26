using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Calculator : Form
    {
        bool IsThereOperator = false;
        bool OperationNeeded = false;
        bool EqualPressed = false;
        string StoreOperand;
        double? FirstNumber;
        double SecondNumber;
        public Calculator()
        {
            InitializeComponent();
        }

        private void lblSeconds_Click(object sender, EventArgs e)
        {
        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }

        private void timerTick_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm");
            lblSeconds.Text = DateTime.Now.ToString("ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timerTick.Start();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPercent_Click(object sender, EventArgs e)
        {
            
            double? Perc;
            double? result;
            if(double.TryParse(lblResult.Text, out double reslt ))
            {
                if (lblResult.Text == null)
                {
                    return;
                }
                else
                {
                    Perc = Convert.ToDouble(lblResult.Text);
                    result = (Perc * FirstNumber) / 100;
                    lblResult.Text = result.ToString();
                }
            }
            else
            {
                return;
            }
        }
        private void btnSqrt_Click(object sender, EventArgs e)
        {
            if (double.TryParse(lblResult.Text, out double reslt))
            {

                if (lblResult.Text == "")
                {
                    return;
                }
                else
                {
                    double result;
                    result = Convert.ToDouble(lblResult.Text);
                    result = Math.Sqrt(result);
                    lblResult.Text = result.ToString();
                }
            }
            else
            {
                return;
            }
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (!lblResult.Text.Contains("."))
            {
                lblResult.Text += ".";
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblResult.Text.Length == 0)
            {
                lblResult.Text += '-';
            }
            else
            {
                if (lblResult.Text[0] == '-')
                {
                    lblResult.Text = lblResult.Text.Substring(1, lblResult.Text.Length - 1);
                }
                else
                {
                    lblResult.Text = '-' + lblResult.Text;
                }
            }

        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            lblResult.Text = "";
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            lblResult.Text = "";
            lblHistory.Text = "";
            FirstNumber = null;
            SecondNumber = 0;
            OperationNeeded = false;
            StoreOperand = "";
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (lblResult.Text == "")
            {
                return;
            }
            else
            {
                lblResult.Text = lblResult.Text.Substring(0, lblResult.Text.Length - 1);
            }
        }

        private void btnSqr_Click(object sender, EventArgs e)
        {
            if (double.TryParse(lblResult.Text, out double reslt))
            {

                if (lblResult.Text == "")
                {
                    return;
                }
                else
                {
                    double result;
                    result = Convert.ToDouble(lblResult.Text);
                    result = Math.Pow(result, 2);
                    lblResult.Text = result.ToString();
                }
            }
            else
            {
                return;
            }
       
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            if (double.TryParse(lblResult.Text, out double reslt))
            {

                if (lblResult.Text == "")
                {
                    return;
                }
                else
                {
                    double result;
                    result = Convert.ToDouble(lblResult.Text);
                    result = 1 / result;
                    lblResult.Text = result.ToString();
                }
            }
            else
            {
                return;
            }
        }

        private void btn_MouseClick(object sender, MouseEventArgs e)
        {
            if (EqualPressed)
            {
                lblResult.Text = ((Button)sender).Text;
                OperationNeeded = false;
                lblHistory.Text = "";
            }
            if (IsThereOperator)
            {
                lblResult.Text = ((Button)sender).Text;
            }
            else
            {
                lblResult.Text += ((Button)sender).Text;
            }
            IsThereOperator = false;
            EqualPressed = false;
        }

        private void operand_MouseClick(object sender, MouseEventArgs e)
        {
            Operation(((Button)sender).Text);
        }

        private void Operation(string Operand)
        {
            double? result;
            IsThereOperator = true;

            if(lblResult.Text == "" && lblHistory.Text == "")
            {
                return;
            }

            lblHistory.Text += " " + lblResult.Text + " " + Operand;

            if (!OperationNeeded)
            {
                StoreOperand = Operand;
                FirstNumber = Convert.ToDouble(lblResult.Text);
                OperationNeeded = true;
            }

            else if(OperationNeeded == true && EqualPressed == false)
            {
                SecondNumber = Convert.ToDouble(lblResult.Text);
                result = calc(StoreOperand);
                lblResult.Text = result.ToString();
                FirstNumber = result;
                StoreOperand = Operand;
            }

            else if(OperationNeeded == true && EqualPressed == true)
            {
                EqualPressed = false;
                StoreOperand = Operand;
            }
        }

        private double? calc(string Operand)
        {
            double? result = 0;
            switch (Operand)
            {
                case "+":
                    result = FirstNumber + SecondNumber;
                    break;
                case "-":
                    result = FirstNumber - SecondNumber;
                    break;
                case "*":
                    result = FirstNumber * SecondNumber;
                    break;
                case "/":
                    result = FirstNumber / SecondNumber;
                    break;
            }
            return result;
        }

        private void btnEql_Click(object sender, EventArgs e)
        {
            double? result;

            if(FirstNumber == null || lblResult.Text.Length == 0)
            {
                return;
            }

            EqualPressed = true;
            IsThereOperator = true;
            SecondNumber = Convert.ToDouble(lblResult.Text);
            result = calc(StoreOperand);
            lblHistory.Text += " " + lblResult.Text + " " + "= " + result.ToString() + "\n";
            lblResult.Text = result.ToString();
            FirstNumber = result;
        }

        private void Calculator_KeyPress(object sender, KeyPressEventArgs e)
        {
            Button button = new Button();

            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                button.Text = e.KeyChar.ToString();
                btn_MouseClick(button, null);
            }
            else if (e.KeyChar == '+' || e.KeyChar == '-' || e.KeyChar == '*' || e.KeyChar == '/')
            {
                Operation(e.KeyChar.ToString());
            }
            else if(e.KeyChar == '\b')
            {
                btnDel_Click(null, null);
            }
            else if(e.KeyChar == '=')
            {
                btnEql_Click(null, null);
            }
            else if(e.KeyChar == '.')
            {
                btnDot_Click(null, null);
            }
            else if(e.KeyChar == '%')
            {
                btnPercent_Click(null, null);
            }

            foreach(Button btn in panelDown.Controls)
            {
                if(btn.Text == e.KeyChar.ToString())
                {
                    btn.Focus();
                }
            }
        }
    }
}
