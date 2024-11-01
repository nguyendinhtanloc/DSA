using System;
using System.Collections.Generic;
using System.Linq;

namespace CTDL
{
    public class Room
    {
        public int RoomNumber;
        public int TotalBeds;
        public int EmptyBeds;
        public string Status;

        public Room(int roomNumber, int totalBeds, int emptyBeds, string status)
        {
            this.RoomNumber = RoomNumber;
            this.TotalBeds = TotalBeds;
            this.EmptyBeds = EmptyBeds;
            this.Status = Status;
        }

        public void UpdateRoomInfo(int totalBeds, int emptyBeds, string status)
        {
            TotalBeds = totalBeds;
            EmptyBeds = emptyBeds;
            Status = status;
        }

        // Calculating occupied beds
        public int OccupiedBeds()
        {
            return TotalBeds - EmptyBeds;
        }
    }

    public class RoomManager
    {
        private List<Room> rooms = new List<Room>();

        // Add room
        public void AddRoom(Room room)
        {
            rooms.Add(room);
        }

        // Remove room
        public void RemoveRoom(int roomNumber)
        {
            rooms.RemoveAll(r => r.RoomNumber == roomNumber);
        }

        // Update room info
        public bool UpdateRoom(int roomNumber, int totalBeds, int emptyBeds, string status)
        {
            Room room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room != null)
            {
                room.UpdateRoomInfo(totalBeds, emptyBeds, status);
                return true;
            }
            return false;
        }

        // Check if room exists
        public bool RoomExists(int roomNumber)
        {
            return rooms.Any(r => r.RoomNumber == roomNumber);
        }

        // Display room list formatted as a table with sequence numbers
        public void DisplayRooms()
        {
            Console.WriteLine("------------------------------------------------------------------------------------");
            Console.WriteLine("| No. | Room Number | Total Beds | Empty Beds | Occupied Beds | Status             |");
            Console.WriteLine("------------------------------------------------------------------------------------");

            int index = 1;
            foreach (var room in rooms.OrderBy(r => r.EmptyBeds))
            {
                Console.WriteLine($"| {index,-3} | {room.RoomNumber,-11} | {room.TotalBeds,-10} | {room.EmptyBeds,-10} | {room.OccupiedBeds(),-13} | {room.Status,-18} |");
                index++;
            }

            Console.WriteLine("------------------------------------------------------------------------------------");
        }

        // Simulate going back to the home page (by showing a message)
        public void GoBackToHome()
        {
            Console.WriteLine("Returning to Home Page...");
        }
    }

    public class CTDT
    {
        public static void Main(string[] args)
        {
           
        static void ManageRooms()
        {
            RoomManager roomManager = new RoomManager();
            bool roomRunning = true;

            while (roomRunning)
            {
                ShowRoomManagementMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddRoomFromInput(roomManager);
                        break;
                    case "2":
                        RemoveRoomFromInput(roomManager);
                        break;
                    case "3":
                        UpdateRoomFromInput(roomManager);
                        break;
                    case "4":
                        roomManager.DisplayRooms();
                        break;
                    case "5":
                        roomRunning = false; // Exit room management
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // Display the room management menu formatted as a table
        static void ShowRoomManagementMenu()
        {
            Console.WriteLine("\nRoom Management Menu:");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("| No. | Action                                     |");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("| 1   | Add Room                                   |");
            Console.WriteLine("| 2   | Remove Room                                |");
            Console.WriteLine("| 3   | Update Room                                |");
            Console.WriteLine("| 4   | Display Room List                          |");
            Console.WriteLine("| 5   | Go Back to Main Menu                       |");
            Console.WriteLine("---------------------------------------------------");
            Console.Write("Your choice: ");
        }

        // Method to input room details and add a room
        static void AddRoomFromInput(RoomManager roomManager)
        {
            Console.Write("Enter the number of rooms to add: ");
            int numberOfRooms = int.Parse(Console.ReadLine());

            for (int i = 1; i <= numberOfRooms; i++)
            {
                Console.WriteLine($"\nEnter details for room {i}:");

                Console.Write("Enter room number (Consists of 4 integers" +
                    " the first 2 digits: floor number, the last 2 digits: room number) : ");

                int roomNumber = int.Parse(Console.ReadLine());


                Console.Write("Enter total number of beds: ");
                int totalBeds = int.Parse(Console.ReadLine());

                int emptyBeds;
                do
                {
                    Console.Write("Enter number of empty beds: ");
                    emptyBeds = int.Parse(Console.ReadLine());

                    if (emptyBeds > totalBeds)
                    {
                        Console.WriteLine("Error: Empty beds cannot be greater than total beds. Please re-enter.");
                    }

                } while (emptyBeds > totalBeds); // Repeat until a valid number is entered

                Console.Write("Enter room status (Available/Occupied): ");
                string status = Console.ReadLine();

                Room newRoom = new Room(roomNumber, totalBeds, emptyBeds, status);
                roomManager.AddRoom(newRoom);

                Console.WriteLine("Room successfully added!");
            }
        }

        // Method to input room number and remove a room
        static void RemoveRoomFromInput(RoomManager roomManager)
        {
            Console.Write("Enter the room number to remove: ");
            int roomNumber = int.Parse(Console.ReadLine());

            roomManager.RemoveRoom(roomNumber);
            Console.WriteLine("Room successfully removed!");
        }

        // Method to input room details and update a room
        static void UpdateRoomFromInput(RoomManager roomManager)
        {
            Console.Write("Enter the room number to update: ");
            int roomNumber = int.Parse(Console.ReadLine());

            if (!roomManager.RoomExists(roomNumber))
            {
                Console.WriteLine("Room does not exist. Would you like to:");
                Console.WriteLine("1. Try again");
                Console.WriteLine("2. Create a new room");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    UpdateRoomFromInput(roomManager); // Recursive call to try again
                }
                else if (choice == "2")
                {
                    AddRoomFromInput(roomManager); // Create new room
                }
                else
                {
                    Console.WriteLine("Invalid choice. Returning to menu.");
                }

                return;
            }

            Console.Write("Enter new total number of beds: ");
            int totalBeds = int.Parse(Console.ReadLine());

            int emptyBeds;
            do
            {
                Console.Write("Enter new number of empty beds: ");
                emptyBeds = int.Parse(Console.ReadLine());

                if (emptyBeds > totalBeds)
                {
                    Console.WriteLine("Error: Empty beds cannot be greater than total beds. Please re-enter.");
                }

            } while (emptyBeds > totalBeds); // Repeat until valid input

            Console.Write("Enter new room status (Available/Occupied): ");
            string status = Console.ReadLine();

            roomManager.UpdateRoom(roomNumber, totalBeds, emptyBeds, status);
            Console.WriteLine("Room information successfully updated!");
        }

        // Placeholder for managing students
        static void ManageStudents()
        {
            Console.WriteLine("Student management is not implemented yet.");
        }

        // Placeholder for managing nets
        static void ManageNets()
        {
            Console.WriteLine("Nets management is not implemented yet.");
        }

        // Placeholder for saving and loading data
        static void SaveAndLoadData()
        {
            Console.WriteLine("Save and load data functionality is not implemented yet.");
        }
    }
}