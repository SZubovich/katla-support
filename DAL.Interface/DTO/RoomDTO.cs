namespace DAL.Interface.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EmployeeDTO RoomOwner { get; set; } 
    }
}
