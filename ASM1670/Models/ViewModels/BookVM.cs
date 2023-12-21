using ASM1670.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM1670.Models.ViewModels;

public class BookVM
{
    public Book Book { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; }
}