namespace HotelManagementSystem.BLL.Data
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomDescription { get; set; }
        public string RoomNumber { get; set; }
        public decimal RoomPriceForOneNight { get; set; }
        public int Sleeps { get; set; }
        public string RoomImage { get; set; }
        public string RoomType { get; set; }
        public string RoomStatus { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomStatusId { get; set; }
    }
}