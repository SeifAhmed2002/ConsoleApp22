#region Q1
// a)
// 1- Fields (Owner, Balance) are public → anyone can change them directly
// 2- Withdraw has no validation → can withdraw more than balance

// b)
// - Make fields private
// - Use properties
// - Add validation in Withdraw

// c)
// Public fields are bad because:
// - No control on data
// - Can break the object easily
#endregion


#region Q2
// Field: normal variable inside class
// Property: controlled access using get/set

// Yes, property can contain logic

using System.Diagnostics;

public double Price = 100;

public double PriceWithTax
{
    get { return Price * 1.14; }
}
#endregion


#region Q3
// a)
// this[int index] is called an Indexer
// It lets you use object like an array → obj[0]

// b)
// register[10] → runtime error (out of range)

// safer version:
public string this[int index]
{
    get
    {
        if (index >= 0 && index < names.Length)
            return names[index];
        return null;
    }
    set
    {
        if (index >= 0 && index < names.Length)
            names[index] = value;
    }
}

// c)
// Yes, class can have more than one indexer
// Example: access by string (search by name)
#endregion


#region Q4
// a)
// static means shared between all objects
// TotalOrders is shared
// Item is for each object

// b)
// No
// static method cannot access non-static fields directly
#endregion
