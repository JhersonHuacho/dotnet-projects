using Huachin.MusicStore.Entidades;

namespace Huachin.MusicStore.AccesoDatos.Contexto;

public partial class Customer : EntidadBase
{
    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
