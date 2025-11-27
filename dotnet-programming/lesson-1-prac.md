## Property

```csharp

```csharp
```
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
```




