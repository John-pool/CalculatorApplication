using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaIculatorApplication
{
    public delegate double Formula(double num1, double num2);

    public class CalculatorClass
    {
        public event Formula CalculateEvent;

        // Methods for addition, subtraction, multiplication, and division
        public double GetSum(double num1, double num2) => num1 + num2;
        public double GetDifference(double num1, double num2) => num1 - num2;
        public double GetProduct(double num1, double num2) => num1 * num2;
        public double GetQuotient(double num1, double num2) => num2 != 0 ? num1 / num2 : double.NaN; // Handle division by zero

        // Add or remove event handlers
        public void AddEvent(Formula operation)
        {
            CalculateEvent += operation;
            Console.WriteLine("Added the Delegate");
        }

        public void RemoveEvent(Formula operation)
        {
            CalculateEvent -= operation;
            Console.WriteLine("Removed the Delegate");
        }
    }
        public partial class Form1 : Form
    {
        private CalculatorClass cal;
        private double num1, num2;

        public Form1()
        {
            InitializeComponent();
            cal = new CalculatorClass();

            // Add items to ComboBox
            cbOperator.Items.AddRange(new object[] { "+", "-", "*", "/" });
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                num1 = Convert.ToDouble(txtBoxInput1.Text);
                num2 = Convert.ToDouble(txtBoxInput2.Text);
            }
            catch (FormatException)
            {
                lblDisplayTotal.Text = "Invalid Input";
                return;
            }

            // Determine the operation and perform calculation
            string selectedOperator = cbOperator.SelectedItem?.ToString();

            if (selectedOperator == "+")
            {
                cal.AddEvent(cal.GetSum);
                lblDisplayTotal.Text = cal.GetSum(num1, num2).ToString();
                cal.RemoveEvent(cal.GetSum);
            }
            else if (selectedOperator == "-")
            {
                cal.AddEvent(cal.GetDifference);
                lblDisplayTotal.Text = cal.GetDifference(num1, num2).ToString();
                cal.RemoveEvent(cal.GetDifference);
            }
            else if (selectedOperator == "*")
            {
                cal.AddEvent(cal.GetProduct);
                lblDisplayTotal.Text = cal.GetProduct(num1, num2).ToString();
                cal.RemoveEvent(cal.GetProduct);
            }
            else if (selectedOperator == "/")
            {
                cal.AddEvent(cal.GetQuotient);
                double result = cal.GetQuotient(num1, num2);
                lblDisplayTotal.Text = double.IsNaN(result) ? "Cannot divide by zero" : result.ToString();
                cal.RemoveEvent(cal.GetQuotient);
            }
            else
            {
                lblDisplayTotal.Text = "Select an operator";
            }
        }
    }
}
