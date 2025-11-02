using Huachin.MusicStore.Entidades;

namespace Huachin.MusicStore.AccesoDatos.Contexto;

public partial class Sale : EntidadBase
{
    public int IdCustomer { get; set; }

    public int IdConcert { get; set; }

    public DateTime SaleDate { get; set; }

    public string OperationNumber { get; set; } = null!;

    public decimal Total { get; set; }

    public short Quantity { get; set; }

    public virtual Concert IdConcertNavigation { get; set; } = null!;

    public virtual Customer IdCustomerNavigation { get; set; } = null!;
}
