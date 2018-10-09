using System;

namespace PropertyGridDemo {
    public class CategoryAttributesViewModel {
        public virtual AttributesSupportGroupingData Data { get; protected set; }
        public CategoryAttributesViewModel() {
            Data = new AttributesSupportGroupingData {
                FirstName = "Anita", LastName = "Benson",
                Group = "Inventory Management", Title = "Purchasing Manager", HireDate = new DateTime(2002, 2, 2), Salary = 65000m,
                Phone = "7138638137", Email = "Anita_Benson@example.com",
                AddressLine1 = "9602 South Main", AddressLine2 = "Seattle, 77025, USA",
                Gender = Gender.Female, BirthDate = new DateTime(1985, 3, 18)
            };
        }
    }
}
