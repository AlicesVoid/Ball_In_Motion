/* 
Silly Code by AMELIA ROTONDO 
Last Edited: 9/5/2022
*/

using System;
//using System.Drawing;
using System.Windows.Forms;  //Needed for "Application" on next to last line of Main
public class ExitMain
{  static void Main(string[] args)
   {System.Console.WriteLine("Welcome to the Main method of the Amelia Rotondo Exit Sign!");
    ExitSignInterface exitapp = new ExitSignInterface();
    Application.Run(exitapp);
    System.Console.WriteLine("Main method will now shutdown.");
   }//End of Main
}//End of Fibonaccimain