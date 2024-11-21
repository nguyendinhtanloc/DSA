using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoAn
{
    public class Node<T>
    {
        internal T data;
        internal Node<T> next;

        public Node(T value)
        {
            data = value;
            next = null;
        }
    }

    public class CustomQueue<T>
    {
        private Node<T> _front;
        private Node<T> _rear;
        private int _size;

        public int count
        {
            get
            {
                return _size;
            }
        }

        public bool isEmpty()
        {
            return _front == null;
        }

        public void enQueue(T item)
        {
            Node<T> newNode = new Node<T>(item);

            if (_rear == null)
            {
                _front = newNode;
                _rear = newNode;
            }
            else
            {
                _rear.next = newNode;
                _rear = newNode;
            }
            _size++;
        }

        public T deQueue()
        {
            if (_front == null)
            {
                throw new InvalidOperationException("Queue is empty!");
            }
            T item = _front.data;
            _front = _front.next;
            _size--;

            if (_front == null)
            {
                _rear = null;
            }

            return item;
        }

    }
    public class LivingExpenseBill
    {
        public string roomNumber;
        public decimal electricityBill;
        public decimal waterBill;
        public decimal serviceFee;
        public bool isPaid;

        public LivingExpenseBill(string roomNumber, decimal electricityBill, decimal waterBill, decimal serviceFee)
        {
            this.roomNumber = roomNumber;
            this.waterBill = waterBill;
            this.serviceFee = serviceFee;
            this.electricityBill = electricityBill;
            this.isPaid = false; // default is not paid
        }

        public decimal caculateTotal()
        {
            return electricityBill + waterBill + serviceFee;
        }
    }

    public class AccommodationBill
    {
        public string studentName;
        public string studentID;
        public string roomNumber;
        public decimal accommodationFee;
        public bool isPaid;

        public AccommodationBill(string studentName, string studentID, string roomNumber, decimal accomodationFee)
        {
            this.studentName = studentName;
            this.studentID = studentID;
            this.roomNumber = roomNumber;
            this.accommodationFee = accomodationFee;
            this.isPaid = false; //Default is not paid
        }

        public decimal caculateTotal()
        {
            return accommodationFee;
        }

    }

    public static class BillManager
    {
        public static CustomQueue<LivingExpenseBill> livingExpenseQueue = new CustomQueue<LivingExpenseBill>();
        public static CustomQueue<AccommodationBill> accommodationQueue = new CustomQueue<AccommodationBill>();

        //public static void addLivingExpenseBill(string numberRoom, decimal electricityBill, decimal waterBill, decimal serviceFee)
        //{
        //    LivingExpenseBill newBill = new LivingExpenseBill(numberRoom, electricityBill, waterBill, serviceFee);
        //    livingExpenseQueue.enQueue(newBill);
        //}

        public static void payLivingExpenseBill(string numberRoom)
        {
            int quantity = livingExpenseQueue.count;
            Console.WriteLine(quantity);
            for (int i = 0; i < quantity; i++)
            {
                LivingExpenseBill invoice = livingExpenseQueue.deQueue();
                if (invoice.roomNumber == numberRoom && !invoice.isPaid)
                {
                    invoice.isPaid = true;
                    Console.WriteLine($"Thanh toán hóa đơn phòng {numberRoom}...");
                    recordLivingExpenseBill(invoice);
                    return;
                }
                livingExpenseQueue.enQueue(invoice);
            }
            Console.WriteLine($"Không tìm thấy hóa đơn chưa thanh toán cho phòng {numberRoom} .");
        }

        public static void loadLivingExpenseBillsFromFile(string filePath)
        {
            Console.WriteLine("Đang nhập...");
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        string roomNumber = parts[0].Trim();
                        decimal electricityBill = decimal.Parse(parts[1].Trim());
                        decimal waterBill = decimal.Parse(parts[2].Trim());
                        decimal serviceFee = decimal.Parse(parts[3].Trim());

                        LivingExpenseBill invoice = new LivingExpenseBill(roomNumber, electricityBill, waterBill, serviceFee);
                        livingExpenseQueue.enQueue(invoice);
                    }
                }
            }
        }

        private static void recordLivingExpenseBill(LivingExpenseBill invoice)
        {
            Console.WriteLine($"Ghi hóa đơn phòng {invoice.roomNumber} vào file LivingExpenseBill.txt...");
            using (StreamWriter writer = new StreamWriter("C:\\Users\\ASUS\\source\\repos\\DoAn\\DoAn\\LivingExpenseBill.txt", true))
            {
                writer.WriteLine($"{invoice.roomNumber}, {invoice.caculateTotal()}, Paid");
            }
        }

        public static void recordUnPaidLivingExpense()
        {
            using (StreamWriter writer = new StreamWriter("C:\\Users\\ASUS\\source\\repos\\DoAn\\DoAn\\UnPaidLivingExpenseBill.txt", true))
            {
                int quantity = livingExpenseQueue.count;
                for (int i = 0; i < quantity; i++)
                {
                    LivingExpenseBill invoice = livingExpenseQueue.deQueue();
                    if (!invoice.isPaid)
                    {
                        Console.WriteLine($"Ghi hóa đơn phòng {invoice.roomNumber} chưa thanh toán vào file UnPaidLivingExpenseBill.txt...");
                        writer.WriteLine($"{invoice.roomNumber},{invoice.caculateTotal()}, Not Paid");
                    }
                    livingExpenseQueue.enQueue(invoice);
                }
            }

        }

        public static void exportLivingExpenseQueue()
        {
            Console.WriteLine("Danh sách hóa đơn chi phí sinh hoạt:");
            int quantity = livingExpenseQueue.count;
            if (quantity == 0)
            {
                Console.WriteLine("Hàng đợi hóa đơn chi phí sinh hoạt trống.");
                return;
            }

            for (int i = 0; i < quantity; i++)
            {
                LivingExpenseBill invoice = livingExpenseQueue.deQueue();
                Console.WriteLine($"Phòng: {invoice.roomNumber}, Tổng tiền: {invoice.caculateTotal()}, Trạng thái: {(invoice.isPaid ? "Đã thanh toán" : "Chưa thanh toán")}");
                livingExpenseQueue.enQueue(invoice);
            }
        }

        //public static void addAccomodationBill(string studentName, string studentID, string roomNumber, decimal accomodationFee)
        //{
        //    AccommodationBill invoice = new AccommodationBill(studentName, studentID, roomNumber, accomodationFee);
        //    accommodationQueue.enQueue(invoice);
        //}

        public static void loadAccommodationBillsFromFile(string filePath)
        {
            Console.WriteLine("Đang load...");
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(','); // Tách các thông tin theo dấu phẩy
                    if (parts.Length == 4)
                    {
                        string studentName = parts[0].Trim();
                        string studentID = parts[1].Trim();
                        string roomNumber = parts[2].Trim();
                        decimal accommodationFee = decimal.Parse(parts[3].Trim());

                        AccommodationBill invoice = new AccommodationBill(studentName, studentID, roomNumber, accommodationFee);
                        accommodationQueue.enQueue(invoice);
                    }
                }
            }
        }

        public static void payAccomodationBill(string studentName, string studentID)
        {
            int quantity = accommodationQueue.count;
            for (int i = 0; i < quantity; i++)
            {
                AccommodationBill invoice = accommodationQueue.deQueue();
                if (invoice.studentName == studentName && invoice.studentID == studentID && !invoice.isPaid)
                {
                    Console.WriteLine($"Thanh toán hóa đơn ở {studentID}...");
                    invoice.isPaid = true;
                    recordAccommodationBill(invoice);
                    return;
                }
                accommodationQueue.enQueue(invoice);
            }
            Console.WriteLine($"Không tìm thấy hóa đơn sinh viên có mssv {studentID}.");
        }



        private static void recordAccommodationBill(AccommodationBill invoice)
        {
            Console.WriteLine($"Ghi hóa đơn sv {invoice.studentID} vào file AccommodationBill.txt...");
            using (StreamWriter writer = new StreamWriter("\\Users\\ASUS\\source\\repos\\DoAn\\DoAn\\AccommodationBill.txt", true))
            {
                writer.WriteLine($"{invoice.studentName}, {invoice.studentID}, {invoice.roomNumber}, {invoice.caculateTotal()}, Paid");
            }
        }

        public static void recordUnpaidAccommodation()
        {
            using (StreamWriter writer = new StreamWriter("\\Users\\ASUS\\source\\repos\\DoAn\\DoAn\\UnPaidAccommodation.txt", true))
            {
                int quantity = accommodationQueue.count;
                for (int i = 0; i < quantity; i++)
                {
                    AccommodationBill invoice = accommodationQueue.deQueue();
                    if (!invoice.isPaid)
                    {
                        Console.WriteLine($"Ghi hóa đơn sv {invoice.studentID} chưa thanh toán vào file UnPaidAccommodationBill.txt...");
                        writer.WriteLine($"{invoice.studentName},{invoice.studentID}, {invoice.roomNumber}, {invoice.caculateTotal()}, Not Paid");
                    }
                    accommodationQueue.enQueue(invoice);
                }
            }
        }
        public static void exportAccommodationQueue()
        {
            Console.WriteLine("Danh sách hóa đơn chỗ ở:");
            int quantity = accommodationQueue.count;
            if (quantity == 0)
            {
                Console.WriteLine("Hàng đợi hóa đơn chỗ ở trống.");
                return;
            }

            for (int i = 0; i < quantity; i++)
            {
                AccommodationBill invoice = accommodationQueue.deQueue();
                Console.WriteLine($"Tên sinh viên: {invoice.studentName}, Mã sinh viên: {invoice.studentID}, Phòng: {invoice.roomNumber}, Tổng tiền: {invoice.caculateTotal()}, Trạng thái: {(invoice.isPaid ? "Đã thanh toán" : "Chưa thanh toán")}");
                accommodationQueue.enQueue(invoice);
            }
        }

    }
}