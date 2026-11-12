public class CustomerService
{
    public static void Run()
    {
        // Test Cases

        // Test 1
        // Scenario: Create a queue of size 2. Add 3 customers.
        // Expected Result: The 3rd customer should trigger an error message saying "Maximum Number of Customers in Queue."
        Console.WriteLine("Test 1");
        var cs = new CustomerService(2);
        cs.AddNewCustomer(); // Type data for customer 1
        cs.AddNewCustomer(); // Type data for customer 2
        cs.AddNewCustomer(); // This one should fail and show the error
        // Defect(s) Found: AddNewCustomer checked if Count > _maxSize instead of >= _maxSize.

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Add a customer, then serve a customer.
        // Expected Result: The exact customer added should be displayed and removed from the queue.
        Console.WriteLine("Test 2");
        var cs2 = new CustomerService(2);
        cs2.AddNewCustomer(); // Type data for customer 1
        cs2.ServeCustomer();  // Should display customer 1
        // Defect(s) Found: ServeCustomer removed the item at index 0 BEFORE saving it to a variable, causing a crash or skipping a customer.

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Try to serve a customer from an empty queue.
        // Expected Result: An error message should be displayed stating the queue is empty.
        Console.WriteLine("Test 3");
        var cs3 = new CustomerService(2);
        cs3.ServeCustomer();
        // Defect(s) Found: ServeCustomer lacked a check to see if the queue was empty, causing an IndexOutOfRangeException crash.
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize)
    {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer
    {
        public Customer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer()
    {
        // FIXED Defect 1: Changed > to >= so it doesn't exceed max size
        if (_queue.Count >= _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer()
    {
        // FIXED Defect 3: Added a check for an empty queue
        if (_queue.Count <= 0)
        {
            Console.WriteLine("Error: The queue is empty.");
            return;
        }

        // FIXED Defect 2: Saved the customer BEFORE removing them from the list
        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}