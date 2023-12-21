using ASM1670.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM1670.Models.ViewModels;

public class BookVM
{
<<<<<<< HEAD
    public Book Book { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; }
}
=======
    public class BookVM
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
>>>>>>> 7fb99d824d7fb4f33e04ee52783ce70f0ed8ad17
