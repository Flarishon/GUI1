namespace GUI1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            txtNumber.Text = Properties.Settings.Default.txtNumber.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int number;
            while (true)
            {
                try
                {
                    number = int.Parse(this.txtNumber.Text);

                    if (number < 1 || number > 9999)
                    {
                        throw new ArgumentOutOfRangeException("", "Число должно быть в диапазоне от 1 до 9999 включительно!\n");
                    }

                    break;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Введено не целое число!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            Properties.Settings.Default.txtNumber = number;
            string answer = Logic.FormWordsByNumber(number);
            MessageBox.Show(answer);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.txtNumber = int.Parse(txtNumber.Text);
            Properties.Settings.Default.Save();
        }
    }
    public class Logic
    {
        public static string FormWordsByNumber(int number)
        {

            int rubles;
            int copeiki;
            string rubles_word = "";
            string copeiki_word = "";
            string result = "";

            rubles = number / 100;
            copeiki = number % 100;

            if (rubles % 10 == 1 && rubles != 11)
            {
                rubles_word = " рубль";
            }
            else if ((rubles % 10 >= 2 && rubles % 10 <= 4) && !(rubles >= 12 && rubles <= 14))
            {
                rubles_word = " рубля";
            }
            else
            {
                rubles_word = " рублей";
            }

            if (copeiki % 10 == 1 && copeiki != 11)
            {
                copeiki_word = " копейка";
            }
            else if ((copeiki % 10 >= 2 && copeiki % 10 <= 4) && !(copeiki >= 12 && copeiki <= 14))
            {
                copeiki_word = " копейки";
            }
            else
            {
                copeiki_word = " копеек";
            }

            if (rubles == 0)
            {
                result = copeiki + copeiki_word;
            }
            else if (rubles != 0 && copeiki != 0)
            {
                result = rubles + rubles_word + ", " + copeiki + copeiki_word;
            }
            else if (rubles != 0 && copeiki == 0)
            {
                result = rubles + rubles_word + " ровно";
            }

            return result;
        }
    }
}
