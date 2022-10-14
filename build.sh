#!/bin/bash
#In the official documemtation the line above always has to be the first line of any script file.  But, students have 
#told me that script files work correctly without that first line.

#Ruler:==1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**
#Author: Amelia Rotondo
#Course: CPSC223N
#Semester: Fall 2022
#Assignment: 2
#Due: September 18, 2022.
#This is the script file that is part of the program Exit Sign

#This is a bash shell script to be used for compiling, linking, and executing the C sharp files of this assignment.
#Execute this file by navigating the terminal window to the folder where this file resides, and then enter the command: ./build.sh

#System requirements: 
#  A Linux system with BASH shell (in a terminal window).
#  The mono compiler must be installed.  If not installed run the command "sudo apt install mono-complete" without quotes.
#  The three source files and this script file must be in the same folder.
#  This file, build.sh, must have execute permission.  Go to the properties window of build.sh and put a check in the 
#  permission to execute box.

echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

#echo Compile Fibonaccilogic.cs to create the file: Fibonaccilogic.dll
#mcs -target:library -out:Fibonaccilogic.dll Fibonaccilogic.cs

echo Compile BallUI.cs to create the file: BallUI.dll
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:BallUI.dll BallUI.cs

echo Compile BallMain.cs and link the two previously created dll files to create an executable file. 
mcs -r:System -r:System.Windows.Forms -r:BallUI.dll -out:BallInMotion.exe BallMain.cs

echo View the list of files in the current folder
ls -l

echo Run the Assignment 1 program.
./BallInMotion.exe

echo The script has terminated.












