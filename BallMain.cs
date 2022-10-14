/* 
Silly Code by AMELIA ROTONDO 
Last Edited: 9/5/2022
*/

using System;
//using System.Drawing;
using System.Windows.Forms;  //Needed for "Application" on next to last line of Main
public class BallMain
{  static void Main(string[] args)
   {
      System.Console.WriteLine("Welcome to the Main method of the Amelia Rotondo Ball-In-Motion UI!");
      BallMotionInterface ball = new BallMotionInterface();
      Application.Run(ball);
      System.Console.WriteLine("Main method will now shutdown.");
   }//End of Main
}//End of BallMain