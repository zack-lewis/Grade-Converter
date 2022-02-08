using System;
using System.Collections.Generic;

namespace GradeConverter
{
    class Program
    {
        // GLOBAL VARIABLES
        const double upperLimit = 110;

        static void Main(string[] args)
        {
            // VARIABLES
            List<double> grades = new List<double>();
            int numGrades = 0;
            string name = "";

            // Greet the user, get their name
            name = greetUser();

            // Get the number of grades to input
            numGrades = getNumGrades();
            
            // loop that many times
            Console.WriteLine("We can do that. Let's get them input into our system. Enter them below.");
            for(int i = 0; i < numGrades; i++) {
                int p = i + 1;
                double input = gradePrompt(p.ToString());
                grades = addGradeToList(input, grades);
            }
            
            string additionalInput;
            bool additional = false;

            do {
                Console.WriteLine("Do you have any more grades to put in? (yes/NO)");

                additionalInput = Console.ReadLine();
                if(additionalInput == "") {
                    additional = false;
                }
                else if(additionalInput.ToUpper() == "YES" || additionalInput.ToUpper() == "Y") {
                    int moreGrades = getNumGrades(true);
                    for(int i = 0; i < moreGrades; i++) {
                        int p = i + 1;
                        double input = gradePrompt(p.ToString());
                        grades = addGradeToList(input, grades);
                    }
                    additional = true;
                }
                else if(additionalInput.ToUpper() == "NO" || additionalInput.ToUpper() == "N") {
                    additional = false;
                }
                else { //if invalid answer, loop again. 
                    additional = true;
                }
            } while(additional == true);

            // Ending the program, show the stats
            showStats(grades);

            // Just trying to make them feel like we care. 
            // Obviously we don't, otherwise this app would be waaaay more intuitive and pretty
            Console.WriteLine($"Thanks {name} for stopping by! See you again soon!");

        }

        // gradePrompt
        // Summary: Prompt user for a grade and return it as a double
        // Arguments: 
        //      string prompt - Prompt to use for user input
        // Returns: double grade - Grade from user 
        private static double gradePrompt(string prompt) {
            
                // temp string for input
                string temp = "";
                double grade = 0;

                // ask user for input
                Console.Write($"{prompt}: ");
                temp = Console.ReadLine();

                try {
                    // if empty, reprompt
                    if (temp == "") {
                        throw new ArgumentNullException();
                    }
                    
                    // if non-numeric, throw exception
                    // grade = Int32.Parse(temp);
                    grade = Double.Parse(temp);

                    // if outside of bounds, throw exception
                    if (grade > upperLimit || grade < 0) {
                        throw new ArgumentOutOfRangeException();
                    }
                     
                }
                catch (FormatException) {
                    Console.WriteLine("Sorry, bad format. Let's try that again.");
                    grade = gradePrompt(prompt);
                }
                catch (ArgumentNullException) {
                    Console.WriteLine("Grade cannot be empty!");
                    grade = gradePrompt(prompt);
                }
                catch (ArgumentOutOfRangeException) {
                    Console.WriteLine($"Sorry! Grade cannot be below 0 or above {upperLimit}");
                    grade = gradePrompt(prompt);
                }
                catch (Exception) {
                    throw;
                }

                return grade;
        }

        // addGradeToList
        // Summary: Take a grade and a list, add the grade to the list, return the list
        // Arguments: 
        //      double grade - grade to add to list
        //      List<double> list - initial list in which to add grade
        // Returns: List<double> list - List with grade added
        private static List<double> addGradeToList(double grade, List<double> list) {
            try {
                list.Add(grade);
            }
            catch (Exception) {
                throw;
            }

            return list;
        }

        // percentToLetterGrade
        // Summary: Take Numeric grade (as a percentage) and return a Letter grade (whole letters only)
        // Arguments:
        //      double grade - Grade to convert from Percent to Letter
        // Return: string letterGrade - Equivilent letter grade in string format
        private static string percentToLetterGrade(double grade) {
            string letterGrade = "";
            if(grade <= 60) {
                letterGrade = "F";
            }
            else if(grade <= 70) {
                letterGrade = "D";
            }
            else if(grade <= 80) {
                letterGrade = "C";
            }
            else if(grade <= 90) {
                letterGrade = "B";
            }
            else if(grade <= 100) {
                letterGrade = "A";
            }
            else if(grade > 100 && grade <= upperLimit) {
                letterGrade = "A+";
            }
            else {
                letterGrade = "U"; // Return Unsatisfactory if all else fails
            }

            return letterGrade;
        }
        
        // listGrades
        // Summary: Take a list of grades and output in pretty format
        // Arguments:
        //      List<double> list - list of grades to display
        // Returns: none
        private static void listGrades(List<double> list) {
            int count = list.Count;
            for(int i = 0; i < count; i++) {
                string letter = percentToLetterGrade(list[i]);
                Console.WriteLine($"{i+1}: {list[i]}  ({letter})");
            }
        }

        // highestGrade
        // Arguments:
        //      List<double> list - list of grades to search
        // Returns: double highest - Highest grade from list as a percentage 
        private static double highestGrade(List<double> list) {
            double highest = 0;
            foreach(double g in list) {
                if(g > highest) {
                    highest = g;
                }
            }
            return highest;
        }
        
        // lowestGrade
        // Arguments:
        //      List<double> list - list of grades to search
        // Returns: double lowest - Lowest grade from list as a percentage 
        private static double lowestGrade(List<double> list) {
            double lowest = 999;
            foreach(double g in list) {
                if(g < lowest) {
                    lowest = g;
                }
            }
            return lowest;
        }

        // greetUser
        // Arguments:
        //      none
        // Returns: string name - Full name of user
        private static string greetUser() {
            string firstName;
            string lastName;
            string name;

            // ask user name
            Console.WriteLine("Welcome to the Mighty Grade Converter. May I get your first and last name?");
            Console.Write("First Name: ");
            firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            lastName = Console.ReadLine();
            Console.WriteLine("\n\n");

            // welcome user
            Console.WriteLine($"Wonderful! Welcome, {firstName} {lastName}, to the Greated Converter of Grades this side of the Mississippi!");

            name = firstName + " " + lastName;
            return name;
        }

        // getNumGrades
        // Arguments:
        //      bool add - if this is the initial input or additional. 
        //          defaults to F, if true changes user prompt
        // Returns: int num - Number of grades user wants to input
        private static int getNumGrades(bool add = false) {
            int num = 0;
            // ask user number of grades to input
            if (add == true) {
                Console.Write("How many grades do you want to add? ");
            }
            else {
                Console.Write("How many grades do you want to input on this fine day? ");
            }
            string numGradesRead = Console.ReadLine();
            try {
                num = Int32.Parse(numGradesRead);
                if(num <= 0) {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (ArgumentOutOfRangeException) {
                Console.WriteLine("Woooah there! We need the number of grades to enter!");
                num = getNumGrades(add);
            }
            catch (Exception) {
                throw;
            }

            return num;
        }
 
        // showStats
        // Summary: Give pretty output of grade stats
        // Arguments: 
        //      List<double> list - list of grades
        // Returns: none
        private static void showStats(List<double> list) {
            int count = list.Count;
            if (count == 0){
                Console.WriteLine("Houston, we have a problem. There's nothing here.");
                return;
            }

            double total = 0;
            
            foreach(double l in list) {
                total = total + l;
            }
            double highest = highestGrade(list);
            string highLetter = percentToLetterGrade(highest);

            double lowest = lowestGrade(list);
            string lowLetter = percentToLetterGrade(lowest);

            double average = total / count;
            string avgLetter = percentToLetterGrade(average);

            Console.WriteLine("Grade Statistics");
            Console.WriteLine("----------------");
            Console.WriteLine($"Total number of grades: {count}");
            Console.WriteLine($"Average Grade: {average} (Letter Grade {avgLetter})");
            Console.WriteLine($"Highest grade: {highest} (Letter Grade {highLetter})");
            Console.WriteLine($"Lowest grade: {lowest} (Letter Grade {lowLetter})");
            Console.WriteLine("\nAll Grades: ");
            listGrades(list);
        }
    }

    class Grade {
        private double grade;
        private string letter;

        Grade(double value) => this.setGrade(value);
        Grade() => this.setGrade(0);

        public double getGrade()
        {
            return grade;
        }

        public void setGrade(double value)
        {
            // Check for Out Of Range exception
            if(value < 0 || value > 110) {
                throw new ArgumentOutOfRangeException();
            }

            // Set the grade variable
            grade = value;

            // Define the corresponding letter grade
            if(grade < 60) {
                letter = "F";
            }
            else if(grade < 70) {
                letter = "D";
            }
            else if(grade < 80) {
                letter = "C";
            }
            else if(grade < 90) {
                letter = "B";
            }
            else if(grade < 100) {
                letter = "A";
            }
            else {
                letter = "A+";
            }
        }



        public string getLetter()
        {
            return letter;
        }
    }
}