using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook;

namespace FBSearch
{
    class FbUser
    {
        //detaili
        private string first_name;
		private string last_name;
		private string id;

		private int day;
        private int month;
        private int year;

		private string gender;
		private string username;
        private string home;
        private string relationship_status;
		private List<string> school;
        private List<string> events;
        private List<string> places;

        //konstruktor, nastavimo podatke
        public FbUser(string username, string id, string first_name, string last_name, string gender, string birthday, string home, List<string> school, string relationship_status, List<string> events, List<string> places)
        {
            this.username = username;
			this.id = id;
			this.first_name = first_name;
			this.last_name = last_name;
			this.gender = gender;

            SetDate(birthday);
		
			this.home = home;
			this.school = school;
            this.relationship_status = relationship_status;
            this.events = events;
            this.places = places;
        }

        //split string to month/day/year
        private void SetDate(string a)
        {
            if (a == "")
            {
                this.day = 0;
                this.month = 0;
                this.year = 0;
            }
            else if (a.Count() < 7)
            {
                //brez year
                string[] razbito = a.Split('/');
                this.month = Int32.Parse(razbito[0]);
                this.day = Int32.Parse(razbito[1]);
                this.year = 0;
            }
            else
            {
                string[] razbito = a.Split('/');
                this.month = Int32.Parse(razbito[0]);
                this.day = Int32.Parse(razbito[1]);
                this.year = Int32.Parse(razbito[2]);
            }
        }

        //geterji
        public string getId()
        {
            return this.id;
        }
        public string getFirstName()
        {
            return this.first_name;
        }
        public string getLastName()
        {
            return this.last_name;
        }
        public int getDay()
        {
            return this.day;
        }
        public int getMonth()
        {
            return this.month;
        }
        public int getYear()
        {
            return this.year;
        }
        public string getGender()
        {
            return this.gender;
        }
        public string getUsername()
        {
            return this.username;
        }
        public string getHome()
        {
            return this.home;
        }
        public List<string> getSchool()
        {
            return this.school;
        }
        public string getRelationship()
        {
            return this.relationship_status;
        }
        public List<string> getEvents()
        {
            return this.events;
        }
        public List<string> getPlaces()
        {
            return this.places;
        }
    }
}
