namespace NamiCustomers.Abstractions.Dtos.Menus
{
    public class MenuDto
    {
        public Guid Id { get; init; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Icon { get; set; }
        public required string Route { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public Guid? ParentMenuId { get; set; }
        public ICollection<MenuDto> SubMenus { get; set; } = [];
    }
}
