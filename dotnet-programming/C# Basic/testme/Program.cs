
Console.WriteLine("Hello, World!");
Console.WriteLine(3 + 4);

string fname = "Hari";

Console.WriteLine(fname);

const double pi = 3.17;

Console.WriteLine(pi);

// int num = 2;


string firstName = "Abi";
string lastName = "Fix You";

Console.WriteLine("Name is: " + firstName + " " + lastName);

int a = 10, b = 20;

string name = "Test User";

Console.WriteLine(name);
// int sum = a + b;

Console.WriteLine("The sum is " + (a + b));


// Dotnet has implicit casting. if i have something, i can change that automatically to another



double mynum = 3.222;


Console.WriteLine(mynum);

double num1 = 0.1;
double num2 = 0.2;

if (num1 + num2 == 0.3)
{
    Console.WriteLine($"{a} + {b} equals 0.3");
}
else
{
    Console.WriteLine($"{a} + {b} equals 0.3");
}


// Console.WriteLine("Enter your username: ");
// string userName = Console.ReadLine();

// Console.WriteLine("Your name is: " + userName);

// // Console always take input as the string so need to convert using the type implicit

// Console.WriteLine("Enter the number");
// int num = Convert.ToInt32(Console.ReadLine());

// Console.WriteLine("You just entered: " + num);

string[] nameC = new String[5];

nameC = ["Ram", "Hari"];
Console.WriteLine(nameC[1]);

string[] cars = { "Volvo", "BMW", "Ford", "Mazda" };
Array.Sort(cars);
foreach (string i in cars)
{
    Console.WriteLine(i);
}

int[,] numbers = { { 1, 4, 2 }, { 3, 6, 8 } };

foreach (int i in numbers)
{
    Console.WriteLine(i);
}

// Method Overloading

// static int PlusMethod(int x, int y)
// {
//     return x + y;
// }

// static double PlusMethod(double x, double y)
// {
//     return x + y;
// }


// int val1 = PlusMethod(1, 2);

// double val2 = PlusMethod(2.44, 2.4);

// Console.WriteLine("Int: " + val1);
// Console.WriteLine("Float: " + val2);

// This above works only inside the class.
