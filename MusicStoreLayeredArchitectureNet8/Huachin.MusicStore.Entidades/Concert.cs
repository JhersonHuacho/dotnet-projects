using Huachin.MusicStore.Entidades;

namespace Huachin.MusicStore.AccesoDatos.Contexto;

public partial class Concert : EntidadBase
{
    public int IdGenre { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Place { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public DateTime DateEvent { get; set; }

    public string? ImageUrl { get; set; }

    public int TicketsQuantity { get; set; }

    public bool Finalized { get; set; }

    public virtual Genre IdGenreNavigation { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
