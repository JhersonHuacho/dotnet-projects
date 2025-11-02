using Huachin.MusicStore.Entidades;

namespace Huachin.MusicStore.AccesoDatos.Contexto;

public partial class Genre : EntidadBase
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Concert> Concerts { get; set; } = new List<Concert>();
}
