using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
namespace _01_ADConnect
{
    public class ADUser
    {
        /// <summary>
        /// Retrieve users GUID based on users account name.
        /// </summary>
        /// <param name="SAMAccountName">Active directory object account name</param>
        public static Guid GetUserGUID(string SAMAccountName)
        {
            // Local variables.
            Guid userGuid = Guid.Empty;
            DirectoryEntry entry = new DirectoryEntry();
            DirectorySearcher searcher = new DirectorySearcher(entry);

            //set the search scope
            searcher.SearchScope = SearchScope.Subtree;

            //Set the filter. For this example we will be looking at all users
            searcher.Filter = string.Format("(&(objectClass=user)(sAMAccountName={0}))", SAMAccountName);

            // Execute the search.
            SearchResult adObject = searcher.FindOne();

            // Check that the search return an object.
            if (adObject != null)
            {
                // Grap the active directory users obejct guid.
                userGuid = new Guid(adObject.Properties["objectguid"][0] as byte[]);
            }

            // Return users guid.
            return userGuid;
        }
        public static void displayAll(string SAMAccountName)
        {
            DirectoryEntry entry = new DirectoryEntry();
            DirectorySearcher searcher = new DirectorySearcher(entry);

            //set the search scope
            searcher.SearchScope = SearchScope.Subtree;

            //Set the filter. For this example we will be looking at all users
            searcher.Filter = string.Format("(&(objectClass=user)(sAMAccountName={0}))", SAMAccountName);

            // Execute the search.
            SearchResult adObject = searcher.FindOne();
            if (adObject != null)
            {
                foreach (string myKey in adObject.Properties.PropertyNames)
                {
                    string tab = "    ";
                    Console.WriteLine(myKey + " = ");
                    foreach (Object myCollection in adObject.Properties[myKey])
                    {
                        Console.WriteLine(tab + myCollection);
                    }
                }
            }
        }
    }
}
