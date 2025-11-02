namespace Huachin.MusicStore.Comun
{
    public static class Helpers
    {
		public static int CalcularNumeroPaginas(int totalFilas, int filas)
		{
			return (int)Math.Ceiling((double)totalFilas / filas);
		}

		public static decimal CalcularTotalNeto(decimal totalBruto)
		{
			return totalBruto / 1.18m;
		}
	}
}
