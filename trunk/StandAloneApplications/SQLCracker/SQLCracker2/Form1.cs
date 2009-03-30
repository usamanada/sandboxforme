using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Configuration;
namespace SQLCracker2
{
    public partial class Form1 : Form
    {
        private PasswordHelper ph;
        private DictionaryHelper dh;
        private HashAlgorithm hash;
        private System.EventHandler EventHandler_txtCurrentPosition_TextChanged;
        private int IncreamentPositionInArray = 3;
        public Form1()
        {
            InitializeComponent();
            ph = new PasswordHelper();
            dh = new DictionaryHelper();
            hash = new SHA1Managed();

            txtMissingPasswordFile.Text = ConfigurationManager.AppSettings["MissingPasswordFile"];
            txtDictionaryFile.Text =  ConfigurationManager.AppSettings["DictionaryFile"];
            txtFoundPasswordFile.Text = ConfigurationManager.AppSettings["FoundPasswordFile"];
            string characterSearch = ConfigurationManager.AppSettings["CharacterSearch"];
            if (characterSearch != null && characterSearch.Length > 0)
            {
                txtCharacterSearch.Text = characterSearch;
                txtCurrentPosition.Text = BruteForceAttack.PositionArrayToWord(ConfigurationManager.AppSettings["CurrentPosition"], characterSearch);
            }
            txtSize.Text = ConfigurationManager.AppSettings["Size"];

            EventHandler_txtCurrentPosition_TextChanged = new System.EventHandler(this.txtCurrentPosition_TextChanged);
            this.txtCurrentPosition.TextChanged += EventHandler_txtCurrentPosition_TextChanged;

        }

        private byte[] getCharArray()
        {
            byte[] plainTextBytes = Encoding.ASCII.GetBytes(txtCharacterSearch.Text);
            return plainTextBytes;
        }
        
        private void PasswordBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Xml Files|*.xml";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtMissingPasswordFile.Text = ofd.FileName;
            }
        }
        private void DictionaryBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text|*.txt";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtDictionaryFile.Text = ofd.FileName;
            }
        }
        private void OutputDirBtb_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Xml Files|*.xml";
            sfd.OverwritePrompt = false;
            DialogResult result = sfd.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFoundPasswordFile.Text = sfd.FileName;
            }
        }
        private void SaveConfig()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Remove("MissingPasswordFile");
            config.AppSettings.Settings.Remove("DictionaryFile");
            config.AppSettings.Settings.Remove("FoundPasswordFile");
            config.AppSettings.Settings.Remove("CharacterSearch");
            config.AppSettings.Settings.Remove("Size");


            config.AppSettings.Settings.Add("MissingPasswordFile", txtMissingPasswordFile.Text);
            config.AppSettings.Settings.Add("DictionaryFile", txtDictionaryFile.Text);
            config.AppSettings.Settings.Add("FoundPasswordFile", txtFoundPasswordFile.Text);
            config.AppSettings.Settings.Add("CharacterSearch", txtCharacterSearch.Text);
            config.AppSettings.Settings.Add("Size", txtSize.Text);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        
        private void GoBtn_Click(object sender, EventArgs e)
        {
            SaveConfig();

            populatePasswords();

            if (cbxDictionary.Checked == true)
            {
                DictionaryAttack();
            }
            if (cbxBruteForce.Checked == true)
            {
                TogglePasswordComponents(true);

                //if (txtServerFinish.Text.Length == 0)
                //{
                //    txtServerFinish.Text = BruteForceAttack.PositionArrayToWord(IncreamentPosition(IncreamentPositionInArray, 1, BruteForceAttack.WordToPostionArray(txtServerFinish.Text, txtCharacterSearch.Text), true), txtCharacterSearch.Text);
                //}
                Worker.RunWorkerAsync();
            }
        }
        private void populatePasswords()
        {
            ph.UsersMissingPasswordFile = txtMissingPasswordFile.Text;
            ph.UsersFoundPasswordFile = txtFoundPasswordFile.Text;

            if (File.Exists(txtMissingPasswordFile.Text))
            {
                ph.BuildMissingPasswordList();
                ph.BuildFoundPasswordList();
                displayMissingPasswords();
                displayFoundPasswords();
            }
        }
        private void TogglePasswordComponents(bool state)
        {
            txtCharacterSearch.ReadOnly = state;
            txtSize.ReadOnly = state;
            txtCurrentPosition.ReadOnly = state;
            if (state == true)
            {
                this.txtCurrentPosition.TextChanged -= EventHandler_txtCurrentPosition_TextChanged;
            }
            else
            {
                this.txtCurrentPosition.TextChanged += EventHandler_txtCurrentPosition_TextChanged;
            }
            gbxAttacks.Enabled = !state;
        }
        private void displayMissingPasswords()
        {
            lblMissingPasswordCount.Text = ph.UsersMissingPassword.Count.ToString();
            buildTreeView(UsersMissingPassWordTrv, ph.UsersMissingPassword);
        }
        private void displayFoundPasswords()
        {
            lblFoundPasswordCount.Text = ph.UsersFoundPassword.Count.ToString();
            buildTreeView(UsersHavingPassWordTrv, ph.UsersFoundPassword);
        }

        private void DictionaryAttack()
        {
            if (File.Exists(txtDictionaryFile.Text))
            {
                dh.BuildDictionaryList(txtDictionaryFile.Text);
                bool Match;
                bool Found = false;
                byte[] result;

                foreach (Dictionary d in dh.Dictionarys)
                {
                    foreach (User u in ph.UsersMissingPassword)
                    {
                        u.Salt.CopyTo(d.SaltedWord, d.UniCode8WordLength);
                        result = hash.ComputeHash(d.SaltedWord);

                        if (result[0] == u.UpperHash[0])
                        {
                            Match = true;
                            for (int index = 0; index < 20; index++)
                            {
                                if (result[index] != u.UpperHash[index])
                                {
                                    Match = false;
                                    break;
                                }
                            }
                            if (Match == true)
                            {
                                u.UppercasePassword = d.UpperCaseWord;
                                lowerCaseAttack(d.SaltedWord, u, d.WordLength - 1, d.WordLength);
                                SavePassword(u);
                                Found = true;
                            }
                        }
                    }
                    if (Found == true)
                    {
                        removeFoundUserFromMissingUsers();
                        RebuildTreeViews();
                        Found = false;
                    }                    
                }
            }
        }
        private void RebuildTreeViews()
        {
            displayMissingPasswords();
            displayFoundPasswords();
            this.Refresh();
        }
        private byte[] lowerCaseAttack(byte[] SaltedWord, User u, int index, int WordLength)
        {
            if (Char.IsLetter((char)SaltedWord[index * 2]))
            {
                SaltedWord[index * 2] = (byte)Char.ToLower((char)SaltedWord[index * 2]);

                if (checkResult(hash.ComputeHash(SaltedWord), u, WordLength, SaltedWord))
                {
                    return SaltedWord;
                }
                else
                {
                    if (0 < index)
                    {
                        SaltedWord = lowerCaseAttack(SaltedWord, u, index - 1, WordLength);
                    }
                }

                SaltedWord[index * 2] = (byte)Char.ToUpper((char)SaltedWord[index * 2]);

                if (checkResult(hash.ComputeHash(SaltedWord), u, WordLength, SaltedWord))
                {
                    return SaltedWord;
                }
                else
                {
                    if (0 < index)
                    {
                        SaltedWord = lowerCaseAttack(SaltedWord, u, index - 1, WordLength);
                    }
                }
            }
            else
            {
                if (0 < index)
                {
                    SaltedWord = lowerCaseAttack(SaltedWord, u, index - 1, WordLength);
                }
            }
            return SaltedWord;
        }
        private bool checkResult(byte[] result, User u, int wordLength,  byte[] SlatedWord)
        {
            bool match = false;
            if (result[0] == u.LowerHash[0])
            {
                match = true;
                for (int checkIndex = 0; checkIndex < 20; checkIndex++)
                {
                    if (result[checkIndex] != u.LowerHash[checkIndex])
                    {
                        match = false;
                        break;
                    }
                }
                if (match == true)
                {
                    byte[] word = new byte[wordLength * 2];
                    for (int copyIndex = 0; copyIndex < word.Length; copyIndex++)
                    {
                        word[copyIndex] = SlatedWord[copyIndex];
                    }

                    u.Password = Encoding.Unicode.GetString(word);
                }
            }
            return match;
        }
        private void removeFoundUserFromMissingUsers()
        {
            foreach (User u in ph.UsersFoundPassword)
            {
                ph.RemoveUserMissingPassword(u);
                ph.SaveUserFoundPassword();
                ph.SaveUserMissingPassword();
            }
        }
        private void SavePassword(User u)
        {
            ph.AddUserFoundPassword(u);
        }
        private TreeNode FindDatabaseTreeNode(TreeView tv, string DatabaseName)
        {
            if (tv.Nodes.Count != 0)
            {
                foreach (TreeNode DataBasesTn in tv.Nodes)
                {
                    if (DataBasesTn.Text == DatabaseName)
                    {
                        return DataBasesTn;
                    }
                }
            }

            TreeNode AddDataBasesTn = new TreeNode();
            AddDataBasesTn.Name = DatabaseName;
            AddDataBasesTn.Text = AddDataBasesTn.Name;
            tv.Nodes.Add(AddDataBasesTn);
            return AddDataBasesTn;
        }
        private void buildTreeView(TreeView tv, List<User> Users)
        {
            tv.Nodes.Clear();
            TreeNode DataBasesTn = new TreeNode();
            string CurrentDatabase = String.Empty;

            foreach (User u in Users)
            {
                if (CurrentDatabase != u.DatabaseName)
                {
                    CurrentDatabase = u.DatabaseName;
                    DataBasesTn = FindDatabaseTreeNode(tv, CurrentDatabase);
                }
                UserTreeNode ut = new UserTreeNode();
                ut.u = u;
                ut.Text = "username: " + u.Username;
                if (u.Password != null && u.Password.Length != 0)
                {
                    ut.Text += " password: " + u.Password;
                }
                DataBasesTn.Nodes.Add(ut);
                DataBasesTn.Text = DataBasesTn.Name + " (" + DataBasesTn.Nodes.Count + ")"; 
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Worker.CancelAsync();
        }
        
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BruteForceAttack bfa = new BruteForceAttack();
            bfa.Characters = txtCharacterSearch.Text;
            bfa.MaximumArraySize = Convert.ToInt32(txtSize.Text);
            bfa.PasswordHelper = ph;
            bfa.mbgWorker = Worker;

            bfa.StartfromPosition = ConfigurationManager.AppSettings["CurrentPosition"];

            //bfa.StartfromPosition = BruteForceAttack.WordToPositionArrayString(txtServerStart.Text, bfa.Characters);
            
            //bfa.FinishAtPosition = BruteForceAttack.WordToPositionArrayString(txtServerFinish.Text, bfa.Characters);
            

            bfa.run();

            if (Worker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                e.Result = bfa.BruteForceStatus;
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch ((BruteForceAttack.Status)e.ProgressPercentage)
            {
                case BruteForceAttack.Status.TEXT_UPDATE:
                    {
                        txtCurrentPosition.Text = e.UserState.ToString();
                    }
                    break;
                case BruteForceAttack.Status.USER_FOUND:
                    {
                        RebuildTreeViews();
                    }
                    break;
                case BruteForceAttack.Status.CURRENT_POSITION:
                    {
                        ConfigCurrentPositionSave(e.UserState.ToString());
                    }
                    break;
                case BruteForceAttack.Status.POSITION_FINISHED_REACHED:
                    {
                        Console.WriteLine("Finished");
                    }
                    break;
            }            
        }
        private void ConfigCurrentPositionSave(string currentPosition)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("CurrentPosition");
            config.AppSettings.Settings.Add("CurrentPosition", currentPosition);
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Attacks Stopped");
            }
            else
            {
                switch ((BruteForceAttack.Status)e.Result)
                {
                    case BruteForceAttack.Status.POSITION_FINISHED_REACHED:
                        IncreamentPosition();
                        break;
                    case BruteForceAttack.Status.SEARCH_COMPLETE:
                        Console.WriteLine("Search Complete");
                        break;
                }
            }
            TogglePasswordComponents(false);
        }

        private void txtCurrentPosition_TextChanged(object sender, EventArgs e)
        {
            ConfigCurrentPositionSave(BruteForceAttack.WordToPositionArrayString(txtCurrentPosition.Text, txtCharacterSearch.Text));
        }

        private void IncreamentPosition()
        {
            int[] PositionArray = BruteForceAttack.WordToPostionArray(txtServerFinish.Text, txtCharacterSearch.Text);

            PositionArray = IncreamentPosition(1, 1, PositionArray, false);
            txtServerStart.Text = BruteForceAttack.PositionArrayToWord(PositionArray, txtCharacterSearch.Text);

            txtServerFinish.Text = BruteForceAttack.PositionArrayToWord(IncreamentPosition(IncreamentPositionInArray, 1, PositionArray, true), txtCharacterSearch.Text);

        }

        private int[] CreatePositionArray(int Length, int fill)
        {
            int[] PositionArray = new int[Length];
            for (int i = 0; i < fill; i++)
            {
                PositionArray[i] = txtCharacterSearch.Text.Length - 1;
            }
            return PositionArray;
        }
        private int[] IncreamentPosition(int position, int amount, int[] PositionArray, bool fill)
        {
            if (PositionArray.Length < position + 1)
            {
                return CreatePositionArray(position + 1, position + 1);
            }
            bool bIncreaseArray = false;
            for (int i = position; i < PositionArray.Length; i++)
            {
                if (PositionArray[i] == txtCharacterSearch.Text.Length - 1)
                {
                    if (i == PositionArray.Length - 1)
                    {
                        bIncreaseArray = true;
                        break;
                    }
                    PositionArray[i] = 0;
                    continue;
                }
                PositionArray[i] += amount;
                break;
            }
            if (bIncreaseArray)
            {
                if (fill)
                {
                    PositionArray = CreatePositionArray(PositionArray.Length + 1, position + 1);
                }
                else
                {
                    PositionArray = BruteForceAttack.CreatePositionArray(PositionArray.Length + 1);
                }
            }
            else if(fill)
            {
                for (int i = 0; i < position + 1; i++)
                {
                    PositionArray[i] = txtCharacterSearch.Text.Length - 1;
                }
            }
            return PositionArray;
        }
    }
}