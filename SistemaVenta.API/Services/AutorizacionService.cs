using Microsoft.IdentityModel.Tokens;
using SistemaVenta.API.Custom;
using SistemaVenta.API.Services;
using SistemaVenta.DAL.DBContext;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaVenta.API.Services
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly DbventaContext context;
        private readonly IConfiguration configuration;

        public AutorizacionService(DbventaContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        private string GenerarToken(string idUsuario)
        {
            var key = configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;
        }

        public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
        {
            var usuario_encontrado = context.Usuarios.FirstOrDefault(x => x.Correo == autorizacion.correo &&
            x.Clave == autorizacion.clave);

            if (usuario_encontrado == null)
            {
                return await Task.FromResult<AutorizacionResponse>(null);
            }

            string tokenCreado = GenerarToken(usuario_encontrado.Correo.ToString());

            return new AutorizacionResponse() { token = tokenCreado, resultado = true, msg = "OK" };
        }
    }
}
