using Kuddn.TaskManager.Domain.Logic;
using Kuddn.TaskManager.Domain.Models;
using Kuddn.TaskManager.Domain.Models.ENUMS;
using Kuddn.TaskManager.DataAccess;

using System.Text.Json.Serialization;
using System;


public class Program
{

    public static void Main(string[] args)
    {
        
        
        FileWorkItemsRepository fileWorkItemsRepository = new FileWorkItemsRepository();
        SimpleTaskManager simpleTaskManager = new SimpleTaskManager(fileWorkItemsRepository);

        //------------------------------------------------

        while (true)
        {
            Console.WriteLine("Menu: ");
            Console.WriteLine("1. Create Task");
            Console.WriteLine("2. Tasks");
            Console.WriteLine("3. Delete Task");
            Console.WriteLine("4. Mark task as complited");
            Console.WriteLine("5. Exit\n");

            int input = Int32.Parse(Console.ReadLine());

            if (input == 1)
            {
                Console.Clear();

                WorkItem workItem = new WorkItem();


                Console.WriteLine("Enter task info: ");

                
                Title(workItem);
                Description(workItem);
                DueDate(workItem);
                Priority(workItem);
                Complexity(workItem);
                CreationDate(workItem);
                workItem.IsComplited = false;

                fileWorkItemsRepository.Add(workItem);

                
                Console.Clear();
            }
            else if (input == 2)
            {
                Console.Clear();
                var workItemArray = simpleTaskManager.CreatePlan();
                int flag = 1;
                foreach (var item in workItemArray)
                {
                    Console.WriteLine($"{flag}. {item.ToString()}");
                    flag++;
                }

                if (workItemArray.Length > 0)
                {
                    Console.WriteLine();
                }

            }
            else if (input == 3)
            {
                Console.Clear();
                int flag = 1;
                var sortedArray2 = simpleTaskManager.CreatePlan();

                foreach (var item in sortedArray2)
                {
                    Console.WriteLine($"{flag}. {item.ToString()}");
                    flag++;
                }

                Console.Write("\nWhich task you would like to remove?: ");
                var number = int.Parse(Console.ReadLine());

                if (number > 0 && number <= sortedArray2.Length)
                {
                    Guid guid = sortedArray2[number - 1].Id;
                    fileWorkItemsRepository.Remove(guid);
                }
                else
                {
                    Console.WriteLine("Invalid id");
                }
                Console.Clear();
            }
            else if (input == 4)
            {
                Console.Clear();
                int flag = 0;
                var sortedArray = simpleTaskManager.CreatePlan();

                foreach (var item in sortedArray)
                {
                    flag++;
                    Console.WriteLine($"{flag}. {item.ToString()}");
                }

                Console.Write("\nChoose task number to mark as complited: ");
                int option = Int32.Parse(Console.ReadLine());


                if (option > 0 && option <= sortedArray.Length)
                {
                    sortedArray[option - 1].IsComplited = true;
                    fileWorkItemsRepository.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Choose another option");
                }
                    
            }
            else if (input == 5)
            {
                break;
            }
            else
            {
                Console.WriteLine("\nChoose another option\n");
            }
        }

        
    }


    private static void Title (WorkItem workItem)
    {
        Console.Write("Title: ");
        workItem.Title = Console.ReadLine();
    }

    private static void Description(WorkItem workItem)
    {
        Console.Write("Description: ");
        workItem.Description = Console.ReadLine();
    }

    private static void DueDate(WorkItem workItem)
    {
        while (true)
        {
            Console.Write("Due date (dd.MM.yyyy): ");
            string input = Console.ReadLine();

            if (DateTime.TryParseExact(input, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                workItem.DueDate = result;
                break;
            }
            else
            {
                Console.WriteLine("Wrong format!");

            }
        }
    }

    private static void Priority(WorkItem workItem)
    {

        while (true)
        {
            Console.WriteLine("Priority: ");
            Console.WriteLine("1. None");
            Console.WriteLine("2. Low");
            Console.WriteLine("3. Medium");
            Console.WriteLine("4. High");
            Console.WriteLine("5. Urgent");

            Console.Write("Choose a priority: ");
            string prior = Console.ReadLine();

            if (int.TryParse(prior, out int priorityNum) && Enum.IsDefined(typeof(Priority), priorityNum))
            {
                workItem.Priority = (Priority)priorityNum - 1;
                break;
            }
            else
            {
                Console.WriteLine("\nChoose another option\n");
            }
        }
        
    }

    private static void Complexity(WorkItem workItem)
    {

        while (true)
        {
            
            Console.WriteLine("Complexity: ");
            Console.WriteLine("1. None");
            Console.WriteLine("2. Minutes");
            Console.WriteLine("3. Hours");
            Console.WriteLine("4. Days");
            Console.WriteLine("5. Weeks");

            Console.Write("Choose a complexity: ");
            string compl = Console.ReadLine();

            if (int.TryParse(compl, out int complNum) && Enum.IsDefined(typeof(Complexity), complNum))
            {
                workItem.Complexity = (Complexity)complNum - 1;
                break;
            }
            else
            {
                Console.WriteLine("\nChoose another option\n");
            }
        }
        
    }

    private static void CreationDate(WorkItem workItem)
    {
        workItem.CreationDate = DateTime.Now;
    }

    private static void MarkAsComplited(WorkItem workItem)
    {
        workItem.IsComplited = true;
    }

}
