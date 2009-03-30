using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;
namespace SQLCracker2
{
    public class PasswordHelper
    {

        public List<User> UsersMissingPassword;
        public List<User> UsersFoundPassword;

        public string UsersMissingPasswordFile;
        public string UsersFoundPasswordFile;
        
        private User CurrentUser;
        
        public PasswordHelper()
        {
            UsersMissingPassword = new List<User>();
            UsersFoundPassword = new List<User>();
        }
        public void BuildMissingPasswordList()
        {
            ReadXmlToUsers(UsersMissingPasswordFile, UsersMissingPassword);
        }
        public void BuildFoundPasswordList()
        {
            ReadXmlToUsers(UsersFoundPasswordFile, UsersFoundPassword);
        }
        private bool checkFile(string szFile)
        {
            if (szFile == null || szFile.Length == 0)
            {
                throw new Exception("Invalid File Passed");
            }
            if (File.Exists(szFile))
            {
                return true;
            }
            throw new Exception("File Not Found: " + szFile);
        }
        private void ReadXmlToUsers(string szFile, List<User> Users)
        {
            if (!checkFile(szFile))
            {
                return;
            }
            
            XmlDocument doc = new XmlDocument();

            doc.Load(szFile);
            foreach (XmlNode DatabaseXn in doc.DocumentElement.ChildNodes)
            {
                string DatabaseName = DatabaseXn.Attributes["name"].Value;
                string DatabaseType = DatabaseXn.Attributes["type"].Value;

                foreach (XmlNode UserXn in DatabaseXn.ChildNodes)
                {
                    CurrentUser = new User();
                    CurrentUser.DatabaseName = DatabaseName;
                    CurrentUser.DatabaseType = DatabaseType;
                    CurrentUser.Username = UserXn.Attributes["name"].Value;
                    if (UserXn.Attributes["password"] != null)
                    {
                        CurrentUser.Password = UserXn.Attributes["password"].Value;
                    }
                    CurrentUser.Hash = UserXn.Attributes["hash"].Value.ToString();
                    if (CurrentUser.Hash.Length == 94 && DatabaseType == "SQL2000")
                    {
                        string body = CurrentUser.Hash.Substring(6);
                        string salt = body.Substring(0, 8);

                        for (int index = 0; index < 4; index++)
                        {
                            CurrentUser.Salt[index] = Convert.ToByte(salt.Substring(index * 2, 2), 16);
                        }

                        string lower = body.Substring(8, 40);
                        string upper = body.Substring(48, 40);

                        for (int index = 0; index < 20; index++)
                        {
                            CurrentUser.LowerHash[index] = Convert.ToByte(lower.Substring(index * 2, 2), 16);
                            CurrentUser.UpperHash[index] = Convert.ToByte(upper.Substring(index * 2, 2), 16);
                        }
                        if (!Users.Exists(FindUserPredicate))
                        {
                            Users.Add(CurrentUser);
                        }
                    }
                }
            }
        }
        private bool FindUserPredicate(User user)
        {
            if (user.DatabaseName == CurrentUser.DatabaseName &&
               user.Username == CurrentUser.Username)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SaveUserFoundPassword()
        {
            SaveUsersToXml(UsersFoundPasswordFile, UsersFoundPassword);
        }
        public void SaveUserMissingPassword()
        {
            SaveUsersToXml(UsersMissingPasswordFile, UsersMissingPassword);
        }
        private void SaveUsersToXml(string szFile, List<User> Users)
        {
            if (szFile == null || szFile.Length == 0)
            {
                throw new Exception("Invalid File Passed");
            }
            XmlDocument doc = new XmlDocument();
            XPathNavigator navigator = doc.CreateNavigator();

            XmlWriter writer = navigator.AppendChild();

            writer.WriteStartElement("Databases");
            writer.WriteEndElement();
            writer.Close();

            navigator.MoveToRoot();
            navigator.MoveToFirstChild();

            foreach (User u in Users)
            {
                if (navigator.HasChildren)
                {
                    bool NodeFound = false;
                    navigator.MoveToFirstChild();

                    do
                    {
                        string DatabaseName = navigator.GetAttribute("name", "");

                        if (DatabaseName != null && DatabaseName == u.DatabaseName)
                        {
                            NodeFound = true;
                            AddUserNode(navigator.AppendChild(), u, true);
                            break;
                        }
                    }
                    while (navigator.MoveToNext());

                    navigator.MoveToParent();

                    if (NodeFound == false)
                    {
                        AddDatabaseNode(navigator.AppendChild(), u);
                    }
                }
                else
                {
                    AddDatabaseNode(navigator.AppendChild(), u);
                }
            }

            doc.Save(szFile);
        }
        private void AddUserNode(XmlWriter writer, User u, bool closeWriter)
        {
            writer.WriteStartElement("User");
            writer.WriteAttributeString("name", u.Username);
            writer.WriteAttributeString("password", u.Password);
            writer.WriteAttributeString("hash", u.Hash);
            writer.WriteEndElement();
            if (closeWriter)
            {
                writer.Close();
            }
        }
        private void AddDatabaseNode(XmlWriter writer, User u)
        {
            writer.WriteStartElement("Database");
            writer.WriteAttributeString("name", u.DatabaseName);
            writer.WriteAttributeString("type", u.DatabaseType);
            
            AddUserNode(writer, u, false);

            writer.WriteEndElement();
            writer.Close();
        }

        public void AddUserFoundPassword(User u)
        {
            CurrentUser = u;
            if (!UsersFoundPassword.Exists(FindUserPredicate))
            {
                UsersFoundPassword.Add(CurrentUser);
            }
        }
        public void RemoveUserMissingPassword(User u)
        {
            UsersMissingPassword.Remove(u);
        }
    }
}
