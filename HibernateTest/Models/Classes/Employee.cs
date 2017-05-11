
//Main Class with Accessors
//Names must match nibernate mapping
namespace HibernateTest.Models.Classes
{
   
        public class Employee
        {
            public virtual int Id { get; set; }
            public virtual string FirstName { get; set; }
            public virtual string LastName { get; set; }
            public virtual string Designation { get; set; }
            public virtual string SecondDesignation { get; set; }
            public virtual string ThirdDesignation { get; set; }
            public virtual string Four { get; set; }
            public virtual string Five { get; set; }
            public virtual string Six { get; set; }
          
        }
    
}
