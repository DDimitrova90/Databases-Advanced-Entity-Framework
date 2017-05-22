namespace _03_Oldest_Family_Member
{
    using System.Collections.Generic;
    using System.Linq;

    public class Family
    {
        public Family()
        {
            this.Members = new List<Person>();
        }

        public List<Person> Members { get; set; }

        public void AddMember(Person member)
        {
            this.Members.Add(member);
        }

        public Person GetOldestMember()
        {
            Person oldestMember = new Person();
            int maxAge = int.MinValue;

            foreach (Person member in this.Members)
            {
                if (member.Age > maxAge)
                {
                    maxAge = member.Age;
                    oldestMember = member;
                }
            }

            return oldestMember;
            // return this.Members.OrderByDescending(m => m.Age).FirstOrDefault();

        }
    }
}
