using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace HelloWorld
{

// abstract class Vehicle // abstract class
// {
//     public abstract void Moving(); // abstract method
//     public void Honk()
//     {
//         Console.WriteLine("BEEP...");
//     }
// }

// class Car : Vehicle // inheritance 
// {
//         public override void Moving() // overriding method of parent class
//       {
//        Console.WriteLine("This car is Moving"); 
//       }
//     public string? Name { get; set; }
// }




// polymorphism: having many forms, uses virtual keyword in base class and override in derived
// class Vehicle
//   {
//     public virtual void Status()
//     {
//       Console.WriteLine("Vehicle is Moving");
//     }
//   }
// class Car: Vehicle
//   {
//         public override void Status()
//         {
//             //base.Status();
//             Console.WriteLine("Car is Moving to india");
//         }
//   }
//   class Boat: Vehicle
//   {
//     //  public override void Status()
//     //     {
//     //         base.Status(); // can add this method but output will be same
//     //     }
//   }
//   class Van: Vehicle
//   {
//      public override void Status()
//         {
//             //base.Status();
//             Console.WriteLine("Van is Moving on the Jungle");
//         }
//   }



// Interface code  multiple inheritance 
// interface Iprey
//   {
//     void flee();
//   }
// interface Ipreditor
//   {
//     void hunt();
//   }
// class Rabit: Iprey
//   {
//     public void flee()
//     {
//       Console.WriteLine("The Rabbit is a prey");
//     }
//   }
// class Hawk:Ipreditor
//   {
//     public void hunt()
//     {
//       Console.WriteLine("The Hawk is hunting");
//     }
//   }
// class Jackal : Iprey,Ipreditor  // multiple inheritance 
//   {
//     public void flee()
//     {
//       Console.WriteLine("jackal  flee when he get hunted");
//     }
//     public void hunt()
//     {
//       Console.WriteLine("Jackal hunts with their family");
//     }
//   }



// Array of objects 
// public class Students
//   {
//     // properties
//     public string Name { get ; set ;}  
//     public int Age { get ; set ;}
//      // constructor
//     public Students(string name, int age)
//     {
//       Name = name;
//       Age  = age;
//     }
//     public void display()
//     {
//       Console.WriteLine("The name of Student is: "+Name);
//       Console.WriteLine("The age of student: "+Age);
//     }
//   }





//Objects as arguments
// public class Car
// {
//     public string model;
//     public string color;
//     public Car(string Model, string Color)
//     {
//      this.model=Model;
//      this.color=Color;
//     }
// }

  class Program
  {

    // objects as an arguments copyting the car1. to car2 data type is Car
    // static Car Copy(Car car)
    // {
    //   return new Car(car.model, car.color);
    // }



      static void Print(object x)
      {
          Console.WriteLine(x);
      }
      static string? Userinput()
    {
      // the ? symbol allows the null value return 
       return Console.ReadLine();
       // we can use ! symbol to promish it will not be null
       //return Console.ReadLine()!;
       // and remove the ? from data type return 
    }
    static void Main(string[] args)
    {
      /*Object-Oriented-Programming: is about creating objects that contains both data and methods 
      - Oops make program faster and easier to execute
      -It provides clear structure for program

      -Class and Objects are two main aspects of OOPs.
      */

      // abstract classes and inheritance 
      // Car myobj= new Car();
      //  myobj.Name="Tata";
      //  Print("The name of the car: "+myobj.Name);
      //  Print("The horn sounds like: ");  myobj.Honk();
      //  Print("The status of the car: "); myobj.Moving();


      // polymorphism , will create array of objects since they all are related inherited by 
      // Vehicle class
      // Car car= new Car();
      // Boat boat = new Boat();
      // Van van = new Van();
      // // array of objects for these 3 classes 
      // Vehicle[] vehicles= {car,van,boat};
      // foreach(Vehicle v in vehicles)
      //   {
      //     v.Status();
      //   }



      // Interface: the classes are completely abstract, means the properties and methods
      // cannot contains body. no virutal , abstract , override keyword is required
      // by default the class and methods ,property are abstract and public .
      // it is called implementation of a class not inheritance here
      // we can achieve multiple inheritance with interfaces.

      // Rabit rabit= new Rabit();
      // rabit.flee();
      // Hawk hawk = new Hawk();
      // hawk.hunt();
      // Jackal jackal= new Jackal(); // this class will implement both interfaces method 
      // jackal.flee(); 
      // jackal.hunt();






      // Array of objects 
      // try
      // {
      //     Students [] students= new Students[3];
      //     students[0]= new Students("Vinay", 22);
      //     students[1]= new Students("Shweta", 24);
      //     students[2]= new Students("Vivek", 17);
      //     Print("Student Details");
      //     foreach(Students st in  students)
      //         {
      //           st.display();
      //         }
      // }
      // catch(Exception e)
      // {
      //   Print("The error is Caught: "+e.Message);
      // }
      // finally
      // {
      //   Print("The details are here");
      // }



     // Objects as an arguments copy one object to other of same class 
    //  Car car1= new Car("Mustang","Red");
    //  Print("This is car1 objects model : "+car1.model+" color: "+car1.color);
    //  //copyting car1 object to car2 
    //  Car car2= Copy(car1);
    //  car2.color="Silver";
    //  Print("This is car2 objects model : "+car2.model+"  Color :"+car2.color);






    //   Console.WriteLine("I changed this line in the konsole using nano\n");

    //   // Variables and data types
    //   // This works exactly same as C++

    //  int x = 555, y = 432, z = x + y ;
    //  string s="Vinay Sen";

    //   Print($"{x} {y} Their sum = {z} and name is {s}");
    //   Console.WriteLine(x+" "+y +"Their sum ="+z+ " and name  is "+s);

    //   // smaller to larger data types , just assign 
    //    int myint=5;
    //   double mydouble=myint;
    //   Print("this is int: "+ myint);
    //   Console.WriteLine($"This is double :{mydouble}");

    //   //larger to smaller size we have to do explicitly

    //   double myd= 52.25;
    //   int    myi= (int)myd;
    //   string mys="64455";
    //   string val="true";
    //   bool   tal=Convert.ToBoolean(val);

    //  Print("converting double to string :"+Convert.ToString(myd));
    //  Print($"myd double to int : {myi}");

    //  Print("string to integer: "+Convert.ToInt64(mys).GetType());
    //  Print("data type of mys: "+mys.GetType());
    //  Print(tal+" "+ tal.GetType());
     // Console.WriteLine(typeof(string)+" "+typeof(int)+" "+typeof(double)+" "+ typeof(long));




      // Taking User input:- 
      // Print("Enter Your Name : ");
      // string? name = Userinput();
      // Print($"your name is {name}");
      // Print("Enter Your Age : ");
      // int age=Convert.ToInt32(Userinput());
      // Print($"And your age is : {age}");




      // Math class in c# is has methods that allows us to perform operations on numbers.
      // int j=25;
      // double k= 9.99;
      // Print(Math.Round(k));
      // Print(Math.Sqrt(j));
      // Print(Math.Abs(34-j));
      // Print(Math.Min(j,30));

      // // generating Random numbers using built-int functions; Need a class
      // Random rand = new(); // or new Random();
      // //  csharp_style_implicit_object_creation_when_type_is_apparent = false; 'Random rand=  new Random()';
      // j= rand.Next(1,j);
      // Print("the random number is: "+j);
      // k= rand.NextDouble();
      // Print("the double Random is: "+k);



      // String Methods: a string var contains a collection of characters surrounded by double quotes. 
      // txt.Length  txt.ToUpper   txt.ToLower   txt.Replace('' with, '' char)
    //   string  myname = "vinay";
    //   string  lastname = "sen" ;
    //   string  fullname= myname+" "+lastname;

    //   Print("Lenth of my name is : "+fullname.Length);
    //   Print(fullname.Length.GetType()); // returns Int32
    //   Print("my full using ToUpper is : "+ fullname.ToUpper());

    //   string phonenumber="70-7001-04-04";
    //   Print("My number as in database: "+phonenumber);
    //   Print("My number in case of use: "+phonenumber.Replace('-',' '));
    //   Print("find the index of 0 : "+phonenumber.IndexOf("0"));
       



    // Array: is variable which can store multiple values of same data types, fixed size. 
    //   string[] cars= {"bmw", "Tata", "Mustang"};
      //   string[] cars= new string[4];
      //   cars[0]="BMW";
      //   cars[1]="Tesla";
      //   cars[2]="Tata";
      //   cars[3]="Mahindra";
      //   for(int i=0; i<cars.Length; i++)
      // {
      //   Print(cars[i]);
      // }
      //   Print(cars);
      //   int[] numbers;
      //   numbers=new int[5];
      //   numbers[0]=1; numbers[3]=100;
      //   for (int i = 0; i < numbers.Length; i++)
      //   {
      //       if (numbers[i] == numbers[i] % 3)
      //           continue;
      //       else
      //           numbers[i] = i;
      //   }

      //   foreach(int it in numbers) // read-only loop , contains a temp copy of each element
      //   // 
      //   {
      //   Console.Write(it+" ");
      //   }


      /*Methods: methods performs a block of code which only run when the method is invoked
       methods takes arguments to invoked and use it as parameters */
      // params keywords: a method's paramter that takes variable numbers of arguments:
      // the paramter type must be a signle : one dimensional array

      // double deal= Checkout(100.25,45.65,128); //params keywords
      // Print("Total deal of : "+deal);




      /* Exception Error: When coding errors made by programmers, wrong input by the use
       or any other unforeseeable things. 
       -try{} block: statement allows us to define a block of code tested for errors while it
                     is executed .
       -catch(){}: statement allows us to define a block of code to be executed if an error
                   occurs in try block
       -finally{}: statement always executes regardless if an exception is caught or not
       */
      // double x; double y; double result;
      // try
      // {
      //   Print("Enter the divident: ");
      //   x=Convert.ToDouble(Userinput());
      //   Print("Enter the Divisor :");
      //   y=Convert.ToDouble(Userinput());
      //   result= x/y;
      //   Print("Quentent : "+result);
      // }
      // catch(FormatException e)
      // {
      //   Print("Please Enter valid input"+e.Message);
      // }
      // catch(DivideByZeroException e)
      // {
      //   Print("You cannot divide by Zero 0 idiot: "+e.Message);
      // }
      // catch(Exception e)
      // {
      //   Print("Something Went Wrong 404"+e);
      // }
      // finally
      // {
      //   Print("Excuted or not ");
      // }




      // List : is a data structure that represents a list of objectss that can be acesses by index
      //        List are similar to array but dynamic in nature.
      // using System.Collection.Generic; to use list.
      // List <string> ls= new List<string>();
      // for(int i=0; i<5; i++)
      // {
      //   string? value=Userinput();
      //   ls.Add(value);
      // }
      // ls.Add("Vinay");
      // Print("The size of List: "+ls.Count());
      // Print("the fisrt index of Vinay : "+ls.IndexOf("Vinay"));
      // Print("the last index of Vinay : "+ls.LastIndexOf("Vinay"));
      // Print("The list contains Ravi or not : "+ls.Contains("Ravi"));
      // ls.Sort();
      // Print("Sorted List of Names: ");
      // foreach(String st in ls)
      // {
      //   Console.Write(st+" ");
      // }
      // Print("\n");
      // Print("Reverse List: ");
      // ls.Reverse();
      // foreach(String st in ls)
      // {
      //   Console.Write(st+" ");
      // }
      // Print("\n");
      // ls.Remove("Vinay"); // removed vinay from list;
      // Print("The list contains Vinay or not : "+ls.Contains("Vinay"));
      // Print("Clear the list");
      // ls.Clear();
      // Print("Now size of list: "+ls.Count());








      // getter and setter : add security to the fields by encapsulation. 
      // They're accessors found withing properties {get; set;}
      // properties: combine aspects of both field and method
      // Car car = new Car(490);
      // car.Speed=390;
      // Print("the speed of car is : "+car.Speed);

    }


    // getter and setter  class 
    // public class Car
    // {
    //   private int speed;
    //   public Car(int speed) //constructor
    //   {
    //     Speed=speed;
    //   }
    //   //when no logic required
    //   public int Speed
    //   {
    //     get;set;
    //   }
    //   // public int Speed // when required 
    //   // {
    //   //   get { return speed; }
    //   //   set
    //   //   {
    //   //     if(value>500)
    //   //     {
    //   //       speed= 400;
    //   //     }
    //   //     else
    //   //     {
    //   //       speed=value;
    //   //     }
    //   //   }
    //   // }
    // }




    // static double Checkout(params double[]prices)
    // {
    //   double total=0;
    //   foreach(double price in prices)
    //   {
    //     total+=price;
    //   }
    //   return total; 
    // }
  }
}
