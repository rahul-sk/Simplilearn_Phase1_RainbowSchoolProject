/*
* Created on September, 2021
* @Author Rahul S Koimattur.
* @brief  This is a menu-driven application for storing and updating teachers record in a file,
*         The name of the file is Teachers.txt
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SelfLearning
{

    class Teacher
    {
        private string id;
        private string name;
        private string className;
        private string section;

        public string Id { get; set; }
        public string Name { get; set; }

        public string ClassName { get; set; }

        public string Section { get; set; }

        public Teacher()
        {

        }

        public Teacher(string id, string name, string className, string section)
        {
            Id = id;
            Name = name;
            ClassName = className;
            Section = section;
        }



 /*
 *@ Function      : findTeacher
 *@ return        : bool
 *@ Argument      : Array of records fetched from the text file and the ID of the teacher to be searched.
 *@ brief         : This function finds if a teacher with a given id exists in the array of records.
 */
        public static bool findTeacher(string[] arr,string id)
        {
            foreach(string s in arr)
            {
                string[] line = s.Split(",");

                    if (line[0].Trim() == id)
                    {
                        return true;
                    }
            
            }
            return false;
        }

/*
*@ Function      : getTeacherString
*@ return        : string
*@ brief         : This function returns the object properties in a comma seperated string format
*/

        public string getTeacherString()
        {
            return Id + "," + Name + "," + ClassName + "," + Section;
        }



/*
*@ Function      : printTeacher
*@ return        : void
*@ Argument      : A comma seperated string consisting of teacher fields.
*@ brief         : This function prints the various fields of a teacher object.
*/
        public static void printTeacher(string entry)
        {
            string[] fields = entry.Split(",");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("ID : {0}  ||  Name : {1}  ||  Class : {2}  ||  Section : {3}", fields[0],
                fields[1], fields[2],fields[3]);
            Console.ResetColor();
        }
    }
    class MainEntry
    {
        public static void Main(string[] args)
        {
            FileStream fsw = null;
            FileStream fsr = null;
            StreamReader str = null;
            StreamWriter stw = null;
            string fileLocation = "C:\\Users\\11033258\\Desktop\\C#\\new123.txt";
            int inp;
            bool done = false;
            while (!done)
            {
                Console.WriteLine();
                Console.WriteLine("***** Please choose an option *****\n");
                Console.WriteLine("1 To print all Teacher records\n2 Print a given Teacher record\n3 to add a new Teacher record\n4 to update an existing Teacher record \n5 to check if a Teacher record exists\n6 to delete a Teacher record\n-1 to terminate");
                Console.WriteLine();
                inp = int.Parse(Console.ReadLine());
                switch (inp)
                {
                    case 1:
                        try
                        {
                            fsr = new FileStream(fileLocation, FileMode.Open, FileAccess.Read);
                            str = new StreamReader(fsr);
                            str.BaseStream.Seek(0, SeekOrigin.Begin);
                            string st = str.ReadLine();
                            while (st != null)
                            {
                                Teacher.printTeacher(st);
                                Console.WriteLine();
                                st = str.ReadLine();
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        finally
                        {
                            str.Close();
                            fsr.Close();
                        }

                        break;
                    case 2:
                        Console.WriteLine("Enter the ID of the Teacher");
                        string id0 = Console.ReadLine();


                        var slist = new List<string>();
                        bool printed = false;
                        using (var sr = new StreamReader(fileLocation))
                        {

                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] splt = line.Split(",");
                                if (splt[0].Trim() == id0)
                                {
                                    printed = true;
                                    Teacher.printTeacher(line);
                                    break;
                                }
                            }
                        }
                        if (!printed)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("No Teacher record found with the given ID");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine();

                        }

                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine("Enter the ID");
                            string id1 = Console.ReadLine();
                            var list0 = new List<string>();
                            using (var sr = new StreamReader(fileLocation))
                            {
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    list0.Add(line);
                                }
                            }
                            fsw = new FileStream(fileLocation, FileMode.Append, FileAccess.Write);

                            stw = new StreamWriter(fsw);
                            string[] re = list0.ToArray();
                            bool alreadyPresent = Teacher.findTeacher(re, id1);
                            if (alreadyPresent)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.Write("Teacher with this ID already exists");
                                Console.ResetColor();
                                Console.WriteLine();
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("Enter the Name");
                                string nm = Console.ReadLine();
                                Console.WriteLine("Enter the class");
                                string cls = Console.ReadLine();
                                Console.WriteLine("Enter the section");
                                string sec = Console.ReadLine();
                                Teacher th = new Teacher(id1, nm, cls, sec);
                                String rs = th.getTeacherString();
                                stw.WriteLine(rs);
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        finally
                        {
                            stw.Close();
                            fsw.Close();

                        }
                        break;
                    case 4:
                        Console.WriteLine("Enter the ID of the teacher to update");
                        string idd1 = Console.ReadLine();
                        var list = new List<string>();
                        bool updated = false;

                        using (var sr = new StreamReader(fileLocation))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] splt = line.Split(",");
                                if (splt[0].Trim() == idd1)
                                {
                                    updated = true;
                                    Console.Write("Existing record : ");
                                    Teacher.printTeacher(line);
                                    Console.WriteLine();
                                    Console.WriteLine("Enter the new data");
                                    Console.WriteLine("Enter the Name");
                                    string nm1 = Console.ReadLine();
                                    Console.WriteLine("Enter the class");
                                    string cls1 = Console.ReadLine();
                                    Console.WriteLine("Enter the section");
                                    string sec1 = Console.ReadLine();
                                    Teacher th = new Teacher(idd1, nm1, cls1, sec1);
                                    String rs = th.getTeacherString();
                                    list.Add(rs);
                                }
                                else
                                list.Add(line);
                            }
                        }
                        if (!updated)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("Record you are trying to update doesn't exist");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                        else
                        {
                            string[] result1 = list.ToArray();
                            using (StreamWriter writer = File.CreateText(fileLocation))
                            {
                                foreach (string data in result1)
                                {
                                    writer.WriteLine(data);
                                }
                            }
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write("Record updated successfully");
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                        break;
                    case 5:
                        Console.WriteLine("Enter the id to search");
                        string idd = Console.ReadLine();
                        var list1 = new List<string>();
                        using (var sr = new StreamReader(fileLocation))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                list1.Add(line);
                            }
                        }
                        string[] result = list1.ToArray();
                        bool isPresent =  Teacher.findTeacher(result, idd);
                        if (isPresent)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;

                            Console.Write("Teacher with this ID exists");
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Red;

                            Console.Write("Teacher doesn't exist ");
                        }
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine();

                        break;
                    case 6:
                        Console.WriteLine("Enter the ID of the teacher you want to delete");
                        string idd2 = Console.ReadLine();
                        var list2 = new List<string>();

                        bool deleted = false;


                        using (var sr = new StreamReader(fileLocation))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] splt = line.Split(",");
                                if (splt[0].Trim() == idd2)
                                {
                                    deleted = true;
                                    
                                }
                                else
                                    list2.Add(line);

                            }
                        }
                        if (!deleted)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("Record you are trying to delete doesn't exist");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                        else
                        {
                            string[] result1 = list2.ToArray();
                            using (StreamWriter writer = File.CreateText(fileLocation))
                            {
                                foreach (string data in result1)
                                {
                                    writer.WriteLine(data);
                                }
                            }
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write("Record deleted successfully");
                            Console.ResetColor();
                            Console.WriteLine();
                        }

                        break;
                    case -1:done = true;
                        break;
                    default:
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("Please enter a valid input");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                }

            }
        }
    }
}
