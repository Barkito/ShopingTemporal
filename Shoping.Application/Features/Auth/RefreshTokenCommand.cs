using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shoping.Domain;

namespace Shoping.Application.Features.Auth;
public class RefreshTokenCommand : IRequest<RefreshTokenCommandResponse>
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenCommandResponse>
{
    //private readonly AppDbContext _context;
    private readonly AuthService _authService;
    private readonly ILogger<RefreshTokenCommand> _logger;

    public RefreshTokenCommandHandler( AuthService authService, ILogger<RefreshTokenCommand> logger)
    {
        //_context = context;
        _authService = authService;
        _logger = logger;
    }

    public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = new RefreshToken(); /*await _context.RefreshTokens.FirstOrDefaultAsync(q => q.RefreshTokenValue == request.RefreshToken);*/

        // Refresh token no existe, expiró o fue revocado manualmente
        // (Pensando que el usuario puede dar click en "Cerrar Sesión en todos lados" o similar)
        if (refreshToken is null ||
            refreshToken.Active == false ||
            refreshToken.Expiration <= DateTime.UtcNow)
        {
            throw new ForbiddenAccessException();
        }

        // Se está intentando usar un Refresh Token que ya fue usado anteriormente,
        // puede significar que este refresh token fue robado.
        if (refreshToken.Used)
        {
            _logger.LogWarning("El refresh token del {UserId} ya fue usado. RT={RefreshToken}", refreshToken.UserId, refreshToken.RefreshTokenValue);

            var refreshTokens = new List< RefreshToken>(); // await _context.RefreshTokens
                //.Where(q => q.Active && q.Used == false && q.UserId == refreshToken.UserId)
                //.ToListAsync();

            foreach (var rt in refreshTokens)
            {
                rt.Used = true;
                rt.Active = false;
            }

           // await _context.SaveChangesAsync();

            throw new ForbiddenAccessException();
        }

        // TODO: Podríamos validar que el Access Token sí corresponde al mismo usuario

        refreshToken.Used = true;

        var user= new User(); /*= await _context.Users.FindAsync(refreshToken.UserId);*/

        if (user is null)
        {
            throw new ForbiddenAccessException();
        }

        var jwt = await _authService.GenerateAccessToken(user);

        var newRefreshToken = new RefreshToken
        {
            Active = true,
            Expiration = DateTime.UtcNow.AddDays(7),
            RefreshTokenValue = Guid.NewGuid().ToString("N"),
            Used = false,
            UserId = user.Id
        };

        //_context.Add(newRefreshToken);

        //await _context.SaveChangesAsync();

        return new RefreshTokenCommandResponse
        {
            AccessToken = jwt,
            RefreshToken = newRefreshToken.RefreshTokenValue
        };
    }
}

public class RefreshTokenCommandResponse
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}