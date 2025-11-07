using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Huachin.MusicStore.Infrastructure.Configurations.Entities.IdentityContext
{
	public class UserExtensionIdentity : IdentityUser
	{
		public Guid CustomerId { get; set; }

		[StringLength(200)]
		public string FullName { get; set; } = default!;
	}
}
