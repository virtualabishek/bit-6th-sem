Console.WriteLine("Hello C sharp");

// Declare and use varaibles

string myNickName = "Virtual Abishek";
Console.WriteLine( "Helo" + myNickName);
Console.WriteLine($"Hello {myNickName}");

string myName = "Abishek Neupane";
Console.WriteLine($"My name is: {myName}, but you can call me: {myNickName}");
Console.WriteLine($"Fun fact, my name has: {myName.Length} length and my nickname has: {myNickName.Length} length.");


// Trimming

string greeting = "            Namaste         ";
Console.WriteLine($"[{greeting}]");
string trimmedGreeting = greeting.TrimStart();
Console.WriteLine($"[{trimmedGreeting}]");
trimmedGreeting = greeting.TrimEnd();
Console.WriteLine($"[{trimmedGreeting}]");
trimmedGreeting = greeting.Trim();
Console.WriteLine($"[{trimmedGreeting}]");

// Replacing

string peace = "Peace persists";
Console.WriteLine(peace);
peace = peace.Replace("Peace", "War");
Console.WriteLine(peace);

// ToLower(), ToUpper()
Console.WriteLine(peace.ToLower());
Console.WriteLine(peace.ToUpper());

// Serching, returns boolean
string favLyrics = "look at the stars how they sign for you.";
Console.WriteLine(favLyrics.Contains("stars"));
Console.WriteLine(favLyrics.Contains("yellow"));
