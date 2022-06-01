using Newtonsoft.Json;
using System.Media;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Anket
{
    
    public partial class Form1 : Form
    {
        public List<User> Users = new();
        public string Select { get; set; }
        public Form1()
        {
            InitializeComponent();

        }

        

        private void btn_Add_Click(object sender, EventArgs e)
        {
            User user = new();
            user.Name = textBoxName.Text;
            user.SurName = textBoxSurname.Text;
            user.Email = textBoxEmail.Text;
            user.Phone = textBoxPhone.Text;
            user.Birthday = dateTimePicker1.Text.ToString();
            Users.Add(user);
            listBox1.Items.Add(user);
            textBoxName.Text = null;
            textBoxSurname.Text = null;
            textBoxEmail.Text = null;
            textBoxPhone.Text = null;
            dateTimePicker1.Text = null;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var user = listBox1.SelectedItem as User;
            textBoxName.Text = user?.Name;
            textBoxSurname.Text = user?.SurName;
            textBoxEmail.Text = user?.Email;
            textBoxPhone.Text = user?.Phone;
            dateTimePicker1.Text = user?.Birthday;

        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            var user = listBox1.SelectedItem as User;
            var user1 = Users.Find(u => u.Name == user.ToString());
            user1.Name = textBoxName.Text;
            user1.SurName = textBoxSurname.Text;
            user1.Email = textBoxEmail.Text;
            user1.Phone = textBoxPhone.Text;
            user1.Birthday = dateTimePicker1.Text.ToString();
            textBoxName.Text = null;
            textBoxSurname.Text = null;
            textBoxEmail.Text = null;
            textBoxPhone.Text = null;
            dateTimePicker1.Text = null;
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            textBoxName.Text = null;
            textBoxSurname.Text = null;
            textBoxEmail.Text = null;
            textBoxPhone.Text = null;
            dateTimePicker1.Text = null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label6.Text = null;
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            
            User? userFromJson = null;        
            if (File.Exists($"{textBox1.Text}.json"))
            {
                var jsonStr = File.ReadAllText($"{textBox1.Text}.json");
                userFromJson = JsonConvert.DeserializeObject<User>(jsonStr);
                textBoxName.Text = userFromJson?.Name;
                textBoxSurname.Text = userFromJson?.SurName;
                textBoxEmail.Text = userFromJson?.Email;
                textBoxPhone.Text = userFromJson?.Phone;
                dateTimePicker1.Text = userFromJson?.Birthday;
            }
            else
            {
                SoundPlayer soundPlayer = new("error.wav");
                soundPlayer.Play();
                MessageBox.Show("There is no User in this name");
            }
                


        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            var user = listBox1.SelectedItem;
            string jsonString = JsonConvert.SerializeObject(user, Formatting.Indented);
            File.WriteAllText($"{textBox1.Text}.json", jsonString);
            SoundPlayer soundPlayer = new("success.wav");
            soundPlayer.Play();
              
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(textBoxEmail.Text);
            if(!match.Success)
                textBoxEmail.ForeColor = Color.Red;
            else
                textBoxEmail.ForeColor = Color.Black;
        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$");
            Match match = regex.Match(textBoxPhone.Text);
            if (!match.Success)
                textBoxPhone.ForeColor = Color.Red;
            else
                textBoxPhone.ForeColor = Color.Black;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"[0-9]");
            Match match = regex.Match(textBoxName.Text);
            if (match.Success)
                textBoxName.ForeColor = Color.Red;
            else
                textBoxName.ForeColor = Color.Black;
        }

        private void textBoxSurname_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"[0-9]");
            Match match = regex.Match(textBoxSurname.Text);
            if (match.Success)
                textBoxSurname.ForeColor = Color.Red;
            else
                textBoxSurname.ForeColor = Color.Black;
        }
    }
}