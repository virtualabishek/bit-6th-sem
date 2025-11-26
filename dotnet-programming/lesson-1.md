Now I have gathered comprehensive information. Let me compile the complete exam-ready notes for Unit 1.

# Unit 1 – Language Preliminaries (Complete Exam Notes)  
## CSIT/BIT Net Centric Computing (TU IoST)

***

# THEORY + SOLUTIONS WITH EXAMPLES

## 1. Overview of .NET Framework

**.NET Framework** is a software development platform by Microsoft that provides:
- **CLR (Common Language Runtime)**: Manages code execution
- **Base Class Library (BCL)**: Reusable classes for I/O, collections, etc.
- **Language Interoperability**: Multiple languages (C#, VB, F#) work together
- **Garbage Collection**: Automatic memory management

***

## 2. Constructor

A **constructor** is a special method that initializes an object when it is created. It has the same name as the class and no return type.

### Types of Constructors:
- **Default Constructor**: No parameters
- **Parameterized Constructor**: Takes parameters
- **Copy Constructor**: Creates object from another object
- **Static Constructor**: Initializes static members

### Example:
```csharp
class Student
{
    public string Name;
    public int Age;

    // Default Constructor
    public Student()
    {
        Name = "Unknown";
        Age = 0;
    }

    // Parameterized Constructor
    public Student(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

class Program
{
    static void Main()
    {
        Student s1 = new Student();              // Default
        Student s2 = new Student("Ram", 20);     // Parameterized
        Console.WriteLine(s1.Name);  // Unknown
        Console.WriteLine(s2.Name);  // Ram
    }
}
```

***

## 3. Properties

**Properties** provide controlled access to class fields using `get` and `set` accessors.

### Example:
```csharp
class Employee
{
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    // Auto-implemented property
    public int Age { get; set; }
}

class Program
{
    static void Main()
    {
        Employee emp = new Employee();
        emp.Name = "John";      // set
        emp.Age = 25;
        Console.WriteLine(emp.Name);  // get
    }
}
```

***

## 4. Arrays and Strings

### Array Example:
```csharp
int[] numbers = { 10, 20, 30, 40, 50 };

// Loop through array
foreach (int num in numbers)
{
    Console.WriteLine(num);
}

// 2D Array
int[,] matrix = { { 1, 2 }, { 3, 4 } };
```

### String Example:
```csharp
string str = "Hello World";
Console.WriteLine(str.Length);           // 11
Console.WriteLine(str.ToUpper());        // HELLO WORLD
Console.WriteLine(str.Substring(0, 5));  // Hello
Console.WriteLine(str.Replace("World", "C#"));  // Hello C#
```

***

## 5. Indexers (Very Important – Asked in 2081)

**Indexers** allow objects to be indexed like arrays using `this[index]` syntax.[1][2]

### Example:
```csharp
class StudentCollection
{
    private string[] students = new string[5];

    // Indexer
    public string this[int index]
    {
        get { return students[index]; }
        set { students[index] = value; }
    }
}

class Program
{
    static void Main()
    {
        StudentCollection sc = new StudentCollection();
        sc[0] = "Ram";
        sc[1] = "Shyam";
        Console.WriteLine(sc[0]);  // Ram
        Console.WriteLine(sc[1]);  // Shyam
    }
}
```

**Definition for exam**: An indexer allows an instance of a class to be indexed like an array. It is defined using the `this` keyword with square brackets `[]`.

***

## 6. Inheritance and `base` Keyword (Very Important – Asked in 2081)

**Inheritance** allows a class (child) to inherit members from another class (parent) using `:` operator.[3][4]

### `base` Keyword Uses:[5][6]
1. Call base class constructor
2. Call base class method from overridden method

### Example:
```csharp
class Animal  // Base class
{
    public string Name;

    public Animal(string name)
    {
        Name = name;
    }

    public virtual void Sound()
    {
        Console.WriteLine("Animal makes sound");
    }
}

class Dog : Animal  // Derived class
{
    public Dog(string name) : base(name)  // Calling base constructor
    {
    }

    public override void Sound()
    {
        base.Sound();  // Calling base method
        Console.WriteLine("Dog barks");
    }
}

class Program
{
    static void Main()
    {
        Dog d = new Dog("Tommy");
        Console.WriteLine(d.Name);  // Tommy
        d.Sound();
        // Output:
        // Animal makes sound
        // Dog barks
    }
}
```

### Exam Answer: "What is the use of base keyword?"
The `base` keyword is used to:
1. **Call base class constructor**: `public Child() : base(parameters)`
2. **Access base class method**: `base.MethodName()` inside overridden method
3. **Access base class properties/fields** from derived class

***

## 7. Method Hiding vs Method Overriding (Very Important)[7][8]

| Feature | Method Overriding | Method Hiding |
|---------|-------------------|---------------|
| **Keyword** | `override` | `new` |
| **Base method** | Must be `virtual` or `abstract` | Can be any method |
| **Polymorphism** | Yes (runtime) | No (compile-time) |
| **Behavior** | Derived method called via base reference | Base method called via base reference |

### Example:
```csharp
class Parent
{
    public virtual void Display()  // For overriding
    {
        Console.WriteLine("Parent Display");
    }

    public void Show()  // For hiding
    {
        Console.WriteLine("Parent Show");
    }
}

class Child : Parent
{
    // Method Overriding
    public override void Display()
    {
        Console.WriteLine("Child Display");
    }

    // Method Hiding
    public new void Show()
    {
        Console.WriteLine("Child Show");
    }
}

class Program
{
    static void Main()
    {
        Parent obj = new Child();
        obj.Display();  // Output: Child Display (Overriding - runtime)
        obj.Show();     // Output: Parent Show (Hiding - compile-time)
    }
}
```

***

## 8. Polymorphism in Code Extensibility (Asked in 2081)

**Polymorphism** means "many forms" – allows same method to behave differently based on object type.[9][10]

### Two Types:
1. **Compile-time (Static)**: Method overloading
2. **Runtime (Dynamic)**: Method overriding using `virtual` and `override`

### Example for Code Extensibility:
```csharp
abstract class Employee
{
    public string Name { get; set; }
    public abstract double CalculateSalary();  // Abstract method
}

class Manager : Employee
{
    public override double CalculateSalary()
    {
        return 50000 + 10000;  // Base + Bonus
    }
}

class Developer : Employee
{
    public override double CalculateSalary()
    {
        return 40000 + 5000;
    }
}

class Program
{
    static void Main()
    {
        // Polymorphism - same reference, different behavior
        Employee emp1 = new Manager { Name = "Ram" };
        Employee emp2 = new Developer { Name = "Shyam" };

        Console.WriteLine($"{emp1.Name}: {emp1.CalculateSalary()}");  // 60000
        Console.WriteLine($"{emp2.Name}: {emp2.CalculateSalary()}");  // 45000
    }
}
```

**Explanation**: Polymorphism allows extensibility because new employee types can be added without modifying existing code. Each new class just needs to implement `CalculateSalary()`.

***

## 9. Abstract Class vs Sealed Class (Asked in 2080, 2081)[11][12][13]

| Feature | Abstract Class | Sealed Class |
|---------|----------------|--------------|
| **Inheritance** | Must be inherited | Cannot be inherited |
| **Instantiation** | Cannot create object | Can create object |
| **Methods** | Can have abstract + concrete methods | Only concrete methods |
| **Keyword** | `abstract` | `sealed` |
| **Purpose** | Base class template | Prevent inheritance |

### Abstract Class Example:
```csharp
abstract class Shape
{
    public abstract double Area();  // Abstract method (no body)

    public void Display()  // Concrete method
    {
        Console.WriteLine("This is a shape");
    }
}

class Circle : Shape
{
    public double Radius { get; set; }

    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }
}
```

### Sealed Class Example:
```csharp
sealed class DatabaseConnection
{
    public void Connect()
    {
        Console.WriteLine("Connected to database");
    }
}

// ERROR: Cannot inherit from sealed class
// class MyConnection : DatabaseConnection { }
```

***

## 10. Interface (Very Important)[14][15][16]

An **interface** is a contract that defines what a class must implement. It contains only method declarations (no implementation).

### Example:
```csharp
interface IAnimal
{
    void MakeSound();  // No body
}

interface IMovable
{
    void Move();
}

// Implementing multiple interfaces
class Dog : IAnimal, IMovable
{
    public void MakeSound()
    {
        Console.WriteLine("Bark!");
    }

    public void Move()
    {
        Console.WriteLine("Dog runs");
    }
}

class Program
{
    static void Main()
    {
        IAnimal animal = new Dog();
        animal.MakeSound();  // Bark!
    }
}
```

### Interface vs Abstract Class:
| Feature | Interface | Abstract Class |
|---------|-----------|----------------|
| Methods | Only declarations | Both abstract and concrete |
| Multiple inheritance | Supported | Not supported |
| Access modifiers | Public by default | Can have any |
| Fields | Not allowed | Allowed |

***

## 11. Delegates and Events[17][18][19]

### Delegate
A **delegate** is a type-safe function pointer that holds reference to methods.

```csharp
// Declare delegate
public delegate void Notify(string message);

class Program
{
    public static void ShowMessage(string msg)
    {
        Console.WriteLine("Message: " + msg);
    }

    static void Main()
    {
        // Assign method to delegate
        Notify del = ShowMessage;

        // Invoke delegate
        del("Hello from delegate!");  // Message: Hello from delegate!
    }
}
```

### Event
An **event** is based on delegate and allows publisher-subscriber pattern.

```csharp
class Publisher
{
    public delegate void NotifyHandler();
    public event NotifyHandler OnNotify;

    public void DoWork()
    {
        Console.WriteLine("Work completed!");
        OnNotify?.Invoke();  // Raise event
    }
}

class Program
{
    static void Main()
    {
        Publisher pub = new Publisher();

        // Subscribe to event
        pub.OnNotify += () => Console.WriteLine("Subscriber notified!");

        pub.DoWork();
        // Output:
        // Work completed!
        // Subscriber notified!
    }
}
```

***

## 12. Partial Class[20][21]

A **partial class** splits class definition across multiple files. All parts are combined at compile time.

### File1.cs:
```csharp
public partial class Employee
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

### File2.cs:
```csharp
public partial class Employee
{
    public void Display()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}");
    }
}
```

### Usage:
```csharp
Employee emp = new Employee { Name = "Ram", Age = 25 };
emp.Display();  // Name: Ram, Age: 25
```

***

## 13. Collections vs Generics (Asked in 2080)[22][23][24]

| Feature | Non-Generic Collections | Generic Collections |
|---------|------------------------|---------------------|
| **Namespace** | `System.Collections` | `System.Collections.Generic` |
| **Type Safety** | No (stores `object`) | Yes (stores specific type) |
| **Boxing/Unboxing** | Required for value types | Not required |
| **Performance** | Slower | Faster |
| **Examples** | `ArrayList`, `Hashtable` | `List<T>`, `Dictionary<K,V>` |

### Example:
```csharp
// Non-Generic (ArrayList)
ArrayList list1 = new ArrayList();
list1.Add(10);
list1.Add("Hello");  // Mixed types allowed (not safe)

// Generic (List<T>)
List<int> list2 = new List<int>();
list2.Add(10);
list2.Add(20);
// list2.Add("Hello");  // ERROR - type safe
```

***

## 14. File I/O[25][26]

### Read and Write Files:
```csharp
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath = "test.txt";

        // Write to file
        File.WriteAllText(filePath, "Hello, World!");

        // Read from file
        string content = File.ReadAllText(filePath);
        Console.WriteLine(content);  // Hello, World!

        // Append to file
        File.AppendAllText(filePath, "\nNew line added");
    }
}
```

### Using StreamReader/StreamWriter:
```csharp
// Write
using (StreamWriter writer = new StreamWriter("data.txt"))
{
    writer.WriteLine("Line 1");
    writer.WriteLine("Line 2");
}

// Read
using (StreamReader reader = new StreamReader("data.txt"))
{
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        Console.WriteLine(line);
    }
}
```

***

## 15. Try-Catch Exception Handling (Asked in 2081-II)[27][28][29]

### Differentiate Error vs Exception:
| Error | Exception |
|-------|-----------|
| Compile-time problem | Runtime problem |
| Syntax errors, type errors | Division by zero, null reference |
| Detected by compiler | Handled by try-catch |

### Example:
```csharp
class Program
{
    static void Main()
    {
        try
        {
            int a = 10, b = 0;
            int result = a / b;  // Exception occurs
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Cannot divide by zero: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Finally block always executes");
        }
    }
}
```

### Program with Exception (From 2081-II Q1):
```csharp
using System;

class Program
{
    static void Main()
    {
        int[] marks = new int[5];
        string[] subjects = { "Math", "Science", "English", "Nepali", "Computer" };

        try
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Enter marks for {subjects[i]}: ");
                marks[i] = int.Parse(Console.ReadLine());

                // Throw exception if negative or > 100
                if (marks[i] < 0 || marks[i] > 100)
                {
                    throw new Exception($"Invalid marks for {subjects[i]}. Must be 0-100.");
                }
            }

            Console.WriteLine("\nAll marks are valid!");
            foreach (int m in marks)
            {
                Console.WriteLine(m);
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter numeric values only.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
```

***

## 16. Attributes[30][31]

**Attributes** are declarative tags that add metadata to code elements (classes, methods, properties).

### Built-in Attributes:
- `[Obsolete]` – marks deprecated code
- `[Serializable]` – enables serialization
- `[Required]` – validation attribute

### Custom Attribute Example:
```csharp
// Define custom attribute
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorAttribute : Attribute
{
    public string Name { get; }      // Positional parameter
    public string Version { get; set; }  // Named parameter

    public AuthorAttribute(string name)
    {
        Name = name;
        Version = "1.0";
    }
}

// Apply attribute
[Author("John", Version = "2.0")]
class MyClass
{
    [Author("Jane")]
    public void MyMethod() { }
}

// Read attribute using reflection
class Program
{
    static void Main()
    {
        var attrs = typeof(MyClass).GetCustomAttributes(typeof(AuthorAttribute), true);
        foreach (AuthorAttribute attr in attrs)
        {
            Console.WriteLine($"Author: {attr.Name}, Version: {attr.Version}");
        }
    }
}
```

***

# PAST QUESTIONS WITH SOLUTIONS

## Q1: "Define indexers. What is the use of base keyword? How can you apply polymorphism in code extensibility?" [2+3+5] – 2081

See sections 5, 6, and 8 above for complete answers.

***

## Q2: "Differentiate between error and exception. Write a program to take input for any five subjects and throw exception if marks is negative or exceed 100." [2+8] – 2081-II

See section 15 above.

***

## Q3: "Differentiate between collection and generics."  – 2080[32]

See section 13 above.

***

## Q4: "Why do we need abstract class? Explain with example."  – 2080[32]

**Answer:**
Abstract classes are needed when:
1. We want to provide a base template that cannot be instantiated
2. We want to enforce certain methods to be implemented by derived classes
3. We want to share common code among related classes

See section 9 for example.

***

# PROBABLE QUESTIONS FOR EXAM

## Theory Questions (5 marks each):
1. Define indexers with example.
2. What is the use of base keyword? Explain with example.
3. Differentiate between method hiding and method overriding.
4. Differentiate between abstract class and sealed class.
5. Differentiate between interface and abstract class.
6. Differentiate between collections and generics.
7. Explain delegates and events with example.
8. What is partial class? When do we use it?
9. Explain try-catch-finally with example.
10. What are attributes? Explain custom attributes.

## Long Questions (10 marks):
1. Differentiate between error and exception. Write a program to throw exception for invalid input.
2. How can you apply polymorphism in code extensibility? Explain with example.
3. Define indexers. Explain inheritance with base keyword example.

***

# QUICK REVISION CHECKLIST

- [ ] Constructor types (default, parameterized)
- [ ] Properties (get/set, auto-implemented)
- [ ] Indexers (`this[int index]`)
- [ ] `base` keyword (call constructor, call method)
- [ ] Method Overriding (`virtual`/`override`) vs Hiding (`new`)
- [ ] Polymorphism (compile-time, runtime)
- [ ] Abstract class vs Sealed class
- [ ] Interface (multiple inheritance)
- [ ] Delegates and Events
- [ ] Partial class
- [ ] Collections vs Generics (`ArrayList` vs `List<T>`)
- [ ] File I/O (`File.ReadAllText`, `StreamReader`)
- [ ] Try-catch-finally exception handling
- [ ] Attributes (built-in, custom)

**Best of luck for your exam tomorrow!** 🎯

[1](https://www.programiz.com/csharp-programming/indexer)
[2](https://dotnettutorials.net/lesson/indexers-csharp/)
[3](https://www.programiz.com/csharp-programming/inheritance)
[4](https://www.w3schools.com/cs/cs_inheritance.php)
[5](https://www.geeksforgeeks.org/c-sharp/c-sharp-inheritance-in-constructors/)
[6](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/base)
[7](https://dev.to/renukapatil/method-hiding-and-method-overriding-in-c-14b4)
[8](https://www.geeksforgeeks.org/c-sharp/difference-between-method-overriding-and-method-hiding-in-c-sharp/)
[9](https://www.scaler.com/topics/csharp/polymorphism-in-csharp/)
[10](https://www.c-sharpcorner.com/article/concept-of-polymorphism-late-binding-in-c-sharp/)
[11](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/abstract-and-sealed-classes-and-class-members)
[12](https://www.c-sharpcorner.com/article/sealed-class-vs-abstract-class-with-real-time-cases/)
[13](https://www.c-sharpcorner.com/article/difference-between-sealed-class-abstract-class-in-c-sharp/)
[14](https://www.scholarhat.com/tutorial/csharp/a-deep-dive-into-csharp-interface)
[15](https://www.w3schools.com/cs/cs_interface.php)
[16](https://www.geeksforgeeks.org/c-sharp/c-sharp-interface/)
[17](https://dev.to/sebastiandevelops/understanding-c-delegates-and-events-with-real-world-examples-2an6)
[18](https://www.geeksforgeeks.org/c-sharp/delegates-c-sharp/)
[19](https://dotnettutorials.net/lesson/events-delegates-and-event-handler-in-csharp/)
[20](https://www.geeksforgeeks.org/c-sharp/partial-classes-in-c-sharp/)
[21](https://www.programiz.com/csharp-programming/partial-class-and-methods)
[22](https://www.scholarhat.com/tutorial/csharp/difference-between-generics-and-collections-with-example)
[23](https://www.geeksforgeeks.org/c-sharp/collections-in-c-sharp/)
[24](https://dotnettutorials.net/lesson/generic-collections-csharp/)
[25](https://www.w3schools.com/cs/cs_files.php)
[26](https://www.linkedin.com/pulse/understanding-file-io-c-shubham-raj-gwhvc)
[27](https://www.c-sharpcorner.com/article/csharp-try-catch/)
[28](https://www.programiz.com/csharp-programming/exception-handling)
[29](https://www.geeksforgeeks.org/c-sharp/exception-handling-in-c-sharp/)
[30](https://stackoverflow.com/questions/4879521/how-to-create-a-custom-attribute-in-c-sharp)
[31](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/creating-custom-attributes)
[32](https://nou.edu.ng/coursewarecontent/CIT%20421.pdf)
[33](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/images/21375609/32ca69a7-fa05-44ca-b20d-be8ff12108c7/pastQn-2081-II.jpg?AWSAccessKeyId=ASIA2F3EMEYEZATUB6FA&Signature=he9QGiF970pyxOCogcTahyX7rL8%3D&x-amz-security-token=IQoJb3JpZ2luX2VjELX%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FwEaCXVzLWVhc3QtMSJHMEUCIQCGt%2BukNs38hAduYAOar%2FQLURavFJUIpWhH2OxdfaDpFgIgdttwxz8VeziRiKwOg7Y94bHQvmZ2bj6kUS4nsX0umWMq8wQIfhABGgw2OTk3NTMzMDk3MDUiDMYuDrTPLWMiFqcxuSrQBBnR9c8bSmDQlAqAP36JIlR%2BRFJdIGjMzdKCpAHJOuP4%2BVzQzYMb8YWRnjKqrnkLW26p0jlqPtDYd%2B2mN0LOMvBwWWawuvz0aPcWxRZgCkBmehwayWPOlul4y%2F5fPaDBYtqBhXBvQ5aBXnx80%2BCSmswjbDGSZdxTnkFwNZGCY%2B5qCtL2E2ouHxh2HG%2FShAjV5V7T4bqJQt3i11eHjMxgT6pzamTpBxYYuPrIJy5UwcTKd16MjLXiH2qC8txRzbc9%2FdxRfq5ZKemfwFlXFr7mzTMvLs7C18ZjH0Fsw1%2BVxdqJOrnhnrro%2FKQahpnlBDPZ5LlvUh959GThUaoZThQ0lqYEmV0VBE41c0bGQVqA3n6Jfl3J6Rsk%2FtMWnVGRrJCnCwztcfa3rSG%2BEv2mpugYvBE6Hz5Z%2FVRPbUzwPz%2Bx0ryWEfkb%2B%2B9DeafshhV%2F4j8sVEVJI5iuQp%2F6kkdm3D0iqIzyMig0akrVBmw1K1e5tLC7Ry04WzhgnlWEwDmSR5jfC85%2BHWTLIDqeppPgkdBaBwoHykzjl6DuhzOyNL8rCbQYNhUlECi%2Fh22qPwUrOY6xNAfayp1gk%2BhA6J320l8STLKAnAHl2djdNrfLF81pG6lkoTNV6E9kXqnQBy0IHTdTkMGkp1p7ytqWfMn4qn%2FPhkwT2imm3lcbIcvVYsl5z%2FmUJ3605b%2BMQcHKad1h7Wt3rQHcMNNzNs9nzFbZeRLBg87qxFT2f2TKKRU4fh2oBQeGB%2FYhkqhO1bAeUgs1x5auc0lBXi9Ncl8Fzu4%2BPH%2BOCPwwyJOayQY6mAH6Z%2Bpgh4aFQDX3oqAzT3lRO6kWFthSeLl9cji04rp0U6yUfbTpFNIlvC3%2BJlcbfaN7C%2BmzJBmgFtoQ6fC2wWJqP2gCaEBCbnCFfYQVmYmrf1yLhnWGxjhjLDUf977B4tNjZFY9f4EROHmNhJt%2Bb13jaI0ZpjIDGw018Cf387LmTm7JQLK2U8YDtwdZvNW%2B%2FX4WiisuG8JcEA%3D%3D&Expires=1764134871)
[34](https://www.geeksforgeeks.org/c-sharp/c-sharp-indexers/)
[35](https://www.tutorialspoint.com/csharp/csharp_indexers.htm)
[36](https://www.youtube.com/watch?v=Y_Hgu1MJWE0)
[37](https://dotnettutorials.net/lesson/abstract-class-sealed-class-interview-questions-csharp/)
[38](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/)
[39](https://www.tutorialspoint.com/abstract-vs-sealed-classes-vs-class-members-in-chash)
[40](https://www.c-sharpcorner.com/UploadFile/puranindia/indexers-in-C-Sharp/)
[41](https://www.c-sharpcorner.com/Resources/833/base-keyword-in)
[42](https://stackoverflow.com/questions/4756065/difference-between-interface-abstract-class-sealed-class-static-class-and-par)
[43](https://www.tutorialsteacher.com/csharp/csharp-indexer)
[44](https://stackoverflow.com/questions/2644316/what-really-is-the-purpose-of-base-keyword-in-c)
[45](https://learn.microsoft.com/en-us/dotnet/standard/collections/when-to-use-generic-collections)
[46](https://dotnettutorials.net/lesson/function-hiding-csharp/)
[47](https://www.tutorialsteacher.com/csharp/csharp-event)
[48](https://www.linkedin.com/pulse/generics-collections-c-vikram--dcmif)
[49](https://stackoverflow.com/questions/3838553/overriding-vs-method-hiding)
[50](https://www.youtube.com/watch?v=e4G8VgqdaD4)
[51](https://www.tutorialsteacher.com/csharp/csharp-collection)
[52](https://www.c-sharpcorner.com/UploadFile/8911c4/different-between-method-overriding-method-hiding-new-keyw/)
[53](https://www.c-sharpcorner.com/UploadFile/84c85b/delegates-and-events-C-Sharp-net/)
[54](https://www.reddit.com/r/csharp/comments/1kw4e7a/method_overriding_vs_method_hiding/)
[55](https://learn.microsoft.com/en-us/dotnet/standard/events/)
[56](https://www.c-sharpcorner.com/UploadFile/82b15a/generics-and-generic-collections-in-C-Sharp/)
[57](https://stackoverflow.com/questions/20707317/polymorphism-through-extension-methods)
[58](https://www.c-sharpcorner.com/article/creating-and-using-custom-attributes-in-C-Sharp/)
[59](https://www.oreilly.com/library/view/programming-c-4-0/9781449392192/ch04.html)
[60](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements)
[61](https://www.geeksforgeeks.org/c-sharp/custom-attributes-in-c-sharp/)
[62](https://www.w3schools.com/cs/cs_polymorphism.php)
[63](https://ironpdf.com/blog/net-help/try-catch-csharp/)
[64](https://antondevtips.com/blog/creating-custom-attributes-in-csharp)
[65](https://giannisakritidis.com/blog/Extensibility-Mechanics/)
[66](https://www.w3schools.com/cs/cs_exceptions.php)
[67](https://learn.microsoft.com/en-us/dotnet/standard/attributes/writing-custom-attributes)
[68](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism)
[69](https://stackoverflow.com/questions/14973642/how-using-try-catch-for-exception-handling-is-best-practice)
[70](https://dotnettutorials.net/lesson/partial-classes-partial-methods-csharp/)
[71](https://www.codecademy.com/learn/learn-intermediate-c-sharp/modules/c-sharp-file-i-o/cheatsheet)
[72](https://www.programiz.com/csharp-programming/interface)
[73](https://www.c-sharpcorner.com/UploadFile/3d39b4/partial-classes-in-C-Sharp-with-real-example/)
[74](https://stackoverflow.com/questions/7569904/easiest-way-to-read-from-and-write-to-files)
[75](https://dotnettutorials.net/lesson/interface-c-sharp/)
[76](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods)
[77](https://www.geeksforgeeks.org/c-sharp/how-to-read-and-write-a-text-file-in-c-sharp/)
[78](https://www.tutorialsteacher.com/csharp/csharp-interface)
[79](https://stackoverflow.com/questions/3601901/when-is-it-appropriate-to-use-c-sharp-partial-classes)
[80](https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/read-write-text-file)
[81](https://www.youtube.com/watch?v=JeHNRQiA1-0)
[82](https://www.tutorialsteacher.com/csharp/csharp-partial-class)
