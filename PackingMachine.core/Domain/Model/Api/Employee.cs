namespace PackingMachine.core.Domain.Model.Api
{
    public class Employee
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return LastName+" "+FirstName;
            }
        }
        public Employee (string id,string firstName,string lastName)
        {
            Id=id;
            FirstName=firstName;
            LastName=lastName;
        }
    }
}
