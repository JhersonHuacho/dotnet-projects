using Huachin.MusicStore.Entidades;
using System.Linq.Expressions;

namespace Huachin.MusicStore.Repositorios.Interfaces
{
    public interface IRepositorioBase<TEntidad> where TEntidad : EntidadBase
	{
		Task<ICollection<TEntidad>> ListAsync();
		Task<ICollection<TEntidad>> ListAsync(Expression<Func<TEntidad, bool>> predicado);
		Task<ICollection<TResult>> ListAsync<TResult>(
			Expression<Func<TEntidad, bool>> predicado,
			Expression<Func<TEntidad, TResult>> selector);
		Task<(ICollection<TResult> Collection, int TotalRegistro)> ListAsync<TResult, Tkey>(
			Expression<Func<TEntidad, bool>> predicado,
			Expression<Func<TEntidad, TResult>> selector,
			Expression<Func<TEntidad, Tkey>> orderBy,
			int pagina = 1,
			int filas = 5);
		Task<TEntidad?> FindAsync(int id);
		Task<TEntidad> AddAsync(TEntidad entidad);
		Task UpdateAsync();
		Task DeleteAsync(int id);
	}
}
