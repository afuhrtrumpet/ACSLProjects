using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DraftPicks
{
    public partial class Form1 : Form
    {
        private const int MAX = 10;
        private List<DraftPick> draftPicks;
        public Form1()
        {
            InitializeComponent();
            draftPicks = new List<DraftPick>();
#if DEBUG
            draftPicks.Add(new DraftPick(5, 57.5, 30));
            draftPicks.Add(new DraftPick(6, 56.5, 29));
            draftPicks.Add(new DraftPick(6, 72, 34));
            draftPicks.Add(new DraftPick(6, 60, 26));
            draftPicks.Add(new DraftPick(5, 51, 23));
            draftPicks.Add(new DraftPick(5, 50, 21));
            draftPicks.Add(new DraftPick(5, 49, 19));
            draftPicks.Add(new DraftPick(5, 33.4, 17.177));
            draftPicks.Add(new DraftPick(5, 23, 15.6));
            draftPicks.Add(new DraftPick(5, 18.9, 13.8)); 
            computeResults();
#endif
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (draftPicks.Count < MAX)
            {
                try
                {
                    int length = int.Parse(txtLength.Text);
                    double value = double.Parse(txtValue.Text);
                    double guaranteedMoney = double.Parse(txtGuaranteedMoney.Text);
                    draftPicks.Add(new DraftPick(length, value, guaranteedMoney));
                    lblPlayers.Text = "Players to Go: " + (MAX - draftPicks.Count);
                    if (draftPicks.Count == MAX) computeResults();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please enter input in a valid format.");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Input is too long!");
                } 
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            draftPicks.Clear();
            lblPlayers.Text = "Players to Go: 10";
            lblResults.Text = "";
        }

        private void computeResults()
        {
            double one = DraftPickUtilities.rangeOfSalaries16(draftPicks);
            double two = DraftPickUtilities.midRangeOfSalaries18(draftPicks);
            int three = DraftPickUtilities.highestValuedPlayer16(draftPicks);
            double four = DraftPickUtilities.averageExpectedValue18(draftPicks);
            double five = DraftPickUtilities.medianOfAnnualSalary(draftPicks);
            lblResults.Text = "1. " + one + "\r\n2. " + two + "\r\n3. " + draftPicks[three].expectedValue16() + " by #" + (three + 1) +
                "\r\n4. " + four + "\r\n5. " + five;
        }
    }
}
