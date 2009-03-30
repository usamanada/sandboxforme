using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
namespace SQLCracker2
{
    class BruteForceAttack
    {
        public enum Status
        {
            TEXT_UPDATE = 0,
            USER_FOUND = 1,
            CURRENT_POSITION = 2,
            POSITION_FINISHED_REACHED = 3,
            SEARCH_COMPLETE = 4
        }
        public Status BruteForceStatus;

        public System.ComponentModel.BackgroundWorker mbgWorker;
        
        private int miCharactersLength;
        private int[] miaCurrentPositionArray;
        private int[] miaFinishPositionArray;
        private int miSaltSize = 4;
        private bool mbBruteForceCheckComplete;
        private bool mbPositionFinishedReached;
        public bool PositionFinishedReached
        {
            get
            {
                return PositionFinishedReached;
            }
        }
        private byte[] mbaSaltedWord;
        private int miCurrentArraySize;
        private int miFinishArraySize = 0;
        private HashAlgorithm msHash = new SHA1Managed();
        private int miUniCode8WordLength;

        public int MaximumArraySize;
        public PasswordHelper PasswordHelper;
        public string Characters;        

        public void run()
        {
            miCharactersLength = Characters.Length;
            mbBruteForceCheckComplete = false;
            mbPositionFinishedReached = false;
            int Count = 0;
            int PositionUpdateCount = 0;
            bool Match;
            bool Found = false;
            bool PositionArrayAlreadySet = false;
            byte[] result;

            if (miaCurrentPositionArray != null && miaCurrentPositionArray.Length > 0)
            {
                PositionArrayAlreadySet = true;
            }
            else
            {
                miCurrentArraySize = 0;
            }

            while (miCurrentArraySize < MaximumArraySize && !mbgWorker.CancellationPending && !mbPositionFinishedReached)
            {   
                
                if (PositionArrayAlreadySet)
                {
                    PositionArrayAlreadySet = false;                    
                }
                else
                {
                    miCurrentArraySize++;
                    miUniCode8WordLength = miCurrentArraySize * 2;
                    miaCurrentPositionArray = CreatePositionArray(miCurrentArraySize);
                    mbaSaltedWord = new byte[miUniCode8WordLength + miSaltSize];
                    if (1 < miCurrentArraySize)
                    {
                        for (int index = 1; index < miCurrentArraySize; index++)
                        {
                            mbaSaltedWord[index*2] = (byte)Characters[0];
                        }
                    }
                }
                
                mbBruteForceCheckComplete = false;

                while (PasswordHelper.UsersMissingPassword.Count != 0 && !mbBruteForceCheckComplete && !mbgWorker.CancellationPending )
                {
                    Match = false;
                    foreach (char c in Characters)
                    {
                        mbaSaltedWord[0] = (byte)c;
                        //Console.WriteLine(SaltedWordToString());
                        Count++;
                        if ( Count > 50000)
                        {
                            ReportProgressSaltedWord();
                            Count = 0;
                            PositionUpdateCount++;
                            if (PositionUpdateCount > 100)
                            {
                                ReportProgress(Status.CURRENT_POSITION, PositionArrayToString());
                                PositionUpdateCount = 0;
                            }

                        }

                        foreach (User u in PasswordHelper.UsersMissingPassword)
                        {
                            u.Salt.CopyTo(mbaSaltedWord, miUniCode8WordLength);
                            result = msHash.ComputeHash(mbaSaltedWord);

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
                                    int SaltedWordLength = (mbaSaltedWord.Length - 4) / 2;
                                    lowerCaseAttack(mbaSaltedWord, u, SaltedWordLength - 1, SaltedWordLength);
                                    PasswordHelper.AddUserFoundPassword(u);
                                    Found = true;
                                }
                            }
                        }
                    }
                    //Remove Users out of foreach loop as an error will be
                    //thrown if the List is modified will in the foreach
                    if (Found == true)
                    {
                        removeFoundUserFromMissingUsers();
                        Found = false;
                    }
                    incrementNext();
                }
            }
            if(mbgWorker.CancellationPending)
            {
                ReportProgress(Status.CURRENT_POSITION, PositionArrayToString());
            }
            else if (mbPositionFinishedReached)
            {
                ReportProgressSaltedWord();
                ReportProgress(Status.CURRENT_POSITION, PositionArrayToString());
                BruteForceStatus = Status.POSITION_FINISHED_REACHED;
            }
            else
            {
                BruteForceStatus = Status.SEARCH_COMPLETE;
            }
         }
        private void removeFoundUserFromMissingUsers()
        {
            foreach (User u in PasswordHelper.UsersFoundPassword)
            {
                PasswordHelper.RemoveUserMissingPassword(u);
                PasswordHelper.SaveUserFoundPassword();
                PasswordHelper.SaveUserMissingPassword();
            }
            ReportProgress(Status.USER_FOUND, null);
        }
        private bool checkResult(byte[] result, User u, int wordLength, byte[] SlatedWord)
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
        private byte[] lowerCaseAttack(byte[] SaltedWord, User u, int index, int WordLength)
        {
            if (Char.IsLetter((char)SaltedWord[index * 2]))
            {
                SaltedWord[index * 2] = (byte)Char.ToLower((char)SaltedWord[index * 2]);

                if (checkResult(msHash.ComputeHash(SaltedWord), u, WordLength, SaltedWord))
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

                if (checkResult(msHash.ComputeHash(SaltedWord), u, WordLength, SaltedWord))
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
        private void incrementNext()
        {
            //Need to increase array
            if (1 == miCurrentArraySize)
            {
                mbBruteForceCheckComplete = true;
                return;
            }
            //Check to see Finish has been reached
            if (miCurrentArraySize == miFinishArraySize)
            {
                bool Match = true;
                for (int i = 1; i < miCurrentArraySize; i++)
                {
                    if (miaCurrentPositionArray[i] != miaFinishPositionArray[i])
                    {
                        Match = false;
                        break;
                    }
                }
                if (Match)
                {
                    mbBruteForceCheckComplete = true;
                    mbPositionFinishedReached = true;
                    return;
                }
            }

            for (int i = 1; i < miCurrentArraySize; i++)
            {

                if (miaCurrentPositionArray[i] == miCharactersLength - 1)
                {
                    if (i == miCurrentArraySize - 1)
                    {
                        mbBruteForceCheckComplete = true;
                    }
                    miaCurrentPositionArray[i] = 0;
                    mbaSaltedWord[i * 2] = (byte)Characters[0];
                    continue;
                }
                miaCurrentPositionArray[i]++;
                mbaSaltedWord[i * 2] = (byte)Characters[miaCurrentPositionArray[i]];
                
                break;
            }
        }
        private string SaltedWordToString()
        {
            StringBuilder CurrentPostion = new StringBuilder();
            for (int i = 0; i < ((mbaSaltedWord.Length - 4) / 2); i++)
            {
                CurrentPostion.Append((char)mbaSaltedWord[i * 2]);
            }
            return CurrentPostion.ToString();            
        }
        private void ReportProgressSaltedWord()
        {
            ReportProgress(Status.TEXT_UPDATE, SaltedWordToString());
        }
        private void ReportProgress(Status s, object userStatus)
        {
            BruteForceStatus = s;
            if(userStatus != null)
            {
                mbgWorker.ReportProgress((int)s, userStatus);
            }
            else
            {
                mbgWorker.ReportProgress((int)s);
            }

        }

        public static string PositionArrayToWord(string CurrentPosition, string Characters)
        {
            if (String.IsNullOrEmpty(CurrentPosition) || CurrentPosition.Length <= 0)
            {
                return "";
            }
            string[] sa = CurrentPosition.Split(new char[] { ',' });

            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < sa.Length; index++)
            {
                sb.Append(Characters[Convert.ToInt32(sa[index])]);
            }
            return sb.ToString();
        }
        public static string PositionArrayToWord(int[] Position, string Characters)
        {
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < Position.Length; index++)
            {
                sb.Append(Characters[Position[index]]);
            }
            return sb.ToString();
        }

        public static string WordToPositionArrayString(string Word, string Characters)
        {
            if (String.IsNullOrEmpty(Word) || Word.Length <= 0)
            {
                return "";
            }

            int[] pa = CreatePositionArray(Word.Length);

            StringBuilder sb = new StringBuilder();

            for (int index = 1; index < Word.Length; index++)
            {
                pa[index] = Characters.IndexOf(Word[index]);
            }
            
            return PositionArrayToString(pa);
        }
        public static int[] WordToPostionArray(string Word, string Characters)
        {
            string szPositionArray = WordToPositionArrayString(Word, Characters);
            string[] sa = szPositionArray.Split(new char[] { ',' });
            int[] PositionArray = CreatePositionArray(Word.Length);

            for (int index = 0; index < Word.Length; index++)
            {
                PositionArray[index] = Convert.ToInt32(sa[index]);
            }
            return PositionArray;
        }
        public static int[] CreatePositionArray(int Length)
        {
            return new int[Length];
        }
        private string PositionArrayToString()
        {
            return PositionArrayToString(this.miaCurrentPositionArray);
        }
        public static string PositionArrayToString(int[] PositionArray)
        {
            if (PositionArray.Length < 1)
            {
                return "";
            }
            StringBuilder sbPositionArray = new StringBuilder();

            foreach (int Position in PositionArray)
            {
                sbPositionArray.Append(Position.ToString() + ",");
            }
            sbPositionArray.Remove(sbPositionArray.Length - 1, 1);
            return sbPositionArray.ToString();
        }
        public string StartfromPosition
        {
            set
            {
                if (!String.IsNullOrEmpty(value) && value.Length > 0)
                {
                    string[] sa = value.Split(new char[]{','});
                    miCurrentArraySize = sa.Length;
                    miaCurrentPositionArray = CreatePositionArray(miCurrentArraySize);
                    
                    for (int index = 0; index < miCurrentArraySize; index++)
                    {
                        miaCurrentPositionArray[index] = Convert.ToInt32(sa[index]);
                    }

                    miUniCode8WordLength = miCurrentArraySize * 2;
                    mbaSaltedWord = new byte[miUniCode8WordLength + miSaltSize];

                    for (int index = 1; index < miaCurrentPositionArray.Length; index++)
                    {
                        mbaSaltedWord[(index) * 2] = (byte)Characters[miaCurrentPositionArray[index]];
                    }
                }
            }
        }
        public string FinishAtPosition
        {
            set
            {
                if (!String.IsNullOrEmpty(value) && value.Length > 0)
                {
                    string[] sa = value.Split(new char[] { ',' });
                    miFinishArraySize = sa.Length;
                    miaFinishPositionArray = CreatePositionArray(miFinishArraySize);

                    for (int index = 0; index < miFinishArraySize; index++)
                    {
                        miaFinishPositionArray[index] = Convert.ToInt32(sa[index]);
                    }
                }
            }
        }
    }
}
