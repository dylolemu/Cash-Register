//Created by Dylon Lemus
//February 23, 2016
/*Description:Cash register for a taco store. 
Allows you to input quantities of tacos, fries 
and drinks.Then once tendered value is inputed, 
change is calculated and a receipt can be printed out.
A button can then be clicked to make a new order.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace Cash_Register
{
    public partial class cashRegister : Form
    {
        int fries = 0;
        int tacos;
        int drinks;
        const double HST = 0.13;
        double tendered;
        double firsttotal; 
        double total;
        double tax;
        double change;

        Boolean canPrint = false;

        public cashRegister()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (friesBox.Text == "")//if no quanity is inputed it will mark it as 0
                {
                    fries = 0;
                }
                else
                {
                   fries = Convert.ToInt16(friesBox.Text);  
                }

                if (fries < 0)
                {
                    fries = 0;
                }

                if (tacosBox.Text == "")
                {
                    tacos = 0;
                }
                else
                {
                    tacos = Convert.ToInt16(tacosBox.Text);
                }

                if (tacos < 0)
                {
                    tacos = 0;
                }

                if (drinkBox.Text == "" )
                {
                    drinks = 0;
                }
                else
                {
                    drinks = Convert.ToInt16(drinkBox.Text);
                }
                if (drinks < 0)
                {
                    drinks = 0;
                }



                //Calculating sub total, tax, and total and placing it in specified labels
                firsttotal = (tacos * 3.25 + fries * 1.75 + drinks * 1.25);
                total = (((firsttotal) * HST) + firsttotal);
                tax = ((firsttotal) * HST);

                //rounding to two decimal places
                tax = Math.Round(tax, 2);
                total = Math.Round(total, 2);
                labelSub.Text = "" + firsttotal.ToString("0.00");
                labelTax.Text = "" + tax.ToString("0.00");
                labelTotal.Text = "" + total.ToString("0.00");

            }
            catch
            {
                //if an integer is not placed then it marks an error
                labelSub.Text = "You must enter \n an Integer";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {  
            try
            {
                tendered = Convert.ToDouble(tenderedBox.Text);
                change = (tendered - total);
                change = Math.Round(change, 2);

                //checking to ensure cust. gave enough money
                if (tendered < total)
                {
                    labelChange.Text = "Insufficient \n Funds";
                }
                else
                {
                    canPrint = true;//you will be able to print receipt
                    labelChange.Text = "" + change.ToString("0.00");
                }
            }
            catch
            {
                canPrint = false;//you won't be able to print receipt
                labelChange.Text = "You must enter \n an Integer";
            }

        }

        private void Receipt_Click(object sender, EventArgs e)
        {
            //making sure the customer gave enough money and an integer was given
            if (change < 0 || canPrint == false)
            {

            }
            else
            { 
                // printing receipt sound is played
                SoundPlayer player = new SoundPlayer(Properties.Resources.receipt);
                player.Play();
                Thread.Sleep(4500);//waits until sound is over

                Random randNum = new Random();
                labelRandom.Text = Convert.ToString(randNum.Next(1000, 2001));


                labelTacos.Text = "x " + tacos + " @ 3.25";
                labelFries.Text = "x " + fries + " @ 1.75";
                labelDrinks.Text = "x " + drinks + " @ 1.25";
                sub.Text = "$" + firsttotal.ToString("0.00");
                taxLabel.Text = "$" + tax.ToString("0.00");
                totalLabel.Text = "$" + total.ToString("0.00");
                labelTendered.Text = "$" + tendered.ToString("0.00");
                changeLabel.Text = "$" + change.ToString("0.00");

                Graphics formGraphics = this.CreateGraphics();
                Font drawFont = new Font("NSimSun", 11);
                SolidBrush drawBrush = new SolidBrush(Color.Black);

                formGraphics.DrawString("Taco Town Inc.", drawFont, drawBrush, 360, 70);
                formGraphics.DrawString("Order Number", drawFont, drawBrush, 306, 91);
                formGraphics.DrawString("Tacos", drawFont, drawBrush, 306, 140);
                formGraphics.DrawString("Fries", drawFont, drawBrush, 306, 165);
                formGraphics.DrawString("Drinks", drawFont, drawBrush, 306, 190);
                formGraphics.DrawString("Subtotal", drawFont, drawBrush, 306, 237);
                formGraphics.DrawString("Tax", drawFont, drawBrush, 306, 261);
                formGraphics.DrawString("Total", drawFont, drawBrush, 306, 287);
                formGraphics.DrawString("Tendered", drawFont, drawBrush, 306, 337);
                formGraphics.DrawString("Change", drawFont, drawBrush, 306, 359);
                formGraphics.DrawString("Have a Great Day!", drawFont, drawBrush, 306, 390);
            }
        }

        private void v_Click(object sender, EventArgs e)
        {
            //clears everything so customer can make new order
            tacosBox.Clear();
            friesBox.Clear();
            drinkBox.Clear();
            tenderedBox.Clear();
            labelSub.Text = "";
            labelTax.Text = "";
            labelTotal.Text = "";
            labelChange.Text = "";
            labelTacos.Text = "";
            labelFries.Text = "";
            labelDrinks.Text = "";
            sub.Text = "";
            taxLabel.Text = "";
            totalLabel.Text = "";
            labelTendered.Text = "";
            changeLabel.Text = "";
            labelRandom.Text = "";
            //clears the DrawString
            Refresh();
        }

    }
}

