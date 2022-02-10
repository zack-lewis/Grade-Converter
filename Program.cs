using System;
using System.Collections.Generic;

namespace GradeConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            // VARIABLES
            List<Grade> grades = new List<Grade>();
            int numGrades = 0;
            string name = "";

            Console.WriteLine("Welcome to the Mighty Grade Converter. ");

            // Greet the user, get their name
            name = greetUser();

            // Get the number of grades to input
            numGrades = getNumGrades();
            
            // loop that many times
            Console.WriteLine("We can do that. Let's get them input into our system. Enter them below.");
            for(int i = 0; i < numGrades; i++) {
                int p = i + 1;
                Grade input = gradePrompt(p.ToString());
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
                        Grade input = gradePrompt(p.ToString());
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


        // greetUser
        // Arguments:
        //      none
        // Returns: string name - Full name of user
        private static string greetUser() {
            string firstName;
            string lastName;
            string name;

            // ask user name
            Console.WriteLine("May I get your first and last name?");
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

        // addGradeToList
        // Summary: Take a grade and a list, add the grade to the list, return the list
        // Arguments: 
        //      Grade g - Grade object to add to list
        //      List<Grade> list - initial list in which to add Grade object
        // Returns: List<Grade> list - List with Grade object added
        private static List<Grade> addGradeToList(Grade g, List<Grade> list) {
            list.Add(g);
            return list;
        }
		
		// gradePrompt
        // Summary: Prompt user for a grade and return it as a double
        // Arguments: 
        //      string prompt - Prompt to use for user input
        // Returns: Grade g - Grade object from user 
        private static Grade gradePrompt(string prompt) {
            
                // temp string for input
                const int upperLimit = 110;
                string input = "";
                Grade g = new Grade();

                // ask user for input
                Console.Write($"{prompt}: ");
                input = Console.ReadLine();

                try {
                    // if empty, reprompt
                    if (input == "") {
                        throw new ArgumentNullException();
                    }
                    
                    // if non-numeric, throw exception
                    // grade = Int32.Parse(temp);
                    g.setGradePercent(Double.Parse(input));
                   
                }
                catch (FormatException) {
                    Console.WriteLine("Sorry, bad format. Let's try that again.");
                    g = gradePrompt(prompt);
                }
                catch (ArgumentNullException) {
                    Console.WriteLine("Grade cannot be empty!");
                    g = gradePrompt(prompt);
                }
                catch (ArgumentOutOfRangeException) {
                    Console.WriteLine($"Sorry! Grade cannot be below 0 or above {upperLimit}");
                    g = gradePrompt(prompt);
                }
                catch (Exception) {
                    throw;
                }

                return g;
        }
    
	    // listGrades
        // Summary: Take a list of grades and output in pretty format
        // Arguments:
        //      List<Grade> list - list of Grade objects to display
        // Returns: none
        private static void listGrades(List<Grade> list) {
            int count = list.Count;
            for(int i = 0; i < count; i++) {
                string letter = list[i].getGradeLetter();
                Console.WriteLine($"{i+1}: {list[i]}  ({letter})");
            }
        }
	
	
		// showStats
        // Summary: Give pretty output of grade stats
        // Arguments: 
        //      List<Grade> list - list of grades
        // Returns: none
        private static void showStats(List<Grade> list) {
            int count = list.Count;
            if (count == 0){
                Console.WriteLine("Houston, we have a problem. There's nothing here.");
                return;
            }

            double total = 0;
            
            foreach(Grade l in list) {
                total = total + l.getGradePercent();
            }

            Grade highest = highestGrade(list);
            Grade lowest = lowestGrade(list);
            Grade avg = averageGrade(list);

            Console.WriteLine("Grade Statistics");
            Console.WriteLine("----------------");
            Console.WriteLine($"Total number of grades: {count}");
            Console.WriteLine($"Average Grade: {avg.getGradePercent()} (Letter Grade {avg.getGradeLetter()})");
            Console.WriteLine($"Highest grade: {highest.getGradePercent()} (Letter Grade {highest.getGradeLetter()})");
            Console.WriteLine($"Lowest grade: {lowest.getGradePercent()} (Letter Grade {lowest.getGradeLetter()})");
            Console.WriteLine("\nAll Grades: ");
            listGrades(list);
        }
	
		// highestGrade
        // Arguments:
        //      List<Grade> list - list of Grade objects to search
        // Returns: Grade highest - Highest grade from list as a percentage 
        private static Grade highestGrade(List<Grade> list) {
            Grade highest = new Grade(0);
            foreach(Grade g in list) {
                if(g.getGradePercent() > highest.getGradePercent()) {
                    highest = g;
                }
            }
            return highest;
        }
        
        // lowestGrade
        // Arguments:
        //      List<Grade> list - list of grades to search
        // Returns: Grade lowest - Lowest grade from list as a percentage 
        private static Grade lowestGrade(List<Grade> list) {
            Grade lowest = new Grade(999);
            foreach(Grade g in list) {
                if(g.getGradePercent() < lowest.getGradePercent()) {
                    lowest = g;
                }
            }
            return lowest;
        }
	
        // averageGrade
        // Summary: Take a list of Grade objects and return the average as a Grade object
        // Arguments: 
        //      List<Grade> list - list of Grade objects
        // Returns: Grade g - New Grade object with value of list average
        private static Grade averageGrade(List<Grade> list) {
            Grade g = new Grade();
            int numGrades = list.Count;
            double total = 0;

            foreach(Grade l in list) {
                total = total + l.getGradePercent();
            }

            g.setGradePercent(total/numGrades);

            return g;
        }
    
    }

    class Grade {
        // Upper limit of Grades
        const double upperLimit = 110;

        // Grade as Percentage        
        private double gradePercent;

        // Grade as Letter
        private string gradeLetter;

        // Constructors
        public Grade(double value) => this.setGradePercent(value);
        public Grade() => this.setGradePercent(0);

        // getGradePercent
        // Summary: Return the gradePercent value as a double
        // Arguments: none
        // Returns: double gradePercent
        public double getGradePercent()
        {
            return gradePercent;
        }

        // setGradePercent
        // Summary: Set the Grade Percent value
        // Arguments: double value - Percent to set 
        // Returns: none
        public void setGradePercent(double value)
        {
            // Check for Out Of Range exception
            if(value < 0 || value > upperLimit) {
                throw new ArgumentOutOfRangeException();
            }

            // Set the grade variable
            gradePercent = value;

            // Define the corresponding gradeLetter grade
            if(gradePercent < 60) {
                gradeLetter = "F";
            }
            else if(gradePercent < 70) {
                gradeLetter = "D";
            }
            else if(gradePercent < 80) {
                gradeLetter = "C";
            }
            else if(gradePercent < 90) {
                gradeLetter = "B";
            }
            else if(gradePercent < 100) {
                gradeLetter = "A";
            }
            else if(gradePercent > 100 && gradePercent < upperLimit) {
                gradeLetter = "A+";
            }
            else {
                gradeLetter = "Unsatisfactory";
            }
        }

        // getGradeLetter
        // Summary: Get the Letter Grade corresponding to the Percent Grade value
        // Arguments: none
        // Returns: string gradeLetter
        public string getGradeLetter()
        {
            return gradeLetter;
        }
    }
}