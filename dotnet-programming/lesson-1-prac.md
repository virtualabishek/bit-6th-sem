## Property


```csharp
using System;
public class Student
{
    private int _id;
    private string _name;
    public int Id {get; set;}
    public string Name
    {
        get {return _name; }
        set 
        {
            if(string.IsNullOrEmpty(value))
            throw new ArgumentException("Name cannot be empty");
            _name = value;
        }
    }
}

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Student s1 = new Student();
        s1.Id = 1;
        s1.Name = "";
        Console.WriteLine(s1.Id);
        Console.WriteLine(s1.Name);
    }
}
```

##indexers

```csharp

using System;

public class Student
{
    private string[] students = new string[4];
    // indexer initialization
    public string this[int index]
    {
        get {return students[index]; }
        set {students[index] = value;}
    }
}

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Student s = new Student();
        s[0] = "Ram";
        s[1] = "Shyam";
        Console.WriteLine(s[0]);
        Console.WriteLine(s[1]);
    }
}
```


## Inherience with base keyword,


```csharp


using System;

public class Person 
{
    public string Name {get; set;}
    public Person (string name)
    {
        Name = name;
        Console.WriteLine("Your name is" + Name);
    }
    public virtual void Greet() 
    {
        Console.WriteLine("Namaste!" + Name);
    }
}

public class Employee : Person 
{
    public string employeePosi {get; set;}
    public Employee (string name, string posi) : base(name)
    {
        employeePosi = posi;
        Console.WriteLine("Employee positiion set");
    }
    public override void Greet()
    {
        base.Greet();
        Console.WriteLine($"I am working at the {employeePosi} as a position");
    }
}

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Employee e = new Employee ("Abishek", "Junior Assistant");
        e.Greet();
    }
}
```


## Method Hiding Overloading Overriding

```csharp
using System;

class Parent
{
    // ----- FOR OVERRIDING -----
    public virtual void Show()
    {
        Console.WriteLine("Parent Show (virtual)");
    }

    // ----- FOR HIDING -----
    public void Display()
    {
        Console.WriteLine("Parent Display");
    }

    // ----- FOR OVERLOADING -----
    public void Add(int a, int b)
    {
        Console.WriteLine("Add(int, int): " + (a + b));
    }

    public void Add(double a, double b)
    {
        Console.WriteLine("Add(double, double): " + (a + b));
    }
}

class Child : Parent
{
    // ----- METHOD OVERRIDING -----
    public override void Show()
    {
        Console.WriteLine("Child Show (override)");
    }

    // ----- METHOD HIDING -----
    public new void Display()
    {
        Console.WriteLine("Child Display (hidden)");
    }
}

class Program
{
    static void Main()
    {
        Parent p = new Parent();
        Child c = new Child();
        Parent pc = new Child();

        Console.WriteLine("=== METHOD OVERRIDING ===");
        p.Show();     // Parent version
        c.Show();     // Child version
        pc.Show();    // Child version (runtime polymorphism)

        Console.WriteLine("\n=== METHOD HIDING ===");
        p.Display();  // Parent display
        c.Display();  // Child display
        pc.Display(); // Parent display (because of hiding)

        Console.WriteLine("\n=== METHOD OVERLOADING ===");
        p.Add(5, 10);      // int version
        p.Add(2.5, 3.5);   // double version
    }
}
```




