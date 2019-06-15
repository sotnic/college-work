using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorApplication
{
    public partial class Form1 : Form
    {
        // declaring two delegates
        delegate double TwoValueCalc(double x, double y);
        delegate double OneValueCalc(double x);
        // declaring variables
        double value = 0;
        string operation = "";
        bool isPressed = false;
        public Form1()
        {
            InitializeComponent();
        }
        // assigning button_click event to the buttons 0 - 9 to take values
        private void button_Click(object sender, EventArgs e)
        {
            if (txtResult.Text == "0" || isPressed == true)
            {
                txtResult.Clear();
            }
            isPressed = false;
            Button btn = (Button)sender;
            txtResult.Text += btn.Text;
            
        }
        // assigning button_click event to the button "C" in order to cleare the text box
        private void button1_Click(object sender, EventArgs e)
        {
            txtResult.Text = "0";
        }
        // assigning button_click event to the button "."
        private void buttonDot_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (txtResult.Text != "" && !txtResult.Text.Contains("."))
            {
                txtResult.Text += ".";
            }
        }
        // assigning button_click event to the addition, subtraction, multiplication, and division operators
        private void operator_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            operation = btn.Text;
            value = Double.Parse(txtResult.Text);
            isPressed = true;
        }
        // assigning button_click event to the backspace button to remove last digit
        private void backspace_Click(object sender, EventArgs e)
        {
            txtResult.Text = txtResult.Text.Remove(txtResult.Text.Length - 1);
            if (txtResult.Text == "")
            {
                txtResult.Text = "0";
            }
        }
        // assigning button_click event to negate button
        private void negateBtn_Click(object sender, EventArgs e)
        {
            if (txtResult.Text != "") 
            {
                double value = Double.Parse(txtResult.Text) * -1;
                txtResult.Text = value.ToString();
            }
            if (txtResult.Text.Contains("0"))
            {
                txtResult.Text = "-";
            }
        }
        // declaring methods for different operators
        // addition
        static double Addition(double x, double y)
        {
            return x + y;
        }
        // subtraction
        static double Subtraction(double x, double y)
        {
            return x - y;
        }
        // multiplication
        static double Multiplication(double x, double y)
        {
            return x * y;
        }
        // division
        static double Division(double x, double y)
        {
            return x / y;
        }
        // square
        static double Square(double x)
        {
            return x * x;
        }
        // fraction
        static double Fraction(double x)
        {
            return 1 / x;
        }
        // assigning button_click event to the square operation
        private void squre_Click(object sender, EventArgs e)
        {
            OneValueCalc Del = new OneValueCalc(Square);
            txtResult.Text = Del(Double.Parse(txtResult.Text)).ToString();
            isPressed = true;
        }
        // assigning button_click event to the fraction operation
        private void fraction_Click(object sender, EventArgs e)
        {
            OneValueCalc Del = new OneValueCalc(Fraction);
            txtResult.Text = Del(Double.Parse(txtResult.Text)).ToString();
            isPressed = true;
        }
        // assigning button_click event to the equal button to calculate results
        private void button14_Click(object sender, EventArgs e)
        {
            TwoValueCalc Del;
            switch (operation)
            {
                case "+":
                    Del = new TwoValueCalc(Addition);
                    txtResult.Text = Del(value, Double.Parse(txtResult.Text)).ToString();
                    break;
                case "-":
                    Del = new TwoValueCalc(Subtraction);
                    txtResult.Text = Del(value, Double.Parse(txtResult.Text)).ToString();
                    break;
                case "*":
                    Del = new TwoValueCalc(Multiplication);
                    txtResult.Text = Del(value, Double.Parse(txtResult.Text)).ToString();
                    break;
                case "/":
                    Del = new TwoValueCalc(Division);
                    txtResult.Text = Del(value, Double.Parse(txtResult.Text)).ToString();
                    break;
                default:
                    break;
            }
            isPressed = true;
        }        
    }
}
