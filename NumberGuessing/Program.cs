using System;
using System.Net;

namespace Numberguess
{
    class Program
    {
        static void Print(object x)
        {
            Console.WriteLine(x);
        }
        static string Userinput()
        {
            return Console.ReadLine()!;
        }

        static void Main(string [] args)
        {
            Print("Welcome to the Number Guessing Game");
            Random rand= new ();
            bool playagain=true;
            int guess, number, guesses;
            string responsess;

            while(playagain)
            {
                guess=0;
                guesses=0; 
                responsess ="";
                number=rand.Next(1,101);
                
                while(guess!=number)
                {
                    Print("\nGuess the number between 1 to 100 :");
                    guess=Convert.ToInt32(Userinput());
                    if(guess<number)
                    {
                        Print("Guess :"+guess+ "\n"+"Your guess is too low");
                    }
                    else if(guess>number)
                    {
                        Print("Guess :"+guess+ "\n"+guess +" guess is too high");
                    }
                    guesses++;
                }
                Print("Congratulations!! You Winn");
                Print("You Guess is it Right! : "+guess);
                Print("Total Guess : "+guesses);
                Print("Want to Play Again Press (Y/N) ?");
                responsess=Userinput();
                responsess=responsess.ToUpper();
                if(responsess=="Y")
                playagain=true;
                else
                {
                    Print("Thanks For Playing");
                    playagain=false;
                }
            }
        }
    }
}
