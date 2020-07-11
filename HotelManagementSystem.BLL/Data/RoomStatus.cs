namespace HotelManagementSystem.BLL.Data
{
    public class RoomStatus
    {
        public RoomStatus()
        {
        }

        public RoomStatus(int roomStatusId, string name)
        {
            Id = roomStatusId;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}