namespace HotelManagementSystem.BLL.Data
{
    public class CustomerRequestStatus
    {
        public CustomerRequestStatus()
        {
        }

        public CustomerRequestStatus(int customerRequestStatusId, string name)
        {
            Id = customerRequestStatusId;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}