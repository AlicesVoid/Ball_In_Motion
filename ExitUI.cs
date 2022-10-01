/* 
Silly Code by AMELIA ROTONDO 
Last Edited: 9/5/2022
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;




public class ExitSignInterface: Form
{private Label title = new Label();
 private Label exitsign = new Label();
 private Button startbutton = new Button();
private Button fastbutton = new Button();
 private Button quitbutton = new Button();
 private Panel headerpanel = new Panel();
 private Graphicpanel exitarrow = new Graphicpanel();
 private Panel controlpanel = new Panel();
 private Size maximumexitsign = new Size(1024,800);
 private Size minimumexitsign = new Size(1024,800);

 private enum Status {Stop, Start, Fast};
 private enum Arrow {Show, Hide}
 private static Status outcome = Status.Stop;
 private static Arrow  arrow =  Arrow.Show;

 private enum Execution_state {Executing, Waiting_to_terminate};             //<== New in version 2.2
 private Execution_state current_state = Execution_state.Executing;
 private static System.Timers.Timer exit_clock = new System.Timers.Timer();  //<== New in version 2.2
 private static System.Timers.Timer arrow_clock = new System.Timers.Timer();  //<== New in version 2.2

private const double fast_clock = 9.0; //Hz
private const double slow_clock = 2.0; //Hz
private const double one_second = 1000.0; //ms
private const double fast_interval = one_second / fast_clock;
private const double slow_interval = one_second / slow_clock;


 public ExitSignInterface()  //Constructor begins here
   {//Set the size of the user interface box.
    MaximumSize = maximumexitsign;
    MinimumSize = minimumexitsign;
    //Initialize text strings
    Text = "Exit Sign by Amelia Rotondo";
    title.Text = "Exit Sign by Amelia Rotondo";
    exitsign.Text = "EXIT... This Way!?";
    startbutton.Text = "Start Flashing!";
    fastbutton.Text = "EVEN FASTER!";
    quitbutton.Text = "Quit... I Guess-";
    
    //Set sizes
    Size = new Size(400,240);
    title.Size = new Size(800,44);
    exitsign.Size = new Size(400,36);
    startbutton.Size = new Size(240,100);
    fastbutton.Size = new Size(240,100);
    quitbutton.Size = new Size(240,100);
    headerpanel.Size = new Size(1024,200);
    exitarrow.Size = new Size(1024,400);
    controlpanel.Size = new Size(1024,200);
    
    //Set colors
    headerpanel.BackColor = Color.LightPink;
    exitarrow.BackColor = Color.Aquamarine;
    controlpanel.BackColor = Color.Crimson;
    startbutton.BackColor = Color.Yellow;
    fastbutton.BackColor = Color.LightSalmon;
    quitbutton.BackColor = Color.Cyan;
    //quitbutton.BackColor = Color.FromArgb(0xA1,0xD4,0xAA);
    
    //Set fonts
    title.Font = new Font("Impact",33,FontStyle.Bold);
    exitsign.Font = new Font("Comic Sans MS", 30,FontStyle.Regular);
    startbutton.Font = new Font("Comic Sans MS",15,FontStyle.Regular);
    fastbutton.Font = new Font("Comic Sans MS",15,FontStyle.Bold);
    quitbutton.Font = new Font("Comic Sans MS",15,FontStyle.Italic);
    
    //Set position of text within a label
    title.TextAlign = ContentAlignment.MiddleCenter;
    exitsign.TextAlign = ContentAlignment.MiddleLeft;

    //Set locations
    headerpanel.Location = new Point(0,0);
    title.Location = new Point(125,69);
    exitsign.Location = new Point(100,60);
    startbutton.Location = new Point(130,50);
    fastbutton.Location = new Point(400,50);
    quitbutton.Location = new Point(720,50);
    headerpanel.Location = new Point(0,0);
    exitarrow.Location = new Point(0,200);
    controlpanel.Location = new Point(0,600);

    //Add controls to the form
    Controls.Add(headerpanel);
    headerpanel.Controls.Add(title);
    Controls.Add(exitarrow);
    exitarrow.Controls.Add(exitsign);
    Controls.Add(controlpanel);
    controlpanel.Controls.Add(startbutton);
    controlpanel.Controls.Add(fastbutton);
    controlpanel.Controls.Add(quitbutton);

    //Register the event handler.  In this case each button has an event handler, but no other 
    //controls have event handlers.
    startbutton.Click += new EventHandler(startflash);
    fastbutton.Click += new EventHandler(fastflash);
    quitbutton.Click += new EventHandler(stoprun);  //The '+' is required.

    //Configure the clock that controls the shutdown      //<== New in version 2.2
    exit_clock.Enabled = false;     //Clock is turned off at start program execution.
    exit_clock.Interval = 7500;     //7500ms = 7.5seconds.  Clock will tick at intervals of 7.5 seconds
    exit_clock.Elapsed += new ElapsedEventHandler(shutdown);   //Attach a method to the clock.

    arrow_clock.Enabled = false;     //Clock is turned off at start program execution.
    arrow_clock.Interval = slow_interval;     // 2.0 Hz
    arrow_clock.Elapsed += new ElapsedEventHandler(arrowSwap);   //Attach a method to the clock.


    //Open this user interface window in the center of the display.
    CenterToScreen();

   }//End of constructor ExitSignInterface
   
// Clock Method to do the Arrow Stuff
protected void arrowSwap(Object sender, EventArgs events)
{
      switch (arrow)
      {
            case Arrow.Show:
                  exitarrow.Invalidate();
                  arrow = Arrow.Hide;
                  break;
            case Arrow.Hide:
                  arrow = Arrow.Show;
                  exitarrow.Refresh();
                  break;
      }

}

 //Method to execute when the Hide Button is Clicked (Hides the Exit Sign)
 protected void startflash(Object sender, EventArgs events)
   {
    switch(outcome)
     {case Status.Stop: 
            outcome = Status.Start;
            arrow_clock.Interval= slow_interval;     // 2.0 Hz
            arrow_clock.Enabled = true;
            startbutton.Text = "Pause the Flash?-";
           break;
      case Status.Start: 
            outcome = Status.Stop;
            arrow_clock.Enabled = false;
            arrow = Arrow.Show;
            exitarrow.Refresh();
            startbutton.Text = "Resume the Flash!";
           break;
      case Status.Fast: 
            outcome = Status.Stop;
            arrow_clock.Enabled = false;
            startbutton.Text = "Resume the Flash!";
           break;
     }

    //exitarrow.Invalidate();
   }//End of startflash

 protected void fastflash(Object sender, EventArgs events)
   {
    switch(outcome)
     {
      case Status.Stop: 
            outcome = Status.Fast;
            arrow_clock.Interval= fast_interval;     // 10.0 Hz
            arrow_clock.Enabled = true;
            fastbutton.Text = "SLOW DOWN!!!";
           break;
      case Status.Start: 
            outcome = Status.Fast;
            arrow_clock.Interval= fast_interval;     // 10.0 Hz
            arrow_clock.Enabled = true;
            fastbutton.Text = "SLOW DOWN!!!";
           break;
      case Status.Fast: 
            outcome = Status.Stop;
            arrow_clock.Interval= slow_interval;     // 2.0 Hz
            arrow_clock.Enabled = true;
            fastbutton.Text = "Speed up again...?";
           break;
     }
    
   }//End of fastflash

//Method to Exit and LEAVE the Program (waits 2.5 seconds before closing)
protected void stoprun(Object sender, EventArgs events)
   {switch(current_state)
    {case Execution_state.Executing:
             exit_clock.Interval= 2500;     //2500ms = 2.5 seconds
             exit_clock.Enabled = true;
             quitbutton.Text = "Are You Sure!?";
             current_state = Execution_state.Waiting_to_terminate;
             break;
     case Execution_state.Waiting_to_terminate:
             exit_clock.Enabled = false;
             quitbutton.Text = "Quit Again...";
             current_state = Execution_state.Executing;
             break;
     }//End of switch statement
  }//End of method stoprun.  In C Sharp language "method" means "function".

protected void shutdown(System.Object sender, EventArgs even)                   //<== Revised for version 2.2
    {//This function is called when the clock makes its first "tick", 
     //which occurs 3.5 seconds after the clock starts.
     Close();       //That means close the main user interface window.
    }//End of method shutdown


// Method to show a whole bunch of tiny funny red dots in the shape of an Exit Sign
 public class Graphicpanel: Panel
 {private Brush paint_brush = new SolidBrush(System.Drawing.Color.Red);
  public Graphicpanel() 
        {Console.WriteLine("A graphic enabled panel was created");}  //Constructor writes to terminal

//Draws the Arrow
  protected override void OnPaint(PaintEventArgs ee)
  {  
      Graphics graph = ee.Graphics;
      switch(arrow)
      {
            case Arrow.Show: 

            Console.WriteLine("Arrow is Shown!");

            graph.FillEllipse(paint_brush,465,215,25,25);
            graph.FillEllipse(paint_brush,495,210,25,25);
            graph.FillEllipse(paint_brush,525,205,25,25);
            graph.FillEllipse(paint_brush,555,210,25,25);
            graph.FillEllipse(paint_brush,585,215,25,25);
            graph.FillEllipse(paint_brush,615,210,25,25);
            graph.FillEllipse(paint_brush,645,205,25,25);
            graph.FillEllipse(paint_brush,675,210,25,25);
            graph.FillEllipse(paint_brush,705,215,25,25);
            graph.FillEllipse(paint_brush,735,210,25,25);
            graph.FillEllipse(paint_brush,765,205,25,25);
            graph.FillEllipse(paint_brush,795,210,25,25);
            graph.FillEllipse(paint_brush,825,210,25,25);
            
            //The Bottom Part of the Arrow
            graph.FillEllipse(paint_brush,805,230,25,25);
            graph.FillEllipse(paint_brush,785,250,25,25);
            graph.FillEllipse(paint_brush,765,270,25,25);
            graph.FillEllipse(paint_brush,745,290,25,25);
            graph.FillEllipse(paint_brush,725,310,25,25);
            
            //The Top Part of the Arrow
            graph.FillEllipse(paint_brush,805,190,25,25);
            graph.FillEllipse(paint_brush,785,170,25,25);
            graph.FillEllipse(paint_brush,765,150,25,25);
            graph.FillEllipse(paint_brush,745,130,25,25);
            graph.FillEllipse(paint_brush,725,110,25,25);
            
           break;

           case Arrow.Hide: 

            Console.WriteLine("Arrow is Hidden...");
            
           break;
      }
      base.OnPaint(ee);

  }//End of OnPaint

 }//End of class Graphicpanel


}//End of clas ExitSignInterface