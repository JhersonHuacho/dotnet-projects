using Blazored.SessionStorage;
using Huachin.MusicStore.Dto.Response.Login;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Huachin.MusicStore.UI.Servicios
{
	public class AutenticacionService : AuthenticationStateProvider
	{
		private readonly ISessionStorageService _sessionStorageService;

		public AutenticacionService(ISessionStorageService sessionStorageService)
		{
			_sessionStorageService = sessionStorageService;
		}

		private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());
		private bool _initialized = false;

		public async Task InitializeAsync()
		{
			if (!_initialized)
			{
				var sesion = await _sessionStorageService.GetItemAsync<LoginResponse>("userSession");
				if (sesion != null)
				{
					var identity = new ClaimsIdentity(new[]
					{
						new Claim(ClaimTypes.Name, sesion.NombreCompleto),
						new Claim(ClaimTypes.Email, sesion.Email),
						new Claim(ClaimTypes.Role, sesion.Rol),
						new Claim(ClaimTypes.NameIdentifier, sesion.Id.ToString()),
					}, "custom");

					_currentUser = new ClaimsPrincipal(identity);

					NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
				}

				_initialized = true;
			}
		}

		public override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			return Task.FromResult(new AuthenticationState(_currentUser));
		}

		public async Task Login(LoginResponse sesion)
		{
			var identity = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.Name, sesion.NombreCompleto),
				new Claim(ClaimTypes.Email, sesion.Email),
				new Claim(ClaimTypes.Role, sesion.Rol),
				new Claim(ClaimTypes.NameIdentifier, sesion.Id.ToString()),
			}, "custom");

			_currentUser = new ClaimsPrincipal(identity);

			await _sessionStorageService.SetItemAsync("userSession", sesion);

			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
		}

		public async Task Logout()
		{
			_currentUser = new ClaimsPrincipal(new ClaimsIdentity());

			await _sessionStorageService.RemoveItemAsync("userSession");

			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
		}
	}
}
