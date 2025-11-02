using Huachin.MusicStore.AccesoDatos.Contexto;
using Huachin.MusicStore.Entidades;
using Huachin.MusicStore.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Huachin.MusicStore.Repositorios.Implementaciones
{
	public class RepositorioBase<TEntidad> : IRepositorioBase<TEntidad> where TEntidad : EntidadBase
	{
		protected readonly MusicStoreContext _musicStoreContext;

		public RepositorioBase(MusicStoreContext musicStoreContext)
		{
			_musicStoreContext = musicStoreContext;
		}

		public async Task<ICollection<TEntidad>> ListAsync()
		{
			var result = await _musicStoreContext.Set<TEntidad>()
				.Where(x => x.Estado == true)
				.AsNoTracking()
				.ToListAsync();

			return result;
		}

		public async Task<ICollection<TEntidad>> ListAsync(Expression<Func<TEntidad, bool>> predicado)
		{
			var result = await _musicStoreContext.Set<TEntidad>()
				.Where(predicado)
				.AsNoTracking()
				.ToListAsync();

			return result;
		}

		public async Task<ICollection<TResult>> ListAsync<TResult>(
			Expression<Func<TEntidad, bool>> predicado, 
			Expression<Func<TEntidad, TResult>> selector)
		{
			var resultado = await _musicStoreContext.Set<TEntidad>()
				.Where(predicado)
				.AsNoTracking()
				.Select(selector)
				.ToListAsync();

			return resultado;
		}

		public async Task<(ICollection<TResult> Collection, int TotalRegistro)> ListAsync<TResult, Tkey>(
			Expression<Func<TEntidad, bool>> predicado, 
			Expression<Func<TEntidad, TResult>> selector, 
			Expression<Func<TEntidad, Tkey>> orderBy, 
			int pagina = 1, 
			int filas = 5)
		{
			var result = await _musicStoreContext.Set<TEntidad>()
				.Where(predicado)
				.AsNoTracking()
				.OrderBy(orderBy)
				.Skip((pagina - 1) * filas)
				.Take(filas)
				.Select(selector)
				.ToListAsync();

			int totalRegistros = await _musicStoreContext.Set<TEntidad>()
				.Where(predicado)
				.CountAsync();

			return (result, totalRegistros);
		}

		public async Task<TEntidad?> FindAsync(int id)
		{
			var result = await _musicStoreContext.Set<TEntidad>()
				.FirstOrDefaultAsync(p => p.Id == id && p.Estado);

			return result;
		}

		public async Task<TEntidad> AddAsync(TEntidad entidad)
		{
			var result = await _musicStoreContext.Set<TEntidad>()
				.AddAsync(entidad);

			await _musicStoreContext.SaveChangesAsync();

			return result.Entity;
		}

		public async Task UpdateAsync()
		{
			await _musicStoreContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			//var entidad = await _bdPedidoContext.Set<TEntidad>()
			//	.FirstOrDefaultAsync(p => p.Id == id);

			//if (entidad != null)
			//{
			//	entidad.Estado = false;
			//	await _bdPedidoContext.SaveChangesAsync();
			//}

			// Otra forma de hacerlo
			await _musicStoreContext.Set<TEntidad>()
				.Where(p => p.Id == id)
				.ExecuteUpdateAsync(p => p.SetProperty(p => p.Estado, false));
		}
	}
}
