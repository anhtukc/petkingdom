using PetKingdomFN.Models;

namespace PetKingdomFN.BusEntities
{
    public class PetServiceDataList
    {
        public List<PetService> list { get; set; }
        public int numberOfRecords { get; set; }
    }

    public class PetServiceOptionsDataList
    {
        public List<ServiceOption> list { get; set; }
        public int numberOfRecords { get; set; }
    }
    public class AccountDataList
    {
        public List<Account> list { get; set; }
        public int numberOfRecords { get; set; }
    }
    public class EmployeeDataList
    {
        public List<Employee> list { get; set; }
        public int numberOfRecords { get; set; }
    }
}
