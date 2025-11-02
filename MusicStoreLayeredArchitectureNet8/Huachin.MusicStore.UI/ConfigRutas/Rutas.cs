namespace Huachin.MusicStore.UI.ConfigRutas
{
	public static class Rutas
	{
		public const string ListarGeneros = "/Generos";
		public const string RegistrarGeneros = "/Generos/registro";
		public const string EditarGenerosNav = "/Generos/editar/{id:int}";
		public const string EditarGeneros = "/Generos/editar";

		public const string ListarEventos = "/Eventos";
		public const string RegistrarEventos = "/Eventos/registro";
		public const string EditarEventosNav = "/Eventos/editar/{id:int}";
		public const string EditarEventos = "/Eventos/editar";

		public const string ListarPedidos = "/Pedidos";
		public const string RegistrarPedidios = "/Pedidos/registro";
		public const string CompraPedidosNav = "/Pedidos/compra/{id:int}";
		public const string CompraPedidos = "/Pedidos/compra";

		public const string Login = "/Login";
	}
}
