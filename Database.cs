using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FBSearch
{
    class Database
    {
        private List<FbUser> friends;
        private List<string> allId = new List<string>();
        private List<string> allFirstName= new List<string>();
        private List<string> allLastName= new List<string>();
        private List<string> allGender= new List<string>();
        private List<string> allUsername= new List<string>();
        private List<string> allHome= new List<string>();
        private List<string> allRelationship= new List<string>();
        private List<int> allDay = new List<int>();
        private List<int> allMonth = new List<int>();
        private List<int> allYear = new List<int>();
        private List<string> allSchool= new List<string>();
        private List<string> allEvents = new List<string>();
        private List<string> allPlaces = new List<string>();

        public Database(List<FbUser> a)
        {
            this.friends = a;
            Update(a);
        }
        private void Update(List<FbUser> a)
        {
            foreach (FbUser user in a)
            {
                string id = user.getId();
                string firstName = user.getFirstName();
                string lastName = user.getLastName();
                string gender = user.getGender();
                string username = user.getUsername();
                string home = user.getHome();
                string relationship = user.getRelationship();
                int day = user.getDay();
                int month = user.getMonth();
                int year = user.getYear();
                List<string> school = user.getSchool();
                List<string> events = user.getEvents();
                List<string> places = user.getPlaces();

                if (!allId.Contains(id))
                {
                    allId.Add(id);
                }
                if(!allFirstName.Contains(firstName))
                {
                    allFirstName.Add(firstName);
                }
                if (!allLastName.Contains(lastName))
                {
                    allLastName.Add(lastName);
                }
                if (!allGender.Contains(gender))
                {
                    allGender.Add(gender);
                }
                if (!allUsername.Contains(username))
                {
                    allUsername.Add(username);
                }
                if (!allHome.Contains(home))
                {
                    allHome.Add(home);
                }
                if (!allRelationship.Contains(relationship))
                {
                    allRelationship.Add(relationship);
                }
                if (!allDay.Contains(day))
                {
                    allDay.Add(day);
                }
                if (!allMonth.Contains(month))
                {
                    allMonth.Add(month);
                }
                if (!allYear.Contains(year))
                {
                    allYear.Add(year);
                }

                foreach (string sola in school)
                {
                    if (!allSchool.Contains(sola))
                    {
                        allSchool.Add(sola);
                    }
                }

                foreach (string e in events)
                {
                    if (!allEvents.Contains(e))
                    {
                        allEvents.Add(e);
                    }
                }

                foreach (string e in places)
                {
                    if (!allPlaces.Contains(e))
                    {
                        allPlaces.Add(e);
                    }
                }
                
            }
        }

        public List<FbUser> Get(Dictionary<string,List<string>> pogoji)
        {
            List<FbUser> result = friends.ToList();

            foreach (KeyValuePair<string, List<string>> dicItem in pogoji)
            {
                //vse za en kjuč
                List<FbUser> zacasno = new List<FbUser>();

                foreach (string entry in dicItem.Value)
                {
                    List<FbUser> dump = new List<FbUser>();
                    //znotraj ključa imamo več parametrov seštevamo

                    //dicItem.Key --> GetGender
                    //entry --> male

                    if (dicItem.Key == "getId")
                    {
                        dump = friends.Where(x => x.getId() == entry).ToList();

                    }
                    else if (dicItem.Key == "getFirstName")
                    {
                        dump = friends.Where(x => x.getFirstName() == entry).ToList();

                    }
                    else if (dicItem.Key == "getLastName")
                    {
                        dump = friends.Where(x => x.getLastName() == entry).ToList();

                    }
                    else if (dicItem.Key == "getGender")
                    {
                        dump = friends.Where(x => x.getGender() == entry).ToList();

                    }
                    else if (dicItem.Key == "getUsername")
                    {
                        dump = friends.Where(x => x.getUsername() == entry).ToList();

                    }
                    else if (dicItem.Key == "getHome")
                    {
                        dump = friends.Where(x => x.getHome() == entry).ToList();

                    }
                    else if (dicItem.Key == "getRelationship")
                    {
                        dump = friends.Where(x => x.getRelationship() == entry).ToList();

                    }
                    else if (dicItem.Key == "getDay")
                    {
                        dump = friends.Where(x => x.getDay() == Int32.Parse(entry)).ToList();

                    }
                    else if (dicItem.Key == "getMonth")
                    {
                        dump = friends.Where(x => x.getMonth() == Int32.Parse(entry)).ToList();

                    }
                    else if (dicItem.Key == "getYear")
                    {
                        dump = friends.Where(x => x.getYear() == Int32.Parse(entry)).ToList();

                    }
                    else if (dicItem.Key == "getSchool")
                    {
                        dump = friends.Where(x => x.getSchool().Contains(entry)).ToList();

                    }
                    else if (dicItem.Key == "getEvents")
                    {
                        dump = friends.Where(x => x.getEvents().Contains(entry)).ToList();

                    }
                    else if (dicItem.Key == "getPlaces")
                    {
                        dump = friends.Where(x => x.getPlaces().Contains(entry)).ToList();

                    }
                    else
                    {
                        Debug.Print("NAPAKA 1");
                        
                    }
                    //Console.WriteLine("END: 1:" + dicItem.Key + " 2:" + entry + "3:" + dump.Count().ToString());
                    foreach (FbUser a in dump)
                    {
                        if (!zacasno.Contains(a))
                        {
                            zacasno.Add(a);
                        }

                    }
                }
                
                //dump je polen
                //izpišemo
                /*foreach (FbUser a in result)
                {
                    if (!zacasno.Contains(a))
                    {
                        result.Remove(a);
                    }
                }*/

                for(int i = result.Count - 1; i >= 0; i--) 
                {
                    if(!zacasno.Contains(result.ElementAt(i)))
                    {
                        result.RemoveAt(i);
                    }
                }
            }

            return result;
        }
        
        public List<string> getAllid()
        {
            return this.allId;
        }
        public List<string> getAllFirstName()
        {
            return this.allFirstName;
        }
        public List<string> getAllLastName()
        {
            return this.allLastName;
        }
        public List<string> getAllGender()
        {
            return this.allGender;
        }
        public List<string> getAllUsername()
        {
            return this.allUsername;
        }
        public List<string> getAllHome()
        {
            return this.allHome;
        }
        public List<string> getAllRelationship()
        {
            return this.allRelationship;
        }
        public List<string> getAllSchool()
        {
            return this.allSchool;
        }
        public List<int> getAllDay()
        {
            return this.allDay;
        }
        public List<int> getAllMonth()
        {
            return this.allMonth;
        }
        public List<int> getAllYear()
        {
            return this.allYear;
        }
        public List<string> getAllEvents()
        {
            return this.allEvents;
        }
        public List<string> getAllPlaces()
        {
            return this.allPlaces;
        }
    }
}
