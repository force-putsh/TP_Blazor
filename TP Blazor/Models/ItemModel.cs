using System;
using System.ComponentModel.DataAnnotations;

namespace TP_Blazor.Models;

public class ItemModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Le nom affiché ne doit pas dépasser 50 caractères.")]
    public string DisplayName { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Le nom ne doit pas dépasser 50 caractères.")]
    [RegularExpression(@"^[a-z''-'\s]{1,40}$", ErrorMessage = "Seulement les caractères en minuscule sont acceptées.")]
    public string Name { get; set; }

    [Required]
    [Range(1, 64)]
    public int StackSize { get; set; }

    [Required]
    [Range(1, 125)]
    public int MaxDurability { get; set; }

    public List<string> EnchantCategories { get; set; }

    public List<string> RepairWith { get; set; }

    [Required]
    [Range(typeof(bool), "true", "true", ErrorMessage = "Vous devez accepter les conditions.")]
    public bool AcceptCondition { get; set; }

    [Required(ErrorMessage = "L'image de l'item est obligatoire !")]
    public byte[] ImageContent { get; set; }
}

