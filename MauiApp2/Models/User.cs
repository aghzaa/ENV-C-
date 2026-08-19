using System;
using System.Collections.Generic;
using System.Text;

namespace MauiApp2.Models;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; }
    public string Role { get; set; }
}
