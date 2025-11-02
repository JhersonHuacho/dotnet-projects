namespace Huachin.MusicStore.Domain.Entities.MusicStore
{
	public class BaseEntity
	{
		public Guid Id { get; protected set; }
		public bool IsActive { get; protected set; }
		public DateTime CreatedAt { get; protected set; }
		public DateTime? UpdatedAt { get; protected set; }

		protected BaseEntity()
		{
			Id = Guid.NewGuid();
			IsActive = true;
			CreatedAt = DateTime.UtcNow;
		}
	}
}
