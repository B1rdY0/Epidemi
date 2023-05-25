using System;
using System.Windows.Forms;


namespace Epidemi;

public class PandeminForm : Form
{
    Label lblN = new Label(); //Skapar detta för gränsnitt 
    Label lblS = new Label();
    Label lblD = new Label();
    Label lblk = new Label();
    TextBox txtN = new TextBox();
    TextBox txtS = new TextBox();
    TextBox txtD = new TextBox();
    TextBox txtk = new TextBox();
    Button btnCalculate = new Button(); //knappen som räknar
    DataGridView grid = new DataGridView();

    public PandeminForm() //Hela denna konstruktorn används för att rita ut tabellen
    {
        this.Name = "PandeminForm";
        this.Text = "Pandemin Graph";
        this.Size = new System.Drawing.Size(500, 500);
        this.StartPosition = FormStartPosition.CenterScreen;

        lblD.Text = "Varaktighet Veckor:";
        lblD.AutoSize = true;
        lblD.Location = new Point(10, 90);

        lblk.Text = "Hur Lätta man blir Sjuk";
        lblk.AutoSize = true;
        lblk.Location = new Point(10, 120);

        lblN.Text = "Populationen:";
        lblN.AutoSize = true;
        lblN.Location = new Point(10, 30);

        lblS.Text = "Smittsamma Första Veckan:";
        lblS.AutoSize = true;
        lblS.Location = new Point(10, 60);

        txtN.Text = "1000";
        txtN.Location = new Point(180, 30);

        txtS.Text = "1";
        txtS.Location = new Point(180, 60);

        txtD.Text = "1";
        txtD.Location = new Point(180, 90);

        txtk.Text = "0.002";
        txtk.Location = new Point(180, 120);

        grid.Size = new Size(400, 300);
        grid.Location = new Point(10, 150);
        grid.Columns.Add("V", "  V  ");
        grid.Columns.Add("M", "  M  ");
        grid.Columns.Add("S", "  S  ");
        grid.Columns.Add("I", "  I  ");


        btnCalculate.Text = "Räkna";
        btnCalculate.AutoSize = true;
        btnCalculate.Location = new Point(300, 75);
        btnCalculate.Click += new EventHandler(btnCalculate_Clicked);

        this.Controls.AddRange(new Control[] { lblD, lblk, lblN, lblS, txtD, txtk, txtN, txtS, grid, btnCalculate });
    }

    private void btnCalculate_Clicked(object? sender, EventArgs e)
    {
        grid.Rows.Clear();
        //Så rutorna som är tomma skrivs nu med värderna som ska stå 
        int N = ReadIntValueFromTextBox(txtN);
        int S = ReadIntValueFromTextBox(txtS);
        float D = ReadFloatValueFromTextBox(txtD);
        float k = ReadFloatValueFromTextBox(txtk);
        if ((N <= 0) || (S < 0) || (D <= 0.1) || (k <= 0.000001)) //Om värden inte stämmer så skickar den ut fel medelande 
            MessageBox.Show("Fel värde. Försöka igen");
        else
            FillTheValues(N, S, D, k);
    }

    //Dem här metoderna funktion är att dem ska läsa ut vad som står för värden i text boxen 
    private static int ReadIntValueFromTextBox(TextBox box)
    {
        try
        {
            string temp = box.Text;
            if (temp.Length > 0)
            {
                int returnValue = 0;
                bool success = Int32.TryParse(temp, out returnValue);
                if (success)
                    return returnValue;
            }
        }
        catch (Exception) { }
        return -1;
    }

    private static float ReadFloatValueFromTextBox(TextBox box)
    {
        try
        {
            string temp = box.Text;
            if (temp.Length > 0)
            {
                float returnValue = 0;
                bool success = float.TryParse(temp, out returnValue);
                if (success && (returnValue > 0.0F))
                    return returnValue;
            }
        }
        catch (Exception) { }
        return 0.0F;
    }

    //Denna funktion används för att skriva ut och fylla värden tills man får att antalet sjuka (S) är 0 eller i det närmsta värde
    private void FillTheValues(int N, int S0, float d, float k)
    {
        float M = N - S0, I = 0, S = S0;
        int weekNumber = 0;
        while (true)
        {
            float mOld = M;
            float sOld = S;
            weekNumber++;

            float temp = (k * sOld * mOld);
            if (temp > mOld)
                temp = mOld;

            M = mOld - temp;
            S = sOld + temp - (sOld / d);
            I = I + (sOld / d);
            grid.Rows.Add(new object[] { weekNumber, M, S, I });
            if (S < 0.001)
                break;
        }
    }
}
