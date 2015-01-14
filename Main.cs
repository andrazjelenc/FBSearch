using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facebook;
using Newtonsoft.Json.Linq;

namespace FBSearch
{
    public partial class Form1 : Form
    {
        private Database MyFriends;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {

                try
                {
                    textBox1.Enabled = false;
                    button1.Enabled = false;
                    String token = textBox1.Text;
                    List<FbUser> people = ImportFriends(token);
                    MyFriends = new Database(people);

                    EnterValues();



                    tabControl1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    label13.Text = "All data downloaded!";
                }
                catch (Exception ex)
                {
                    label13.Text = "Something goes wrong :(";
                    MessageBox.Show(ex.ToString());
                    textBox1.Clear();
                    textBox1.Enabled = true;
                    button1.Enabled = true;
                }
            }
            else
            {
                label13.Text = "Please enter your access token!";
            }
            
        }

        private static List<FbUser> ImportFriends(string myAccessToken)
        {
            //uporaba tokena za dostop do FBja
            FacebookClient client = new FacebookClient(myAccessToken);

            //pridobimo objekt z podatki prijateljev
            var friendListData = client.Get("/me/friends?fields=first_name,last_name,gender,id,birthday,education,hometown,relationship_status,username,events");

            //spremenimo var v JObject
            JObject friendListJson = JObject.Parse(friendListData.ToString());

            //ustvarimo novo listo z FbUserji
            List<FbUser> fbUsers = new List<FbUser>();

            //sprehodimo se čez vse prijatelje v objektu
            foreach (var friend in friendListJson["data"].Children())
            {
                //nastavimo prazne spremenljivke
                string first_name = "";
                string last_name = "";
                string id = "";
                string birthday = "";
                List<string> school = new List<string>();
                string gender = "";
                string home = "";
                string username = "";
                string relationship_status = "";
                List<string> events = new List<string>();
                List<string> places = new List<string>();
                
                try //events
                {
                    
                    int st = friend["events"].Count();
                    
                    for (int i = 0; i < st; i++)
                    {
                        if (friend["events"]["data"][i]["rsvp_status"].ToString() == "attending")
                        {
                            string a = friend["events"]["data"][i]["name"].ToString() + ":"+ friend["events"]["data"][i]["location"].ToString();
                            events.Add(a);
                            string b = friend["events"]["data"][i]["location"].ToString();
                            places.Add(b);
                            
                        }
                       
                    }
                }
                catch {}

                //poskusimo shraniti detajle v spremenljivke
                try
                {
                    first_name = friend["first_name"].ToString().Replace("\"", "");
                    last_name = friend["last_name"].ToString().Replace("\"", "");
                    id = friend["id"].ToString().Replace("\"", "");
                }
                catch { }

                try
                {
                    birthday = friend["birthday"].ToString().Replace("\"", "");
                }
                catch { }

                try
                {
                    gender = friend["gender"].ToString().Replace("\"", "");
                }
                catch { }

                try
                {
                    home = friend["hometown"]["name"].ToString();
                }
                catch { }

                try
                {
                    username = friend["username"].ToString().Replace("\"", "");
                }
                catch { }

                try
                {
                    relationship_status = friend["relationship_status"].ToString().Replace("\"", "");
                }
                catch { }

                try
                {
                    int st = friend["education"].Count();
                    for (int i = 0; i < st; i++)
                    {
                        school.Add(friend["education"][i]["school"]["name"].ToString() + " - " + friend["education"][i]["type"].ToString());
                    }
                }
                catch { }

                //generiramo nov objekt FbUser z detaili
                FbUser fbUser = new FbUser(username, id, first_name, last_name, gender, birthday, home, school, relationship_status,events,places);

                //dodamo FbUserja na listo
                fbUsers.Add(fbUser);
            }

            //vrnemo listo z vsemi uporabniki
            return fbUsers;
        }

        private void EnterValues()
        {
            List<string> AllFirstName = MyFriends.getAllFirstName().OrderBy(q=>q).ToList();
            foreach (string element in AllFirstName)
            {
                checkedListBox1.Items.Add(element);
            }

            List<string> AllLastName = MyFriends.getAllLastName().OrderBy(q => q).ToList();
            foreach (string element in AllLastName)
            {
                checkedListBox2.Items.Add(element);
            }

            List<string> Gender = MyFriends.getAllGender().OrderBy(q => q).ToList();
            foreach (string element in Gender)
            {
                checkedListBox3.Items.Add(element);
            }

            List<int> Day = MyFriends.getAllDay();
            Day.Sort();
            foreach (int element in Day)
            {
                checkedListBox4.Items.Add(element.ToString());
            }

            List<int> Month = MyFriends.getAllMonth();
            Month.Sort();
            foreach (int element in Month)
            {
                checkedListBox5.Items.Add(element.ToString());
            }

            List<int> Year = MyFriends.getAllYear();
            Year.Sort();
            foreach (int element in Year)
            {
                checkedListBox6.Items.Add(element.ToString());
            }

            List<string> Home = MyFriends.getAllHome().OrderBy(q => q).ToList();
            foreach (string element in Home)
            {
                checkedListBox7.Items.Add(element);
            }

            List<string> School = MyFriends.getAllSchool().OrderBy(q => q).ToList();
            foreach (string element in School)
            {
                checkedListBox8.Items.Add(element);
            }

            List<string> Love = MyFriends.getAllRelationship().OrderBy(q => q).ToList();
            foreach (string element in Love)
            {
                checkedListBox9.Items.Add(element);
            }

            List<string> events = MyFriends.getAllEvents().OrderBy(q => q).ToList();
            foreach (string element in events)
            {
                checkedListBox10.Items.Add(element);
            }

            List<string> places = MyFriends.getAllPlaces().OrderBy(q => q).ToList();
            foreach (string element in places)
            {
                checkedListBox11.Items.Add(element);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            while (checkedListBox1.CheckedIndices.Count > 0)
            {
                checkedListBox1.SetItemChecked(checkedListBox1.CheckedIndices[0], false);
            }
            while (checkedListBox2.CheckedIndices.Count > 0)
            {
                checkedListBox2.SetItemChecked(checkedListBox2.CheckedIndices[0], false);
            }
            while (checkedListBox3.CheckedIndices.Count > 0)
            {
                checkedListBox3.SetItemChecked(checkedListBox3.CheckedIndices[0], false);
            }
            while (checkedListBox4.CheckedIndices.Count > 0)
            {
                checkedListBox4.SetItemChecked(checkedListBox4.CheckedIndices[0], false);
            }
            while (checkedListBox5.CheckedIndices.Count > 0)
            {
                checkedListBox5.SetItemChecked(checkedListBox5.CheckedIndices[0], false);
            }
            while (checkedListBox6.CheckedIndices.Count > 0)
            {
                checkedListBox6.SetItemChecked(checkedListBox6.CheckedIndices[0], false);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            while (checkedListBox7.CheckedIndices.Count > 0)
            {
                checkedListBox7.SetItemChecked(checkedListBox7.CheckedIndices[0], false);
            }
            while (checkedListBox8.CheckedIndices.Count > 0)
            {
                checkedListBox8.SetItemChecked(checkedListBox8.CheckedIndices[0], false);
            }
            while (checkedListBox9.CheckedIndices.Count > 0)
            {
                checkedListBox9.SetItemChecked(checkedListBox9.CheckedIndices[0], false);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            Dictionary<string, List<string>> pogoji = new Dictionary<string, List<string>>();

            List<string> FirstName = new List<string>();
            List<string> LastName = new List<string>();
            List<string> Gender = new List<string>();
            List<string> Day = new List<string>();
            List<string> Month = new List<string>();
            List<string> Year = new List<string>();
            List<string> Home = new List<string>();
            List<string> School = new List<string>();
            List<string> Love = new List<string>();
            List<string> Events = new List<string>();
            List<string> Places = new List<string>();

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    string str = (string)checkedListBox1.Items[i];
                    FirstName.Add(str);
                    Debug.Print(str);
                }
            }

            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    string str = (string)checkedListBox2.Items[i];
                    LastName.Add(str);
                    Debug.Print(str);
                }
            }

            for (int i = 0; i < checkedListBox3.Items.Count; i++)
            {
                if (checkedListBox3.GetItemChecked(i))
                {
                    string str = (string)checkedListBox3.Items[i];
                    Gender.Add(str);
                    Debug.Print(str);
                }
            }

            for (int i = 0; i < checkedListBox4.Items.Count; i++)
            {
                if (checkedListBox4.GetItemChecked(i))
                {
                    string str = (string)checkedListBox4.Items[i];
                    Day.Add(str);
                    Debug.Print(str);
                }
            }

            for (int i = 0; i < checkedListBox5.Items.Count; i++)
            {
                if (checkedListBox5.GetItemChecked(i))
                {
                    string str = (string)checkedListBox5.Items[i];
                    Month.Add(str);
                    Debug.Print(str);
                }
            }

            for (int i = 0; i < checkedListBox6.Items.Count; i++)
            {
                if (checkedListBox6.GetItemChecked(i))
                {
                    string str = (string)checkedListBox6.Items[i];
                    Year.Add(str);
                    Debug.Print(str);
                }
            }

            for (int i = 0; i < checkedListBox7.Items.Count; i++)
            {
                if (checkedListBox7.GetItemChecked(i))
                {
                    string str = (string)checkedListBox7.Items[i];
                    Home.Add(str);
                    Debug.Print(str);
                }
            }

            for (int i = 0; i < checkedListBox8.Items.Count; i++)
            {
                if (checkedListBox8.GetItemChecked(i))
                {
                    string str = (string)checkedListBox8.Items[i];
                    School.Add(str);
                    Debug.Print(str);
                }
            }

            for (int i = 0; i < checkedListBox9.Items.Count; i++)
            {
                if (checkedListBox9.GetItemChecked(i))
                {
                    string str = (string)checkedListBox9.Items[i];
                    Love.Add(str);
                    Debug.Print(str);
                }
            }

            for (int i = 0; i < checkedListBox10.Items.Count; i++)
            {
                if (checkedListBox10.GetItemChecked(i))
                {
                    string str = (string)checkedListBox10.Items[i];
                    Events.Add(str);
                    Debug.Print(str);
                }
            }

            for (int i = 0; i < checkedListBox11.Items.Count; i++)
            {
                if (checkedListBox11.GetItemChecked(i))
                {
                    string str = (string)checkedListBox11.Items[i];
                    Places.Add(str);
                    Debug.Print(str);
                }
            }

            if (FirstName.Count != 0)
            {
                pogoji.Add("getFirstName", FirstName);
            }
            if (LastName.Count != 0)
            {
                pogoji.Add("getLastName", LastName);
            }
            if (Gender.Count != 0)
            {
                pogoji.Add("getGender", Gender);
            }
            if (Day.Count != 0)
            {
                pogoji.Add("getDay", Day);
            }
            if (Month.Count != 0)
            {
                pogoji.Add("getMonth", Month);
            }
            if (Year.Count != 0)
            {
                pogoji.Add("getYear", Year);
            }
            if (Home.Count != 0)
            {
                pogoji.Add("getHome", Home);
            }
            if (School.Count != 0)
            {
                pogoji.Add("getSchool", School);
            }
            if (Love.Count != 0)
            {
                pogoji.Add("getRelationship", Love);
            }
            if (Events.Count != 0)
            {
                pogoji.Add("getEvents", Events);
            }
            if (Places.Count != 0)
            {
                pogoji.Add("getPlaces", Places);
            }

            List<FbUser> filtrirano = MyFriends.Get(pogoji);
            label15.Text = filtrirano.Count.ToString();

            CheckBox[] opcije = new CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12, checkBox13 };

            ArrayList count = new ArrayList();
            for (int i = 0; i < opcije.Count(); i++)
            {
                if (opcije[i].Checked)
                {
                    count.Add(opcije[i].Text);
                }
            }
            if (count.Count > 0)
            {

                dataGridView1.ColumnCount = count.Count;

                for (int i = 0; i < count.Count; i++)
                {
                    dataGridView1.Columns[i].Name = count[i].ToString();
                }

                foreach (FbUser user in filtrirano)
                {
                    ArrayList row = new ArrayList();
                    if (checkBox1.Checked)
                    {
                        row.Add("ID:"+user.getId());
                    }
                    if (checkBox2.Checked)
                    {
                        row.Add(user.getFirstName());
                    }
                    if (checkBox3.Checked)
                    {
                        row.Add(user.getLastName());
                    }
                    if (checkBox4.Checked)
                    {
                        row.Add(user.getUsername());
                    }
                    if (checkBox5.Checked)
                    {
                        row.Add(user.getGender());
                    }
                    if (checkBox6.Checked)
                    {
                        row.Add(user.getDay().ToString());
                    }
                    if (checkBox7.Checked)
                    {
                        row.Add(user.getMonth().ToString());
                    }
                    if (checkBox8.Checked)
                    {
                        row.Add(user.getYear().ToString());
                    }
                    if (checkBox9.Checked)
                    {
                        row.Add(user.getHome());
                    }
                    if (checkBox10.Checked)
                    {
                        string a = "";
                        foreach (string b in user.getSchool())
                        {
                            a += b + ";";
                        }
                        row.Add(a);
                    }
                    if (checkBox11.Checked)
                    {
                        row.Add(user.getRelationship());
                    }
                    if (checkBox12.Checked)
                    {
                        string a = "";
                        foreach (string b in user.getEvents())
                        {
                            a += b + ";";
                        }
                        row.Add(a);
                    }
                    if (checkBox13.Checked)
                    {
                        string a = "";
                        foreach (string b in user.getPlaces())
                        {
                            a += b + ";";
                        }
                        row.Add(a);
                    }

                    string[] array = row.ToArray(typeof(string)) as string[];
                    dataGridView1.Rows.Add(array);
                }
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "export.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //ToCsV(dataGridView1, @"c:\export.xls");
                ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
            } 
        }

        private void ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            while (checkedListBox10.CheckedIndices.Count > 0)
            {
                checkedListBox10.SetItemChecked(checkedListBox10.CheckedIndices[0], false);
            }
            while (checkedListBox11.CheckedIndices.Count > 0)
            {
                checkedListBox11.SetItemChecked(checkedListBox11.CheckedIndices[0], false);
            }
        }  

        

        
    }
}
